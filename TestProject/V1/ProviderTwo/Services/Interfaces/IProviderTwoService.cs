using TestProjectLibrary.Dto.Requests;
using Route = TestProjectLibrary.Db.Entities.Route;

namespace TestProject.V1.ProviderTwo.Services.Interfaces;

public interface IProviderTwoService
{
    public Task<IList<Route>> Search(SearchRequest request);

    public Task<bool> CheckStatus();
}