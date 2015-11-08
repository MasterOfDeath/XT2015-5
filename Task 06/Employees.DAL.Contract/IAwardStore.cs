namespace Employees.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IAwardStore
    {
        bool AddAward(Award award);

        IDictionary<int,string> ListAllAwards();
    }
}
