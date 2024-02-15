using System;
using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class RatingBonus
{
    [Key]
    public Guid Id { get; set; }
    public Guid JudgeRatingId { get; set; }
    public int BonusId { get;}

    #region Entity Relations

    public virtual Bonus Bonus { get; set; }
    public virtual JudgeRating JudgeRating { get; set; }

    #endregion
}