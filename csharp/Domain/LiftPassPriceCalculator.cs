namespace Domain;

public class LiftPassPriceCalculator
{
    public double Calculate(LiftPassPriceCalulatorParameters parameters)
    {
        if (parameters.Age < 6)
        {
            return 0;
        }
        return parameters.Cost;
    }
}

public record LiftPassPriceCalulatorParameters
{
    public int Age { get; set; }
    public bool IsHoliday { get; set; }
    public string Type { get; set; }
    public int Cost { get; set; }
}