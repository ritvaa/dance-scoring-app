using API.Contracts;

namespace API.Services {
    public interface IUserService
    {
        IEnumerable<UserReadModel> GetAllUsers();
        UserReadModel GetUserById(Guid id);
        ICommandResult CreateUser(UserWriteModel user);
        UserReadModel UpdateUser(Guid id, UserWriteModel user);
        public void DeleteUser(Guid id);
    }
}
