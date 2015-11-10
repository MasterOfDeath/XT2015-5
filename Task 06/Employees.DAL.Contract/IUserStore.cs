﻿namespace Employees.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IUserStore
    {
        bool AddUser(User user);

        bool DeleteUser(int id);

        IEnumerable<User> ListAllUsers();

        User GetUserById(int userId);

        bool RewardUser(User user, Award award);

        bool PullOffAward(User user, Award award);
    }
}