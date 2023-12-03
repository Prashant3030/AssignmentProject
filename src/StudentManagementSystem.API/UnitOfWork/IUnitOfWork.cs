using StudentManagementSystem.API.Repository;
using StudentManagentSystem.API.Repository;

namespace StudentManagementSystem.API.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts {get;}
        IStudentRepository Students {get;}
        IStaffRepository StaffMembers {get;}
        IUserRepository Users { get; }
        void ExecuteSqlRaw(string sql);
        void SaveChanges();
    }

    
}