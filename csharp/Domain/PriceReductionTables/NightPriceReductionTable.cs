using System.Runtime.CompilerServices;

namespace Domain.PriceReductionTables;

public class NightPriceReductionTable : IPriceReductionTable
{
    public string Type => "night";
    public async IAsyncEnumerable<double> GetReductions(ReductionParameters parameters, [EnumeratorCancellation]CancellationToken cancellationToken = default)
    {
        switch (parameters.Age)
        {
            case < 6:
                yield return 1.0;
                break;
            case >= 6 and <= 64:
                yield return 0.0;
                break;
            case > 64:
                yield return 0.6;
                break;
        }
    }
}