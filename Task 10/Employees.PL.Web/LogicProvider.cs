namespace Employees.PL.Web
{
    using System;
    using System.Collections.Generic;
    using Employees.BLL.Contract;
    using Employees.Entites;

    public sealed class LogicProvider
    {
        static readonly LogicProvider _instance = new LogicProvider();
        public static LogicProvider Instance
        {
            get { return _instance; }
        }

        public IUserLogic UserLogic { get; } = new BLL.Main.UserMainLogic();
        public IAwardLogic AwardLogic { get; } = new BLL.Main.AwardMainLogic();

        public string clickSaveBtn(string name, string dateStr)
        {
            var bDay = DateTime.Parse(dateStr);
            string result = string.Empty;

            try
            {
                UserLogic.AddUser(new User(name, bDay));
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }


            return result;
        }
    }
}