namespace Employees.Entites
{
    using System;
    using System.Collections.Generic;
    
    public class User : IAwardable
    {
        public static readonly string TableName = "user";
        public static readonly string FId = "id";
        public static readonly string FName = "name";
        public static readonly string FBirthDay = "birth_day";
        public static readonly string FAge = "age";
        public static readonly string FHasAward = "has_award";

        private List<Award> awards;
        
        public User(int id, string name, DateTime birthDay)
        {
            this.awards = new List<Award>();
            
            this.Id = id;
            this.Name = name;
            this.BirthDay = birthDay;
        }
        
        public User(string name, DateTime birthDay)
            : this(0, name, birthDay)
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDay { get; set; }

        public int Age 
        { 
            get
            {
                DateTime nowDate = DateTime.Today;
                int diff = nowDate.Year - this.BirthDay.Year;

                return (this.BirthDay > nowDate.AddYears(-diff)) ? diff - 1 : diff;
            }
        }
        
        public IEnumerable<Award> Awards 
        { 
            get
            {
                return this.awards;
            }
        }
        
        public void AddAward(Award award)
        {
            this.awards.Add(award);
        }
    }
}