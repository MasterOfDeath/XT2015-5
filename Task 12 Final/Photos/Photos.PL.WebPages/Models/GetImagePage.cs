namespace Photos.PL.WebPages.Models
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Entites;
    using System.Web.Helpers;
    public static class GetImagePage
    {
        private static readonly string defaultAlbumCoverFile = 
            ConfigurationManager.AppSettings["defaultAlbumCoverFile"];
        private static readonly string defaultAlbumCoverType = 
            ConfigurationManager.AppSettings["defaultAlbumCoverType"];

        private static readonly IDictionary<string, Func<HttpRequestBase, Tuple<byte[], string>>> _Queries
            = new Dictionary<string, Func<HttpRequestBase, Tuple<byte[], string>>>();

        static GetImagePage()
        {
            _Queries.Add("album", GetAlbumCover);
            _Queries.Add("photo", GetPhoto);
            _Queries.Add("photothumb", GetThumbPhoto);
        }

        public static IDictionary<string, Func<HttpRequestBase, Tuple<byte[], string>>> Queries
        {
            get { return _Queries; }
        }

        private static Tuple<byte[], string> GetAlbumCover(HttpRequestBase request)
        {
            byte[] result = null;
            Photo photo = null;
            string albumIdStr = request["albumid"];

            if (string.IsNullOrEmpty(albumIdStr))
            {
                // TODO to log $"Invalid request: null values of {nameof(albumIdStr)}");
            }

            int albumId = 0;

            try
            {
                albumId = Convert.ToInt32(albumIdStr);
            }
            catch (Exception)
            {
                // TODO to log
            }

            try
            {
                photo = LogicProvider.PhotoLogic.GetPhotoForAlbumCover(albumId);
            }
            catch (Exception)
            {
                // TODO to log
            }

            if (photo == null)
            {
                return GetDefaultAlbumCover();
            }

            try
            {
                var image = new WebImage(LogicProvider.PhotoLogic.GetDataById(photo.Id));
                image.Resize(width: 170, height: 170, preserveAspectRatio: true, preventEnlarge: true);
                result = image.GetBytes();
            }
            catch (Exception)
            {
                // TODO to log
            }

            return new Tuple<byte[], string>(result, photo.Mime);
        }

        private static Tuple<byte[], string> GetDefaultAlbumCover()
        {
            if (!File.Exists(defaultAlbumCoverFile))
            {
                return null;
            }

            var imageArray = File.ReadAllBytes(defaultAlbumCoverFile);

            return new Tuple<byte[], string>(imageArray, defaultAlbumCoverType);
        }

        private static Tuple<byte[], string> GetPhoto(HttpRequestBase request)
        {
            var photoIdStr = request["photoid"];
            Photo photo = null;
            byte[] photoData = null;

            if (string.IsNullOrEmpty(photoIdStr))
            {
                // TODO to log $"Invalid request: null values of {nameof(albumIdStr)}");
                return null;
            }

            int photoId = 0;

            try
            {
                photoId = Convert.ToInt32(photoIdStr);
            }
            catch (Exception)
            {
                // TODO to log
                return null;
            }

            try
            {
                photo = LogicProvider.PhotoLogic.GetPhotoById(photoId);
            }
            catch (Exception)
            {
                // TODO to log
                return null;
            }

            try
            {
                photoData = LogicProvider.PhotoLogic.GetDataById(photoId);
            }
            catch (Exception)
            {
                // TODO to log
                return null;
            }

            return new Tuple<byte[], string>(photoData, photo.Mime);
        }

        private static Tuple<byte[], string> GetThumbPhoto(HttpRequestBase request)
        {
            var result = GetPhoto(request);

            if (result.Item1 != null)
            {
                WebImage image = new WebImage(result.Item1);
                image.Resize(width: 150, height: 150, preserveAspectRatio: true, preventEnlarge: true);
                result = new Tuple<byte[], string>(image.GetBytes(), result.Item2);
            }

            return result;
        }
    }
}