namespace cityBike.Models;

public record StationBalance(string Id, String Name, int Balance)
{
    public override string ToString() => $"{Balance:D4} for {Id} {Name}";
}

public record PeriodComparison(string Id, string Name, double Avg, params int[] PeriodBalance)
{
    public override string ToString()
    {
        string result = "";

        result += $"{Avg:0000.00} |";
        
        
        foreach (var p in PeriodBalance)
        {
            result += $"{p:D4} | ";
        }

        result += $" for **{Id}** {Name}";
        return result;
    }
}
