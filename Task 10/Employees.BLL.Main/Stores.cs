namespace Employees.BLL.Main
{
    using Employees.DAL.Contract;
    using Employees.DAL.MSSql;
    using Employees.DAL.Xml;

    internal class Stores
    {
        static Stores()
        {
            UserStore = new UserSqlStore();
            AwardStore = new AwardSqlStore();
            AuthStore = new AuthXmlStore();
        }
        
        public static IUserStore UserStore { get; }

        public static IAwardStore AwardStore { get; }

        public static IAuthStore AuthStore { get; }
    }
}
