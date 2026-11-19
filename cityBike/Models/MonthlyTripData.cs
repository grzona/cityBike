namespace cityBike.Models;

public record MonthlyTripData(string? Source, IReadOnlyList<Ride> Rides, IReadOnlyList<Station> Stations);