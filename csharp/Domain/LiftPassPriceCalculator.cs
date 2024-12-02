namespace Domain;

public class LiftPassPriceCalculator(IReductionService reductionService)
{
    public double Calculate(LiftPassPriceCalulatorParameters parameters)
    {
        var (age, isHoliday, dayOfWeek, type, cost) = parameters;

        var reductions = reductionService.CalculateReductions(new ReductionParameters(age, type, isHoliday, dayOfWeek));

        foreach (var reduction in reductions)
        {
            cost = (int)Math.Ceiling(cost * (1 - reduction));
        }

        return cost;
    }
}

public record LiftPassPriceCalulatorParameters(int? Age, bool IsHoliday, DayOfWeek? DayOfWeek, string Type, int Cost);