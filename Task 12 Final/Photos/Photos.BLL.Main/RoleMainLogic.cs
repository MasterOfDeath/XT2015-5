namespace Photos.BLL.Main
{
    using System;
    using System.Collections.Generic;
    using Contract;
    using Entites;

    public class RoleMainLogic : IRoleLogic
    {
        private const string adminsRole = "admins";

        public bool GiveRole(int userId, string roleName)
        {
            if (userId < 0)
            {
                throw new ArgumentException($"{nameof(userId)} mustn't be negative");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException($"{nameof(roleName)} mustn't be null or empty");
            }

            ICollection<string> usersRoles = null;

            try
            {
                usersRoles = Stores.RoleStore.ListRolesForUserByUserId(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (usersRoles.Contains(roleName))
            {
                throw new InvalidOperationException($"User: {userId} allready has role: {roleName}");
            }

            return Stores.RoleStore.GiveRole(userId, roleName);
        }

        public ICollection<string> ListAllRoles()
        {
            return Stores.RoleStore.ListAllRoles();
        }

        public ICollection<string> ListRolesForUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"{nameof(userName)} mustn't be null or contains only spaces");
            }

            return Stores.RoleStore.ListRolesForUser(userName);
        }

        public bool PullOffRole(int userId, string roleName)
        {
            if (userId < 0)
            {
                throw new ArgumentException($"{nameof(userId)} mustn't be negative");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException($"{nameof(roleName)} mustn't be null or empty");
            }

            ICollection<string> usersRoles = null;

            try
            {
                usersRoles = Stores.RoleStore.ListRolesForUserByUserId(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (usersRoles == null || usersRoles?.Count <= 1)
            {
                throw new InvalidOperationException("User has to have at least one role");
            }

            ICollection<User> admins = null;

            try
            {
                admins = Stores.UserStore.ListUsersByRoleName(adminsRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (admins.Count <= 1)
            {
                throw new InvalidOperationException($"At least one user has to be in {adminsRole} role");
            }

            return Stores.RoleStore.PullOffRole(userId, roleName);
        }
    }
}
