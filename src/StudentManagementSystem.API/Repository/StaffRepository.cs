using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DataHelper;
using StudentManagementSystem.Models;
using StudentManagentSystem.API.Repository;

namespace StudentManagementSystem.API.Repository
{
    public class StaffRepository : IStaffRepository
    {
            private readonly IStudentManagementDbContext _context;

    public StaffRepository(IStudentManagementDbContext context)
    {
        _context = context;
    }

        public void AddStaff(Staff staff)
        {
            if(staff == null)
            {
                throw new ArgumentException(nameof (staff));
            }
             _context.Staff.Add(staff);
        }

        public void DeleteStaff(int staffId)
    {
        var existingStaff = _context.Staff.Include(s => s.Class).Include(s => s.Account).FirstOrDefault(s => s.Id == staffId);

        if (existingStaff == null)
        {
            throw new KeyNotFoundException("Staff not found");
        }

        if (existingStaff.Class != null)
        {
            _context.Classes.Remove(existingStaff.Class);
        }

        if (existingStaff.Account != null)
        {
            _context.Accounts.Remove(existingStaff.Account);
        }

        _context.Staff.Remove(existingStaff);
        _context.SaveChanges();
    }

        public Staff GetByIDStaff(int id)
        {
            return _context.Staff.FirstOrDefault(x=> x.Id ==id);
        }

        public IEnumerable<Staff> GetStaff()
        {
            return _context.Staff.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateStaff(Staff staff)
        {
            var existingStaff = _context.Staff.FirstOrDefault(s => s.Id == staff.Id);
            if (existingStaff != null)
            {
                existingStaff.Name = staff.Name;
            }
        }
    }
}