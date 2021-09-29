using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class UserRoleViewModel
    {
        public long ID { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public long RoleID { get; set; }
        public string RoleName { get; set; }
        public List<GetUserRoleData>  getUserRoleDatas { get; set; }
        public List<GetuserAllData> GetuserAllDatas { get; set; }
        public List<RoleViewModel> roles { get; set; }
    }
}
