using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Models
{
    public class TypeModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Name.")]
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class TypeCategoryModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int TypeCount { get; set; }
    }
}
