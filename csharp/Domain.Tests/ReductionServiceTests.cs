using FluentAssertions;

namespace Domain.Tests;

public class ReductionServiceTests
{
    [Theory]
    [MemberData(nameof(CalculateReductionMassTestsTestData))]
    public void CalculateReductionMassTests(ReductionParameters parameters, double[] expected)
    {
        var service = new ReductionService();
        var actual = service.CalculateReductions(parameters);
        actual.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> CalculateReductionMassTestsTestData()
    {
        yield return
        [
            new ReductionParameters(5, "unknown"),
            new[] { 1.0 }
        ];
        yield return
        [
            new ReductionParameters(5, "night"),
            new[] { 1.0 }
        ];
        yield return
        [
            new ReductionParameters(10, "1jour", false, DayOfWeek.Monday),
            new[] { 0.3 }
        ];
        yield return
        [
            new ReductionParameters(10, "1jour", false, DayOfWeek.Tuesday),
            new[] { 0.3 }
        ];
        yield return
        [
            new ReductionParameters(null, "1jour", false, DayOfWeek.Monday),
            new[] { 0.35 }
        ];
        yield return
        [
            new ReductionParameters(null, "1jour", false, DayOfWeek.Tuesday),
            new[] { 0.0 }
        ];
        yield return
        [
            new ReductionParameters(null, "1jour", true, DayOfWeek.Monday),
            new[] { 0.0 }
        ];
        yield return
        [
            new ReductionParameters(null, "1jour", true, DayOfWeek.Tuesday),
            new[] { 0.0 }
        ];
        yield return
        [
            new ReductionParameters(65, "1jour", true, DayOfWeek.Friday),
            new[] { 0.25 }
        ];
        yield return
        [
            new ReductionParameters(65, "1jour", false, DayOfWeek.Monday),
            new[] { 0.25, 0.35 }
        ];
        yield return
        [
            new ReductionParameters(40, "1jour", true, DayOfWeek.Monday),
            new[] { 0.0 }
        ];
        yield return
        [
            new ReductionParameters(40, "1jour", false, DayOfWeek.Monday),
            new[] { 0.35 }
        ];
        yield return
        [
            new ReductionParameters(3, "night"),
            new[] { 1.0 }
        ];
        yield return
        [
            new ReductionParameters(65, "night"),
            new[] { 0.6 }
        ];
        yield return
        [
            new ReductionParameters(40, "night"),
            new[] { 0.0 }
        ];
    }
}