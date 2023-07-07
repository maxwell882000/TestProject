using AutoMapper;
using TestProject.V1.ProviderOne.Requests;
using TestProject.V1.ProviderOne.Services.Interfaces;
using TestProjectLibrary.Dto.Requests;
using Route = TestProjectLibrary.Db.Entities.Route;


namespace TestProject.V1.ProviderOne.Services;

public class ProviderOneService : IProviderOneService
{
    private readonly IProviderOneRestEase _providerOneRestEase;
    private readonly IMapper _mapper;

    public ProviderOneService(IProviderOneRestEase providerOneRestEase, IMapper mapper)
    {
        _providerOneRestEase = providerOneRestEase;
        _mapper = mapper;
    }

    public async Task<IList<Route>> Search(SearchRequest inRequest)
    {
        var outRequest = _mapper.Map<ProviderOneSearchRequest>(inRequest);
        var inResponse = await _providerOneRestEase.Search(outRequest);
        var outResponse = _mapper.Map<IList<Route>>(inResponse.Routes);
        return outResponse;
    }

    public async Task<bool> CheckStatus()
    {
        try
        {
            // will throw error if responseCode is not 200 
            await _providerOneRestEase.CheckStatus();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}