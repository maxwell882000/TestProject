using System.Collections;
using TestProjectLibrary.Dto.Requests;

namespace TestProjectStore.Repository.Interfaces;

using Route = TestProjectLibrary.Db.Entities.Route;

public interface IRouteRepository
{
    public IList<Route> Filter(SearchRouteRequest filter);

    public IQueryable<Route> Get();

    public Task<Route> Add(Route route);
    public Task<Route> Update(Route route);
    public Task<bool> Remove(Route route);

    public Task<IList<Route>> Add(IList<Route> route);
    public Task<IList<Route>> Update(IList<Route> route);
    public Task Remove(IList<Route> route);
}