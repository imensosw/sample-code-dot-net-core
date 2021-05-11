using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memento.Helper;
using Microsoft.AspNetCore.Mvc;
using Memento.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace Memento.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // Project List Method
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            int UserId = 1;
            ProjectViewModel model = new ProjectViewModel();
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
            model = model.ProjectList(UserId, 0);
            return View(model);
        }

        //Create Project Get method
        public IActionResult CreateProject(string Id)
        {
            int UserId = 1;
            int ProjectId = 0;
            if (Id != null && Id.Length>0)
            {
                ProjectId = Convert.ToInt32(CommonHelper.DecryptText(Id));
            }
            
            ProjectViewModel model = new ProjectViewModel();
            JsonResult result = new JsonResult(model);
            if (ProjectId == 0)
            {
                model = model.ProjectList(UserId, ProjectId);
            }
            else
            {
                model = model.EditProject(UserId, ProjectId);
            }
            return View("CreateProject", model);

        }        

        //Save Project Method
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public IActionResult SaveProject(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                string ProjectImage = _webHostEnvironment.WebRootPath.ToString();
                ProjectImage = Path.Combine(ProjectImage, @"/ProjectIamges/");
                string ProjectDocument = _webHostEnvironment.WebRootPath.ToString();
                ProjectDocument = Path.Combine(ProjectDocument, @"/Document/");
                string DBImagePath = "../../ProjectImages/";
                string DocumentPath = "../../Document/";
                int UserId = 1;
                string DocFullPath = "", ImageFullPath = "";

                //RUD Document Upload Code
                if (model.ProjectM.flRUDDocumentPath != null)
                {
                    string RUDFileName = Path.GetFileNameWithoutExtension(model.ProjectM.flRUDDocumentPath.FileName);                   
                    string RUDFileExtension = Path.GetExtension(model.ProjectM.flRUDDocumentPath.FileName);                    
                    RUDFileName = RUDFileName.Trim() + RUDFileExtension;                    
                    model.ProjectM.RUDDocumentPath = GetDocFullPath(DocumentPath, RUDFileName);
                    DocFullPath = GetDocFullPath("", RUDFileName);
                    model.ProjectM.flRUDDocumentPath.CopyTo(new FileStream(DocFullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
                }

                //Initial Document Upload Code
                if (model.ProjectM.flInitialScopeDocPath != null)
                {
                    string InitialFileName = Path.GetFileNameWithoutExtension(model.ProjectM.flInitialScopeDocPath.FileName);
                    //To Get File Extension  
                    string InitialFileExtension = Path.GetExtension(model.ProjectM.flInitialScopeDocPath.FileName);
                    //Add Current Date To Attached File Name  
                    InitialFileName = InitialFileName.Trim() + InitialFileExtension;
                    //Its Create complete path to store in server.                 
                    model.ProjectM.InitialScopeDocPath = GetDocFullPath(DocumentPath, InitialFileName);
                    DocFullPath = GetDocFullPath("", InitialFileName);
                    model.ProjectM.flInitialScopeDocPath.CopyTo(new FileStream(DocFullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
                }

                //Initial Document Upload Code
                if (model.ProjectM.flUpdatedScopeDocPath != null)
                {
                    string UpdatedScopeFileName = Path.GetFileNameWithoutExtension(model.ProjectM.flUpdatedScopeDocPath.FileName);
                    //To Get File Extension  
                    string UpdatedScopeFileExtension = Path.GetExtension(model.ProjectM.flUpdatedScopeDocPath.FileName);
                    //Add Current Date To Attached File Name  
                    UpdatedScopeFileName = UpdatedScopeFileName.Trim() + UpdatedScopeFileExtension;
                    //Its Create complete path to store in server.                  
                    model.ProjectM.UpdatedScopeDocPath = GetDocFullPath(DocumentPath, UpdatedScopeFileName);
                    DocFullPath = GetDocFullPath("", UpdatedScopeFileName);
                    model.ProjectM.flUpdatedScopeDocPath.CopyTo(new FileStream(DocFullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
                }

                //Project Image Upload Code
                if (model.ProjectM.flProjectImagePath != null)
                {
                    string ProjectImageFileName = Path.GetFileNameWithoutExtension(model.ProjectM.flProjectImagePath.FileName);
                    //To Get File Extension  
                    string ProjectImageFileExtension = Path.GetExtension(model.ProjectM.flProjectImagePath.FileName);
                    //Add Current Date To Attached File Name  
                    ProjectImageFileName = ProjectImageFileName.Trim() + ProjectImageFileExtension;
                    //Its Create complete path to store in server.                 
                    model.ProjectM.ProjectImagePath = GetFullPath(DBImagePath, ProjectImageFileName);
                    ImageFullPath = GetFullPath("", ProjectImageFileName);
                    model.ProjectM.flProjectImagePath.CopyTo(new FileStream(ImageFullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
                }
                //Author Image Upload Code
                if (model.ProjectM.flFAuthorImagePath != null)
                {
                    string FAuthorFileName = Path.GetFileNameWithoutExtension(model.ProjectM.flFAuthorImagePath.FileName);
                    //To Get File Extension  
                    string FAuthorFileExtension = Path.GetExtension(model.ProjectM.flFAuthorImagePath.FileName);
                    //Add Current Date To Attached File Name  
                    FAuthorFileName = FAuthorFileName.Trim() + FAuthorFileExtension;
                    //Its Create complete path to store in server.  
                    model.ProjectM.FAuthorImagePath = GetFullPath(DBImagePath, FAuthorFileName);
                    ImageFullPath = GetFullPath("", FAuthorFileName);
                    model.ProjectM.flFAuthorImagePath.CopyTo(new FileStream(ImageFullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
                }

                //Client Logo  Upload Code
                if (model.ProjectM.flClientLogoPath != null)
                {
                    string ClientLogoFileName = Path.GetFileNameWithoutExtension(model.ProjectM.flClientLogoPath.FileName);
                    //To Get File Extension  
                    string ClientLogoFileExtension = Path.GetExtension(model.ProjectM.flClientLogoPath.FileName);
                    //Add Current Date To Attached File Name  
                    ClientLogoFileName = ClientLogoFileName.Trim() + ClientLogoFileExtension;
                    //Its Create complete path to store in server.  
                    model.ProjectM.ClientLogoPath = GetFullPath(DBImagePath, ClientLogoFileName);
                    ImageFullPath = GetFullPath("", ClientLogoFileName);
                    model.ProjectM.flClientLogoPath.CopyTo(new FileStream(ImageFullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
                    Response.Clear();
                }

                model = model.SaveProject(model, UserId);
            }
            return RedirectToAction("Index", "Project");
        }

        //Create Project Post Method
        [HttpPost]
        public ActionResult CreateProject(ProjectViewModel model)
        {           
            return View("CreateProject", model);
        }
        //Delete Project Method
        public ActionResult DeleteProject(string ProjectId)
        {
            int UserId = 1;
            ProjectViewModel model = new ProjectViewModel();
            if (ModelState.IsValid)
            {
                model = model.DeleteProject(Convert.ToInt32(ProjectId), UserId);
            }

            return RedirectToAction("Index", "Project");
        }

        //Get Unique File for Upload File (Image, Document)
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        //Get Full path Method for Image File
        string GetFullPath(string DBFilePath, string filename)
        {
            string ProjectImage1 = @_webHostEnvironment.WebRootPath;//CommonHelper.DefaultImagePath();
            string FolderName = @"/ProjectImages/";
            if (DBFilePath.Length > 0)
            {
                return "../../ProjectImages/" + filename;
            }
            return ProjectImage1 + FolderName + filename;
        }
        //Post method for searching
        [HttpPost]
        public ActionResult index(ProjectViewModel searchmodel)
        {
            ProjectViewModel resultmodel = new ProjectViewModel();
            resultmodel = resultmodel.SearchProject(searchmodel);
            return View(resultmodel);
        }
        //Get Full path Method for Document File
        string GetDocFullPath(string DBFilePath, string filename)
        {
            string Document1 = @_webHostEnvironment.WebRootPath;
            string FolderName = @"/Document/";
            if (DBFilePath.Length > 0)
            {
                return "../../Document/" + filename;
            }
            return Document1 + FolderName + filename;
        }
       
    }
}
