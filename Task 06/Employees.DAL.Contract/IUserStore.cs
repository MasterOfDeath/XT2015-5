namespace Employees.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IUserStore
    {
        bool Add(User user);

        bool Delete(int id);

        IEnumerable<User> ListAll();
    }
}
