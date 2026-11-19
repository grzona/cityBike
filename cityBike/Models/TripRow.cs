namespace cityBike.Models;
using CsvHelper.Configuration.Attributes;

public record TripRow(
    [ Name("ride_id")] string RideId,
    [ Name("rideable_type")] string RideableType,
    [ Name("started_at")] DateTime StartedAt,
    [ Name("ended_at")] DateTime EndedAt,
    [ Name("start_station_name")] string? StartStationName,
    [ Name("start_station_id")] string? StartStationId,
    [ Name("end_station_name")] string? EndStationName,
    [ Name("end_station_id")] string? EndStationId,
    [ Name("start_lat")] double StartLat,
    [ Name("start_lng")] double StartLng,
    [ Name("end_lat")] double? EndLat,
    [ Name("end_lng")] double? EndLng,
    [ Name("member_casual")] string MemberCasual);