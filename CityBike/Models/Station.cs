namespace CityBike;

/// <summary>Osobna "tabela" stacji. Przejazdy trzymają tylko Id stacji.</summary>
public record Station(string Id, string Name, double Lat, double Lng);
