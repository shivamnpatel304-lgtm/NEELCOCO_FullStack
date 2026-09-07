using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neelcoco.API.Data;
using Neelcoco.API.DTOs;
using Neelcoco.API.Models;

namespace Neelcoco.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController(NeelcocoDbContext db) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(ContactRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Message))
            return BadRequest(new { message = "Name and message are required" });

        db.ContactInquiries.Add(new ContactInquiry
        {
            Name = request.Name,
            Email = request.Email ?? string.Empty,
            Phone = request.Phone ?? string.Empty,
            Message = request.Message
        });

        await db.SaveChangesAsync();
        return Ok(new { message = "Thank you. Your inquiry has been received." });
    }

    // Admin-only: view all contact inquiries
    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetAll() =>
        Ok(await db.ContactInquiries.OrderByDescending(x => x.CreatedAt).ToListAsync());
}
