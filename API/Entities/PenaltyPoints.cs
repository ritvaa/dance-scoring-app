using System.ComponentModel.DataAnnotations;

namespace DancerScoringApp.Entities;

public class PenaltyPoints
{
    [Key] public int Id { get; set; }

    public PenaltyType Name { get; set; }
    public decimal PenaltyScore { get; set; }

    #region Entity Relations

    public virtual ICollection<PenaltyPointsRating> PenaltyPointsRating { get; set; }

    #endregion
}

public enum PenaltyType {
    //-0.05
    MissingGreeting,
    PropOrCostumePieceDrop,
    LeavingPropOnStageAfterDrop,
    TimeOverrun,

    //-0.1
    NotReadyAfterAnnouncement,
    CrossingLine,
    IncorrectlyMixedMusic,
    DancerSupport,

    //-0.2
    EarlyEntrance,
    MissingStopFigure,

    //-0.3
    MissingSingleRequiredCostumeElement,
    CommunicationDuringPresentation,
    MissingPropContact,
    MissingLiftAssurance,

    //-0.4
    MissingSingleRequiredElement,
    CompetitorFall,
    PuttingDownPropForMoreThan16MarchingSteps,
    NonRegulationMusic,
    DifferentChoreographyFromPreviousQualifications,
    NonRegulationPropOrCostumeElement,

    //-3.0
    ThreeTieredPyramids,
    CategoryProhibitedElements,
    UnjustifiedLiftsAndThrows,
    PlacingNonDedicatedPropsOnStage,

    Disqualification
}