using API.Contracts;
using API.Entities;
using AutoMapper;

namespace API.Profiles;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<User, UserReadModel>();
        CreateMap<UserWriteModel, User>();
        CreateMap<Dancer, DancerReadModel>();
        CreateMap<DancerWriteModel, Dancer>()
            .ForMember(dest => dest.Team,
                opt => opt.MapFrom(src => new Team { Name = src.TeamName, Location = src.TeamLocationName }));
    }
}