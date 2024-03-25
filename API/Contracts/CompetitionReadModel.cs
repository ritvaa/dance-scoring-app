namespace API.Contracts;

public class CompetitionReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public string Location { get; set; }
    public IReadOnlyCollection<UserReadModel> Users { get; set; }
    public IReadOnlyCollection<RoutineReadModel> Routines { get; set; }
}