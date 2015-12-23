namespace Photos.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IPhotoStore
    {
        bool AddPhoto(Photo photo, byte[] data);

        bool InsertPhoto(Photo photo, byte[] data);

        bool RemovePhoto(int photoId);

        ICollection<Photo> ListPhotosInAlbum(int albumId);

        Photo GetPhotoById(int photoId);

        byte[] GetDataById(int photoId);
    }
}
