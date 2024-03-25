using System.Collections;
using System.Text;
using API.Contracts;
using API.Contracts.Results;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Services;

public class RatingService : IRatingService
{
    private readonly IMapper _mapper;
    private readonly DancerScoringAppDbContext _dbContext;

    public RatingService(IMapper mapper, DancerScoringAppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public RoutineWithScoresReadModel GetRatingForRoutine(Guid routineId)
    {
        var routineWithScores = GetRoutines()
            .FirstOrDefault(x => x.Id == routineId);

        if (routineWithScores != null)
        {
            routineWithScores.Score = GetTotalScore(routineWithScores);
        }

        var routineWithScoresReadModel = _mapper.Map<RoutineWithScoresReadModel>(routineWithScores);

        var judgeRating = _mapper.Map<ICollection<JudgeRatingReadModel>>(routineWithScores.JudgeRating);
        var techJudgeRatingRead = new TechJudgeRatingReadModel
        {
            JudgeId = routineWithScores.TechJudgeRating.Select(y=>y.User.Id).FirstOrDefault(),
            PenaltyPoints =
                _mapper.Map<List<PenaltyPointsReadModel>>(routineWithScores.TechJudgeRating.Select(x => x.PenaltyPoint).ToList())
        };

        routineWithScoresReadModel.JudgeRating = judgeRating;
        routineWithScoresReadModel.TechJudgeRating = techJudgeRatingRead;
        
        return routineWithScoresReadModel;
    }

    private IIncludableQueryable<Routine, Category> GetRoutines()
    {
        return _dbContext.Routines.Include(x => x.JudgeRating)
            .ThenInclude(x => x.User)
            .Include(x => x.TechJudgeRating)
            .ThenInclude(x=>x.PenaltyPoint)
            .Include(x=>x.TechJudgeRating)
            .ThenInclude(x=>x.User)
            .Include(x => x.Squad)
            .ThenInclude(x=>x.Team)
            .Include(x=>x.Squad.Dancers)
            .ThenInclude(x=>x.Dancer)
            .Include(x => x.Category);
    }

    private decimal GetTotalScore(Routine routine)
    {
        //todo change all decimal to double
        decimal totalScore = 0;
        foreach (var judgeRating in routine.JudgeRating)
        {
            var score = judgeRating.ChoreographyPoints + judgeRating.BodyTechniquePoints +
                        judgeRating.RequisiteWorkPoints;

            totalScore += score;
        }

        var penaltyPoints = _dbContext.TechJudgeRatings.Include(x => x.PenaltyPoint)
            .Where(x => x.RoutineId == routine.Id).Select(x => x.PenaltyPoint.PenaltyScore).Sum();

        return totalScore + penaltyPoints;
    }

    public async Task<OperationResult<Guid>> AddJudgeRatingToRoutine(Guid routineId, Guid judgeId,
        JudgeRatingWriteModel judgeRating)
    {
        var routine = _dbContext.Routines.FirstOrDefault(x => x.Id == routineId);
        if (routine == null) return OperationResult<Guid>.Fail("Routine does not exist");

        var judge = _dbContext.Users.FirstOrDefault(x => x.Id == judgeId && x.Roles.Any(y =>
            y.Role.Name == RoleType.Judge.ToString()));
        if (judge == null) return OperationResult<Guid>.Fail("Judge does not exist");

        var newJudgeRating = _mapper.Map<JudgeRating>(judgeRating);
        newJudgeRating.Id = Guid.NewGuid();
        newJudgeRating.Routine = routine;
        newJudgeRating.User = judge;

        try
        {
            await _dbContext.JudgeRatings.AddAsync(newJudgeRating);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<Guid>.Fail("An error occurred while adding judge rating: " + ex.Message);
        }

        return OperationResult<Guid>.Success(newJudgeRating.Id);
    }

    public OperationResult<string> AddTechJudgeRatingToRoutine(Guid routineId, Guid judgeId,
        TechJudgeRatingWriteModel techJudgeRating)
    {
        var routine = _dbContext.Routines.FirstOrDefault(x => x.Id == routineId);
        if (routine == null) return OperationResult<string>.Fail("Routine does not exist");

        var judge = _dbContext.Users.FirstOrDefault(x => x.Id == judgeId && x.Roles.Any(y =>
            y.Role.Name == RoleType.Judge.ToString() || y.Role.Name == RoleType.TechnicalJudge.ToString()));

        if (judge == null) return OperationResult<string>.Fail("Judge does not exist");

        var penaltyPointsList = _dbContext.PenaltyPoints.ToList();
        foreach (var penaltyPointId in techJudgeRating.PenaltyPointIds)
        {
            var techJudgeRatingEntity = new TechJudgeRating
            {
                Id = Guid.NewGuid(),
                RoutineId = routineId,
                PenaltyPoint = penaltyPointsList.FirstOrDefault(x => x.Id == penaltyPointId)!,
                User = judge
            };
            _dbContext.TechJudgeRatings.Add(techJudgeRatingEntity);
            _dbContext.SaveChanges();
        }


        return OperationResult<string>.Success("Tech judge rating was added successfully");
    }
    
    public static void ExportRoutineScoresToCsvByCategory(string filePath, Guid competitionId)
    {
        using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            writer.WriteLine("Lp.,Imię, Nazwisko, Nazwa Drużyny, Punkty, Miejsce");

            var routines = GetRoutinesWithScoresByCompetitionId(competitionId);

            foreach (var category in routines.Keys)
            {
                foreach (var routine in routines[category])
                {
                    string dancerNames;
                    string teamName = "";
                    string teamLocation = "";

                    if (routine.Squad != null)
                    {
                        teamName = routine.Squad.Team.Name;
                        teamLocation = routine.Squad.Team.Location;
                    }
                    else if (routine.JudgeRating.Count() == 1)
                    {
                        dancerNames = routine.Squad.Dancers.Select(x=>x.Dancer.FirstName + " " + x.Dancer.LastName).FirstOrDefault();
                    }
                    else // Duo/trio
                    {
                        var dancerNamesList = routine.Squad.Dancers.Select(x=>x.Dancer.FirstName + " " + x.Dancer.LastName).ToList();
                        dancerNames = string.Join(", ", dancerNamesList);
                    }
                    
                    string combinedCategory = $"{category} - {routine.Category}";

                    writer.WriteLine($"{combinedCategory},{routine.PlaceInRank},{dancerNames},{teamName},{teamLocation},{routine.Score}");
                }
            }
        }
        
        
    }
    
   private Dictionary<string, IEnumerable<RoutineWithScoresReadModel>> GetRoutinesWithScoresByCompetitionId(Guid competitionId)
{
    // Pobierz wszystkie oceny dla zawodów
    var allJudgeRatings = GetRoutines()
        .Where(x => x.Competition.Id == competitionId)
        .ToList();
    
    var alTechJudgeRatings = _dbContext.TechJudgeRatings
        .Include(jr => jr.User)
        .Include(jr => jr.Routine)
        .Where(jr => jr.Routine.Competition.Id == competitionId)
        .ToList();

    // Pobierz wszystkie drużyny
    var allSquads = _dbContext.Squads.ToList();

    // Grupuj oceny według identyfikatora rutyny
    var routinesWithRatings = allJudgeRatings
        .GroupBy(jr => jr.Routine.Id)
        .Select(g => new Routine()
        {
            Id = g.Key,
            Score = GetTotalScore(g.First().Routine), 
            Category = g.First().Routine.Category,
            JudgeRating = g.Select(jr => new JudgeRating()
            {
                Id = jr.Id,
                ChoreographyPoints = jr.ChoreographyPoints,
                BodyTechniquePoints = jr.BodyTechniquePoints,
                RequisiteWorkPoints = jr.RequisiteWorkPoints,
                Comment = jr.Comment,
            }).ToList(),
            TechJudgeRating = g.Select(x=>x.)
            Squad = allSquads.FirstOrDefault(s => s.Id == g.First().Routine.Squad.Id)!
        })
        .ToList();

    // Ustal pozycje w rankingu dla każdej kategorii
    foreach (var category in routinesWithRatings.Select(r => r.Category).Distinct())
    {
        var categoryRoutines = routinesWithRatings.Where(r => r.Category == category).OrderByDescending(r => r.Score).ToList();
        for (int i = 0; i < categoryRoutines.Count; i++)
        {
            categoryRoutines[i].PlaceInRank = i + 1;
        }
    }

    // Zgrupuj rutyny według kategorii
    var routinesByCategory = routinesWithRatings.GroupBy(r => r.Category).ToDictionary(g => g.Key, g => g);

    return routinesByCategory;
}



}