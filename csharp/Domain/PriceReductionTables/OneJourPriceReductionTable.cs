using System.Runtime.CompilerServices;

namespace Domain.PriceReductionTables;

public class OneJourPriceReductionTable(ISpecialReductionDaysService specialReductionDaysService) : IPriceReductionTable
{
    public string Type => "1jour";
    
    public async IAsyncEnumerable<double> GetReductions(ReductionParameters parameters, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var (age, date) = parameters;
        
        switch (age)
        {
            case < 6:
                yield return 1.0;
                break;
            case >= 6 and < 15:
                yield return 0.3;
                break;
            case > 64:
                yield return 0.25;
                break;
        }

        var isSpecialDay = await specialReductionDaysService.IsSpecialDay(new SpecialReductionDaysParameters
        {
            Date = date
        }, cancellationToken);
        
        switch (age)
        {
            case null or > 15 when isSpecialDay:
                yield return 0.35;
                break;
            case null or > 15 and <= 64:
                yield return 0.0;
                break;
        }
    }
}