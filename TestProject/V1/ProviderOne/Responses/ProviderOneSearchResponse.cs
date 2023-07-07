namespace TestProject.V1.ProviderOne.Responses;

public class ProviderOneSearchResponse
{
    // Mandatory
    // Array of routes
    public IList<ProviderOneRoute> Routes { get; set; }
}