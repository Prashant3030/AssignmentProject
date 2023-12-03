using StudentManagementSystem.Enums;

namespace StudentManagementSystem.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserType UserType { get; set; }
        public double Fees { get; set; }
        public double Credit { get; set; }
        public double Salary { get; set; }
        public double Debit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
        public ICollection<Student> Students { get; set; }
        public ICollection<Staff> Staff { get; set; }
        public string GetFormattedCreatedAt()
        {
            return CreatedAt.ToString("yyyy-MM-dd");
        }
        
    }
}