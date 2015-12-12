namespace Employees.DAL.Contract
{
    using System.Collections.Generic;

    public interface IAuthStore
    {
        ICollection<string> GetRolesForUser(string username);

        bool IsUserInRole(string username, string roleName);

        bool GiveRole(string username, string roleName);

        bool RevokeRole(string username, string roleName);

        ICollection<string> GetUsersInRole(string roleName);

        bool AddAuth(string username, string hash);

        string GetHashByUserName(string username);

        ICollection<string> GetAllUserNames();
    }
}
