using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Models
{
    public class SMTPSettingsModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool SSLRequired { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailFrom { get; set; }
        public string PayReceiptEmail { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
