using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class TechJudgeRating
{
    public Guid Id { get; set; }
    public Guid RoutineId { get; set; }

    #region Entity Relations

    public virtual User User { get; set; }
    public virtual PenaltyPoint PenaltyPoint { get; set; }
    public virtual Routine Routine { get; set; }

    #endregion
}