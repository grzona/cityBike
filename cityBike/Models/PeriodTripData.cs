namespace cityBike.Models;

public record PeriodTripData(string? Source,string? Description, DateTime? Begin, DateTime? End,
    IReadOnlyList<Ride> Rides, IReadOnlyList<Station> Stations);