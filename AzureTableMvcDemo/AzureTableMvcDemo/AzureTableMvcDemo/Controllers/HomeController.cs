using Microsoft.AspNetCore.Mvc;

namespace AzureTableMvcDemo.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
