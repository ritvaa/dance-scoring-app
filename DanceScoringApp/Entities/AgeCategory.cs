﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class AgeCategory
{
    [Key]
    public int Id { get; set; }
    public AgeCategoryType AgeCategoryType { get; set; }

    #region Entity Relations

    public virtual ICollection<Category> Categories { get; set; }

    #endregion
}

public enum AgeCategoryType
{
    Cadet,
    Junior,
    Senior,
    GrandSenior
}