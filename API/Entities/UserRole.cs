using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class UserRole
{
    [Key] public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    #region Entity Relations

    public User User { get; set; }
    public Role Role { get; set; }

    #endregion
}