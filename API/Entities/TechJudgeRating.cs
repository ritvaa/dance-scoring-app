﻿using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class TechJudgeRating
{
    [Key] public Guid Id { get; set; }

    public Guid RoutineId { get; set; }
    public Guid PenaltyPointId { get; set; }


    #region Entity Relations

    public virtual User User { get; set; }
    public virtual ICollection<PenaltyPointsRating> PenaltyPoints { get; set; }
    public virtual Routine Routine { get; set; }

    #endregion
}