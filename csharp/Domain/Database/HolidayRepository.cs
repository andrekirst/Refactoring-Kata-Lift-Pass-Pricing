using Microsoft.EntityFrameworkCore;

namespace Domain.Database;

public class HolidayRepository(LiftPassDbContext dbContext)
{
    public async Task<bool> IsHoliday(DateTime date, CancellationToken cancellationToken = default)
    {
        return await dbContext.Holidays
            .AnyAsync(h => h.Holiday.Date == date.Date, cancellationToken: cancellationToken);
    }
}