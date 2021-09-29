using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class SignUpViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Your Discipline")]
        public string Discipline { get; set; }
        [Required(ErrorMessage = "Please Enter Your JobTitle")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Please Enter Your Phone")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "Please Enter Your App")]
        public string App { get; set; }
        [Required(ErrorMessage = "Please Enter Your Company")]
        public int companyID { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email")]
        public int emailID { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter ConfirmPassword")]
        public string CPassword { get; set; }
    }
}
