namespace CityBike;

/// <summary>
/// Przejazd po normalizacji: bez nazw stacji, tylko ich Id (klucze obce do Station).
/// StartStationId / EndStationId są null, gdy rower był wzięty/zostawiony poza stacją.
/// </summary>
public record Ride(
    string Id,
    BikeType BikeType,
    UserType UserType,
    DateTime StartedAt,
    DateTime EndedAt,
    string? StartStationId,
    string? EndStationId,
    double StartLat,
    double StartLng,
    double? EndLat,
    double? EndLng)
{
    public TimeSpan Duration => EndedAt - StartedAt;
}
