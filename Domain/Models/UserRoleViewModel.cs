using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
   public class UserRoleViewModel
    {
        public long ID { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public long RoleID { get; set; }
        public string RoleName { get; set; }
        public List<tblUserRole> getUserRoleDatas { get; set; }
        public List<GetuserAllData> GetuserAllDatas { get; set; }
        public List<tblRole> roles { get; set; }
    }
}
