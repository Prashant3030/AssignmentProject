using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DataHelper;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.API.Repository
{
    public class UserRepository : IUserRepository
{
    private readonly IStudentManagementDbContext _context;

    public UserRepository(IStudentManagementDbContext context)
    {
        _context = context;
    }

    public async Task<UserLogin> GetUserByEmailAndPassword(string email, string password)
    {
        return await _context.Login.FirstOrDefaultAsync(u => u.EmailId == email && u.Password == password);
    }

      
    }
}