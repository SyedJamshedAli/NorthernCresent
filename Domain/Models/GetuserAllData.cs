using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
   public class GetuserAllData
    {
        public long ID { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public long RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
