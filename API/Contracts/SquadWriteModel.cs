namespace API.Contracts;

public class SquadWriteModel
{
    public string SquadType { get; set; }

    public ICollection<DancerSimpifliedReadModel> Dancers { get; set; }
}