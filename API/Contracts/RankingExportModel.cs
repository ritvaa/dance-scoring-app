namespace API.Contracts;

public class RankingExportModel
{
    public string Category { get; set; }
    public List<RoutineExportModel> Routines { get; set; }
}