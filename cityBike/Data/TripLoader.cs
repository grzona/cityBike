namespace cityBike.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using cityBike.Models;

public static class TripLoader
{
    // Czyta plik ze ścieżkami i zwraca osobne dane dla każdego miesiąca (pliku).
    public static List<PeriodTripData> Load(string pathsFile)
    {
        return File.ReadLines(pathsFile)
            .Select(l => l.Trim())                          // obcinamy spacje
            .Where(l => l != "" && !l.StartsWith('#'))      // bez pustych i komentarzy
            .Where(File.Exists)                             // tylko istniejące pliki
            .Select(LoadOne)                                // każda ścieżka -> dane miesiąca
            .ToList();
    }

    // Wczytuje jeden plik CSV jako dane jednego miesiąca.
    private static PeriodTripData LoadOne(string path)
    {
        var rows = ReadRows(path).ToList();

        var rides = rows
            .Select(r => new Ride(r.RideId, r.RideableType, r.StartedAt, r.EndedAt,
                                  r.StartStationId, r.EndStationId, r.MemberCasual))
            .ToList();

        var stations = BuildStations(rows);

        return new PeriodTripData(rides, stations, Path.GetFileNameWithoutExtension(path));
    }

    // Czyta jeden plik CSV i zwraca jego wiersze po jednym naraz.
    private static IEnumerable<TripRow> ReadRows(string path)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.Replace("_", "").ToLower(),
            MissingFieldFound = null,             // brak kolumny: bez błędu
            BadDataFound = null,                  // zepsute dane: ignoruj
            ReadingExceptionOccurred = _ => false // wiersz nie do sparsowania: pomiń
        };

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);

        foreach (var row in csv.GetRecords<TripRow>())
            yield return row;
    }

    // Twoja wersja, z DistinctBy, bez współrzędnych i z `!`.
    private static List<Station> BuildStations(List<TripRow> rows)
    {
        var stationsBeg = rows
            .Where(r => r.StartStationId != null && r.StartStationName != null)
            .Select(r => new Station(r.StartStationId!, r.StartStationName!));

        var stationsEnd = rows
            .Where(r => r.EndStationId != null && r.EndStationName != null)
            .Select(r => new Station(r.EndStationId!, r.EndStationName!));

        return stationsBeg
            .Concat(stationsEnd)
            .DistinctBy(s => s.Id)
            .ToList();
    }
}