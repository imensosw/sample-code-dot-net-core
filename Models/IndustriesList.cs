using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Models
{
    public class IndustriesList
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TypeId { get; set; }
        public int ProjectId { get; set; }

    }
    public class ServicesList
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TypeId { get; set; }
        public int ProjectId { get; set; }
    }
    public class TechnologiesList
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TypeId { get; set; }
        public int ProjectId { get; set; }
    }
    public class TagsList
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TypeId { get; set; }
        public int ProjectId { get; set; }
    }
}
