using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DatabaseContext
{
   public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Discipline { get; set; }
        public string JobTitle { get; set; }
        public int Phone { get; set; }
        public string App { get; set; }
        public int companyID { get; set; }
        public int emailID { get; set; }
    }
}
