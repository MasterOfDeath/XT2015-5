namespace Employees.BLL.Main
{
    using System;
    using System.Collections.Generic;
    using Employees.BLL.Contract;
    using Employees.Exceptions;

    public class AuthMainLogic : IAuthLogic
    {
        private const string AdmRole = "admins";
        private const string DefaultRole = "users";

        public bool AddAuth(string username, string password)
        {
            var hash = SHA1Util.GetHashString(password);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username or password mustn't be NULL");
            }

            if (Stores.AuthStore.GetHashByUserName(username) != null)
            {
                throw new InvalidOperationException(username + " allready registered");
            }

            if (!Stores.AuthStore.AddAuth(username, hash))
            {
                return false;
            }
            else
            {
                return Stores.AuthStore.GiveRole(username, DefaultRole);
            }
        }

        public bool GiveRole(string username, string roleName)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Username or rolename mustn't be NULL");
            }

            if (Stores.AuthStore.GetRolesForUser(username).Contains(roleName))
            {
                throw new UserHasThisRoleException("The user allready has this role");
            }

            return Stores.AuthStore.GiveRole(username, roleName);
        }

        public bool GiveAdminRole(string username)
        {
            return this.GiveRole(username, AdmRole);
        }

        public bool CanLogin(string username, string password)
        {
            var hash = SHA1Util.GetHashString(password);

            return hash == Stores.AuthStore.GetHashByUserName(username);
        }

        public ICollection<string> GetRolesForUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username mustn't be NULL");
            }

            return Stores.AuthStore.GetRolesForUser(username);
        }

        public bool IsUserInRole(string username, string roleName)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Username or rolename mustn't be NULL");
            }

            return Stores.AuthStore.IsUserInRole(username, roleName);
        }

        public bool RevokeRole(string username, string roleName)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Username or rolename mustn't be NULL");
            }

            var rolesForUser = Stores.AuthStore.GetRolesForUser(username);

            if (!rolesForUser.Contains(roleName))
            {
                throw new UserDoesntHaveThisRoleException("The User doesn't have this role");
            }

            if (rolesForUser.Count <= 1)
            {
                throw new InvalidOperationException("The User has to have at least one role");
            }

            if (roleName == AdmRole)
            {
                if (Stores.AuthStore.GetUsersInRole(AdmRole).Count <= 1)
                {
                    throw new InvalidOperationException("At least one user has to have \"" + AdmRole + "\" role");
                }
            }
            
            return Stores.AuthStore.RevokeRole(username, roleName);
        }

        public bool RevokeAdminRole(string username)
        {
            return this.RevokeRole(username, AdmRole);
        }

        public ICollection<string> GetAllUserNames()
        {
            return Stores.AuthStore.GetAllUserNames();
        }
    }
}
