using Memento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Memento.Helper;
using System;
using System.Web;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;


namespace Memento.Controllers
{

    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            AccountViewModel model = new AccountViewModel();
            model.loginModel = new LoginModel();
            int currentUserID = 0;         
            var us = HttpContext.Session.GetObjectFromJson<UserSession>("UserSession");
            if (us != null)
            {
                currentUserID = us.UserID;
            }
            if(currentUserID==0)
            {
                return View(model);
            }
                   
            return View(model);
        }

        //Login Post Action
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Login(string emailAddress,string password)
        {
            AccountViewModel model = new AccountViewModel();
            LoginModel login = new LoginModel();
            login.EmailAddress = emailAddress;
            login.Password=password;
           
            HttpContext.Session.Clear();
            var isAjax = CommonHelper.IsAjaxRequest(Request);
            if (isAjax)
            {               
                String subDomain ="local";
                model.loginModel = model.Login(login, subDomain);
                if (!string.IsNullOrWhiteSpace(model.loginModel.UserName) && model.loginModel.UserId > 0)
                {                  
                    HttpContext.Session.SetObjectAsJson("UserSession", model.loginModel.userSession);
                    model.loginModel.FullApplicationPath = string.Format("{0}/Dashboard/Index", model.loginModel.FullApplicationPath);
                    var data = new { success = true, text = model.loginModel.ErrorCode, value = model.loginModel.ErrorMessage };                    
                    return Json(new { success = true, responseText = model.loginModel.ErrorMessage });
                }                
            }         
            return Json(new { success = false, responseText = model.loginModel.ErrorMessage,responseCode=model.loginModel.ErrorCode });
        }
        //Logout Action
        public IActionResult Logout()
        {
            AccountViewModel model = new AccountViewModel();
            model.loginModel = new LoginModel();            
            HttpContext.Session.SetObjectAsJson("UserSession", model.loginModel.userSession);
            return RedirectToAction("Index", "Account");
        }
    }
}
