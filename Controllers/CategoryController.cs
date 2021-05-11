using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memento.Helper;
using Microsoft.AspNetCore.Mvc;
using Memento.Models;

namespace Memento.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            int UserId = 1;
            CategoryViewModel model = new CategoryViewModel();
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
            model = model.TypeCategoryCount(UserId);

            model.SelectedTypeName = "Name";
            model.SelectedTypeId = -1;
            return View(model);
        }
        //Get the Category List
        public ActionResult CategoryList(string TypeId)
        {
            int UserId = 1;
            CategoryViewModel model = new CategoryViewModel();
            model = model.CategoryList(UserId, Convert.ToInt32(TypeId), 0);

            return PartialView("_CategoryList", model);

        }
        //Get method for Edit the Seleted Category
        public ActionResult EditCategory(string Id)
        {
            int UserId = 1;
            CategoryViewModel model = new CategoryViewModel();
            model.category = new CategoryModel();
            model = model.SelectCategoryList(UserId, Convert.ToInt32(Id));

            return PartialView("_CategoryList", model);
        }
        //Post method for Save Category
        [HttpPost]        
        public ActionResult SaveCategory(CategoryViewModel model)
        {
            int UserId = 1;
            if (ModelState.IsValid)
            {
                model = model.SaveCategory(model, UserId);
            }
            return RedirectToAction("Index", model);
        }
        //Delete Category method
        public ActionResult DeleteCategory(string CategoryId)
        {
            int UserId = 1;
            CategoryViewModel model = new CategoryViewModel();
            if (ModelState.IsValid)
            {
                model = model.DeleteCategory(Convert.ToInt32(CategoryId), UserId);
            }

            return PartialView("_CategoryList", model);
        }

        //Update Category Method
        [HttpPost]
        public ActionResult UpdateCategory(CategoryViewModel model)
        {           
            int UserId = 1;    
            if (ModelState.IsValid)
            {
                model = model.SaveCategory(model, UserId);
            }
            return RedirectToAction("Index", model);
        }

    }
}
