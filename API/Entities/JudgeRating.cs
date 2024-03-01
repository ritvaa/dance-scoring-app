using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class JudgeRating
{
    [Key] public Guid Id { get; set; }
    public decimal ChoreographyPoints { get; set; }
    public decimal BodyTechniquePoints { get; set; }
    public decimal PropWorkPoints { get; set; }
    public string Comment { get; set; } = string.Empty;
    public bool HasBonus { get; set; }

    #region Entity Relations

    public virtual User User { get; set; }
    public virtual Routine Routine { get; set; }
    public virtual ICollection<RatingBonus> RatingBonus { get; set; }

    #endregion
}