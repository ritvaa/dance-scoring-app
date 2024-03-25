namespace API.Contracts;

public class JudgeRatingWriteModel
{
    public decimal ChoreographyPoints { get; set; }
    public decimal BodyTechniquePoints { get; set; }
    public decimal RequisiteWorkPoints { get; set; }
    public string? Comment { get; set; }
    public List<BonusReadModel>? Bonuses { get; set; }
}