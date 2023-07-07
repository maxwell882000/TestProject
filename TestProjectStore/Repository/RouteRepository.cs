using LinqKit;
using Microsoft.EntityFrameworkCore;
using TestProjectLibrary.Db.Context;
using TestProjectLibrary.Dto.Requests;
using TestProjectStore.Repository.Interfaces;
using Route = TestProjectLibrary.Db.Entities.Route;

namespace TestProjectStore.Repository;

public class RouteRepository : IRouteRepository
{
    private readonly PgContext _context;
    private readonly ILogger<RouteRepository> _logger;

    public RouteRepository(PgContext context, ILogger<RouteRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Route> Update(Route route)
    {
        _context.Routes.Update(route);
        await _context.SaveChangesAsync();
        return route;
    }

    public IList<Route> Filter(SearchRouteRequest filter)
    {
        var predicate = PredicateBuilder.New<Route>();

        predicate = predicate.Start(e => !filter.Hashes.Contains(e.Hash));

        if (filter.SearchRequest != null)
        {
            predicate = predicate.And(e => e.Destination == filter.SearchRequest.Destination);

            predicate = predicate.And(e => e.Origin == filter.SearchRequest.Origin);

            predicate = predicate.And(e =>
                e.OriginDateTime.AddHours(-5) <= filter.SearchRequest.OriginDateTime
                && filter.SearchRequest.OriginDateTime <= e.OriginDateTime.AddHours(5));

            if (filter.SearchRequest.Filters?.MaxPrice != null)
            {
                predicate = predicate.And(e => e.Price <= filter.SearchRequest.Filters.MaxPrice);
            }

            if (filter.SearchRequest.Filters?.MinTimeLimit != null)
            {
                predicate = predicate.And(e => e.TimeLimit >= filter.SearchRequest.Filters.MinTimeLimit);
            }
            else
            {
                predicate = predicate.And(e => e.TimeLimit >= DateTime.Now.AddMinutes(5));
            }

            if (filter.SearchRequest.Filters?.DestinationDateTime != null)
            {
                predicate = predicate.And(e =>
                    e.DestinationDateTime.AddHours(-5) <= filter.SearchRequest.Filters.DestinationDateTime
                    && filter.SearchRequest.Filters.DestinationDateTime <= e.DestinationDateTime.AddHours(5));
            }
        }

        return _context.Routes
            .Where(predicate)
            .Include(e => e.Points).ToList();
    }

    public IQueryable<Route> Get()
    {
        return _context.Routes;
    }

    public async Task<Route> Add(Route route)
    {
        _context.Routes.Add(route);
        await _context.SaveChangesAsync();
        return route;
    }

    public async Task<bool> Remove(Route route)
    {
        var result = _context.Routes.Remove(route);
        await _context.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }


    public async Task<IList<Route>> Update(IList<Route> route)
    {
        _context.Routes.UpdateRange(route);
        await _context.SaveChangesAsync();
        return route;
    }

    public async Task<IList<Route>> Add(IList<Route> route)
    {
        _context.Routes.AddRange(route);
        await _context.SaveChangesAsync();
        return route;
    }

    public async Task Remove(IList<Route> route)
    {
        _context.Routes.RemoveRange(route);
        await _context.SaveChangesAsync();
    }
}