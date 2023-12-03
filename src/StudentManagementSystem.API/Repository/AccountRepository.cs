using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DataHelper;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IStudentManagementDbContext _context;

        public AccountRepository(IStudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<double> GetTotalProfit(DateTime startDate, DateTime endDate)
        {
            var accounts = await _context.Accounts
                .Where(a => EF.Functions.DateDiffDay(startDate.Date, a.CreatedAt.Date) >= 0 &&
                            EF.Functions.DateDiffDay(endDate.Date, a.CreatedAt.Date) <= 0)
                .ToListAsync();

            var totalSalary = accounts.Sum(a => a.Debit);
            var totalFees = accounts.Sum(a => a.Credit);

            return totalSalary - totalFees;
        }


        

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
