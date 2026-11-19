namespace cityBike.Models;
using CsvHelper.Configuration.Attributes;

public record TripRow(
    [property: Name("ride_id")] string RideId,
    [property: Name("rideable_type")] string RideableType,
    [property: Name("started_at")] DateTime StartedAt,
    [property: Name("ended_at")] DateTime EndedAt,
    [property: Name("start_station_name")] string? StartStationName,
    [property: Name("start_station_id")] string? StartStationId,
    [property: Name("end_station_name")] string? EndStationName,
    [property: Name("end_station_id")] string? EndStationId,
    [property: Name("start_lat")] double StartLat,
    [property: Name("start_lng")] double StartLng,
    [property: Name("end_lat")] double? EndLat,
    [property: Name("end_lng")] double? EndLng,
    [property: Name("member_casual")] string MemberCasual);