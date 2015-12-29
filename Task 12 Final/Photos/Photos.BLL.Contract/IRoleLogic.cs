namespace Photos.BLL.Contract
{
    using System.Collections.Generic;

    public interface IRoleLogic
    {
        ICollection<string> ListRolesForUser(string userName);

        ICollection<string> ListAllRoles();

        bool GiveRole(int userId, string roleName);

        bool PullOffRole(int userId, string roleName);
    }
}
