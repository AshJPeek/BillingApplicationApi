using Microsoft.AspNetCore.Mvc;

namespace BillingApplicationApi.Controllers;

public class InvoicesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}