using DomainEntities.Entities;

namespace Infrastrucure.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserAsync(int userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(int Id, User user);
        Task RemoveUserAsync(int userId);
    }
}
