using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class GetUserRoleData
    {
        public int ID { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public string RoleName { get; set; }
    }
}
