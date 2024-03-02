using System.Data.Entity.Infrastructure;
using API.Contracts;
using API.Entities;
using AutoMapper;
using static BCrypt.Net.BCrypt;

namespace API.Services;

public class CoachService : ICoachService
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CoachService(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<CoachModel> GetAllCoaches()
    {
        var coachs = _mapper.Map<IEnumerable<CoachModel>>(_dbContext.Coaches);
        return coachs;
    }

    public OperationResult<CoachModel> GetCoachById(Guid id)
    {
        var coach = _dbContext.Coaches.FirstOrDefault(x => x.Id == id);
        if (coach == null)
        {
            return OperationResult<CoachModel>.Fail($"Coach with id {id} does not exist");
        }

        var coachModel = _mapper.Map<CoachModel>(coach);
        return OperationResult<CoachModel>.Success(coachModel);
    }

    public OperationResult<Guid> CreateCoach(CoachModel coach)
    {
        var existingCoachName = _dbContext.Coaches.FirstOrDefault(x => x.FirstName == coach.FirstName && x.LastName == coach.LastName);
        if (existingCoachName != null)
        {
            return OperationResult<Guid>.Fail("Coach with this name already exists");
        }

        var newCoach = _mapper.Map<Coach>(coach);
        newCoach.Id = Guid.NewGuid();
        
        try
        {
            _dbContext.Coaches.Add(newCoach);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<Guid>.Fail("An error occurred while adding coach: " + ex.Message);
        }
    
        return OperationResult<Guid>.Success(newCoach.Id);
    }
    
    public OperationResult<string> UpdateCoach(Guid id, CoachModel coach)
    {
        var existingCoach = _dbContext.Coaches.FirstOrDefault(x => x.Id == id);
        if (existingCoach == null) return OperationResult<string>.Fail("Coach does not exist");

        var existingCoachName = _dbContext.Coaches.FirstOrDefault(x => x.FirstName == coach.FirstName && x.LastName == coach.LastName && x.Id != id);
        if (existingCoachName != null) return OperationResult<string>.Fail("Coach with this name already exists");
        
        _mapper.Map(coach, existingCoach);

        _dbContext.SaveChanges();

        return OperationResult<string>.Success($"Coach with id: {id} updated successfully");
    }
    
    public OperationResult<string> DeleteCoach(Guid id)
    {
        var existingCoach = _dbContext.Coaches.FirstOrDefault(x => x.Id == id);
        if (existingCoach == null)
        {
            return OperationResult<string>.Fail("Coach not found");
        }

        try
        {
            _dbContext.Coaches.Remove(existingCoach);
            _dbContext.SaveChanges();
            return OperationResult<string>.Success($"Coach with id: {id} deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail($"An error occurred while deleting coach: {ex.Message}");
        }
    }
}