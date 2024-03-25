using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Squad
{
    [Key] public Guid Id { get; set; }

    public SquadType SquadType { get; set; }

    #region Entity Relations

    public virtual ICollection<SquadDancer> Dancers { get; set; }
    public virtual Team Team { get; set; }

    #endregion
}

public enum SquadType
{
    Formation = 1,
    MiniFormation,
    DuoTrio,
    Solo
}