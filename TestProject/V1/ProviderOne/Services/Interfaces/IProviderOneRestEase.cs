using RestEase;
using TestProject.V1.ProviderOne.Requests;
using TestProject.V1.ProviderOne.Responses;

namespace TestProject.V1.ProviderOne.Services.Interfaces;

public interface IProviderOneRestEase
{
    [Post("api/v1/search")]
    public Task<ProviderOneSearchResponse> Search(ProviderOneSearchRequest request);

    [Get("api/v1/ping")]
    public Task CheckStatus();
}