using Domain.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Database;

public class LiftPassDbContext : DbContext
{
    public DbSet<BasePriceEntity> BasePrices => Set<BasePriceEntity>();
    public DbSet<HolidayEntity> Holidays => Set<HolidayEntity>();
}