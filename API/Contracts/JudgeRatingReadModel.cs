using System.Text.Json.Serialization;

namespace API.Contracts;

public class JudgeRatingReadModel
{
    public Guid Id { get; set; }
    public decimal ChoreographyPoints { get; set; }
    public decimal BodyTechniquePoints { get; set; }
    public decimal RequisiteWorkPoints { get; set; }
    public string Comment { get; set; } = string.Empty;
    [JsonIgnore]
    public IEnumerable<BonusReadModel> Bonuses { get; set; }
}
