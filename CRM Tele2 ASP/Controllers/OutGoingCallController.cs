using CRM_Tele2_ASP.Models;
using CRM_Tele2_ASP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.RegularExpressions;

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
            await CreateClientIfClientNotExist(call);

            await ValidateCallData(call);

            if (ModelState.IsValid)
            {
                call.DateOfCall = DateTime.Now;
                db.Calls.Add(call);

                await db.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View("OutGoingCallIndex");
        }

        public async Task ValidateCallData(Call call)
        {
            if (call.ClientPhoneNumber == null || call.ClientPhoneNumber.Length < 12)
                ModelState.AddModelError("Call.ClientPhoneNumber", "Это поле является обязательным");

            if (!Regex.IsMatch(call.ClientPhoneNumber!, @"^\+77\d{9}"))
                ModelState.AddModelError("Call.ClientPhoneNumber", "Неверный формат номера. Введите телефон в формате +77...");
        }

        public async Task CreateClientIfClientNotExist(Call call)
        {
            bool isClientExist = db.Clients.Contains(new Client { PhoneNumber = call.ClientPhoneNumber });

            if (!isClientExist)
            {
                db.Add(new Client { Name = call.ClientName, Address = call.ClientAddress, PhoneNumber = call.ClientPhoneNumber });
                await db.SaveChangesAsync();
            }
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
            var calls = results.Select(c => new
            {
                c.label,
                dateOfCall = c.dateOfCall.ToString("dd.MM.yyyy HH:mm"),
                dateOfScheduledCall = c.dateOfScheduledCall?.ToString("dd.MM.yyyy HH: mm")
            });
            return Json(calls);
        }
    }
}
