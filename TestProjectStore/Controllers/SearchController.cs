using Microsoft.AspNetCore.Mvc;
using TestProjectLibrary.Dto.Requests;
using TestProjectStore.Repository.Interfaces;
using Route = TestProjectLibrary.Db.Entities.Route;

namespace TestProjectStore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IRouteRepository _repository;

        public SearchController(IRouteRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult<IList<Route>> Search(SearchRouteRequest searchRequest)
        {
            return Ok(_repository.Filter(searchRequest));
        }
    }
}