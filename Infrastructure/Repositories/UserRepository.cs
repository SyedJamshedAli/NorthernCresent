using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
   public class UserRepository: IUser
    {
        private DataBaseContext db;
       
        public UserRepository(DataBaseContext _db)
        {
            db = _db;
        }
        public UserRoleViewModel GetRole()
        {
            UserRoleViewModel user = new UserRoleViewModel();
            user.roles = db.roles.ToList();
            user.getUserRoleDatas = db.userRoles.ToList();
            
            var userrole = new UserRoleViewModel()
            {
                roles = user.roles,
                getUserRoleDatas=user.getUserRoleDatas

            };
            return userrole;
        }
        public List<GetuserAllData> Getuserdata()
        {
            List<GetuserAllData> user = new List<GetuserAllData>();
            user = (from role in db.roles
                    join userdata in db.userRoles on role.ID equals userdata.RoleID
                    where userdata.IsDeleted != true
                    select new GetuserAllData
                    {
                        ID = userdata.ID,
                        RoleID=userdata.RoleID,
                        UserEmail = userdata.UserEmail,
                        Status = userdata.Status,
                        RoleName = role.RoleName
                    }).ToList();
            return user;
        }
        public UserRoleViewModel GetById(long id)
        {
            UserRoleViewModel View = new UserRoleViewModel();
            
            var eve = db.userRoles.Find(id);
            var userView = new UserRoleViewModel()
            {

                ID = eve.ID,

                RoleName = (from role in db.roles
                            join user in db.userRoles on role.ID equals user.RoleID
                            where user.ID == id
                            select role.RoleName).FirstOrDefault(),
                Status = eve.Status,
                UserEmail = eve.UserEmail,
                RoleID = eve.RoleID,
                roles=db.roles.ToList(),
                GetuserAllDatas= (from role in db.roles
                                  join userdata in db.userRoles on role.ID equals userdata.RoleID
                                  select new GetuserAllData
                                  {
                                      ID = userdata.ID,
                                      RoleID = userdata.RoleID,
                                      UserEmail = userdata.UserEmail,
                                      Status = userdata.Status,
                                      RoleName = role.RoleName
                                  }).ToList()

            };
            return userView;
        }


        public UserRoleViewModel CompanyUserRole(string email)
        {
            //UserRoleViewModel user = new UserRoleViewModel();

            var data = (from azuser in db.roles
                        join role in db.userRoles on azuser.ID equals role.RoleID
                        where role.UserEmail == email
                        select new UserRoleViewModel
                        {
                            RoleName = azuser.RoleName
                        }
                        ).FirstOrDefault();

            return data;
                       

        }
        }
}
