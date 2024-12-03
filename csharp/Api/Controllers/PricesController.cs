using System.Net;
using Domain;
using Domain.Database;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PricesController(
    LiftPassPriceCalculator calculator,
    BasePriceRepository basePriceRepository) : ControllerBase
{
    [HttpGet(Name = "GetPrices")]
    [ProducesResponseType<GetPricesResult>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get(int? age, string type, DateTime date, CancellationToken cancellationToken = default)
    {
        var cost = await basePriceRepository.GetCostByType(type, cancellationToken);
        var calculatedCosts = await calculator.Calculate(new LiftPassPriceCalulatorParameters(type, age, date, cost), cancellationToken);
        
        return Ok(new GetPricesResult
        {
            Cost = calculatedCosts
        });
    }

    [HttpPut(Name = "Put Price")]
    public Task<IActionResult> Put(int liftPassCost, string liftPassType)
    {
        throw new NotImplementedException();
    }
}

public class GetPricesResult
{
    public double? Cost { get; set; }
}