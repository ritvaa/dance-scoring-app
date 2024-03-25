namespace API.Contracts;

public class SquadReadModel
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public string TeamName { get; set; }
    public string SquadType { get; set; }

    public ICollection<DancerSimpifliedReadModel> Dancers { get; set; }
}