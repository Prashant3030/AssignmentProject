using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DataHelper;
using StudentManagementSystem.Models;
using StudentManagentSystem.API.Repository;

namespace StudentManagementSystem.API.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IStudentManagementDbContext _context;

        public StudentRepository(IStudentManagementDbContext context)
        {
            _context = context;
        }
        public void AddStudent(Student student)
        {
            if(student == null )
            {
                throw new ArgumentException(nameof (student));
            }
            _context.Students.Add(student);
        }

        public void DeleteStudent(int studentId)
    {
        var existingStudent = _context.Students.Include(s => s.Class).Include(s => s.Account).FirstOrDefault(s => s.Id == studentId);

        if (existingStudent == null)
        {
            throw new KeyNotFoundException("Student not found");
        }

        if (existingStudent.Class != null)
        {
            _context.Classes.Remove(existingStudent.Class);
        }

        if (existingStudent.Account != null)
        {
            _context.Accounts.Remove(existingStudent.Account);
        }

        _context.Students.Remove(existingStudent);
        _context.SaveChanges();
    }

        public Student GetByID(int id)
        {
            return _context.Students.FirstOrDefault(x=> x.Id ==id);
        }

        public IEnumerable<Student> GetStudent()
        {
            return _context.Students.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.Id == student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
            }
        }
    }
}