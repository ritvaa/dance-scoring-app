using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class AgeCategory
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }

    #region Entity Relations

    public virtual ICollection<Category> Categories { get; set; }

    #endregion
}

public enum AgeCategoryType
{
    Cadet = 1,
    Junior,
    Senior,
    GrandSenior,
}