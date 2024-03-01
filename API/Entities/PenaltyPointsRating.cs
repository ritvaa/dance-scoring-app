using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class PenaltyPointsRating
{
    [Key] public Guid Id { get; set; }
    public Guid PenaltyPointsId { get; set; }
    public Guid TechJudgeRatingId { get; set; }

    #region Entity Relations

    public virtual PenaltyPoints PenaltyPoints { get; set; }
    public virtual TechJudgeRating TechJudgeRating { get; set; }

    #endregion
}