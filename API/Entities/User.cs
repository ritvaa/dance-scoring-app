using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class User
{
    [Key] public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }

    #region EntityRelations

    public virtual ICollection<UserRole> Roles { get; set; }
    public virtual ICollection<TechJudgeRating> TechJudgeRating { get; set; }
    public virtual ICollection<JudgeRating> JudgeRating { get; set; }
    public virtual ICollection<UserCompetition> UserCompetitions { get; set; }

    #endregion
}