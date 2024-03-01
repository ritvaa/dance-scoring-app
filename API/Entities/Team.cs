using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Team
{
    [Key] public Guid Id { get; set; }

    public string Name { get; set; }
    public string Location { get; set; }

    #region Entity Relations

    public virtual ICollection<TeamCoach> Coaches { get; set; }
    public virtual ICollection<Squad> Squads { get; set; }

    #endregion
}