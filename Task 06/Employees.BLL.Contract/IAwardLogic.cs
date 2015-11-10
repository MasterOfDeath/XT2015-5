namespace Employees.BLL.Contract
{
    using System;
    using System.Collections.Generic;
    using Employees.Entites;
    
    public interface IAwardLogic 
    {
        Award GetOrAddAward(string awardTitle);

        int AddAward(string awardTitle);

        IEnumerable<Award> ListAllAwards();
    }
}
