namespace Photos.BLL.Contract
{
    using Entites;

    public interface ILikeLogic
    {
        bool AddLike(Like like);

        int GetLikesCount(int photoId);
    }
}
