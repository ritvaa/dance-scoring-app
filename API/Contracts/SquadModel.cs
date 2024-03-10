namespace API.Contracts;

public class SquadModel
{
    public Guid Id { get; set; }

    public string PerformanceType { get; set; }

    public ICollection<DancerSimpifliedReadModel> Dancers { get; set; }
}