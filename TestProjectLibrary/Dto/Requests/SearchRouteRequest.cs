using System.Security.Policy;

namespace TestProjectLibrary.Dto.Requests;

public class SearchRouteRequest
{
    public SearchRequest? SearchRequest { get; set; }
    public IList<string> Hashes { get; set; } = new List<string>();
}