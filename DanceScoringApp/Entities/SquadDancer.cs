﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class SquadDancer
{
    [Key]
    public Guid Id { get; set; }
    public Guid SquadId { get; set; }
    public Guid DancerId { get; set; }

    #region Entity Relations

    public virtual Dancer Dancer { get; set; }
    public virtual Squad Squad { get; set; }

    #endregion
}