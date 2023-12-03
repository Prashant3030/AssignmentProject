using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.DataHelper
{
    public interface IStudentManagementDbContext : IDisposable
    {
        DbSet<Student> Students { get; set; }
        DbSet<Class> Classes { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Staff> Staff { get; set; }
        DbSet<UserLogin> Login { get; set; }
        DatabaseFacade Database { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}