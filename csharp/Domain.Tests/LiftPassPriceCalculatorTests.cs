using Domain.PriceReductionTables;
using FluentAssertions;
using NSubstitute;

namespace Domain.Tests;

public class LiftPassPriceCalculatorTests
{
    private readonly IReductionService _reductionService = Substitute.For<IReductionService>();

    [Theory]
    [InlineData(10, new[] { 1.0 }, 0.0)]
    [InlineData(10, new[] { 0.5, 0.5 }, 3)]
    [InlineData(10, new[] { 0.5, 0.5, 0.5 }, 2)]
    public async Task Test1(int cost, double[] reductions, double expected)
    {
        _reductionService
            .CalculateReductions(Arg.Any<string>(), Arg.Any<ReductionParameters>())
            .Returns(reductions.ToList());
        
        var sut = new LiftPassPriceCalculator(_reductionService);
        
        var actual = await sut.Calculate(new LiftPassPriceCalulatorParameters("", null, DateTime.Now, cost));

        actual.Should().Be(expected);
    }
}