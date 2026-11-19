namespace CityBike;

/// <summary>
/// Tu piszesz zapytania LINQ. Masz dwie "tabele":
///   data.Rides    – przejazdy (StartStationId / EndStationId to klucze do stacji)
///   data.Stations – stacje (Id, Name, Lat, Lng)
/// Nazwy stacji dostajesz przez join Rides z Stations po Id.
///
/// Wzorzec: zapytanie zwraca kolekcję recordów wynikowych,
/// a Output.Table("tytuł", wynik) wyświetla je jako tabelę.
/// Recordy wynikowe możesz deklarować na dole tego pliku (przykład: StationCount).
/// </summary>
public class Queries(CityBikeData data)
{
    private readonly IReadOnlyList<Ride> _rides = data.Rides;
    private readonly IReadOnlyList<Station> _stations = data.Stations;

    public void Solve()
    {
        Output.Header("Dane");
        Output.Line($"Przejazdy: {_rides.Count}, stacje: {_stations.Count}, pominięte wiersze: {data.SkippedRows}");

        TopStartStations();
        TopRoutes();
        PeakHoursWeekdayVsWeekend();
        DurationDistribution();
        MemberVsCasual();
        BikeTypes();
    }

    // TODO: 10 najpopularniejszych stacji startowych (z nazwą: join z _stations, group by, count).
    //       Wynik np. jako IEnumerable<StationCount>, potem Output.Table(...).
    private void TopStartStations()
    {
    }

    // TODO: najpopularniejsze trasy (para StartStationId -> EndStationId), nazwy obu stacji przez join.
    private void TopRoutes()
    {
    }

    // TODO: liczba przejazdów wg godziny startu, osobno dni robocze i weekend (StartedAt.DayOfWeek, StartedAt.Hour).
    private void PeakHoursWeekdayVsWeekend()
    {
    }

    // TODO: rozkład czasu przejazdu (Duration) w przedziałach, np. <5 min, 5-15, 15-30, 30-60, >60.
    //       Pamiętaj o odfiltrowaniu podejrzanych danych (ujemny lub absurdalnie długi czas).
    private void DurationDistribution()
    {
    }

    // TODO: member vs casual: liczba przejazdów, średni czas, ulubione stacje.
    private void MemberVsCasual()
    {
    }

    // TODO: typy rowerów (Classic / Electric): udział, średni czas, ew. w podziale na UserType.
    private void BikeTypes()
    {
    }
}

// Recordy wynikowe zapytań – dopisuj kolejne według potrzeby.
public record StationCount(string Station, int Rides);
