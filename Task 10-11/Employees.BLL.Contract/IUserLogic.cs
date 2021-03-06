﻿namespace Employees.BLL.Contract
{
    using System;
    using System.Collections.Generic;
    using Employees.Entites;
    
    public interface IUserLogic 
    {
        bool AddUser(User user);
        
        bool DeleteUser(int id);
        
        IEnumerable<User> GetAllUsers();

        int GetAge(DateTime birthDay);

        IEnumerable<User> ListUsersByAwardId(int awardId);

        bool SaveAvatar(int userId, byte[] imageArray, string imageType);

        Tuple<byte[], string> GetAvatar(int userId);
    }
}
