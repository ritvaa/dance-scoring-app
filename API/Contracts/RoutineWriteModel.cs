namespace API.Contracts;

public class RoutineWriteModel
{
    public Guid CompetitionId { get; set; }
    public int CategoryId { get; set; }
    public string RoutineName { get; set; }
    public Guid SquadId { get; set; }
}