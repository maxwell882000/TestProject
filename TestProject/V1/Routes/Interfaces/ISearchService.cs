using RestEase;
using TestProjectLibrary.Dto.Requests;
using Route = TestProjectLibrary.Db.Entities.Route;

namespace TestProject.V1.Routes.Interfaces;

public interface ISearchService
{
    [Post("api/v1/Search")]
    Task<IList<Route>> SearchAsync([Body] SearchRouteRequest request);

    [Get("/ping")]
    Task IsAvailableAsync();
}