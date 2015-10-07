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
        /// Name's field
        /// </summary>
        private string firstName;

        /// <summary>
        /// Surname's field
        /// </summary>
        private string lastName;

        /// <summary>
        /// Father's name field
        /// </summary>
        private string midleName;

        /// <summary>
        /// Birth Day's field
        /// </summary>
        private DateTime birthDate = default(DateTime);

        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="firstName">Name value</param>
        /// <param name="lastName">Surname value</param>
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
            get { return this.birthDate; }
            set { this.birthDate = value; }
        }

        /// <summary>
        /// Gets an age of current user
        /// </summary>
        public int Age
        {
            get { return this.GetAge(); }
        }

        /// <summary>
        /// Calculates an age of current user
        /// </summary>
        /// <returns>The age</returns>
        private int GetAge()
        {
            DateTime nowDate = DateTime.Today;
            int age = nowDate.Year - this.birthDate.Year;
            
            return (this.birthDate > nowDate.AddYears(-age)) ? age - 1 : age;
        }
    }
}
