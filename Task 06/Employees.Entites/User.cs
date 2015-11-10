﻿namespace Employees.Entites
{
    using System;
    using System.Collections.Generic;
    
    public class User : IAwardable
    {
        public const string TableName = "user";
        public const string FId = "id";
        public const string FName = "name";
        public const string FBirthDay = "birth_day";
        public const string FAge = "age";
        public const string FHasAward = "has_award";

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