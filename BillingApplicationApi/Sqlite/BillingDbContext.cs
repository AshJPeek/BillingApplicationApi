using BillingApplicationApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingApplicationApi.Sqlite;

public class BillingDbContext : DbContext
{
    public BillingDbContext(DbContextOptions<BillingDbContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Customer> Customers { get; set; } 
    public DbSet<Product> Products { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("Data Source=billing.db");
}