namespace Photos.DAL.Contract
{
    using System.Collections.Generic;

    public interface IRoleStore
    {
        ICollection<string> ListRolesForUser(string userName);

        bool GiveRole(int userId, string roleName);
    }
}
