namespace API.Contracts;

public class RoutineExportModel
{
    public int OrdinalNumber { get; set; }
    public string Name { get; set; } //only if solist or duo/trio
    public string TeamName { get; set; }
    public string Sum { get; set; }
    public string PlaceInRank { get; set; }
}