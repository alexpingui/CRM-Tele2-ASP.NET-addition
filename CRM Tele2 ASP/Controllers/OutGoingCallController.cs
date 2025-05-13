using CRM_Tele2_ASP.Models;
using CRM_Tele2_ASP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.Common;

namespace CRM_Tele2_ASP.Controllers
{
    public class OutGoingCallController : Controller
    {
        private readonly DataBaseContext db;
        
        public OutGoingCallController(DataBaseContext context)
        {
            db = context;
        }

        public ActionResult OutGoingCallIndex(string phone)
        {
            if (phone == null) return View();

            var clients = (List <Client>)SearchClientsByNumber(phone).Value!;
            Client client = clients[0];

            ClientCallViewModel cc = new()
            {
                Call = new Call(),
                Client = client
            };

            return View(cc);           
        }

        public async Task<IActionResult> CreateCall(Call call)
        {
            call.DateOfCall = DateTime.Now;
            db.Calls.Add(call);

            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public JsonResult SearchClientsByNumber(string numberPart)
        {
            var results = db.Clients.Where(c => c.PhoneNumber.Contains(numberPart))
                                    .ToList();
            return Json(results);
        }
        public JsonResult LoadCallHistory(string phoneNumber)
        {
            var results = db.Calls.Where(c => c.ClientPhoneNumber == phoneNumber)
                                  .Select(c => new
                                  {
                                      label = c.Comment,
                                      dateOfCall = c.DateOfCall,
                                      dateOfScheduledCall = c.DateOfScheduledCall
                                  })
                                  .ToList();
            return Json(results);
        }
    }
}
