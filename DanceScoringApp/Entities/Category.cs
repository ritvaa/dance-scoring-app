using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DancerScoringApp.Entities;

public class Category
{
    [Key]
    public Guid Id { get; set; }
    public PropType Prop { get; set; }
    public CategoryType CategoryType { get; set; }

    #region Entity Relations

    public virtual AgeCategory AgeCategory { get; set; }
    public virtual ICollection<Routine> Routines { get; set; }

    #endregion
}

public enum PropType
{
    Baton,
    Pompon
}

public enum CategoryType
{
    Sport,
    Classic, 
    Basic,
    Acrobatic,
    TwoBatons
}