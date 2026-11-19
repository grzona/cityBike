using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace CityBike;

/// <summary>
/// Wczytuje pliki CSV (strumieniowo, wiersz po wierszu) i buduje znormalizowany CityBikeData.
/// </summary>
public class TripLoader
{
    public const string PathsFileName = "datapaths.txt";

    public int SkippedRows { get; private set; }

    /// <summary>Czyta datapaths.txt: jedna ścieżka na linię, pomija puste linie i komentarze (#).</summary>
    public static List<string> ReadDataPaths(string? pathsFile = null)
    {
        pathsFile ??= Path.Combine(AppContext.BaseDirectory, PathsFileName);
        if (!File.Exists(pathsFile))
        {
            Console.Error.WriteLine($"Nie znaleziono pliku {pathsFile}");
            return [];
        }

        var paths = new List<string>();
        foreach (var raw in File.ReadLines(pathsFile))
        {
            var line = raw.Trim();
            if (line.Length == 0 || line.StartsWith('#'))
                continue;

            if (File.Exists(line))
                paths.Add(line);
            else
                Console.Error.WriteLine($"Uwaga: plik nie istnieje, pomijam: {line}");
        }
        return paths;
    }

    /// <summary>
    /// Strumieniowo zwraca wiersze ze wszystkich plików po kolei (yield).
    /// Wiersze, których nie da się sparsować, są pomijane i liczone w SkippedRows.
    /// </summary>
    public IEnumerable<TripRow> ReadRows(IEnumerable<string> paths)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MissingFieldFound = null,
            BadDataFound = null,
            ReadingExceptionOccurred = _ =>
            {
                SkippedRows++;
                return false; // false = pomiń wiersz zamiast rzucać wyjątek
            },
        };

        foreach (var path in paths)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);
            foreach (var row in csv.GetRecords<TripRow>())
                yield return row;
        }
    }

    /// <summary>Wczytuje wszystko i rozdziela na tabele Rides + Stations.</summary>
    public CityBikeData Load(IEnumerable<string> paths)
    {
        SkippedRows = 0;
        var rows = ReadRows(paths).ToList();

        var rides = rows.Select(ToRide).ToList();

        // Stacje: każda stacja pojawia się jako start albo koniec przejazdu.
        // Bierzemy obie strony, grupujemy po Id; nazwa = najczęstsza, współrzędne = średnia.
        var stations = rows
            .Select(r => (Id: r.StartStationId, Name: r.StartStationName, Lat: (double?)r.StartLat, Lng: (double?)r.StartLng))
            .Concat(rows.Select(r => (Id: r.EndStationId, Name: r.EndStationName, Lat: r.EndLat, Lng: r.EndLng)))
            .Where(s => !string.IsNullOrWhiteSpace(s.Id) && !string.IsNullOrWhiteSpace(s.Name)
                        && s.Lat is not null && s.Lng is not null)
            .GroupBy(s => s.Id!)
            .Select(g => new Station(
                g.Key,
                g.GroupBy(s => s.Name!).MaxBy(n => n.Count())!.Key,
                g.Average(s => s.Lat!.Value),
                g.Average(s => s.Lng!.Value)))
            .OrderBy(s => s.Id)
            .ToList();

        return new CityBikeData(rides, stations, SkippedRows);
    }

    private static Ride ToRide(TripRow r) => new(
        Id: r.RideId,
        BikeType: r.RideableType switch
        {
            "classic_bike" => BikeType.Classic,
            "electric_bike" => BikeType.Electric,
            _ => BikeType.Unknown,
        },
        UserType: r.MemberCasual switch
        {
            "member" => UserType.Member,
            "casual" => UserType.Casual,
            _ => UserType.Unknown,
        },
        StartedAt: r.StartedAt,
        EndedAt: r.EndedAt,
        StartStationId: string.IsNullOrWhiteSpace(r.StartStationId) ? null : r.StartStationId,
        EndStationId: string.IsNullOrWhiteSpace(r.EndStationId) ? null : r.EndStationId,
        StartLat: r.StartLat,
        StartLng: r.StartLng,
        EndLat: r.EndLat,
        EndLng: r.EndLng);
}
