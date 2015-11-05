namespace Employees.DAL.Xml
{
    using System;
    using System.Configuration;
    using Employees.DAL.Contract;
    
    public class UserXmlStoreCreator : IUserStoreCreator
    {
        private static readonly string AssamblyName = ConfigurationManager.AppSettings["StoreAssamblyName"];
        private static readonly string ClassName = ConfigurationManager.AppSettings["StoreClassName"];

        public UserXmlStoreCreator()
        {
            if (string.IsNullOrWhiteSpace(AssamblyName) || string.IsNullOrWhiteSpace(ClassName))
            {
                throw new ArgumentException("Config file has to have AssamblyName and ClassName");
            }
        }

        public IUserStore CreateInstance()
        {
            Type type = Type.GetType(ClassName + ", " + AssamblyName);

            if (type == null)
            {
                throw new ArgumentException("Incorrect values of AssamblyName and ClassName in config file");
            }

            return (IUserStore)Activator.CreateInstance(type);
        }
    }
}
