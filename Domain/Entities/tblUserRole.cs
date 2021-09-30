using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Text;

namespace Domain.Entities
{
   public class tblUserRole
    {
        [Key]
        public long ID { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public long RoleID { get; set; }
        public bool IsDeleted { get; set; }

    }
}
