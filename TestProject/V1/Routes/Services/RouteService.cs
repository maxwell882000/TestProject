using TestProject.V1.ProviderOne.Services.Interfaces;
using TestProject.V1.ProviderTwo.Services.Interfaces;
using TestProject.V1.Routes.Interfaces;
using TestProjectLibrary.Dto.Requests;
using TestProjectLibrary.Dto.Responses;
using TestProjectLibrary.RabbitMq.Events;
using TestProjectLibrary.RabbitMq.Interfaces;
using Route = TestProjectLibrary.Db.Entities.Route;

namespace TestProject.V1.Routes.Services;

public class RouteService : IRouteService
{
    private readonly ISearchService _searchService;
    private readonly IProviderOneService _providerOneService;
    private readonly IProviderTwoService _providerTwoService;
    private readonly IEventBus _eventBus;


    public RouteService(ISearchService searchService, IProviderOneService providerOneService,
        IProviderTwoService providerTwoService, IEventBus eventBus)
    {
        _searchService = searchService;
        _providerOneService = providerOneService;
        _providerTwoService = providerTwoService;
        _eventBus = eventBus;
    }


    public async Task<SearchResponse> Search(SearchRequest request)
    {
        var response = new List<Route>();

        if (request.Filters?.OnlyCached != true)
        {
            var checkOneProvider = await _providerOneService.CheckStatus();
            if (checkOneProvider)
            {
                response.AddRange(await _providerOneService.Search(request));
            }

            var checkTwoProvider = await _providerTwoService.CheckStatus();
            if (checkTwoProvider)
            {
                response.AddRange(await _providerTwoService.Search(request));
            }
        }

        _eventBus.Publish(new RouteEvent() { Id = Guid.NewGuid(), Routes = response });

        var checkRouteProvider = await CheckStatus();
        if (checkRouteProvider)
        {
            response.AddRange(await _searchService.SearchAsync(new SearchRouteRequest()
            {
                Hashes = response.Select(e => e.Hash).ToList(),
                SearchRequest = request
            }));
        }

        if (!response.Any()) return new SearchResponse();

        return new SearchResponse()
        {
            Routes = response.ToArray(),
            MaxPrice = response.Max(e => e.Price),
            MinPrice = response.Min(e => e.Price),
            MinMinutesRoute = (int)response.Min(e =>
                e.DestinationDateTime - e.OriginDateTime).TotalMinutes,
            MaxMinutesRoute = (int)response.Max(e =>
                e.DestinationDateTime - e.OriginDateTime).TotalMinutes
        };
    }

    public async Task<bool> CheckStatus()
    {
        try
        {
            await _searchService.IsAvailableAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}