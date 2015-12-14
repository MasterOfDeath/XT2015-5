namespace Employees.BLL.Main
{
    using System.Configuration;
    using Employees.DAL.Contract;
    using Employees.DAL.MSSql;
    using Employees.DAL.Xml;

    internal class Stores
    {
        private static readonly string StoreAssambly =
            ConfigurationManager.AppSettings["StoreAssamblyName"];

        static Stores()
        {
            if (StoreAssambly == "Employees.DAL.MSSql")
            {
                UserStore = new UserSqlStore();
                AwardStore = new AwardSqlStore();
                AuthStore = new AuthSqlStore();
            }

            if (StoreAssambly == "Employees.DAL.Xml")
            {
                UserStore = new UserXmlStore();
                AwardStore = new AwardXmlStore();
                AuthStore = new AuthXmlStore();
            }
        }
        
        public static IUserStore UserStore { get; }

        public static IAwardStore AwardStore { get; }

        public static IAuthStore AuthStore { get; }
    }
}

