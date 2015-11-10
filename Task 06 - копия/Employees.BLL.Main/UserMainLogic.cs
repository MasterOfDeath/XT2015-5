namespace Employees.BLL.Main
{
    using System;
    using System.Collections.Generic;
    using Employees.BLL.Contract;
    using Employees.Entites;
    
    public class UserMainLogic : IUserLogic
    {
        public bool AddUser(User user)
        {
            return Stores.UserStore.AddUser(user);
        }
        
        public bool DeleteUser(int id)
        {
            try
            {
                return Stores.UserStore.DeleteUser(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public IEnumerable<User> ListAll()
        {
            return Stores.UserStore.ListAllUsers();
        }

        public Award GetOrAddAward(string awardStr)
        {
            //var awards = Stores.UserStore.

            return new Award("cd");
        }
    }
}
