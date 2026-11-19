using System.Net.NetworkInformation;
using cityBike.Models;

namespace cityBike.Controllers;

public class Queries
{ 
    public void Solve()
    {
        string dataPathFiles = "/home/grzona/pw/p3/projects/cityBike/cityBike/datapath.txt";
        List<PeriodTripData> months  = Data.TripLoader.Load(dataPathFiles);
        
        int monthsCount = months.Count;
        Console.WriteLine($"Pomyślnie załadowano *{monthsCount}* miesięcy");
        
        var firstMonth = months[0];
        
        /*
         * STACJE, NA KTÓRYCH UBYWA ROWERÓW
         */
        
    }

    List<Station> DeficitStations(PeriodTripData dataset)
    {
        var rides = dataset.Rides;


        throw new NotImplementedException();
        return default;
    }
}