using API.Contracts;
using API.Contracts.Results;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class CompetitionService : ICompetitionService
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CompetitionService(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<CompetitionReadModel> GetAllCompetitions()
    {
        var competitions = _mapper.Map<IEnumerable<CompetitionReadModel>>(_dbContext.Competitions);
        return competitions;
    }

    public IEnumerable<CompetitionReadModel> GetAllUserCompetitions(Guid userId)
    {
        var competitions =
            _mapper.Map<IEnumerable<CompetitionReadModel>>(
                _dbContext.Competitions.Where(x => x.Users.Any(y => y.UserId == userId)));
        return competitions;
    }

    public OperationResult<CompetitionReadModel> GetCompetitionById(Guid id)
    {
        var competition = _dbContext.Competitions.FirstOrDefault(x => x.Id == id);
        if (competition == null)
            return OperationResult<CompetitionReadModel>.Fail($"Competition with id {id} does not exist");

        var competitionReadModel = _mapper.Map<CompetitionReadModel>(competition);
        return OperationResult<CompetitionReadModel>.Success(competitionReadModel);
    }

    public async Task<OperationResult<Guid>> CreateCompetition(CompetitionWriteModel competition)
    {
        var existingCompetitionName =
            _dbContext.Competitions.FirstOrDefault(x =>
                x.Name == competition.Name && x.StartDate == competition.StartDate && x.EndDate == competition.EndDate);

        if (existingCompetitionName != null)
            return OperationResult<Guid>.Fail("Competition with the same name and date already exists");

        var newCompetition = _mapper.Map<Competition>(competition);
        newCompetition.Id = Guid.NewGuid();

        try
        {
            await _dbContext.Competitions.AddAsync(newCompetition);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<Guid>.Fail("An error occurred while adding competition: " + ex.Message);
        }

        return OperationResult<Guid>.Success(newCompetition.Id);
    }

    public async Task<OperationResult<string>> UpdateCompetition(Guid id, CompetitionReadModel competition)
    {
        var existingCompetition = await _dbContext.Competitions.FirstOrDefaultAsync(x => x.Id == id);
        if (existingCompetition == null) return OperationResult<string>.Fail("Competition does not exist");

        var existingNameAndDate =
            await _dbContext.Competitions.FirstOrDefaultAsync(x =>
                x.Name == competition.Name && x.StartDate == competition.StartDate);
        if (existingNameAndDate != null)
            return OperationResult<string>.Fail("Competition with this name and date already exists");

        _mapper.Map(competition, existingCompetition);

        await _dbContext.SaveChangesAsync();

        return OperationResult<string>.Success($"Competition with id: {id} updated successfully");
    }

    public async Task<OperationResult<string>> DeleteCompetition(Guid id)
    {
        var existingCompetition = await _dbContext.Competitions.FirstOrDefaultAsync(x => x.Id == id);
        if (existingCompetition == null) return OperationResult<string>.Fail("Competition not found");

        if (existingCompetition.Users.Count != 0)
            return OperationResult<string>.Fail("Users are already assigned to this competition");

        if (existingCompetition.Routinines.Count != 0)
            return OperationResult<string>.Fail("Routines are already assigned to this competition");

        try
        {
            _dbContext.Competitions.Remove(existingCompetition);
            await _dbContext.SaveChangesAsync();
            return OperationResult<string>.Success($"Competition with id: {id} deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail($"An error occurred while deleting dancer: {ex.Message}");
        }
    }

    public async Task<OperationResult<string>> AddUserToCompetition(Guid competitionId, IEnumerable<Guid> userIds)
    {
        var existingCompetition = await _dbContext.Competitions.FirstOrDefaultAsync(x => x.Id == competitionId);
        if (existingCompetition == null) return OperationResult<string>.Fail("Competition does not exists");

        var userCompetitions = new List<UserCompetition>();
        foreach (var userId in userIds)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (existingUser == null) return OperationResult<string>.Fail($"User {userId} does not exists");

            var hasOverlappingCompetitions = await
                HasOverlappingCompetitions(userId, existingCompetition.StartDate, existingCompetition.EndDate);
            if (hasOverlappingCompetitions.HasOverlap)
                return OperationResult<string>.Fail(
                    $"User {userId} has overlapping competition - {hasOverlappingCompetitions.OverlappingCompetition!.Id}, {hasOverlappingCompetitions.OverlappingCompetition.Name}");

            var userCompetition = new UserCompetition
            {
                Id = Guid.NewGuid(),
                CompetitionId = competitionId,
                UserId = userId
            };

            userCompetitions.Add(userCompetition);
        }

        try
        {
            await _dbContext.UserCompetitions.AddRangeAsync(userCompetitions);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<string>.Fail("An error occurred while adding user to competition: " +
                                                ex.Message);
        }

        return OperationResult<string>.Success("Users are added to competition");
    }

    public async Task<(bool HasOverlap, Competition? OverlappingCompetition)> HasOverlappingCompetitions(Guid userId,
        DateTime competitionStart, DateTime competitionEnd)
    {
        var overlappingCompetition = await _dbContext.UserCompetitions
            .Where(x => x.UserId == userId &&
                        !(x.Competition.EndDate < competitionStart || x.Competition.StartDate > competitionEnd))
            .Select(x => x.Competition)
            .FirstOrDefaultAsync();

        return (overlappingCompetition != null, overlappingCompetition);
    }
}