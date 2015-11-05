namespace Employees.BLL.Contract
{
    using System;
    using System.Collections.Generic;
    using Employees.Entites;
    
    public interface IUserLogic 
    {
        bool Add(User user);
        
        bool Delete(int id);
        
        IEnumerable<User> ListAll();
    }
}
