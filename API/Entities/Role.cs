using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Role
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }

    #region Entity Relations

    public virtual ICollection<UserRole> Users { get; set; }

    #endregion
}

public enum RoleType
{
    SuperAdmin = 1,
    Judge,
    TechnicalJudge,
    Scrutineer
}