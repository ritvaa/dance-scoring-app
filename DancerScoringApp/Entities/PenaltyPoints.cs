using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class PenaltyPoints
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal PenaltyScore { get; set; }

    #region Entity Relations

    public virtual ICollection<PenaltyPointsRating> PenaltyPointsRating { get; set;}

    #endregion
}