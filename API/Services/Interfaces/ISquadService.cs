using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface ISquadService
{
    IEnumerable<SquadReadModel> GetSquads(Guid teamId);
    OperationResult<SquadReadModel> GetSquadById(Guid teamId, Guid id);
    OperationResult<Guid> CreateSquad(SquadWriteModel squad);
    OperationResult<string> UpdateSquad(Guid id, SquadWriteModel squad);
    OperationResult<string> DeleteSquad(Guid id);
    Task<OperationResult<string>> AddDancerToSquad(Guid squadId, List<Guid> dancerIds);
    
}