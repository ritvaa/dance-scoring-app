namespace API.Contracts;

public class RoutineModel
{
    public Guid Id { get; set; }
    public string? RoutineName { get; set; }
    public decimal Score { get; set; }

    public bool? GrandPrix { get; set; }
    // public CategoryModel Category { get; set; }
    // public SquadModel Squad { get; set; }
    // public TechJudgeRatingModel TechJudgeRating { get; set; }
    // public virtual IEnumerable<JudgeRatingModel> JudgeRating { get; set; }
}