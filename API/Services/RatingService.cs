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

        if (routineWithScores != null) routineWithScores.Score = GetTotalScore(routineWithScores);

        var routineWithScoresReadModel = _mapper.Map<RoutineWithScoresReadModel>(routineWithScores);

        var judgeRating = _mapper.Map<ICollection<JudgeRatingReadModel>>(routineWithScores.JudgeRating);
        var techJudgeRatingRead = new TechJudgeRatingReadModel
        {
            JudgeId = routineWithScores.TechJudgeRating.Select(y => y.User.Id).FirstOrDefault(),
            PenaltyPoints =
                _mapper.Map<List<PenaltyPointsReadModel>>(routineWithScores.TechJudgeRating.Select(x => x.PenaltyPoint)
                    .ToList())
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
            .ThenInclude(x => x.PenaltyPoint)
            .Include(x => x.TechJudgeRating)
            .ThenInclude(x => x.User)
            .Include(x => x.Squad)
            .ThenInclude(x => x.Team)
            .Include(x => x.Squad.Dancers)
            .ThenInclude(x => x.Dancer)
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

    public void ExportRoutineScoresToCsvByCategory(string filePath, Guid competitionId)
    {

        var routines = GetRoutinesWithScoresByCompetitionId(competitionId);
        // using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        // {
        //     writer.WriteLine("Lp.,Imię, Nazwisko, Nazwa Drużyny, Punkty, Miejsce");
        //
        //     var routines =  GetRoutines()
        //         .Where(x => x.Competition.Id == competitionId)
        //         .GroupBy(x=>x.Category.Id).ToArray();
        //     
        //      foreach (var category in routines)
        //      {
        //          foreach (var routine in routines[category])
        //          {
        //              string dancerNames;
        //              string teamName = "";
        //              string teamLocation = "";
        //     
        //              if (routine.Squad != null)
        //              {
        //                  teamName = routine.Squad.Team.Name;
        //                  teamLocation = routine.Squad.Team.Location;
        //              }
        //              else if (routine.JudgeRating.Count() == 1)
        //              {
        //                  dancerNames = routine.Squad.Dancers.Select(x=>x.Dancer.FirstName + " " + x.Dancer.LastName).FirstOrDefault();
        //              }
        //              else // Duo/trio
        //              {
        //                  var dancerNamesList = routine.Squad.Dancers.Select(x=>x.Dancer.FirstName + " " + x.Dancer.LastName).ToList();
        //                  dancerNames = string.Join(", ", dancerNamesList);
        //              }
        //              
        //              string combinedCategory = $"{category} - {routine.Category}";
        //     
        //              writer.WriteLine($"{combinedCategory},{routine.PlaceInRank},{dancerNames},{teamName},{teamLocation},{routine.Score}");
        //          }
        //     }
        // }
    }

    private List<RankingExportModel> GetRoutinesWithScoresByCompetitionId(Guid competitionId)
    {
        var routineByCategoryList = new List<RoutineExportModel>();
        var categoriesWithRoutines = new List<RankingExportModel>();
        
        var allRoutinesWithRatings = _dbContext.Routines
            .Where(x => x.Competition.Id == competitionId);

        var categoriesInCompetition = allRoutinesWithRatings.Select(x => x.Category).ToList();

        foreach (var category in categoriesInCompetition)
        {
            var routinesForCategory =
                GetRoutines().Where(x => x.Competition.Id == competitionId && x.Category.Id == category.Id).ToList();

            foreach (var routine in routinesForCategory)
            {

                var mappedRoutine = _mapper.Map<RoutineExportModel>(routine);
                mappedRoutine.Sum = GetTotalScore(routine).ToString();
                
                routineByCategoryList.Add(mappedRoutine);
            }

            var categoryWithRoutine = new RankingExportModel
            {
                Category = $"{category.Requisite} {category.CategoryType} {category.SquadType} {category.AgeCategory}",
                Routines = routineByCategoryList
            };
            
            categoriesWithRoutines.Add(categoryWithRoutine);
            
        }
        return categoriesWithRoutines;
    }
}