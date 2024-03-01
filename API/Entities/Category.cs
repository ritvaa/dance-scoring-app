using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Category
{
    [Key] public int Id { get; set; }
    public int AgeCategoryId { get; set; }
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