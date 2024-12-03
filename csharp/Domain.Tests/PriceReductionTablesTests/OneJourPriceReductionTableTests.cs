using Domain.PriceReductionTables;
using FluentAssertions;
using NSubstitute;

namespace Domain.Tests.PriceReductionTablesTests;

public class OneJourPriceReductionTableTests
{
    private readonly ISpecialReductionDaysService _specialReductionDaysService = Substitute.For<ISpecialReductionDaysService>();

    [Theory]
    [MemberData(nameof(MassTestsTestData))]
    public async Task MassTests(ReductionParameters parameters, bool isSpecialDay, double[] expected)
    {
        _specialReductionDaysService
            .IsSpecialDay(Arg.Any<SpecialReductionDaysParameters>())
            .Returns(isSpecialDay);
            
        var sut = new OneJourPriceReductionTable(_specialReductionDaysService);
        
        var actual = await sut
            .GetReductions(parameters)
            .ToListAsync();

        actual.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> MassTestsTestData()
    {
        yield return [
            new ReductionParameters(10, DateTime.Today),
            true,
            new[] { 0.3 }
        ];
        yield return [
            new ReductionParameters(10, DateTime.Today),
            false,
            new[] { 0.3 }
        ];
        yield return [
            new ReductionParameters(null, DateTime.Today),
            true,
            new[] { 0.35 }
        ];
        yield return [
            new ReductionParameters(null, DateTime.Today),
            false,
            new[] { 0.0 }
        ];
        yield return [
            new ReductionParameters(65, DateTime.Today),
            false,
            new[] { 0.25 }
        ];
        yield return [
            new ReductionParameters(65, DateTime.Today),
            true,
            new[] { 0.25, 0.35 }
        ];
        yield return [
            new ReductionParameters(40, DateTime.Today),
            false,
            new[] { 0.0 }
        ];
        yield return [
            new ReductionParameters(40, DateTime.Today),
            true,
            new[] { 0.35 }
        ];
    }
}