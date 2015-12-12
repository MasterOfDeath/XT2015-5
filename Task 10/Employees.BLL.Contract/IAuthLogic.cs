namespace Employees.BLL.Contract
{
    using System.Collections.Generic;

    public interface IAuthLogic
    {
        ICollection<string> GetRolesForUser(string username);

        bool IsUserInRole(string username, string roleName);

        bool GiveRole(string username, string roleName);

        bool GiveAdminRole(string username);

        bool RevokeRole(string username, string roleName);

        bool RevokeAdminRole(string username);

        bool AddAuth(string username, string password);

        bool CanLogin(string username, string password);

        ICollection<string> GetAllUserNames();
    }
}