namespace BillingApplicationApi.Domain.Models;

public class Invoice
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal Amount { get; set; }
    public List<InvoiceItem> Items { get; set; }
}