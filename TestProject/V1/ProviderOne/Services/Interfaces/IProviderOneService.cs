using TestProjectLibrary.Dto.Requests;
using Route = TestProjectLibrary.Db.Entities.Route;

namespace TestProject.V1.ProviderOne.Services.Interfaces;

public interface IProviderOneService
{
    public Task<IList<Route>> Search(SearchRequest request);

    public Task<bool> CheckStatus();
}