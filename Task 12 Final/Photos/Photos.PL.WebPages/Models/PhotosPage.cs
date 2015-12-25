namespace Photos.PL.WebPages.Models
{
    using System;

    public static class PhotosPage
    {
        public static int GetLikesCount(int photoId)
        {
            int result = -1;
            try
            {
                result = LogicProvider.LikeLogic.GetLikesCount(photoId);
            }
            catch (Exception)
            {
                // TODO to log
            }

            return result;
        }

        
    }
}