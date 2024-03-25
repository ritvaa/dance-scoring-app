using API.Entities;

namespace API.Contracts;

public record RoutineByCategoryReadModel(CategoryReadModel Category, IReadOnlyCollection<RoutineReadModel> Routines);