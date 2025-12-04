using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Application.DTOs;

public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class TokenResponseDTO
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public int UserId { get; set; }
    public string Role { get; set; }
}