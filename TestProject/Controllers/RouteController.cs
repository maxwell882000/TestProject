using Microsoft.AspNetCore.Mvc;
using TestProject.V1.Routes.Interfaces;
using TestProjectLibrary.Dto.Requests;
using TestProjectLibrary.Dto.Responses;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpPost("Search")]
        public async Task<ActionResult<SearchResponse>> Search(SearchRequest searchRequest)
        {
            return Ok(await _routeService.Search(searchRequest));
        }

        [HttpGet("IsAvailableAsync")]
        public async Task<ActionResult<bool>> IsAvailableAsync()
        {
            return Ok(await _routeService.CheckStatus());
        }
    }
}