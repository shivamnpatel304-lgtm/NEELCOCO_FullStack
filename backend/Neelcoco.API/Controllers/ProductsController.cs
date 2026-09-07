using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neelcoco.API.Data;

namespace Neelcoco.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(NeelcocoDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] int? categoryId)
    {
        var query = db.Products.Include(x => x.Category).Where(x => x.IsActive).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x => x.Name.Contains(search));

        if (categoryId.HasValue)
            query = query.Where(x => x.CategoryId == categoryId);

        return Ok(await query.OrderByDescending(x => x.IsFeatured).ThenBy(x => x.Name).ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await db.Products.Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

        return product is null ? NotFound(new { message = "Product not found" }) : Ok(product);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> Categories() =>
        Ok(await db.Categories.OrderBy(x => x.Name).ToListAsync());
}
