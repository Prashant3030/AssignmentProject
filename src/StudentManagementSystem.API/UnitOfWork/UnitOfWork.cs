using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.API.Repository;
using StudentManagementSystem.DataHelper;
using StudentManagentSystem.API.Repository;

namespace StudentManagementSystem.API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IStudentManagementDbContext _context;

        public UnitOfWork(IStudentManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof (context));
            Accounts = new AccountRepository(_context);
            Students = new StudentRepository(_context);
            StaffMembers = new StaffRepository(_context);
            Users = new UserRepository(_context);
        }
        public IStudentRepository Students {get;}

        public IStaffRepository StaffMembers {get;}
        public IAccountRepository Accounts { get; }

        public IUserRepository Users {get;}

        public void Dispose()
        {
            _context.Dispose();
        }

        public void ExecuteSqlRaw(string sql)
        {
            _context.Database.ExecuteSqlRaw(sql);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}