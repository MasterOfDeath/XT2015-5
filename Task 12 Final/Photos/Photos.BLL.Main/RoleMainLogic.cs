namespace Photos.BLL.Main
{
    using System;
    using System.Collections.Generic;
    using Contract;

    public class RoleMainLogic : IRoleLogic
    {
        public ICollection<string> ListRolesForUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"{nameof(userName)} mustn't be null or contains only spaces");
            }

            return Stores.RoleStore.ListRolesForUser(userName);
        }
    }
}
