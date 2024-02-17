using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class Coach
{
    [Key] public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    #region Entity Relations

    public virtual ICollection<TeamCoach> TeamCoaches { get; set; }

    #endregion
}