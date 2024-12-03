using Microsoft.EntityFrameworkCore;

namespace Domain.Database;

public class HolidayRepository(LiftPassDbContext dbContext)
{
    public async Task<bool> IsHoliday(DateTime? date, CancellationToken cancellationToken = default)
    {
        if (date is null)
        {
            return false;
        }
        
        return await dbContext.Holidays
            .AnyAsync(h => h.Holiday.Date == date.Value.Date, cancellationToken: cancellationToken);
    }
}