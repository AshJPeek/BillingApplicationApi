using BillingApplicationApi.Domain.Models;
using BillingApplicationApi.Sqlite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingApplicationApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly BillingDbContext _context;

    public ProductsController(BillingDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(Product product)
    {
        if (product == null || string.IsNullOrEmpty(product.Name) || product.Price <= 0)
        {
            return BadRequest("Incorrect data");
        }
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound($"Product with id {id} was not found");
        }
        
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound($"Product with id {id} was not found");
        }
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}