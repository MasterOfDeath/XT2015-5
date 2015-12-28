namespace Photos.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IUserStore
    {
        bool AddUser(User user);

        bool InsertUser(User user);

        bool RemoveUser(int userId);

        ICollection<User> ListAllUsers();

        User GetUserById(int userId);

        User GetUserByUserName(string userName);
    }
}
