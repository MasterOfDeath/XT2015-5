namespace Employees.DAL.Contract
{
    using System;
    using System.Collections.Generic;
    using Entites;

    public interface IAwardStore
    {
        int AddAward(Award award);

        bool DeleteAward(int awardId);

        ICollection<Award> ListAllAwards();

        Award GetAwardById(int id);

        IEnumerable<Award> ListAwardsByUserId(int userId);

        bool PresentAward(int userId, int awardId);

        bool PullOffAward(int userId, int awardId);

        bool SaveAvatar(int awardId, byte[] imageArray, string imageType);

        Tuple<byte[], string> GetAvatar(int awardId);
    }
}
