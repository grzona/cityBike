namespace cityBike.Models;

public record PeriodTripData(IReadOnlyList<Ride> Rides, IReadOnlyList<Station> Stations, 
    string? Source = null, string? Description = null, DateTime? Begin = null, DateTime? End = null);