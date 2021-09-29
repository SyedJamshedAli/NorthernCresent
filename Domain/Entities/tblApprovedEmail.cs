using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
   public class tblApprovedEmail
    {
        [Key]
        public int emailID { get; set; }
        public string email { get; set; }
        public string Status { get; set; }
    }
}
