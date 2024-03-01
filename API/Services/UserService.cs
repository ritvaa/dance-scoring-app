using API.Contracts;
using API.Entities;
using AutoMapper;

namespace API.Services;

public class UserService : IUserService
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<UserReadModel> GetAllUsers()
    {
        var users = _mapper.Map<IEnumerable<UserReadModel>>(_dbContext.Users);
        return users;
    }

    public UserReadModel GetUserById(Guid id)
    {
        var user = _dbContext.Users.Where(x => x.Id == id);
        return _mapper.Map<UserReadModel>(user);
    }
    
    // public UserReadModel CreateUser(UserWriteModel user)
    // {
    //     
    // }
}