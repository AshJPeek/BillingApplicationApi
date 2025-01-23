using Microsoft.AspNetCore.Mvc;

namespace BillingApplicationApi.Controllers;

public class ProductsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}