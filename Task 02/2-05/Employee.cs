namespace _2_05
{
    using System;
    using _2_03;

    public class Employee : User
    {
        private int idPosition;
        private DateTime startWork;

        public Employee(string firstName, string lastName, string midleName, DateTime birthDate, int idPosition, DateTime startWork)
            : base(firstName, lastName, midleName, birthDate)
        {
            this.IdPosition = idPosition;
            this.StartWork = startWork;
        }

        public Employee(string firstName, string lastName, string midleName, DateTime birthDate, int idPosition)
            : this(firstName, lastName, midleName, birthDate, idPosition, DateTime.Today)
        {
        }

        public int IdPosition
        {
            get
            {
                return this.idPosition;
            }

            set
            {
                if (value >= 0)
                {
                    this.idPosition = value;
                }
                else
                {
                    throw new ArgumentException("Wrong code of position!");
                }
            }
        }

        public DateTime StartWork
        {
            get
            {
                return this.startWork;
            }

            set
            {
                if (value <= DateTime.Now)
                {
                    this.startWork = value;
                }
                else
                {
                    throw new ArgumentException($"This date: {value} from future!");
                }
            }
        }

        public int Experience
        {
            get { return User.GetDiffByYears(this.startWork); }
        }

        public override string ToString()
        {
            return "First name: \t\"" + this.FirstName + "\"\n" +
                "Last name: \t\"" + this.LastName + "\"\n" +
                "Midle name: \t\"" + this.MidleName + "\"\n" +
                "Birth day: \t\"" + this.BirthDate.ToShortDateString() + "\"\n" +
                "Id Position: \t\"" + this.IdPosition.ToString() + "\"\n" +
                "Experience: \t\"" + this.Experience.ToString() + "\"\n";
        }
    }
}
