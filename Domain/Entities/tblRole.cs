using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
   public class tblRole
    {
        [Key]
        public long ID { get; set; }
        public string RoleName { get; set; }
    }
}
