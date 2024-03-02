namespace API.Contracts;

public class DancerWriteModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string LicenceId { get; set; }
    public string TeamName { get; set; }
    public string TeamLocationName { get; set; }
}