namespace Domain.PriceReductionTables;

public interface IPriceReductionTable
{
    string Type { get; }
    IAsyncEnumerable<double> GetReductions(ReductionParameters parameters, CancellationToken cancellationToken = default);
}