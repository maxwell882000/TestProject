using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using AutoMapper;
using NuGet.Common;
using TestProject.V1.ProviderTwo.Requests;
using TestProject.V1.ProviderTwo.Services.Interfaces;
using TestProjectLibrary.Common;
using TestProjectLibrary.Db.Entities;
using TestProjectLibrary.Dto.Requests;
using Route = TestProjectLibrary.Db.Entities.Route;

namespace TestProject.V1.ProviderTwo.Services;

public class ProviderTwoService : IProviderTwoService
{
    private readonly IProviderTwoRestEase _providerTwoRestEase;
    private readonly IMapper _mapper;

    public ProviderTwoService(IProviderTwoRestEase providerTwoRestEase, IMapper mapper)
    {
        _providerTwoRestEase = providerTwoRestEase;
        _mapper = mapper;
    }

    public async Task<IList<Route>> Search(SearchRequest inRequest)
    {
        var outRequest = _mapper.Map<ProviderTwoSearchRequest>(inRequest);
        var inResponse = await _providerTwoRestEase.Search(outRequest);
        var outResponse = inResponse.Routes.Select(e =>
        {
            var splitDeparture = e.Departure.Point.Split("/");
            var originPoint = new Point()
            {
                Id = Guid.NewGuid(),
                Date = e.Departure.Date,
                Origin = splitDeparture[0],
                Destination = splitDeparture[1],
            };

            var splitArrival = e.Arrival.Point.Split("/");
            var destPoint = new Point()
            {
                Id = Guid.NewGuid(),
                Date = e.Arrival.Date,
                Origin = splitArrival[0],
                Destination = splitArrival[1]
            };

            return new Route()
            {
                Id = Guid.NewGuid(),
                Origin = originPoint.Origin,
                Destination = destPoint.Destination,
                OriginDateTime = originPoint.Date,
                DestinationDateTime = destPoint.Date,
                Price = e.Price,
                TimeLimit = e.TimeLimit,
                Hash = Hasher.Hash(
                    $"{originPoint.Origin}{originPoint.Destination}{originPoint.Date}{destPoint.Date}{e.TimeLimit}"),
                Points = new List<Point>()
                {
                    originPoint,
                    destPoint
                }
            };
        }).ToList();
        return outResponse;
    }

    public async Task<bool> CheckStatus()
    {
        try
        {
            // will throw error if responseCode is not 200 
            await _providerTwoRestEase.CheckStatus();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}