using Domain.Database;

namespace Domain;

public interface ISpecialReductionDaysService
{
    Task<bool> IsSpecialDay(SpecialReductionDaysParameters parameters, CancellationToken cancellationToken = default);
}

public class SpecialReductionDaysService(HolidayRepository holidayRepository) : ISpecialReductionDaysService
{
    public async Task<bool> IsSpecialDay(SpecialReductionDaysParameters parameters, CancellationToken cancellationToken = default)
    {
        var isHoliday = await holidayRepository.IsHoliday(parameters.Date, cancellationToken);
        
        return parameters is { Date.DayOfWeek: DayOfWeek.Monday } && !isHoliday;
    }
}

public class SpecialReductionDaysParameters
{
    public DateTime? Date { get; set; }
}