namespace Domain;

public class LiftPassPriceCalculator(IReductionService reductionService)
{
    public async Task<double> Calculate(LiftPassPriceCalulatorParameters parameters, CancellationToken cancellationToken = default)
    {
        var (type, age, date, cost) = parameters;

        var reductions = await reductionService.CalculateReductions(type, new ReductionParameters(age, date), cancellationToken);

        foreach (var reduction in reductions)
        {
            cost = (int)Math.Ceiling(cost * (1 - reduction));
        }

        return cost;
    }
}