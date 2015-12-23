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
            var albumIdStr = request["albumid"];
            var name = request["name"];

            if (string.IsNullOrWhiteSpace(name))
            {
                return new AjaxResponse("New name mustn't be empty");
            }

            int albumId = 0;

            try
            {
                albumId = Convert.ToInt32(albumIdStr);
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

        private static AjaxResponse ClickPromptRemoveAlbumBtn(HttpRequestBase request)
        {
            var albumIdStr = request["albumid"];

            int albumId = 0;

            try
            {
                albumId = Convert.ToInt32(albumIdStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
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

        private static AjaxResponse UploadPhoto(HttpRequestBase request)
        {
            var name = request["name"];
            var albumIdStr = request["albumid"];
            var sizeStr = request["size"];
            var image = WebImage.GetImageFromRequest();

            if (image == null)
            {
                return new AjaxResponse("Incorrect format of the image");
            }

            var imageArray = image.GetBytes();
            var mime = "image/" + image.ImageFormat;

            if (imageArray == null || !imageArray.Any() || string.IsNullOrEmpty(mime))
            {
                return new AjaxResponse("Incorrect format of the image");
            }

            int albumId = 0, size = 0;

            try
            {
                albumId = Convert.ToInt32(albumIdStr);
                size = Convert.ToInt32(sizeStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            var result = false;
            var photo = new Photo(name, albumId, size, mime, DateTime.Now);

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
    }
}