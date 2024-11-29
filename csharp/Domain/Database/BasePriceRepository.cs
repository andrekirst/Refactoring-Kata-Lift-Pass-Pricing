using Domain.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Database;

public interface IBasePriceRepository
{
    Task<bool> CreateBasePrice(string type, int cost, CancellationToken cancellationToken = default);
    Task<int> GetCostByType(string type, CancellationToken cancellationToken = default);
}

public class BasePriceRepository(LiftPassDbContext dbContext) : IBasePriceRepository
{
    public async Task<bool> CreateBasePrice(string type, int cost, CancellationToken cancellationToken = default)
    {
        if (await dbContext.BasePrices.AnyAsync(bp => bp.Type == type, cancellationToken: cancellationToken))
        {
            var affectedRowsUpdated = await dbContext.BasePrices
                .Where(bp => bp.Type == type)
                .ExecuteUpdateAsync(calls => calls.SetProperty(entity => entity.Cost, cost), cancellationToken: cancellationToken);

            return affectedRowsUpdated == 1;
        }

        dbContext.BasePrices.Add(new BasePriceEntity
        {
            Cost = cost,
            Type = type
        });

        var affectedRowsInserted = await dbContext.SaveChangesAsync(cancellationToken);
        return affectedRowsInserted == 1;
    }

    public Task<int> GetCostByType(string type, CancellationToken cancellationToken = default)
    {
        return dbContext.BasePrices
            .Where(bp => bp.Type == type)
            .Select(bp => bp.Cost)
            .FirstOrDefaultAsync(cancellationToken);
    }
}