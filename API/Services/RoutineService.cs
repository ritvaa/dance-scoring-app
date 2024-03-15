using API.Contracts;
using API.Contracts.Results;
using API.Entities;
using API.Migrations;
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
    public IEnumerable<RoutineReadModel> GetAllRoutines()
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

    public OperationResult<Guid> CreateRoutine(RoutineWriteModel routine)
    {
        //todo: add checking if routine exists

        var newRoutine = _mapper.Map<Routine>(routine);
        newRoutine.Id = Guid.NewGuid();
        
        try
        {
            _dbContext.Routines.Add(newRoutine);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<Guid>.Fail("An error occurred while adding routine: " + ex.Message);
        }

        return OperationResult<Guid>.Success(newRoutine.Id);

    }

    public OperationResult<string> UpdateRoutine(Guid id, RoutineWriteModel routine)
    {
        //todo: add checking if routine exists
        var existingRoutine = _dbContext.Routines.FirstOrDefault(x => x.Id == id);
        if (existingRoutine == null) return OperationResult<string>.Fail("Routine does not exist");

        _mapper.Map(routine, existingRoutine);

        _dbContext.SaveChanges();
        return OperationResult<string>.Success($"Routine with id: {id} updated successfully");
    }
    
    // public OperationResult<string> SetGrandPrixProperty()
    // {
        //todo
        //raczej nie powinna przyjmować niczego, tylko wyliczyć, która routine ma najwiyższy score z wszystkich w danym competition
    // }

    public OperationResult<string> DeleteRoutine(Guid id)
    {
        var existingRoutine = _dbContext.Routines.FirstOrDefault(x => x.Id == id);
        if (existingRoutine == null) return OperationResult<string>.Fail("Routine does not exist");
        
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
}