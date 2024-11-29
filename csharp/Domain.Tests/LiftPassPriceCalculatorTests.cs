using FluentAssertions;

namespace Domain.Tests;

public class LiftPassPriceCalculatorTests
{
    [Theory]
    [InlineData("", 5, true, 19, 0.0)]
    [InlineData("night", 23, true, 19, 19.0)]
    public void Test1(string type, int age, bool isHoliay, int cost, double expected)
    {
        var calculator = new LiftPassPriceCalculator();
        var actual = calculator.Calculate(new LiftPassPriceCalulatorParameters
        {
            Age = age,
            Cost = cost,
            IsHoliday = isHoliay,
            Type = type
        });

        actual.Should().Be(expected);
    }
}