using API.Contracts;
using API.Contracts.Results;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class RoutineService : IRoutineService
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public RoutineService(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<RoutineReadModel> GetAllRoutines(Guid competitionId)
    {
        var routines = _mapper.Map<IEnumerable<RoutineReadModel>>(_dbContext.Routines);
        return routines;
    }

    public OperationResult<RoutineReadModel> GetRoutineById(Guid id)
    {
        var routine = _dbContext.Routines.FirstOrDefault(x => x.Id == id);
        if (routine == null) return OperationResult<RoutineReadModel>.Fail($"Routine with id {id} does not exist");

        var routineModel = _mapper.Map<RoutineReadModel>(routine);
        return OperationResult<RoutineReadModel>.Success(routineModel);
    }

    public async Task<OperationResult<Guid>> CreateRoutine(RoutineWriteModel routine)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == routine.CategoryId);
        if (category is null)
        {
            return OperationResult<Guid>.Fail($"Category with id {routine.CategoryId} does not exist");
        }
        
        var competition = await _dbContext.Competitions.FirstOrDefaultAsync(x => x.Id == routine.CompetitionId);
        if (competition is null)
        {
            return OperationResult<Guid>.Fail($"Competition with id {routine.CompetitionId} does not exist");
        }
        var existingRoutine = await _dbContext.Routines
            .FirstOrDefaultAsync(x =>
            x.Category == category
            && x.RoutineName == routine.RoutineName
            && x.Squad.Id == routine.SquadId);

        if (existingRoutine != null)
        {
            return OperationResult<Guid>.Fail("Routine already exists");
        }

        var squad = await _dbContext.Squads.FirstOrDefaultAsync(x => x.Id == routine.SquadId);
        if (squad is null)
        {
            return OperationResult<Guid>.Fail($"Squad with id {routine.SquadId} does not exist");
        }
        
        var newRoutine = _mapper.Map<Routine>(routine);
        newRoutine.Id = Guid.NewGuid();
        newRoutine.Category = category;
        newRoutine.Competition = competition;
        newRoutine.Squad = squad;


        try
        {
            await _dbContext.Routines.AddAsync(newRoutine);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<Guid>.Fail("An error occurred while adding routine: " + ex.Message);
        }

        return OperationResult<Guid>.Success(newRoutine.Id);
    }

    public OperationResult<string> UpdateRoutine(Guid id, RoutineWriteModel routine)
    {
        var existingRoutine = _dbContext.Routines.FirstOrDefault(x => x.Id == id);
        if (existingRoutine == null) return OperationResult<string>.Fail("Routine does not exist");

        _mapper.Map(routine, existingRoutine);

        _dbContext.SaveChanges();

        return OperationResult<string>.Success($"Routine with id: {id} updated successfully");
    }

    public OperationResult<string> DeleteRoutine(Guid id)
    {
        var existingRoutine = _dbContext.Routines.FirstOrDefault(x => x.Id == id);
        if (existingRoutine == null) return OperationResult<string>.Fail("Routine not found");

        try
        {
            _dbContext.Routines.Remove(existingRoutine);
            _dbContext.SaveChanges();
            return OperationResult<string>.Success($"Routine with id: {id} deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail($"An error occurred while deleting routine: {ex.Message}");
        }
    }

    public IEnumerable<RoutineByCategoryReadModel> GetCompetitionRoutinesByCategory(Guid competitionId)
    {
        var competitionRoutines =
            _mapper.Map<IEnumerable<RoutineByCategoryReadModel>>(
                _dbContext.Routines.Where(x => x.Competition.Id == competitionId));
        return competitionRoutines;
    }

    public IEnumerable<RoutineByCategoryReadModel> GetCategoryRoutines(Guid competitionId, int categoryId)
    {
        var competitionRoutines = _mapper.Map<IEnumerable<RoutineByCategoryReadModel>>(_dbContext.Routines.Where(x =>
            x.Competition.Id == competitionId
            && x.Category.Id == categoryId));
        return competitionRoutines;
    }
}