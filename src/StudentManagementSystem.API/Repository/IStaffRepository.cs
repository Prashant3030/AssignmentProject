using StudentManagementSystem.Models;

namespace StudentManagementSystem.API.Repository
{
    public interface IStaffRepository
    {
        void AddStaff(Staff staff);
        IEnumerable<Staff> GetStaff();
        Staff GetByIDStaff(int id);
        void DeleteStaff(int staffId);
        void UpdateStaff(Staff staff);
        void SaveChanges();
    }
}