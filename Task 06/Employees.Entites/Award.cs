namespace Employees.Entites
{
    public class Award
    {
        public Award(int id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        public Award(string title)
            : this(0, title)
        {
        }

        public int Id { get; set; }
        
        public string Title { get; set; }
    }
}
