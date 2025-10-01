using AzureTableMvcDemo.Models;
using AzureTableMvcDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureTableMvcDemo.Controllers;

public class CustomersController : Controller
{
    private readonly ITableStorageService _tableService;

    public CustomersController(ITableStorageService tableService)
    {
        _tableService = tableService;
    }

    // GET /Customers
    public async Task<IActionResult> Index()
    {
        var customers = await _tableService.GetCustomersAsync();
        return View(customers);
    }

    // GET /Customers/Create
    public IActionResult Create()
    {
        return View(new CustomerCreateViewModel());
    }

    // POST /Customers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerCreateViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var entity = new CustomerEntity
        {
            Name = vm.Name,
            Email = vm.Email
        };

        await _tableService.AddCustomerAsync(entity);
        TempData["Message"] = "Customer added";
        return RedirectToAction(nameof(Index));
    }
}
