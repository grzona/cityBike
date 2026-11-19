# CityBike – analiza danych LINQ

1. Wpisz ścieżkę(i) do plików CSV Citi Bike w `CityBike/datapaths.txt` (jedna na linię, najlepiej bezwzględne).
   Ewentualnie podaj ścieżki jako argumenty programu.
2. Uruchom projekt (Rider: Run, albo `dotnet run --project CityBike`).
3. Zapytania LINQ pisz w `CityBike/Queries/Queries.cs` – metody z komentarzami `TODO`.

Struktura:
- `Models/` – recordy: `TripRow` (wiersz CSV 1:1), `Ride` (przejazd, tylko Id stacji), `Station`, `CityBikeData` (cały zbiór: Rides + Stations).
- `Data/TripLoader.cs` – strumieniowe wczytywanie CSV (CsvHelper), pomijanie złych wierszy, budowa tabeli stacji.
- `Presentation/Output.cs` – `Output.Table(tytuł, kolekcja recordów)` rysuje tabelę (Spectre.Console).
- `Queries/Queries.cs` – Twoje zapytania.
