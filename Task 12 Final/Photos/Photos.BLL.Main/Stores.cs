namespace Photos.BLL.Main
{
    using DAL.Contract;
    using DAL.Sql;

    internal static class Stores
    {
        static Stores()
        {
            UserStore = new UserSqlStore();
            RoleStore = new RoleSqlStore();
            PhotoStore = new PhotoSqlStore();
            AlbumStore = new AlbumSqlStore();
        }

        public static IUserStore UserStore { get; }

        public static IRoleStore RoleStore { get; }

        public static IPhotoStore PhotoStore { get; }

        public static IAlbumStore AlbumStore { get; }
    }
}
