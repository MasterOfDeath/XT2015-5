namespace Photos.PL.WebPages.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Entites;

    public static class SignUpPage
    {
        public static bool AddUser(string firstname, string lastname, string username, string password)
        {
            var result = false;

            if (firstname == null || lastname == null || username == null || password == null)
            {
                throw new ArgumentException("Arguments mustn't be null");
            }

            var user = new User(firstname, lastname, username, hash: null, tariff_id: 0);

            try
            {
                result = LogicProvider.UserLogic.AddUser(user, password);
            }
            catch (Exception ex)
            {
                // TODO Fix this catch.
                throw ex;
            }

            return result;
        }
    }
}