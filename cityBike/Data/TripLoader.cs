namespace cityBike.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;


public static class TripLoader
{
    public static Models.TripData Load(string pathsFile)
    {
        // Czytamy datapath.txt linijka po linijce (każda linijka to string).
        var paths = File.ReadLines(pathsFile)
            .Select(l => l.Trim())                          // każdą linijkę obcinamy ze spacji
            .Where(l => l != "" && !l.StartsWith('#'))      // wyrzucamy puste i komentarze
            .Where(File.Exists);                            // zostają tylko istniejące pliki

        // Dla każdej ścieżki ReadRows daje wiersze tego pliku.
        // SelectMany skleja wiersze ze wszystkich plików w jedną sekwencję.
        // ToList() faktycznie czyta wszystko i zapisuje w pamięci.
        var rows = paths.SelectMany(ReadRows).ToList();

        // Każdy surowy wiersz przepisujemy na Ride (bez nazw i współrzędnych stacji).
        var rides = rows
            .Select(r => new Models.Ride(r.RideId, r.RideableType, r.StartedAt, r.EndedAt,
                                  r.StartStationId, r.EndStationId, r.MemberCasual))
            .ToList();

        var stations = BuildStations(rows);   // osobna tabela stacji

        return new Models.TripData(rides, stations);
    }

    // Czyta jeden plik CSV i zwraca jego wiersze jako TripRow, po jednym naraz.
    private static IEnumerable<Models.TripRow> ReadRows(string path)
    {
        // Ustawienia CsvHelpera:
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) // kropka jako separator dziesiętny
        {
            MissingFieldFound = null,             // brak kolumny w wierszu: nie rzucaj błędu
            BadDataFound = null,                  // zepsute cudzysłowy itp.: ignoruj
            ReadingExceptionOccurred = _ => false // wiersz nie do sparsowania: pomiń go
                                                  // (_ to parametr, którego nie używamy)
        };

        using var reader = new StreamReader(path);       // otwiera plik do czytania
        using var csv = new CsvReader(reader, config);   // czyta z niego CSV

        // GetRecords dopasowuje kolumny do TripRow po atrybutach [Name].
        // yield return oddaje wiersze pojedynczo, bez ładowania całego pliku naraz.
        // Plik zamyka się (using) dopiero gdy przeczytamy wszystkie wiersze.
        
        
        
        foreach (var row in csv.GetRecords<Models.TripRow>())
            yield return row;
    }
    
    private static List<Models.Station> BuildStations(List<Models.TripRow> rows)
    {
        var stations_beg = rows
            .Where(r => r.StartStationId != null && r.StartStationName != null)
            .Select(r => new Models.Station(r.StartStationId!, r.StartStationName!));
         
        var stations_end = rows
            .Where(r => r.EndStationId != null)
            .Select(r => new Models.Station(r.EndStationId!, r.EndStationName!));
        
        var stations_dups = stations_beg.Concat(stations_end);
        
        var stations = stations_dups.DistinctBy(s => s.Id);
        
        return stations.ToList();
    }
}