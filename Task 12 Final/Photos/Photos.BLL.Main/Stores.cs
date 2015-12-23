namespace Photos.BLL.Main
{
    using DAL.Contract;
    using DAL.Sql;

    internal static class Stores
    {
        //static Stores()
        //{
        //    UserStore = new UserSqlStore();
        //    RoleStore = new RoleSqlStore();
        //    PhotoStore = new PhotoSqlStore();
        //    AlbumStore = new AlbumSqlStore();
        //    LikeStore = new LikeSqlStore();
        //}

        public static IUserStore UserStore { get; } = new UserSqlStore();

        public static IRoleStore RoleStore { get; } = new RoleSqlStore();

        public static IPhotoStore PhotoStore { get; } = new PhotoSqlStore();

        public static IAlbumStore AlbumStore { get; } = new AlbumSqlStore();

        public static ILikeStore LikeStore { get; } = new LikeSqlStore();
    }
}
