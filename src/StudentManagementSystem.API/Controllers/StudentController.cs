using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.API.UnitOfWork;
using StudentManagementSystem.Models;
using StudentManagentSystem.API.Repository;

namespace StudentManagementSystem.API.Controllers
{
    
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _repo;

        public StudentController(IUnitOfWork repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("api/addstudent")]
        
        public IActionResult AddStudent([FromBody]Student student)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(student == null)
            {
                return BadRequest("Student Data is needed");
            }
            _repo.Students.AddStudent(student);
            _repo.Students.SaveChanges();
            return Ok("STUDENT ADDED");
        }

        [HttpGet]
    [Route("api/Student")]
    public IActionResult GetStudent([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var studentsQuery = _repo.Students.GetStudent();
        var totalStudent = studentsQuery.Count();
        var totalPages = (int)Math.Ceiling((double)totalStudent / pageSize);
        if (page < 1 || page > totalPages)
        {
            return BadRequest("Invalid page number. Please provide a valid page number within the range.");
        }
        var pagedStudents = studentsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var response = new
        {
            TotalStudents = totalStudent,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            Students = pagedStudents
        };

        return Ok(response);
    }

        [HttpGet]
        [Route("api/Student/{id}")]
        public IActionResult GetById([Bind(Prefix= "id")] int id)
        {
            var student = _repo.Students.GetByID(id);
            return Ok(student);
        }

        [HttpDelete]
        [Route("api/DeleteStudent/{id}")]
        public IActionResult DeleteStudent([Bind(Prefix= "id")] int id)
        {
            _repo.Students.DeleteStudent(id);
            _repo.Students.SaveChanges();
            ResetIdentitySeed("Students");
            ResetIdentitySeed("Accounts");
            ResetIdentitySeed("Classes");

            return Ok("STUDENT DELETED");
        }

        [HttpPut]
        [Route("api/UpdateStudent/{id}")]
        public IActionResult UpdateStudent([Bind(Prefix= "id")] int id,[FromBody]Student student)
        {
            if(student == null)
            {
                return BadRequest("Data is not null ");
            }
            var students = _repo.Students.GetByID(id);
            if (students == null)
        {
            return NotFound($"Student with ID {id} not found");
        }
        students.Name = student.Name;
        _repo.Students.Update(students);
        _repo.Students.SaveChanges();
        return Ok("Student Updated");
        }
        private void ResetIdentitySeed(string tableName)
        {
            string resetseedsql = $"DBCC CHECKIDENT ('{tableName}',RESEED,0);";
            _repo.ExecuteSqlRaw(resetseedsql);
        }  
    }
}