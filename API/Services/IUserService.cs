using API.Contracts;

namespace API.Services {
    public interface IUserService
    {
        IEnumerable<UserReadModel> GetAllUsers();
        UserReadModel GetUserById(Guid id);
        // UserReadModel CreateUser(UserWriteModel user);
    }
}
