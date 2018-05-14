using System;
using System.Web.Security;
using BLL;
using BLL.Services.Users;
using Contracts.DomainEntities.Users;

namespace Grey_Airlines.Providers
{
    public class AirlinesRoleProvider:RoleProvider
    {
        public override string[] GetRolesForUser(string login)
        {
            string[] roles;
            UserService userService = new BllUnit().UserService;
            {
                User user = userService.GetUserByLogin(login);
                Role role = user.Role;
                roles = new string[]{role.Title};
            }
            return roles;
        }

        public override bool IsUserInRole(string login, string roleName)
        {
            bool result = false;
            UserService userService = new BllUnit().UserService;
            {
                User user = userService.GetUserByLogin(login);
                Role role = user.Role;
                result = role.Title.Equals(roleName);
            }
            return result;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string rolename)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}