using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRM_Tele2_ASP.Models;
using CRM_Tele2_ASP.Services;
using Microsoft.EntityFrameworkCore;

namespace CRM_Tele2_ASP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly DataBaseContext db;
    public HomeController(ILogger<HomeController> logger, DataBaseContext context)
    {
        _logger = logger;
        db = context;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<JsonResult> GetScheduledCalls()
    {
        var calls = await db.Calls.Where(c => c.DateOfScheduledCall <= DateTime.Today).ToListAsync();
        return Json(calls);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
