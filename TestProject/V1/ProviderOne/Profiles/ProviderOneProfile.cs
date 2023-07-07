using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using AutoMapper;
using TestProject.V1.ProviderOne.Requests;
using TestProject.V1.ProviderOne.Responses;
using TestProjectLibrary.Common;
using TestProjectLibrary.Dto.Requests;
using Route = TestProjectLibrary.Db.Entities.Route;

namespace TestProject.V1.ProviderOne.Profiles;

public class ProviderOneProfile : Profile
{
    public ProviderOneProfile()
    {
        CreateMap<SearchRequest, ProviderOneSearchRequest>()
            .ForMember(dest => dest.From,
                src =>
                    src.MapFrom(from => from.Origin))
            .ForMember(dest => dest.To,
                src =>
                    src.MapFrom(from => from.Destination))
            .ForMember(dest => dest.DateFrom,
                src =>
                    src.MapFrom(from => from.OriginDateTime))
            .ForMember(dest => dest.DateTo,
                src =>
                    src.MapFrom(from => from.Filters == null
                        ? null
                        : from.Filters.DestinationDateTime))
            .ForMember(dest => dest.MaxPrice,
                src =>
                    src.MapFrom(from => from.Filters == null
                        ? null
                        : from.Filters.MaxPrice));

        CreateMap<ProviderOneRoute, Route>()
            .ForMember(dest => dest.Id, src =>
                src.MapFrom(e => Guid.NewGuid()))
            .ForMember(dest => dest.Origin, src =>
                src.MapFrom(from => from.From))
            .ForMember(dest => dest.Destination, src =>
                src.MapFrom(from => from.To))
            .ForMember(dest => dest.OriginDateTime, src =>
                src.MapFrom(from => from.DateFrom))
            .ForMember(dest => dest.DestinationDateTime, src =>
                src.MapFrom(from => from.DateTo))
            .ForMember(dest => dest.Price, src =>
                src.MapFrom(from => from.Price))
            .ForMember(dest => dest.Hash, src =>
                src.MapFrom(from =>
                    Hasher.Hash($"{from.From}{from.To}{from.DateFrom}{from.DateTo}{from.TimeLimit}")
                ));
    }
}