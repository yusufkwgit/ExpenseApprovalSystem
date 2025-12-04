using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseApprovalSystem.Application.Services;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var secretKey = _configuration["JwtSettings:SecretKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        // 2. Kimlik Bilgilerini Hazırla
        // Token'ın içine gömülecek bilgiler. (Frontend buradan ID'yi okuyacak)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            new Claim(ClaimTypes.Name, user.NameSurname),                
            new Claim(ClaimTypes.Email, user.Email),                     
            new Claim(ClaimTypes.Role, user.Role.ToString())             
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 4. Token Ayarları
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpireMinutes"])),
            signingCredentials: creds
        );

        // 5. Oluştur ve String olarak dön
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}