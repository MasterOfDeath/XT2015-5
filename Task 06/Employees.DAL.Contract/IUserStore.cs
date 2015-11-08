namespace Employees.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IUserStore
    {
        bool AddUser(User user);

        bool DeleteUser(int id);

        IEnumerable<User> ListAllUsers();
    }
}
