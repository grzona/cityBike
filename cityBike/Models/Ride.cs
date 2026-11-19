namespace cityBike.Models;

public record Ride(
    string RideId,
    string RideableType,
    DateTime StartedAt,
    DateTime EndedAt,
    string? StartStationId,
    string? EndStationId,
    string MemberCasual)
{
    public TimeSpan Duration => EndedAt - StartedAt;
}