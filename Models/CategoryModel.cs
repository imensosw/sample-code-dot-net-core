using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter Name.")]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter TypeId.")]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Status { get; set; }
        public string OrderNo { get; set; }
    }
}
