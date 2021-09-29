using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class GetuserAllData
    {
        public long ID { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
