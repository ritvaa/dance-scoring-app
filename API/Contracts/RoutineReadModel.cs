using AutoMapper.Configuration.Annotations;

namespace API.Contracts;

public class RoutineReadModel
{
    public Guid Id { get; set; }
    [Ignore] public CategoryReadModel CategoryModel { get; set; }
    public string Category =>
        $"{CategoryModel.Requisite} {CategoryModel.CategoryType} {CategoryModel.SquadType} {CategoryModel.AgeCategory}";
    public string Name { get; set; }
    public decimal Score { get; set; }
    public bool GrandPrix { get; set; }
    public SquadWriteModel SquadWrite { get; set; }
}