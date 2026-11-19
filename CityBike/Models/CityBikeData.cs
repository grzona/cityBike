namespace CityBike;

/// <summary>
/// Cały wczytany zbiór danych jako jeden record. To dostaje klasa Queries.
/// Rides i Stations to dwie "tabele" połączone przez Id stacji (join).
/// </summary>
public record CityBikeData(
    IReadOnlyList<Ride> Rides,
    IReadOnlyList<Station> Stations,
    int SkippedRows);
