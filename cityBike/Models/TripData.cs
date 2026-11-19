namespace cityBike.Models;

public record TripData(IReadOnlyList<Ride> Rides, IReadOnlyList<Station> Stations);