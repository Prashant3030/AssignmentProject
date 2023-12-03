using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystem.API.Repository;
using StudentManagementSystem.API.UnitOfWork;
using StudentManagementSystem.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.API.Controllers;

[ApiController]

public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        var user = await _unitOfWork.Users.GetUserByEmailAndPassword(userLogin.EmailId, userLogin.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        if (!user.IsAdmin)
        {
            return Forbid();
        }

        var token = GenerateJwtToken(user);

        return Ok(new { Token = token });
    }

   private string GenerateJwtToken(UserLogin user)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.EmailId),
        new Claim(ClaimTypes.Role, "Admin"),
    };

    var key = GenerateSymmetricSecurityKey(); 
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var expires = DateTime.Now.AddDays(1); 

    var token = new JwtSecurityToken(
        "your_issuer",
        "your_audience",
        claims,
        expires: expires,
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

private SymmetricSecurityKey GenerateSymmetricSecurityKey()
{
    using var provider = new RNGCryptoServiceProvider();
    var key = new byte[32]; // 256 bits
    provider.GetBytes(key);

    return new SymmetricSecurityKey(key);
}


}