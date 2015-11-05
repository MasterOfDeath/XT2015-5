namespace Employees.BLL.Main
{
    using System;
    using System.Collections.Generic;
    using Employees.BLL.Contract;
    using Employees.DAL.Contract;
    using Employees.DAL.Xml;
    using Employees.Entites;
    
    public class UserMainLogic : IUserLogic
    {
        public bool Add(User user)
        {
            return Stores.UserDao.Add(user);
        }
        
        public bool Delete(int id)
        {
            return Stores.UserDao.Delete(id);
        }
        
        public IEnumerable<User> ListAll()
        {
            return Stores.UserDao.ListAll();
        }
    }
}
