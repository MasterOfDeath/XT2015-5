namespace Photos.DAL.Contract
{
    using Entites;

    public interface ILikeStore
    {
        bool AddLike(Like like);

        int GetLikesCount(int photoId);
    }
}
