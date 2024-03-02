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

    public OperationResult<UserReadModel> GetUserById(Guid id)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            return OperationResult<UserReadModel>.Fail($"User with id {id} does not exist");
        }

        var userReadModel = _mapper.Map<UserReadModel>(user);
        return OperationResult<UserReadModel>.Success(userReadModel);
    }

    public OperationResult<Guid> CreateUser(UserWriteModel user)
    {
        var existingUsername = _dbContext.Users.FirstOrDefault(x => x.Username == user.UserName);
        if (existingUsername != null)
        {
            return OperationResult<Guid>.Fail("User with this username already exists");
        }

        var existingEmail = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email);
        if (existingEmail != null)
        {
            return OperationResult<Guid>.Fail("User with this email already exists");
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
            return OperationResult<Guid>.Fail("An error occurred while adding user: " + ex.Message);
        }
    
        return OperationResult<Guid>.Success(newUser.Id);
    }
    
    public OperationResult<string> UpdateUser(Guid id, UserWriteModel user)
    {
        var existingUser = _dbContext.Users.FirstOrDefault(x => x.Id == id);
        if (existingUser == null) return OperationResult<string>.Fail("User does not exist");

        var existingUsername = _dbContext.Users.FirstOrDefault(x => x.Username == user.UserName && x.Id != id);
        if (existingUsername != null) return OperationResult<string>.Fail("User with this username already exists");

        var existingEmail = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email && x.Id != id);
        if (existingEmail != null) return OperationResult<string>.Fail("User with this email already exists");
        
        _mapper.Map(user, existingUser);

        _dbContext.SaveChanges();

        return OperationResult<string>.Success($"User with id: {id} updated successfully");
    }
    
    public OperationResult<string> DeleteUser(Guid id)
    {
        var existingUser = _dbContext.Users.FirstOrDefault(x => x.Id == id);
        if (existingUser == null)
        {
            return OperationResult<string>.Fail("User not found");
        }

        try
        {
            _dbContext.Users.Remove(existingUser);
            _dbContext.SaveChanges();
            return OperationResult<string>.Success($"User with id: {id} deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail($"An error occurred while deleting user: {ex.Message}");
        }
    }
}