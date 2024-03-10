namespace API.Contracts;

public class TeamModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<CoachModel> Coaches { get; set; }
    public List<SquadModel> Squads { get; set; }
}