namespace Domain;

public interface IReductionService
{
    List<double> CalculateReductions(ReductionParameters parameters);
}

public class ReductionService : IReductionService
{
    private readonly List<ReductionRule> _reductionRules = [
        new ReductionRule("1", param => param.Age < 6, 1.0),
        new ReductionRule("2", param => param is { Age: >= 6 and < 15, Type: not "night" }, 0.3),
        new ReductionRule("3", param => param is { Age: null or > 15, Type: not "night", IsHoliday: false, DayOfWeek: DayOfWeek.Monday }, 0.35),
        new ReductionRule("4", param => param is { Type: not "night" }, 0.0),
        new ReductionRule("5", param => param is { Age: > 64, Type: not "night" }, 0.25),
        new ReductionRule("6", param => param is { Age: > 64, Type: "night" }, 0.6),
        new ReductionRule("7", param => param is { Age: >= 6 and <= 64, Type: "night" }, 0.0)
    ];
    
    public List<double> CalculateReductions(ReductionParameters parameters)
    {
        var reductions = _reductionRules
            .Where(reductionRule => reductionRule.Condition(parameters))
            .Select(reductionRule => reductionRule.Result)
            .ToList();
        
        var x = _reductionRules
            .Where(reductionRule => reductionRule.Condition(parameters))
            .Select(reductionRule => reductionRule)
            .ToList();

        return reductions.Any(r => r > 0.0)
            ? reductions
                .Where(r => r > 0.0)
                .ToList()
            : reductions;
    }
}

public record ReductionRule(string Name, Func<ReductionParameters, bool> Condition, double Result);

public record ReductionParameters(int? Age, string Type, bool IsHoliday = false, DayOfWeek? DayOfWeek = null);