using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media.Animation;

namespace DancerScoringApp.Entities;

public class Routine
{
    [Key]
    public Guid Id { get; set; }
    public string? RoutineName { get; set; }
    public decimal Score { get; set; }
    public bool? GrandPrix { get; set; }

    #region Entity Relations

    public virtual Category Category { get; set; }
    public virtual Squad Squad { get; set; }
    public virtual TechJudgeRating TechJudgeRating { get; set; }
    public virtual JudgeRating JudgeRating { get; set; }

    #endregion


}