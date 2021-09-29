using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
   public class tblCompany
    {
        [Key]
        public int companyID { get; set; }
        public string companyName { get; set; }
        public string status { get; set; }
    }
}
