using TestProjectLibrary.Dto.Requests;
using TestProjectLibrary.Dto.Responses;

namespace TestProject.V1.Routes.Interfaces;

public interface IRouteService
{
    public Task<SearchResponse> Search(SearchRequest request);

    public Task<bool> CheckStatus();
}