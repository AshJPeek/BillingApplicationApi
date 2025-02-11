using BillingApplicationApi.Domain.Models;
using BillingApplicationApi.Sqlite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingApplicationApi.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoicesController : ControllerBase
{
    private readonly BillingDbContext _context;

    public InvoicesController(BillingDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] Invoice invoice)
    {
        var customer = await _context.Customers.FindAsync(invoice.CustomerId);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }
        decimal totalAmount = 0;

        foreach (var item in invoice.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product == null)
            {
                return NotFound($"Product {item.ProductId} not found");
            }
            
            item.Price = product.Price * item.Quantity;
            totalAmount += item.Price;
        }
        
        invoice.TotalAmount = totalAmount;
        invoice.InvoiceDate = DateTime.Now;
        
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();
        
        return Ok(invoice);
    }

    [HttpGet]
    public async Task<IActionResult> GetInvoices()
    {
        var invoices = await _context.Invoices
            .Include(i => i.Items)
            .ThenInclude(ii => ii.Product)
            .ToListAsync();

        if (invoices == null || invoices.Count == 0)
        {
            return NoContent();
        }
        
        return Ok(invoices);
    }
}