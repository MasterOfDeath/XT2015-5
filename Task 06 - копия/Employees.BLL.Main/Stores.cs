namespace Employees.BLL.Main
{
    using System;
    using Employees.DAL.Contract;
    using Employees.DAL.Xml;
    
    internal class Stores
    {
        static Stores()
        {
            UserStore = new UserXmlStore();
        }
        
        public static IUserStore UserStore { get; }
    }
}
