using RestEase;
using TestProject.V1.ProviderTwo.Requests;
using TestProject.V1.ProviderTwo.Responses;

namespace TestProject.V1.ProviderTwo.Services.Interfaces;

public interface IProviderTwoRestEase
{
    [Post("api/v1/search")]
    public Task<ProviderTwoSearchResponse> Search(ProviderTwoSearchRequest request);

    [Get("api/v1/ping")]
    public Task CheckStatus();
}