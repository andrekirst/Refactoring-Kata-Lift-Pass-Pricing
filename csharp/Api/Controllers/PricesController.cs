using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PricesController(ILogger<PricesController> logger) : ControllerBase
{
    [HttpGet(Name = "GetPrices")]
    public Task<IActionResult> Get(int? age)
    {
        throw new NotImplementedException();
    }

    [HttpPut(Name = "Put Price")]
    public Task<IActionResult> Put(int liftPassCost, string liftPassType)
    {
        throw new NotImplementedException();
    }
}