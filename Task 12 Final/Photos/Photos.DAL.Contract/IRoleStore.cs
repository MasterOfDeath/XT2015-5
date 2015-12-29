namespace Photos.DAL.Contract
{
    using System.Collections.Generic;

    public interface IRoleStore
    {
        ICollection<string> ListRolesForUser(string userName);

        ICollection<string> ListRolesForUserByUserId(int userId);

        ICollection<string> ListAllRoles();

        bool GiveRole(int userId, string roleName);

        bool PullOffRole(int userId, string roleName);
    }
}
