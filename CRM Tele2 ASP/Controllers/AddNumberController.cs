using CRM_Tele2_ASP.Models;
using CRM_Tele2_ASP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Tele2_ASP.Controllers
{
    public class AddNumberController : Controller
    {
        private readonly DataBaseContext db;
        public AddNumberController(DataBaseContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientCallViewModel model)
        {
            db.Clients.Add(model.Client);

            model.Call.ClientName = model.Client.Name;
            model.Call.ClientAddress = model.Client.Address;
            model.Call.ClientPhoneNumber = model.Client.PhoneNumber;
            model.Call.DateOfCall = DateTime.Now;

            db.Calls.Add(model.Call);

            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
