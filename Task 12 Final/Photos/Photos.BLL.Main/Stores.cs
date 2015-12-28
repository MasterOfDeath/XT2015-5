namespace Photos.BLL.Main
{
    using DAL.Contract;
    using DAL.Sql;

    internal static class Stores
    {
        public static IUserStore UserStore { get; } = new UserSqlStore();

        public static IRoleStore RoleStore { get; } = new RoleSqlStore();

        public static IPhotoStore PhotoStore { get; } = new PhotoSqlStore();

        public static IAlbumStore AlbumStore { get; } = new AlbumSqlStore();

        public static ILikeStore LikeStore { get; } = new LikeSqlStore();
    }
}
