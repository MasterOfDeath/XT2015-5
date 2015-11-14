namespace Employees.BLL.Main
{
    using Employees.DAL.Contract;
    using Employees.DAL.Xml;
    
    internal class Stores
    {
        static Stores()
        {
            UserStore = new UserXmlStore();
            AwardStore = new AwardXmlStore();
        }
        
        public static IUserStore UserStore { get; }

        public static IAwardStore AwardStore { get; }
    }
}
