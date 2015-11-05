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
        
        private int id;
        private string name;
        private DateTime birthDay;
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
        
        public int Id 
        { 
            get 
            {
                return this.id;
            }
            
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id mustn't be less then 0");
                }
                else
                {
                    this.id = value;   
                }
            }
        }
        
        public string Name 
        { 
            get
            {
                return this.name;
            }
            
            private set
            { 
                this.name = value;
            }
        }
        
        public DateTime BirthDay 
        { 
            get
            { 
                return this.birthDay;
            } 
            
            private set
            { 
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Birth day mustn't be in the future!");
                }
                else
                {
                    this.birthDay = value;
                }
            }
        }
        
        public int Age 
        { 
            get
            {
                DateTime nowDate = DateTime.Today;
                int diff = nowDate.Year - this.birthDay.Year;

                return (this.birthDay > nowDate.AddYears(-diff)) ? diff - 1 : diff;
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