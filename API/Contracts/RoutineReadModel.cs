namespace API.Contracts;

public class RoutineReadModel
{
    public Guid Id { get; set; }
    public string? RoutineName { get; set; }
    public decimal Score { get; set; }
    public bool? GrandPrix { get; set; }
    public required string CategoryName { get; set; }
    public required SquadModel Squad { get; set; }
    //public TechJudgeRatingReadModel TechJudgeRating { get; set; }
    //public ICollection<JudgeRating> JudgeRating { get; set; }
}