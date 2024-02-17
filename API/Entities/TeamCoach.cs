using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class TeamCoach
{
    [Key] public Guid Id { get; set; }

    public Guid CoachId { get; set; }
    public Guid TeamId { get; set; }

    #region Entity Relations

    public virtual Team Team { get; set; }
    public virtual Coach Coach { get; set; }

    #endregion
}