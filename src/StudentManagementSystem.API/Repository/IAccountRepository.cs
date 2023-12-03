using StudentManagementSystem.Models;

namespace StudentManagementSystem.API.Repository
{
    public interface IAccountRepository
    {
        Task<double> GetTotalProfit(DateTime startDate, DateTime endDate);
        Task SaveChangesAsync();
    }
}