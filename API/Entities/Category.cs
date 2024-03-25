using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Category
{
    [Key] public int Id { get; set; }
    public AgeCategory AgeCategory { get; set; }
    public RequisiteType Requisite { get; set; }
    public CategoryType CategoryType { get; set; }
    public SquadType SquadType { get; set; }
    

    #region Entity Relations
    
    public virtual ICollection<Routine> Routines { get; set; }

    #endregion
}

public enum AgeCategory
{
    MiniCadet = 1,
    Cadet ,
    Junior,
    Senior,
    GrandSenior
}

public enum RequisiteType
{
    Baton = 1,
    Pompon
}

public enum CategoryType
{
    Sport = 1,
    Classic,
    Basic,
    Acrobatic,
    TwoBatons
}

