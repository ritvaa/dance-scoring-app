using System.Data.Entity.Infrastructure;
using API.Contracts;
using API.Entities;
using AutoMapper;
using static BCrypt.Net.BCrypt;

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

    public ICommandResult CreateUser(UserWriteModel user)
    {
        var existingUsername = _dbContext.Users.FirstOrDefault(x => x.Username == user.UserName);
        if (existingUsername != null)
        {
            return CommandResult.Fail(new Conflict("User with this username already exists"));
        }

        var existingEmail = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email);
        if (existingEmail != null)
        {
            return CommandResult.Fail(new Conflict("User with this email already exists"));
        }

        var newUser = _mapper.Map<User>(user);
        newUser.Id = Guid.NewGuid();
        
        //todo: check this password encryption
        string hashedPassword = HashPassword(user.Password);
        newUser.Password = hashedPassword;
        
        try
        {
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            CommandResult.Fail(new DbError("An error occurred while adding user", ex.Message));
        }
        
        return CommandResult.Success(CommandResultData.Get(newUser.Id));
    }
    
    public UserReadModel UpdateUser(Guid id, UserWriteModel user)
    {
        var existingUser = _dbContext.Users.FirstOrDefault(x => x.Id == id);
        if (existingUser == null) throw new Exception("User not found");

        var existingUsername = _dbContext.Users.FirstOrDefault(x => x.Username == user.UserName && x.Id != id);
        if (existingUsername != null) throw new Exception("User with this username already exists");

        var existingEmail = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email && x.Id != id);
        if (existingEmail != null) throw new Exception("User with this email already exists");

        // Mapowanie właściwości z modelu do zapisu na istniejący obiekt użytkownika
        _mapper.Map(user, existingUser);

        _dbContext.SaveChanges();

        return _mapper.Map<UserReadModel>(existingUser);
    }
    
    public void DeleteUser(Guid id)
    {
        var existingUser = _dbContext.Users.FirstOrDefault(x => x.Id == id);
        if (existingUser == null) throw new Exception("User not found");

        _dbContext.Users.Remove(existingUser);
        _dbContext.SaveChanges();
    }
}