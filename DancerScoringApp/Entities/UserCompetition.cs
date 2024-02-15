using System.ComponentModel.DataAnnotations;
using System;

namespace DancerScoringApp.Entities;

public class UserCompetition
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CompetitionId { get; set; }

    #region Entity Relations

    public virtual User User { get; set; }
    public virtual Competition Competition { get; set; }

    #endregion
}