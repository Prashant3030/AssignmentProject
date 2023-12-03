using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.API.Repository;
using StudentManagementSystem.API.UnitOfWork;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("api/getbalance")]
        public async Task<ActionResult<double>> GetAccountBalance([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var totalProfit = await _unitOfWork.Accounts.GetTotalProfit(startDate, endDate);
                return Ok("The total Account Balance " + totalProfit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}