namespace API.Contracts;

public record DancerWriteModel(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateOnly DateOfBirth,
    string LicenceId,
    string TeamName,
    string TeamLocationName)
{
}