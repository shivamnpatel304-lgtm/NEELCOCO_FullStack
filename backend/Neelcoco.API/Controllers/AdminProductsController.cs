using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neelcoco.API.Data;
using Neelcoco.API.Models;

namespace Neelcoco.API.Controllers;

[ApiController]
[Authorize(Roles = "ADMIN")]
[Route("api/admin/products")]
public class AdminProductsController(NeelcocoDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await db.Products.Include(x => x.Category).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await db.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        product.Id = 0;
        db.Products.Add(product);
        await db.SaveChangesAsync();
        // Reference the Get(int id) action so the Location header is valid
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Product input)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return NotFound();

        product.Name = input.Name;
        product.Description = input.Description;
        product.Price = input.Price;
        product.MRP = input.MRP;
        product.ImageUrl = input.ImageUrl;
        product.Unit = input.Unit;
        product.StockQuantity = input.StockQuantity;
        product.CategoryId = input.CategoryId;
        product.IsFeatured = input.IsFeatured;
        product.IsActive = input.IsActive;

        await db.SaveChangesAsync();
        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return NotFound();
        product.IsActive = false;
        await db.SaveChangesAsync();
        return NoContent();
    }
}
