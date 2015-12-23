namespace Photos.BLL.Contract
{
    using System.Collections.Generic;

    public interface IRoleLogic
    {
        ICollection<string> ListRolesForUser(string userName);
    }
}
