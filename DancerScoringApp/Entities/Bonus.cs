using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class Bonus
{
    [Key]
    public int Id { get; set; }
    public BonusType Name { get; set; }

    #region Entity Relations
    public virtual ICollection<RatingBonus> RatingBonus { get; set; }

    #endregion
}

public enum BonusType {
    Originality,
    Synchronization,
    PerfectSynchronization,
    PresenceAndElegance
}