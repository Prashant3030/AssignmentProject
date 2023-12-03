using StudentManagementSystem.Models;

namespace StudentManagentSystem.API.Repository
{
    public interface IStudentRepository
    {
           void AddStudent(Student student);
           IEnumerable<Student> GetStudent();
           Student GetByID(int id);
           void DeleteStudent(int studentId);
           void Update(Student student);
           void SaveChanges();
    }
}