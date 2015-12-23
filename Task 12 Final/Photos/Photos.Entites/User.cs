namespace Photos.Entites
{
    public class User
    {
        public User(
            int id, 
            string firstName, 
            string lastName, 
            string userName, 
            byte[] hash,
            int tariff_id,
            bool enabled)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = userName;
            this.Hash = hash;
            this.Tariff_Id = tariff_id;
            this.Enabled = enabled;
        }

        public User(int id, string firstName, string lastName, string userName, byte[] hash, int tariff_id)
            : this(id, firstName, lastName, userName, hash, tariff_id, enabled: true)
        {
        }

        public User(string firstName, string lastName, string userName, byte[] hash, int tariff_id)
            : this(0, firstName, lastName, userName, hash, tariff_id, enabled: true)
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public byte[] Hash { get; set; }

        public int Tariff_Id { get; set; }

        public bool Enabled { get; set; }
    }
}
