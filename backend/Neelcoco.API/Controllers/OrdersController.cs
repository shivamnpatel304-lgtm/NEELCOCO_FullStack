using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neelcoco.API.Data;
using Neelcoco.API.DTOs;
using Neelcoco.API.Models;

namespace Neelcoco.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(NeelcocoDbContext db) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        if (request.Items.Count == 0)
            return BadRequest(new { message = "Cart is empty" });

        var ids = request.Items.Select(x => x.ProductId).Distinct().ToList();
        var products = await db.Products.Where(x => ids.Contains(x.Id) && x.IsActive).ToDictionaryAsync(x => x.Id);

        if (products.Count != ids.Count)
            return BadRequest(new { message = "One or more products are unavailable" });

        var order = new Order
        {
            CustomerName = request.CustomerName,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            City = request.City
        };

        foreach (var item in request.Items)
        {
            var product = products[item.ProductId];
            if (item.Quantity <= 0 || product.StockQuantity < item.Quantity)
                return BadRequest(new { message = $"Insufficient stock for {product.Name}" });

            product.StockQuantity -= item.Quantity;
            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = item.Quantity
            });
            order.TotalAmount += product.Price * item.Quantity;
        }

        db.Orders.Add(order);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = order.Id }, new
        {
            order.Id,
            order.TotalAmount,
            order.Status,
            order.CreatedAt
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var order = await db.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
        return order is null ? NotFound() : Ok(order);
    }

    // Protect the full orders list — only admins should see all customer orders
    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetAll() =>
        Ok(await db.Orders.Include(x => x.Items).OrderByDescending(x => x.CreatedAt).ToListAsync());
}
