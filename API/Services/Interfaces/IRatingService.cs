using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface IRatingService
{
    //add judge rating to routine (routine id, judge id)
    //add tech judge rating to routine (routine id, judge id, list<penaltyPoints>)
    //update judge rating 
    //update tech judge rating
    //delete judge rating
    //delete tech judge rating

    RoutineWithScoresReadModel GetRatingForRoutine(Guid routineId);

    Task<OperationResult<Guid>> AddJudgeRatingToRoutine(Guid routineId, Guid judgeId, JudgeRatingWriteModel judgeRating);
    OperationResult<string> AddTechJudgeRatingToRoutine(Guid routineId,  Guid judgeId, TechJudgeRatingWriteModel techJudgeRating);
    void ExportRoutineScoresToCsvByCategory(string filePath, Guid competitionId);

}