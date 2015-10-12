//-----------------------------------------------------------------------
// <copyright file="User.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _2_03
{
    using System;

    /// <summary>
    /// User's information
    /// </summary>
    public class User
    {
        /// <summary>
        /// First name's field
        /// </summary>
        private string firstName;

        /// <summary>
        /// Last name's field
        /// </summary>
        private string lastName;

        /// <summary>
        /// Father's name field
        /// </summary>
        private string midleName;

        /// <summary>
        /// Birth Day's field
        /// </summary>
        private DateTime birthDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="firstName">First name value</param>
        /// <param name="lastName">Last name value</param>
        /// <param name="midleName">Father's name value</param>
        /// <param name="birthDate">Birth Day value</param>
        public User(string firstName, string lastName, string midleName, DateTime birthDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MidleName = midleName;
            this.BirthDate = birthDate;
        }

        /// <summary>
        /// Gets or sets name value
        /// </summary>
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.firstName = value;
                }
                else
                {
                    throw new ArgumentException("The first name mustn't be null or empty!");
                }
            }
        }

        /// <summary>
        /// Gets or sets surname value
        /// </summary>
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.lastName = value;
                }
                else
                {
                    throw new ArgumentException("The last name mustn't be null or empty!");
                }
            }
        }

        /// <summary>
        /// Gets or sets father's name value
        /// </summary>
        public string MidleName
        {
            get
            {
                return this.midleName;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.midleName = value;
                }
                else
                {
                    throw new ArgumentException("The midle name mustn't be null or empty!");
                }
            }
        }

        /// <summary>
        /// Gets or sets birthDate value
        /// </summary>
        public DateTime BirthDate
        {
            get
            {
                return this.birthDate;
            }

            set
            {
                if (value <= DateTime.Now)
                {
                    this.birthDate = value;
                }
                else
                {
                    throw new ArgumentException($"This date: {value} from future!");
                }
            }
        }

        /// <summary>
        /// Gets an age of current user
        /// </summary>
        public int Age
        {
            get { return GetDiffByYears(this.birthDate); }
        }

        public static int GetDiffByYears(DateTime date)
        {
            DateTime nowDate = DateTime.Today;
            int diff = nowDate.Year - date.Year;

            return (date > nowDate.AddYears(-diff)) ? diff - 1 : diff;
        }

        public override string ToString()
        {
            return "First name: \t\"" + this.FirstName + "\"\n" +
                "Last name: \t\"" + this.LastName + "\"\n" +
                "Midle name: \t\"" + this.MidleName + "\"\n" +
                "Birth day: \t\"" + this.BirthDate.ToShortDateString() + "\"\n";
        }
    }
}