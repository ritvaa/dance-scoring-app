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
        
        CreateMap<Dancer, DancerReadModel>()
            .ForMember(x => x.Team, 
                opt => opt.MapFrom(y => $"{y.Team.Name} {y.Team.Location}"));
        CreateMap<DancerImportModel, Dancer>()
            .ForMember(dest => dest.Team,
                opt => opt.MapFrom(src => new Team { Name = src.TeamName, Location = src.TeamLocationName }));
        CreateMap<DancerWriteModel, Dancer>()
            .ForMember(x => x.Team, opt => opt.MapFrom(y => MapTeams(y.TeamName, y.TeamLocationName)));
        CreateMap<Dancer, DancerSimpifliedReadModel>();
        
        CreateMap<CompetitionWriteModel, Competition>();
        CreateMap<Competition, CompetitionReadModel>();

        CreateMap<RoutineWriteModel, Routine>();
            // .ForMember(x => x.Category, opt => opt.Ignore())
            // .ForMember(x => x.Competition, opt => opt.Ignore())
            // .ForMember(x => x.Squad, opt => opt.Ignore());
        CreateMap<Routine, RoutineReadModel>();
        CreateMap<Routine, RoutineWithScoresReadModel>()
            .ForMember(x => x.TechJudgeRating, opt => opt.Ignore())
            .ForMember(x => x.JudgeRating, opt => opt.Ignore());
        CreateMap<Category, CategoryReadModel>();
        
        CreateMap<Team, TeamReadModel>();
        CreateMap<TeamReadModel, Team>();
        
        CreateMap<SquadWriteModel, Squad>();
        CreateMap<Squad, SquadReadModel>()
            .ForMember(x => x.TeamId, opt => opt.MapFrom(y => y.Team.Id))
            .ForMember(x => x.TeamName, opt => opt.MapFrom(y => $"{y.Team.Name} {y.Team.Location}"))
            .ForMember(x => x.Dancers, opt => opt.MapFrom(y => MapToSimplifiedDancerModel(y.Dancers)));
        
        CreateMap<JudgeRatingWriteModel, JudgeRating>()
            .ForMember(x => x.Routine, opt => opt.Ignore())
            .ForMember(x => x.User, opt => opt.Ignore());
        CreateMap<JudgeRating, JudgeRatingReadModel>();

        CreateMap<TechJudgeRating, TechJudgeRatingReadModel>()
            .ForMember(dest => dest.JudgeId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.PenaltyPoints, opt => opt.Ignore());

        CreateMap<PenaltyPoint, PenaltyPointsReadModel>();
    }
    

    private List<DancerSimpifliedReadModel> MapToSimplifiedDancerModel(ICollection<SquadDancer> dancers)
    {
        var simplifiedDancers = new List<DancerSimpifliedReadModel>();
        foreach (var dancer in dancers)
        {
            var simplifiedDancer = new DancerSimpifliedReadModel()
            {
                FirstName = dancer.Dancer.FirstName,
                LastName = dancer.Dancer.LastName,
                LicenceId = dancer.Dancer.LicenceId
            };
            simplifiedDancers.Add(simplifiedDancer);
        }
        return simplifiedDancers;

    }

    private Team MapTeams(string teamName, string teamLocationName)
    {
        var team = new Team()
        {
            Id = Guid.NewGuid(),
            Name = teamName,
            Location = teamLocationName
        };

        return team;
    }
}