namespace Employees.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IAwardStore
    {
        // Returns ID of added award 
        int AddAward(Award award);

        IDictionary<int, string> Awards { get; }

        IDictionary<int,string> ListAllAwards();
    }
}
