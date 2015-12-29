namespace Employees.BLL.Contract
{
    using System;
    using System.Collections.Generic;
    using Employees.Entites;
    
    public interface IAwardLogic 
    {
        bool AddAward(Award award);

        bool DeleteAward(int awardId);

        IEnumerable<Award> GetAllAwards();

        IEnumerable<Award> GetAwardsByUserId(int userId);

        bool PresentAward(int userId, int awardId);

        bool PullOffAward(int userId, int awardId);

        bool SaveAvatar(int awardId, byte[] imageArray, string imageType);

        Tuple<byte[], string> GetAvatar(int awardId);
    }
}
