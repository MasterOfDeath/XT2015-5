namespace Employees.BLL.Main
{
    using System;
    using System.Configuration;
    using Employees.BLL.Contract;
    
    public class UserMainLogicCreator : IUserLogicCreator
    {
        private static readonly string AssamblyName = ConfigurationManager.AppSettings["LogicAssamblyName"];
        private static readonly string ClassName = ConfigurationManager.AppSettings["LogicClassName"];

        public UserMainLogicCreator()
        {
            if (string.IsNullOrWhiteSpace(AssamblyName) || string.IsNullOrWhiteSpace(ClassName))
            {
                throw new ArgumentException("Config file has to have AssamblyName and ClassName");
            }
        }
        
        public IUserLogic CreateInstance()
        {  
            Type type = Type.GetType(ClassName + ", " + AssamblyName);

            if (type == null)
            {
                throw new ArgumentException("Incorrect values of AssamblyName and ClassName in config file");
            }

            return (IUserLogic)Activator.CreateInstance(type);
        }
    }
}
