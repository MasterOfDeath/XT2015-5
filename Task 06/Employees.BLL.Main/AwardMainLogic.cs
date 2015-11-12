namespace Employees.BLL.Main
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using Employees.BLL.Contract;
    using Employees.Entites;

    public class AwardMainLogic : IAwardLogic
    {
        private readonly Regex regAwardTitle = new Regex(@"[^\w- \.?!]+");
        private readonly int maxTitleLength = 50;

        public int AddAward(string awardTitle)
        {
            this.CheckAwardsValues(awardTitle);

            var award = Stores.AwardStore.GetAwardByTitle(awardTitle);

            if (award != null)
            {
                throw new InvalidOperationException($"Award '{awardTitle}' allready exests.");
            }

            award = new Award(awardTitle);
            award.Id = Stores.AwardStore.AddAward(award);

            return award.Id;
        }

        public Award GetOrAddAward(string awardTitle)
        {
            var award = Stores.AwardStore.GetAwardByTitle(awardTitle);

            if (award != null)
            {
                return award;
            }

            award = new Award(awardTitle);
            award.Id = Stores.AwardStore.AddAward(award);

            return award;
        }

        public IEnumerable<Award> ListAllAwards()
        {
            return Stores.AwardStore.ListAllAwards();
        }

        public IEnumerable<Award> ListAwardsByUserId(int userId)
        {
            return Stores.AwardStore.ListAwardsByUserId(userId);
        }

        public bool PresentAward(int userId, int awardId)
        {
            if (userId < 0 || awardId < 0)
            {
                throw new ArgumentException($"User ID and Award ID must be positive.");
            }

            var user = Stores.UserStore.GetUserById(userId);
            if (user == null)
            {
                return false;
            }

            var award = Stores.AwardStore.GetAwardById(awardId);
            if (award == null)
            {
                return false;
            }

            return Stores.AwardStore.PresentAward(user.Id, award.Id);
        }

        public bool PullOffAward(int userId, int awardId)
        {
            if (userId < 0 || awardId < 0)
            {
                throw new ArgumentException($"User ID and Award ID must be positive.");
            }

            var user = Stores.UserStore.GetUserById(userId);
            if (user == null)
            {
                return false;
            }

            var award = Stores.AwardStore.GetAwardById(awardId);
            if (award == null)
            {
                return false;
            }

            return Stores.AwardStore.PullOffAward(user.Id, award.Id);
        }

        private void CheckAwardsValues(string strTitle)
        {
            if (string.IsNullOrWhiteSpace(strTitle))
            {
                throw new ArgumentException($"The Title mustn't be empty.");
            }

            if (strTitle.Length > this.maxTitleLength)
            {
                throw new ArgumentException($"The Title mustn't be longer than {this.maxTitleLength}");
            }

            var matches = this.regAwardTitle.Matches(strTitle);

            if (matches.Count != 0)
            {
                StringBuilder strMatches = new StringBuilder(this.maxTitleLength);

                foreach (Match match in matches)
                {
                    strMatches.Append(match.Value);
                }

                throw new ArgumentException($"Characters '{strMatches.ToString()}' aren't correct for the Title.");
            }
        }
    }
}
