using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
   public class GetUserRoleData
    {
        public int ID { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public string RoleID { get; set; }
    }
}
