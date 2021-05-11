using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Models
{
    public class ProjectModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Name.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter Description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter Client Name.")]
        public string ClientName { get; set; }
        
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png",".gif", ".jpeg" })]
        public string ClientLogoPath { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public string ProjectImagePath { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".doc", ".xls", ".ppt", ".pptx", ".pdf", ".html", ".xlsm", ".xlsx", ".txt" })]
        public string RUDDocumentPath { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".doc", ".xls", ".ppt", ".pptx", ".pdf", ".html", ".xlsm", ".xlsx", ".txt" })]
        public string InitialScopeDocPath { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".doc", ".xls", ".ppt", ".pptx", ".pdf", ".html", ".xlsm", ".xlsx", ".txt" })]
        public string UpdatedScopeDocPath { get; set; }
        [Required(ErrorMessage = "Please Demo detail.")]
        public string DemoDetail { get; set; }
        public string FAuthorName { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public string FAuthorImagePath { get; set; }
        public string FFeedback { get; set; } 
        public string Status { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".doc", ".xls", ".ppt", ".pptx", ".pdf", ".html", ".xlsm", ".xlsx", ".txt" })]
        public IFormFile flProjectImagePath { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".doc", ".xls", ".ppt", ".pptx", ".pdf", ".html", ".xlsm", ".xlsx", ".txt" })]
        public IFormFile flRUDDocumentPath { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".doc", ".xls", ".ppt", ".pptx", ".pdf", ".html", ".xlsm", ".xlsx", ".txt" })]
        public IFormFile flInitialScopeDocPath { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".doc", ".xls", ".ppt", ".pptx", ".pdf", ".html", ".xlsm", ".xlsx", ".txt" })]
        public IFormFile flUpdatedScopeDocPath { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile flClientLogoPath { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile flFAuthorImagePath { get; set; }

        public int IndustryId { get; set; }
        public List<int> IndustriesSelectedList { get; set; }
        public List<IndustriesList> IndustriesList { get; set; }
        public int ServiceId { get; set; }
        public List<int> ServicesSelectedList { get; set; }
        public List<ServicesList> ServicesList { get; set; }
        public int TechnologyId { get; set; }
        public List<int> TechnologiesSelectedList { get; set; }
        public List<TechnologiesList> TechnologiesList { get; set; }
   
        public int tagId { get; set; }
        public List<int> TagsSelectedList { get; set; }
        public List<TagsList> TagsList { get; set; }

        public string IndustryCategoryName { get; set; }
        public string ServicesCategoryName { get; set; }
        public string TechnologiesCategoryName { get; set; }
        public string TagsCategoryName { get; set; }
        public string TypeName { get; set; }
        public int EditId { get; set; }
    }
}
