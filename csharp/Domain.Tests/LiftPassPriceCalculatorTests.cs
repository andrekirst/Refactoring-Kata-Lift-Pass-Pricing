using FluentAssertions;

namespace Domain.Tests;

public class LiftPassPriceCalculatorTests
{
    [Theory]
    [InlineData("night", 23, true, DayOfWeek.Monday, 19, 19.0)]
    [InlineData("night", 1, true, DayOfWeek.Monday, 19, 0.0)]
    [InlineData("night", 10, true, DayOfWeek.Monday, 19, 19.0)]
    [InlineData("night", 70, true, DayOfWeek.Monday, 19, 8.0)]
    [InlineData("1jour", 23, true, DayOfWeek.Monday, 35, 35.0)]
    [InlineData("1jour", 1, true, DayOfWeek.Monday, 35, 0.0)]
    [InlineData("1jour", 10, true, DayOfWeek.Monday, 35, 25.0)]
    [InlineData("1jour", 70, true, DayOfWeek.Monday, 35, 27.0)]
    [InlineData("night", 23, false, DayOfWeek.Monday, 19, 19.0)]
    [InlineData("night", 1, false, DayOfWeek.Monday, 19, 0.0)]
    [InlineData("night", 10, false, DayOfWeek.Monday, 19, 19.0)]
    [InlineData("night", 70, false, DayOfWeek.Monday, 19, 8.0)]
    [InlineData("1jour", 23, false, DayOfWeek.Monday, 35, 23.0)]
    [InlineData("1jour", 1, false, DayOfWeek.Monday, 35, 0.0)]
    [InlineData("1jour", 10, false, DayOfWeek.Monday, 35, 25.0)]
    [InlineData("1jour", 70, false, DayOfWeek.Monday, 35, 18.0)]
    public void Test1(string type, int? age, bool isHoliday, DayOfWeek? dayOfWeek, int cost, double expected)
    {
        var calculator = new LiftPassPriceCalculator(new ReductionService());
        var actual = calculator.Calculate(new LiftPassPriceCalulatorParameters(age, isHoliday, dayOfWeek, type, cost));
        
        actual.Should().Be(expected);
    }
}