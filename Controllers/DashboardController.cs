using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memento.Models;
using Microsoft.AspNetCore.Mvc;

namespace Memento.Controllers
{
    public class DashboardController : Controller
    {
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            int currentUserID = 0;
            var us = HttpContext.Session.GetObjectFromJson<UserSession>("UserSession");
            if (us != null)
            {
                currentUserID = us.UserID;
            }
            if (currentUserID == 0)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
    }
}
