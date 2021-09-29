using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUser
    {
        public UserRoleViewModel GetRole();
        public List<GetuserAllData> Getuserdata();
        public UserRoleViewModel GetById(long id);
        public UserRoleViewModel CompanyUserRole(string email);
        

    }
}
