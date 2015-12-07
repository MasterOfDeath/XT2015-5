namespace Employees.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IAwardStore
    {
        int AddAward(Award award);

        bool DeleteAward(int awardId);

        ICollection<Award> ListAllAwards();

        Award GetAwardByTitle(string titleStr);

        Award GetAwardById(int id);

        IEnumerable<Award> ListAwardsByUserId(int userId);

        bool PresentAward(int userId, int awardId);

        bool PullOffAward(int userId, int awardId);
    }
}
