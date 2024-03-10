using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface ISquadService
{
    IEnumerable<SquadModel> GetSquads(Guid teamId);
    OperationResult<SquadModel> GetSquadById(Guid teamId, Guid id);
    OperationResult<Guid> CreateSquad(SquadModel squad);
    OperationResult<string> UpdateSquad(Guid id, SquadModel squad);
    OperationResult<string> DeleteSquad(Guid id);
}