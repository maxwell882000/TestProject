using AutoMapper;
using TestProject.V1.ProviderTwo.Requests;
using TestProjectLibrary.Dto.Requests;

namespace TestProject.V1.ProviderTwo.Profiles;

public class ProviderTwoProfile : Profile
{
    public ProviderTwoProfile()
    {
        CreateMap<SearchRequest, ProviderTwoSearchRequest>()
            .ForMember(dest => dest.Arrival, src =>
                src.MapFrom(from => from.Destination))
            .ForMember(dest => dest.Departure, src =>
                src.MapFrom(from => from.Origin))
            .ForMember(dest => dest.DepartureDate, src =>
                src.MapFrom(from => from.OriginDateTime))
            .ForMember(dest => dest.MinTimeLimit, src =>
                src.MapFrom(from =>
                    from.Filters == null
                        ? null
                        : from.Filters.MinTimeLimit));
    }
}