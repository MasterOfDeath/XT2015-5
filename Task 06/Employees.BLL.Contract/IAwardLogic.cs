namespace Employees.BLL.Contract
{
    using System.Collections.Generic;
    using Employees.Entites;
    
    public interface IAwardLogic 
    {
        bool AddAward(Award award);

        IEnumerable<Award> GetAllAwards();

        IEnumerable<Award> GetAwardsByUserId(int userId);

        bool PresentAward(int userId, int awardId);

        bool PullOffAward(int userId, int awardId);
    }
}
