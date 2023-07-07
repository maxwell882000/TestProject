using TestProjectLibrary.Dto.Requests;
using TestProjectStore.Repository.Interfaces;
using TestProjectStore.Service.Interfaces;
using Route = TestProjectLibrary.Db.Entities.Route;

namespace TestProjectStore.Service;

public class RouteService : IRouteService
{
    private readonly IRouteRepository _repository;

    public RouteService(IRouteRepository repository)
    {
        _repository = repository;
    }

    private IList<Route> ValidateResult(IList<Route> routes)
    {
        var hashes = routes.Select(e => e.Hash).ToList();
        var dbResult = _repository
            .Get()
            .Where(e => hashes.Contains(e.Hash) && e.TimeLimit > DateTime.Now.AddMinutes(5))
            .Select(e => e.Hash).ToList();

        var outRoutes = new List<Route>();

        foreach (var route in routes)
        {
            if (!dbResult.Contains(route.Hash))
            {
                outRoutes.Add(route);
            }
        }

        return outRoutes;
    }

    public async Task<IList<Route>> Add(IList<Route> routes)
    {
        routes = ValidateResult(routes);
        await _repository.Add(routes);
        return routes;
    }
}