﻿namespace Employees.BLL.Contract
{
    using System;
    using System.Collections.Generic;
    using Employees.Entites;
    
    public interface IUserLogic 
    {
        bool AddUser(User user);
        
        bool DeleteUser(int id);
        
        IEnumerable<User> ListAll();

        bool RewardUser(int userId, int awardId);

        bool PullOffAward(int userId, int awardId);
    }
}
