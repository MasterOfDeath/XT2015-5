namespace Employees.Entites
{
    using System;
    
    public class Award
    {
        private int id;
        private string title;

        public int Id
        { 
            get
            {
                return this.id;
            }
            
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id mustn't be negative");
                }
                else
                {
                    this.id = value;
                }
            }
        }
        
        public string Title
        { 
            get
            {
                return this.title;
            }
            
            set
            {
                this.title = value;
            }
        }
    }
}
