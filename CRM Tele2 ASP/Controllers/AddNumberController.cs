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
        public async Task<IActionResult> CreateClientAndCall(ClientCallViewModel model)
        {
            if (model.Call.DateOfScheduledCall == null)
            {
                ModelState.AddModelError("Call.DateOfScheduledCall", "Укажите дату следующего звонка");               
            }
            ModelState.Remove("Call.ClientPhoneNumber");
            if (ModelState.IsValid)
            {
                db.Clients.Add(model.Client);
                await db.SaveChangesAsync();

                model.Call.ClientPhoneNumber = model.Client.PhoneNumber;
                model.Call.ClientName = model.Client.Name;
                model.Call.ClientAddress = model.Client.Address;
                model.Call.DateOfCall = DateTime.Now;
                db.Calls.Add(model.Call);

                await db.SaveChangesAsync();
                return Redirect("/");
            }

            return View("Index");
        }

    }
}
