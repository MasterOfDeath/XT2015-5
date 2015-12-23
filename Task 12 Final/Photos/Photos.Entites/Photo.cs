namespace Photos.Entites
{
    using System;
    
    public class Photo
    {
        public Photo(int id, string name, int albumId, int size, string mime, DateTime date)
        {
            this.Id = id;
            this.Name = name;
            this.AlbumId = albumId;
            this.Size = size;
            this.Mime = mime;
            this.Date = date;
        }

        public Photo(string name, int albumId, int size, string mime, DateTime date)
            : this(0, name, albumId, size, mime, date)
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int AlbumId { get; set; }

        public int Size { get; set; }

        public string Mime { get; set; }

        public DateTime Date { get; set; }
    }
}
