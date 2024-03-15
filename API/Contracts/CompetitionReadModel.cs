namespace API.Contracts;

public class CompetitionReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public string Location { get; set; }
    public IEnumerable<UserReadModel> Users { get; set; }
    public IEnumerable<RoutineWriteModel> Routines { get; set; }
}