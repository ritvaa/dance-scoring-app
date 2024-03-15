namespace API.Contracts;

public record JudgeRatingWriteModel(Guid routineId, Guid judgeId, decimal choreographyPoints, decimal bodyTechniquePoints,
    decimal propWorkPoints, List<BonusModel> bonuses, string comment)
{
}