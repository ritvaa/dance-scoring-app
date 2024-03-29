﻿using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Routine
{
    [Key] public Guid Id { get; set; }
    public string? RoutineName { get; set; }
    
    public bool? GrandPrix { get; set; }

    #region Entity Relations

    public virtual Category Category { get; set; }
    public virtual Squad Squad { get; set; }
    public virtual ICollection<TechJudgeRating> TechJudgeRating { get; set; }
    public virtual ICollection<JudgeRating> JudgeRating { get; set; }
    public virtual Competition Competition { get; set; }

    #endregion
}

