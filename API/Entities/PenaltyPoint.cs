using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class PenaltyPoint
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public decimal PenaltyScore { get; set; }

    #region Entity Relations

    public virtual ICollection<TechJudgeRating> TechJudgeRatings { get; set; }

    #endregion
}

public enum PenaltyType
{
    //-0.05
    MissingGreeting = 1,
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