using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Bonus
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }

    #region Entity Relations

    public virtual ICollection<RatingBonus> RatingBonus { get; set; }

    #endregion
}

public enum BonusType
{
    Originality = 1,
    Synchronization,
    PerfectSynchronization,
    PresenceAndElegance
}