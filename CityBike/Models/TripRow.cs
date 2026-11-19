using CsvHelper.Configuration.Attributes;

namespace CityBike;

/// <summary>
/// Jeden wiersz pliku CSV, 1:1 z kolumnami Citi Bike.
/// Surowe dane: tekst zostaje tekstem, konwersja na enumy dzieje się w TripLoader.
/// Nullable tam, gdzie w danych bywają puste pola.
/// </summary>
public record TripRow
{
    [Name("ride_id")] public string RideId { get; init; } = "";
    [Name("rideable_type")] public string RideableType { get; init; } = "";
    [Name("started_at")] public DateTime StartedAt { get; init; }
    [Name("ended_at")] public DateTime EndedAt { get; init; }
    [Name("start_station_name")] public string? StartStationName { get; init; }
    [Name("start_station_id")] public string? StartStationId { get; init; }
    [Name("end_station_name")] public string? EndStationName { get; init; }
    [Name("end_station_id")] public string? EndStationId { get; init; }
    [Name("start_lat")] public double StartLat { get; init; }
    [Name("start_lng")] public double StartLng { get; init; }
    [Name("end_lat")] public double? EndLat { get; init; }
    [Name("end_lng")] public double? EndLng { get; init; }
    [Name("member_casual")] public string MemberCasual { get; init; } = "";
}
