namespace Employees.BLL.Main
{
    using System;
    using System.Configuration;
    using Employees.DAL.Contract;
    using Employees.DAL.MSSql;
    using Employees.DAL.Xml;

    internal class Stores
    {
        private static readonly string DALImplementation =
            ConfigurationManager.AppSettings["DALImplementation"];

        static Stores()
        {
            switch (DALImplementation)
            {
                case "MSSql":
                    UserStore = new UserSqlStore();
                    AwardStore = new AwardSqlStore();
                    AuthStore = new AuthSqlStore();
                    break;

                case "Xml":
                    UserStore = new UserXmlStore();
                    AwardStore = new AwardXmlStore();
                    AuthStore = new AuthXmlStore();
                    break;

                default:
                    throw new ArgumentException("Incorrect file configuration");
            }
        }
        
        public static IUserStore UserStore { get; }

        public static IAwardStore AwardStore { get; }

        public static IAuthStore AuthStore { get; }
    }
}
