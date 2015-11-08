﻿namespace Employees.BLL.Main
{
    using System;
    using System.Collections.Generic;
    using Employees.BLL.Contract;
    using Employees.Entites;
    
    public class UserMainLogic : IUserLogic
    {
        public bool AddUser(User user)
        {
            return Stores.UserDao.AddUser(user);
        }
        
        public bool DeleteUser(int id)
        {
            try
            {
                return Stores.UserDao.DeleteUser(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public IEnumerable<User> ListAll()
        {
            return Stores.UserDao.ListAllUsers();
        }
    }
}
