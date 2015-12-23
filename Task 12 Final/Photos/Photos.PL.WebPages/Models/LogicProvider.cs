namespace Photos.PL.WebPages.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using BLL.Contract;
    using Entites;

    public static class LogicProvider
    {
        public static IUserLogic UserLogic { get; } = new BLL.Main.UserMainLogic();

        public static IRoleLogic RoleLogic { get; } = new BLL.Main.RoleMainLogic();

        public static IPhotoLogic PhotoLogic { get; } = new BLL.Main.PhotoMainLogic();

        public static IAlbumLogic AlbumLogic { get; } = new BLL.Main.AlbumMainLogic();

        public static Tuple<int, string> GetUserId(string userName)
        {
            User user = null;
            string error = null;

            if (string.IsNullOrWhiteSpace(userName))
            {
                return new Tuple<int, string>(-1, $"The variable \"{nameof(userName)}\" mustn't be null");
            }

            try
            {
                user = LogicProvider.UserLogic.GetUserByUserName(userName);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            if (user == null)
            {
                return new Tuple<int, string>(-1, $"The User \"{userName}\" hasn't fount in Data Store");
            }

            return new Tuple<int, string>(user.Id, error);
        }

        public static Tuple<ICollection<Album>, string> GetAlbums(int userId)
        {
            string error = null;
            ICollection<Album> albums = null;

            if (userId < 0)
            {
                return new Tuple<ICollection<Album>, string>(
                    null,
                    $"The variable \"{nameof(userId)}\" mustn't be negative");
            }

            try
            {
                albums = LogicProvider.AlbumLogic.ListAlbumsByUserId(userId);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return new Tuple<ICollection<Album>, string>(albums, error);
        }

        public static Tuple<ICollection<Photo>, string> GetPhotos(int albumId)
        {
            string error = null;
            ICollection<Photo> photos = null;

            if (albumId < 0)
            {
                return new Tuple<ICollection<Photo>, string>(
                    null,
                    $"The variable \"{nameof(albumId)}\" mustn't be negative");
            }

            try
            {
                photos = PhotoLogic.ListPhotosInAlbum(albumId);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return new Tuple<ICollection<Photo>, string>(photos, error);
        }
    }
}