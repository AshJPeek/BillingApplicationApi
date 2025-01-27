using BillingApplicationApi.Domain.Models;
using BillingApplicationApi.Sqlite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingApplicationApi.Controllers;

[ApiController]
[Route("api/customers")]

public class CustomersController : ControllerBase
{
    private readonly BillingDbContext _context;

    public CustomersController(BillingDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return Ok(customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _context.Customers.ToListAsync();
        return Ok(customers);
    }
}