namespace TestProjectStore.Service.Interfaces;

using Route = TestProjectLibrary.Db.Entities.Route;

public interface IRouteService
{
    public Task<IList<Route>> Add(IList<Route> routes);
}