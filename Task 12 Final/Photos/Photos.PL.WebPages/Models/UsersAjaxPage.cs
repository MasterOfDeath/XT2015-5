namespace Photos.PL.WebPages.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Helpers;
    using Entites;
    
    public static class UsersAjaxPage
    {
        private static readonly IDictionary<string, Func<HttpRequestBase, AjaxResponse>> _Queries
            = new Dictionary<string, Func<HttpRequestBase, AjaxResponse>>();

        static UsersAjaxPage()
        {
            _Queries.Add("clickNewAlbumSaveBtn", ClickNewAlbumSaveBtn);
            _Queries.Add("uploadPhoto", UploadPhoto);
            _Queries.Add("clickPromptEditAlbumBtn", ClickPromptEditAlbumBtn);
            _Queries.Add("clickPromptRemoveAlbumBtn", ClickPromptRemoveAlbumBtn);
            _Queries.Add("clickPromptEditPhotoBtn", ClickPromptEditPhotoBtn);
            _Queries.Add("clickPromptRemovePhotoBtn", ClickPromptRemovePhotoBtn);
            _Queries.Add("clickLikeBtn", ClickLikeBtn);
        }

        public static IDictionary<string, Func<HttpRequestBase, AjaxResponse>> Queries
        {
            get { return _Queries; }
        }

        private static AjaxResponse ClickNewAlbumSaveBtn(HttpRequestBase request)
        {
            var userIdStr = request["userid"];
            var name = request["name"];

            if (string.IsNullOrEmpty(userIdStr) || string.IsNullOrEmpty(name))
            {
                return new AjaxResponse($"Invalid request: null values of {nameof(userIdStr)}, {nameof(name)}");
            }

            int userId;

            try
            {
                userId = Convert.ToInt32(userIdStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            Album album = new Album(name, DateTime.Now, userId);

            try
            {
                LogicProvider.AlbumLogic.AddAlbum(album);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            return new AjaxResponse(null, album.Id);
        }

        private static AjaxResponse ClickPromptEditAlbumBtn(HttpRequestBase request)
        {
            var userIdStr = request.Cookies["userid"].Value;
            var albumIdStr = request["albumid"];
            var name = request["name"];

            if (string.IsNullOrWhiteSpace(name))
            {
                return new AjaxResponse($"Values {nameof(name)} mustn't be empty");
            }

            if (string.IsNullOrWhiteSpace(userIdStr))
            {
                return new AjaxResponse($"Please re-login or allow cookies in your browser");
            }

            int albumId = 0, userId = 0;

            try
            {
                albumId = Convert.ToInt32(albumIdStr);
                userId = Convert.ToInt32(userIdStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            Album album = null;

            try
            {
                album = LogicProvider.AlbumLogic.GetAlbumById(albumId);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            if (album == null)
            {
                return new AjaxResponse($"Album: {albumIdStr} hasn't found");
            }

            //Security. Is UserId owner of AlbumId
            if (album.UserId != userId)
            {
                return new AjaxResponse("You don't have permissions for this operation");
            }

            album.Name = name;
            var result = false;

            try
            {
                result = LogicProvider.AlbumLogic.InsertAlbum(album);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            return new AjaxResponse(null, result);
        }

        private static AjaxResponse ClickPromptEditPhotoBtn(HttpRequestBase request)
        {
            var userIdStr = request.Cookies["userid"].Value;
            var photoIdStr = request["photoid"];
            var name = request["name"];

            if (string.IsNullOrWhiteSpace(name))
            {
                return new AjaxResponse($"Values {nameof(name)} mustn't be empty");
            }

            if (string.IsNullOrWhiteSpace(userIdStr))
            {
                return new AjaxResponse($"Please re-login or allow cookies in your browser");
            }

            int photoId = 0, userId = 0;

            try
            {
                photoId = Convert.ToInt32(photoIdStr);
                userId = Convert.ToInt32(userIdStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            Photo photo = null;

            try
            {
                photo = LogicProvider.PhotoLogic.GetPhotoById(photoId);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            if (photo == null)
            {
                return new AjaxResponse($"Photo: {photoIdStr} hasn't found");
            }

            //Security. Is UserId owner of PhotoId
            if (photo.UserId != userId)
            {
                return new AjaxResponse("You don't have permissions for this operation");
            }

            photo.Name = name;
            var result = false;

            try
            {
                result = LogicProvider.PhotoLogic.InsertPhoto(photo);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            return new AjaxResponse(null, result);
        }

        private static AjaxResponse ClickPromptRemoveAlbumBtn(HttpRequestBase request)
        {
            var userIdStr = request.Cookies["userid"].Value;
            var albumIdStr = request["albumid"];

            int albumId = 0, userId = 0;

            try
            {
                albumId = Convert.ToInt32(albumIdStr);
                userId = Convert.ToInt32(userIdStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            Album album = null;

            try
            {
                album = LogicProvider.AlbumLogic.GetAlbumById(albumId);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            if (album == null)
            {
                return new AjaxResponse($"Album: {albumIdStr} hasn't found");
            }

            //Security. Is UserId owner of AlbumId
            if (album.UserId != userId)
            {
                return new AjaxResponse("You don't have permissions for this operation");
            }

            var result = false;

            try
            {
                result = LogicProvider.AlbumLogic.RemoveAlbum(albumId);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            return new AjaxResponse(null, result);
        }

        private static AjaxResponse ClickPromptRemovePhotoBtn(HttpRequestBase request)
        {
            var userIdStr = request.Cookies["userid"].Value;
            var photoIdStr = request["photoid"];

            int photoId = 0, userId = 0;

            try
            {
                photoId = Convert.ToInt32(photoIdStr);
                userId = Convert.ToInt32(userIdStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            Photo photo = null;

            try
            {
                photo = LogicProvider.PhotoLogic.GetPhotoById(photoId);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            if (photo == null)
            {
                return new AjaxResponse($"Photo: {photoIdStr} hasn't found");
            }

            //Security. Is UserId owner of PhotoId
            if (photo.UserId != userId)
            {
                return new AjaxResponse("You don't have permissions for this operation");
            }

            var result = false;

            try
            {
                result = LogicProvider.PhotoLogic.RemovePhoto(photoId);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            return new AjaxResponse(null, result);
        }

        private static AjaxResponse UploadPhoto(HttpRequestBase request)
        {
            var userIdStr = request.Cookies["userid"].Value;
            var name = request["name"];
            var albumIdStr = request["albumid"];
            var sizeStr = request["size"];
            WebImage image = null;
            try
            {
                image = WebImage.GetImageFromRequest();
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            if (image == null)
            {
                return new AjaxResponse("Incorrect format of an image");
            }

            var imageArray = image.GetBytes();
            var mime = "image/" + image.ImageFormat;

            if (imageArray == null || !imageArray.Any() || string.IsNullOrEmpty(mime))
            {
                return new AjaxResponse("Incorrect format of the image");
            }

            int albumId = 0, size = 0, userId;

            try
            {
                albumId = Convert.ToInt32(albumIdStr);
                userId = Convert.ToInt32(userIdStr);
                size = Convert.ToInt32(sizeStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            var result = false;
            var photo = new Photo(name, albumId, size, mime, DateTime.Now, userId);

            try
            {
                result = LogicProvider.PhotoLogic.AddPhoto(photo, imageArray);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            return new AjaxResponse(null, result);
        }

        private static AjaxResponse ClickLikeBtn(HttpRequestBase request)
        {
            var userIdStr = request.Cookies["userid"].Value;
            var photoIdStr = request["photoid"];

            int photoId = 0, userId = 0;

            try
            {
                photoId = Convert.ToInt32(photoIdStr);
                userId = Convert.ToInt32(userIdStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            Like like = null;

            try
            {
                like = LogicProvider.LikeLogic.GetLikeByUserIdAndPhotoId(userId, photoId);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            if (like != null)
            {
                return new AjaxResponse("You allready liked this photo");
            }

            try
            {
                LogicProvider.LikeLogic.AddLike(new Like(photoId, userId, DateTime.Now));
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            var result = -1;

            try
            {
                result = LogicProvider.LikeLogic.GetLikesCount(photoId);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            return new AjaxResponse(null, result);
        }
    }
}