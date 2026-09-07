using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Neelcoco.API.Data;
using Neelcoco.API.DTOs;
using Neelcoco.API.Services;

namespace Neelcoco.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(NeelcocoDbContext db, JwtService jwt) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await db.AdminUsers.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user is null) return Unauthorized(new { message = "Invalid email or password" });

        var hasher = new PasswordHasher<Models.AdminUser>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
            return Unauthorized(new { message = "Invalid email or password" });

        return Ok(new LoginResponse(jwt.CreateToken(user), user.Name, user.Role));
    }
}
