using Domain.PriceReductionTables;

namespace Domain;

public interface IReductionService
{
    ValueTask<List<double>> CalculateReductions(string type, ReductionParameters parameters, CancellationToken cancellationToken = default);
}

public class ReductionService(IEnumerable<IPriceReductionTable> priceReductionTables) : IReductionService
{
    public ValueTask<List<double>> CalculateReductions(string type, ReductionParameters parameters, CancellationToken cancellationToken = default)
    {
        var priceReductionTable = priceReductionTables.FirstOrDefault(p => p.Type == type);
        
        if (priceReductionTable is null)
        {
            throw new NotSupportedTypeException(type);
        }

        return priceReductionTable
            .GetReductions(parameters, cancellationToken)
            .ToListAsync(cancellationToken);
    }
}