using Domain.PriceReductionTables;
using FluentAssertions;

namespace Domain.Tests.PriceReductionTablesTests;

public class NightPriceReductionTableTests
{
    [Theory]
    [MemberData(nameof(MassTestsTestData))]
    public async Task MassTests(ReductionParameters parameters, double[] expected)
    {
        var sut = new NightPriceReductionTable();
        var actual = await sut.GetReductions(parameters)
            .ToListAsync();

        actual.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> MassTestsTestData()
    {
        yield return
        [
            new ReductionParameters(5),
            new[] { 1.0 }
        ];
        yield return
        [
            new ReductionParameters(65),
            new[] { 0.6 }
        ];
        yield return
        [
            new ReductionParameters(40),
            new[] { 0.0 }
        ];
    }
}