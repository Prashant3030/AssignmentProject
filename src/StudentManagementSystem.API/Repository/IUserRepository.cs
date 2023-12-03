using StudentManagementSystem.Models;

namespace StudentManagementSystem.API.Repository
{
    public interface IUserRepository
    {
        Task<UserLogin> GetUserByEmailAndPassword(string email, string password);
    }
}