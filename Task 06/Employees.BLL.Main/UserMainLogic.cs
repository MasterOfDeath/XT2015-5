namespace Employees.BLL.Main
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Employees.BLL.Contract;
    using Employees.Entites;

    public class UserMainLogic : IUserLogic
    {
        private readonly Regex regUserName = new Regex(@"[^\w- \.]+");
        private readonly int maxNameLength = 50;

        public bool AddUser(User user)
        {
            this.CheckUsersValues(user);

            return Stores.UserStore.AddUser(user);
        }

        public bool DeleteUser(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id mustn't be negative.");
            }

            var user = Stores.UserStore.GetUserById(id);

            if (user == null)
            {
                throw new ArgumentException("The Employee not found.");
            }

            return Stores.UserStore.DeleteUser(user);
        }

        public IEnumerable<User> ListAll()
        {
            return Stores.UserStore.ListAllUsers();
        }

        public int GetAge(User user)
        {
            DateTime nowDate = DateTime.Today;
            int diff = nowDate.Year - user.BirthDay.Year;

            return (user.BirthDay > nowDate.AddYears(-diff)) ? diff - 1 : diff;
        }

        private void CheckUsersValues(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new ArgumentException($"The Name mustn't be empty.");
            }

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

                throw new ArgumentException($"Characters '{strMatches.ToString()}' aren't correct for name.");
            }

            if (user.BirthDay > DateTime.Now)
            {
                throw new ArgumentException($"The BirthDay mustn't be in the future.");
            }
        }
    }
}
