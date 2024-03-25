namespace API.Contracts;

public class PenaltyPointsReadModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal PenaltyScore { get; set; }
}