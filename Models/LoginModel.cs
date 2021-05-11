using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Models
{
     public class LoginModel
        {
            [Required(ErrorMessage = "Please enter E-mail address.")]
            [EmailAddress(ErrorMessage = "E-mail address is not in a valid format.")]
            public string EmailAddress { get; set; }
            [Required(ErrorMessage = "Please enter password.")]
            public string Password { get; set; }
            public string UserName { get; set; }
            public int? UserId { get; set; }
            //public string PaymentCode { get; set; }
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
            public UserSession userSession { get; set; }
            [Required(ErrorMessage = "Please enter old password.")]
            public string oldPassword { get; set; }
            [Required(ErrorMessage = "Please enter new password."),
                MinLength(6, ErrorMessage = "The Password must be at least 6 characters."),
                MaxLength(30, ErrorMessage = "The Password connot be longer than 30 characters.")]
            public string NewPassword { get; set; }
            [Required(ErrorMessage = "Please enter confirm password.")]
            [Compare("NewPassword", ErrorMessage = "Confirm password and new password does not match.")]
            public string ConfirmPassword { get; set; }
            public string UserType { get; set; }
            public string FullApplicationPath { get; set; }
        public string ReturnUrl { get; set; }
        }
    
}
