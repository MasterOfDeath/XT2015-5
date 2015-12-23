namespace Photos.BLL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IUserLogic
    {
        bool AddUser(User user, string password);

        bool InsertUser(User user);

        bool RemoveUser(int userId);

        ICollection<User> ListUsers();

        User GetUserById(int userId);

        User GetUserByUserName(string userName);

        bool CanLogin(User user, string password);
    }
}
