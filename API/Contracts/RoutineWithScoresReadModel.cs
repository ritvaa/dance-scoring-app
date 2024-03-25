using Newtonsoft.Json;

namespace API.Contracts;

public class RoutineWithScoresReadModel
{
    public Guid Id { get; set; }
    public string RoutineName { get; set; }
    public string Category { get; set; }
    public decimal Score { get; set; }
    public int PlaceInRank { get; set; }
    public bool GrandPrix { get; set; }
    public SquadReadModel Squad { get; set; }
    public TechJudgeRatingReadModel TechJudgeRating { get; set; }
    public IEnumerable<JudgeRatingReadModel> JudgeRating { get; set; }
}