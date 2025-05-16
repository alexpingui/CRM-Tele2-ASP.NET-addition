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

    public IActionResult Index()
    {
        return View();
    }

    public async Task<JsonResult> GetScheduledCalls()
    {
        var allCalls = await db.Calls.ToListAsync();

        var latestCalls = allCalls
            .GroupBy(c => c.ClientPhoneNumber)
            .Select(g => g.OrderByDescending(c => c.DateOfCall).First())
            .Where(c => c.DateOfScheduledCall != null
                     && c.DateOfScheduledCall < DateTime.Today.AddDays(1))
            .OrderByDescending(c => c.DateOfScheduledCall);

        var result = latestCalls.Select(c => new
        {
            c.ClientName,
            c.ClientAddress,
            c.ClientPhoneNumber,
            c.Comment,
            DateOfCall = c.DateOfCall.ToString("dd.MM.yyyy HH:mm"),
            DateOfScheduledCall = c.DateOfScheduledCall?.ToString("dd.MM.yyyy HH:mm")
        });

        return Json(result);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
