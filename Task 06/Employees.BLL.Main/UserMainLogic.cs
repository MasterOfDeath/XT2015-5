namespace Employees.BLL.Main
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Employees.BLL.Contract;
    using Employees.Entites;
    
    public class UserMainLogic : IUserLogic
    {
        private readonly Regex regUserName = new Regex(@"[^\w- \.]+");
        private readonly int maxNameLength = 50;

        public bool AddUser(User user)
        {
            this.checkUsersValues(user);

            return Stores.UserStore.AddUser(user);
        }

        public bool DeleteUser(int id)
        {
            try
            {
                return Stores.UserStore.DeleteUser(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public IEnumerable<User> ListAll()
        {
            return Stores.UserStore.ListAllUsers();
        }

        public bool RewardUser(int userId, int awardId)
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

            return Stores.UserStore.RewardUser(user, award);
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

            return Stores.UserStore.PullOffAward(user, award);
        }

        private void checkUsersValues(User user)
        {
            if (user.Name.Length > this.maxNameLength)
            {
                throw new ArgumentException($"The Name mustn't be longer than {this.maxNameLength}");
            }

            var matches = this.regUserName.Matches(user.Name);

            if (matches.Count != 0)
            {
                StringBuilder strMatches = new StringBuilder(this.maxNameLength);

                foreach (Match match in matches)
                {
                    strMatches.Append(match.Value);
                }

                throw new ArgumentException($"Characters \"{strMatches.ToString()}\" aren't correct for name.");
            }

            if (user.BirthDay > DateTime.Now)
            {
                throw new ArgumentException($"The BirthDay mustn't be in the future.");
            }
        }
    }
}
