using Microsoft.AspNetCore.Mvc;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ExpenseApprovalSystem.Application.Interfaces; 

namespace ExpenseApprovalSystem.API.Controllers;

[Route("api/auth")] 
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IExpenseApprovalDbContext _context; 

    public AuthController(ITokenService tokenService, IExpenseApprovalDbContext context)
    {
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == loginDto.Email && x.IsActive);

        if (user == null || user.PasswordHash != loginDto.Password)
        {
            return Unauthorized(new { message = "Email veya şifre hatalı!" });
        }

        var tokenString = _tokenService.GenerateToken(user);

        return Ok(new { token = tokenString });
    }
}   