namespace CityBike;

public static class Program
{
    public static void Main(string[] args)
    {
        // Ścieżki z argumentów albo z datapaths.txt
        var paths = args.Length > 0 ? args.ToList() : TripLoader.ReadDataPaths();
        if (paths.Count == 0)
        {
            Console.Error.WriteLine("Brak plików CSV. Dopisz ścieżki do CityBike/datapaths.txt.");
            return;
        }

        var data = new TripLoader().Load(paths);
        new Queries(data).Solve();
    }
}
