﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class Competition
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public string Location { get; set; }


    #region Entity Relations
    public virtual ICollection<UserCompetition> Users { get; set; }
    public virtual ICollection<Routine> Routinines { get; set; }


    #endregion
}