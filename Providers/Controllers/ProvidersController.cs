using Microsoft.AspNetCore.Mvc;

namespace Providers.Controllers;

[ApiController]
[Route("api/v1/")]
public class ProvidersController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ProvidersController> _logger;

    public ProvidersController(ILogger<ProvidersController> logger)
    {
        _logger = logger;
    }

    [HttpPost("search")]
    public IActionResult SearchOne()
    {
        return Ok(new
        {
            Routes = new List<dynamic>()
            {
                new
                {
                    From = "Moscow",
                    To = "Tashkent",
                    DateFrom = new DateTime(2023, 7, 7).AddHours(10),
                    DateTo = new DateTime(2023, 7, 7).AddHours(100),
                    Price = 124124,
                    TimeLimit = new DateTime(2023, 7, 7).AddHours(2)
                },
                new
                {
                    From = "Moscow_2",
                    To = "Tashkent_2",
                    DateFrom = new DateTime(2023, 7, 7).AddHours(24),
                    DateTo = new DateTime(2023, 7, 7).AddHours(54),
                    Price = 1242,
                    TimeLimit = new DateTime(2023, 7, 7).AddHours(65)
                },
                new
                {
                    From = "Moscow_3",
                    To = "Tashkent_3",
                    DateFrom = new DateTime(2023, 7, 7).AddHours(1),
                    DateTo = new DateTime(2023, 7, 7).AddHours(4),
                    Price = 1242,
                    TimeLimit = new DateTime(2023, 7, 7).AddHours(2)
                },
            }
        });
    }

    [HttpPost("search_2")]
    public IActionResult SearchTwo()
    {
        return Ok(
            new
            {
                Routes = new List<dynamic>()
                {
                    new
                    {
                        Departure =
                            new
                            {
                                Point = "Moscow/Samarkand",
                                Date = new DateTime(2023, 7, 7).AddHours(4),
                            },
                        Arrival =
                            new
                            {
                                Point = "Samarkand/Tashkent",
                                Date = new DateTime(2023, 7, 7).AddHours(8),
                            },
                        Price = 24124,
                        TimeLimit = new DateTime(2023, 7, 7).AddHours(1)
                    },
                    new
                    {
                        Departure =
                            new
                            {
                                Point = "Moscow_2/Samarkand_2",
                                Date = new DateTime(2023, 7, 7).AddHours(4),
                            },
                        Arrival =
                            new
                            {
                                Point = "Samarkand_2/Tashkent_2",
                                Date = new DateTime(2023, 7, 7).AddHours(8),
                            },
                        Price = 5432,
                        TimeLimit = new DateTime(2023, 7, 7).AddHours(1)
                    },
                    new
                    {
                        Departure =
                            new
                            {
                                Point = "Moscow_3/Samarkand_3",
                                Date = new DateTime(2023, 7, 7).AddHours(6),
                            },
                        Arrival =
                            new
                            {
                                Point = "Samarkand_3/Tashkent_3",
                                Date = new DateTime(2023, 7, 7).AddHours(12),
                            },
                        Price = 124,
                        TimeLimit = new DateTime(2023, 7, 7).AddHours(4)
                    },
                }
            }
        );
    }

    [HttpGet("ping")]
    public IActionResult check()
    {
        return Ok(true);
    }
}