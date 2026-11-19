using System.Net.NetworkInformation;
using cityBike.Models;

namespace cityBike.Controllers;

public class Queries
{ 
    public void Solve()
    {
        string dataPathFiles = "/home/grzona/pw/p3/projects/cityBike/cityBike/cityBikePaths.sh";
        List<PeriodTripData> months  = Data.TripLoader.Load(dataPathFiles);
        
        int monthsCount = months.Count;
        Console.WriteLine($"Pomyślnie załadowano *{monthsCount}* miesięcy");
        
        var firstMonth = months[0];
        
        /*
         * STACJE, NA KTÓRYCH UBYWA ROWERÓW 
         */
        var deficitStations = DeficitStations(firstMonth);
        Console.WriteLine("STACJE DEFICYTOWE:\n");
        Console.WriteLine(String.Join(Environment.NewLine, deficitStations));
        Console.WriteLine("\n");
        
        
    }

    List<StationBalance> DeficitStations(PeriodTripData dataset)
    {
        var rides = dataset.Rides;
        
        var outgoing = rides
            .Where(r => r.StartStationId != null)
            .GroupBy(r => r.StartStationId!)
            .Select(r => new { Id = r.Key, OutgointCount = r.Count() });
        
        var incoming = rides
            .Where(r => r.EndStationId != null)
            .GroupBy(r => r.EndStationId!)
            .Select(r => new { Id = r.Key, IncomingCount = r.Count() });

        var unnamedBalance = outgoing.Join(incoming,
            i => i.Id, o => o.Id,
            (o, i) => new { Id = o.Id, Balace = i.IncomingCount - o.OutgointCount });

        var namedBalance = unnamedBalance.Join(dataset.Stations,
            u => u.Id,
            n => n.Id,
            (u, n) => new StationBalance(n.Id, n.Name, u.Balace)
        );

        return namedBalance
                .Where(p => p.Balance < 0)
                .OrderBy(s => s.Balance)
                .ToList();
    }
}