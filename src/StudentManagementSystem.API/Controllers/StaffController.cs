using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.API.Repository;
using StudentManagementSystem.API.UnitOfWork;
using StudentManagementSystem.Models;
using StudentManagentSystem.API.Repository;

namespace StudentManagementSystem.API.Controllers
{
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IUnitOfWork _repo;

        public StaffController(IUnitOfWork repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("api/addstaff")]
        public IActionResult AddStaff([FromBody]Staff staff)
        {
            if(staff == null)
            {
                return BadRequest("Staff Data is needed");
            }
            _repo.StaffMembers.AddStaff(staff);
            _repo.SaveChanges();
            return Ok("STAFF ADDED");
        }

          [HttpGet]
        [Route("api/Staff")]
        public IActionResult GetStaff([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
        var staffQuery = _repo.StaffMembers.GetStaff();
        var totalStaff = staffQuery.Count();
        var totalPages = (int)Math.Ceiling((double)totalStaff / pageSize);
        if (page < 1 || page > totalPages)
        {
            return BadRequest("Invalid page number. Please provide a valid page number within the range.");
        }
        var pagedStudents = staffQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var response = new
        {
            TotalStaff = totalStaff,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            Staff = pagedStudents
        };

        return Ok(response);
        }

        [HttpGet]
        [Route("api/Staff/{id}")]
        public IActionResult GetByIDStaff([Bind(Prefix= "id")] int id)
        {
            var staff = _repo.StaffMembers.GetByIDStaff(id);
            return Ok(staff);
        }

        [HttpDelete]
        [Route("api/DeleteStaff/{id}")]
        public IActionResult DeleteStaff([Bind(Prefix= "id")] int id)
        {
            try
            {
            _repo.StaffMembers.DeleteStaff(id);
            _repo.SaveChanges();
            ResetIdentitySeed("Staff");
            ResetIdentitySeed("Accounts");
            ResetIdentitySeed("Classes");
            return Ok("STAFF DELETED");
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500,$"An error occured: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("api/UpdateStaff/{id}")]
        public IActionResult UpdateStaff([Bind(Prefix= "id")] int id,[FromBody]Staff staff)
        {
            if(staff == null)
            {
                return BadRequest("staff Data is not null ");
            }
            var staffs = _repo.StaffMembers.GetByIDStaff(id);
            if (staffs == null)
            {
                return NotFound($"Student with ID {id} not found");
            }
        staffs.Name = staff.Name;
        _repo.StaffMembers.UpdateStaff(staffs);
        _repo.SaveChanges();
        return Ok("Staff Updated");
        }

        private void ResetIdentitySeed(string tableName)
        {
            string resetseedsql = $"DBCC CHECKIDENT ('{tableName}',RESEED,0);";
            _repo.ExecuteSqlRaw(resetseedsql);
        }
    }
}