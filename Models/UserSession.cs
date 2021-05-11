using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Models
{
    public class UserSession
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string UserType { get; set; }
        public string EmailAddress { get; set; }
        public string SubDomainName { get; set; }
    }
}
