using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class Dancer
{
    [Key] public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string LicenceId { get; set; }

    #region Entity Relations

    public virtual ICollection<SquadDancer> SquadDancers { get; set; }
    public virtual Team Team { get; set; }

    #endregion
}