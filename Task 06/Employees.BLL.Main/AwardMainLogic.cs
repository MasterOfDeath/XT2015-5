namespace Employees.BLL.Main
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using Employees.Entites;
    using Employees.BLL.Contract;

    public class AwardMainLogic : IAwardLogic
    {
        private readonly Regex regAwardTitle = new Regex(@"[^\w- \.?!]+");
        private readonly int maxTitleLength = 50;

        public int AddAward(string awardTitle)
        {
            checkAwardsValues(awardTitle);

            var award = Stores.AwardStore.GetAwardByTitle(awardTitle);

            if (award != null)
            {
                throw new InvalidOperationException($"Award \"{awardTitle}\" allready exests.");
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
            return Stores.AwardStore.Awards;
        }

        private void checkAwardsValues(string strTitle)
        {
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

                throw new ArgumentException($"Characters \"{strMatches.ToString()}\" aren't correct for the Title.");
            }
        }
    }
}
