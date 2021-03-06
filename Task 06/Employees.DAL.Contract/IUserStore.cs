﻿namespace Employees.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IUserStore
    {
        bool AddUser(User user);

        bool DeleteUser(User user);

        IEnumerable<User> ListAllUsers();

        User GetUserById(int userId);
    }
}
