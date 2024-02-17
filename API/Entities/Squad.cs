using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class Squad
{
    [Key] public Guid Id { get; set; }

    public PerformanceType PerformanceType { get; set; }

    #region Entity Relations

    public virtual ICollection<SquadDancer> Dancers { get; set; }
    public virtual Team Team { get; set; }

    #endregion
}

public enum PerformanceType
{
    Formation,
    MiniFormation,
    Duet,
    Solo
}