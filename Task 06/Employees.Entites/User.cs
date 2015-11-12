namespace Employees.Entites
{
    using System;
    
    public class User
    {
        public const string TableName = "user";
        public const string FId = "id";
        public const string FName = "name";
        public const string FBirthDay = "birth_day";
        
        public User(int id, string name, DateTime birthDay)
        {
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
    }
}