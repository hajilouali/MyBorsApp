using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DataLayer;
using Partosazancnc.Models;

namespace Tools
{
    public class MyCMSRollProvider:RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            using (MyContext db=new MyContext())
            {
                if (db.Userses.FirstOrDefault(s => s.UserName.Contains(username) ).Roles.RoleName==roleName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (MyContext db=new MyContext())
            {
                return db.Userses.Where(u => u.UserName == username).Select(u => u.Roles.RoleName).ToArray();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}