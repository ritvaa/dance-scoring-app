namespace API.Contracts;

public record RatingReadModel(Guid routineId, RatingReadModel routine, IReadOnlyCollection<JudgeRatingReadModel> jugdeRatings, TechJudgeRatingReadModel techJudgeRating);