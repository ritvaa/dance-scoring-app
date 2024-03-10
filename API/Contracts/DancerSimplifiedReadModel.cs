namespace API.Contracts;

public class DancerSimpifliedReadModel
{
    public Guid LicenceId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}