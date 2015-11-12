namespace Employees.BLL.Contract
{
    using System.Collections.Generic;
    using Employees.Entites;
    
    public interface IAwardLogic 
    {
        int AddAward(string awardTitle);

        IEnumerable<Award> ListAllAwards();

        IEnumerable<Award> ListAwardsByUserId(int userId);

        bool PresentAward(int userId, int awardId);

        bool PullOffAward(int userId, int awardId);
    }
}
