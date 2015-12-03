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

        public bool AddAward(Award award)
        {
            this.CheckAwardsValues(award);

            var awardNew = Stores.AwardStore.GetAwardByTitle(award.Title);

            if (awardNew != null)
            {
                throw new InvalidOperationException($"Award '{award.Title}' allready exests.");
            }

            Stores.AwardStore.AddAward(new Award(award.Title));

            return true;
        }

        public IEnumerable<Award> GetAllAwards()
        {
            return Stores.AwardStore.ListAllAwards();
        }

        public IEnumerable<Award> GetAwardsByUserId(int userId)
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
            if (userId <= 0 || awardId <= 0)
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

        private void CheckAwardsValues(Award award)
        {
            if (award == null)
            {
                throw new ArgumentException($"The Award mustn't be null.");
            }

            if (string.IsNullOrWhiteSpace(award.Title))
            {
                throw new ArgumentException($"The Title mustn't be empty.");
            }

            if (award.Title.Length > this.maxTitleLength)
            {
                throw new ArgumentException($"The Title mustn't be longer than {this.maxTitleLength}");
            }

            var matches = this.regAwardTitle.Matches(award.Title);

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
