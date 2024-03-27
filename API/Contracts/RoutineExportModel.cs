using API.Entities;

namespace API.Contracts;

public class RoutineExportModel
{
    public int OrdinalNumber { get; set; }
    public string DancersNames { get; set; } //only if solist or duo/trio
    public string TeamName { get; set; }
    public string ScoreSum { get; set; }
    public string PlaceInRank { get; set; }

    public SquadType SquadType { get; set; }
    
}