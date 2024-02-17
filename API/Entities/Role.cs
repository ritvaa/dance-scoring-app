using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class Role
{
    [Key] public int Id { get; set; }

    public RoleType Name { get; set; }

    #region Entity Relations

    public virtual ICollection<UserRole> Users { get; set; }

    #endregion
}

public enum RoleType
{
    SuperAdmin,
    Judge,
    TechnicalJudge,
    Scrutineer
}