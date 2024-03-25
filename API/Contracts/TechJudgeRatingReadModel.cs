namespace API.Contracts;

public class TechJudgeRatingReadModel
{
    public Guid JudgeId { get; set; }
    public List<PenaltyPointsReadModel> PenaltyPoints { get; set; }
}