namespace Employees.DAL.Xml
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Contract;

    public class AuthXmlStore : IAuthStore
    {
        private const string TableName = "auth";
        private const string FUserName = "username";
        private const string FRole = "role";
        private const string FHash = "hash";

        private readonly string pathAuthXml = ConfigurationManager.AppSettings["pathAuthXml"];

        private XDocument document;

        public AuthXmlStore()
        {
            if (!File.Exists(this.pathAuthXml))
            {
                new XDocument(new XElement("head"))
                    .Save(this.pathAuthXml);
            }

            this.document = XDocument.Load(this.pathAuthXml);
        }

        public ICollection<string> GetRolesForUser(string username)
        {
            var elements = this.document
                .Root
                .Elements(TableName)
                .Where(el => (string)el.Attribute(FUserName) == username);

            if (!elements.Any())
            {
                return null;
            }

            return elements.First().Elements(FRole).Select(el => (string)el).ToArray();
        }

        public bool IsUserInRole(string username, string roleName)
        {
            var elements = this.document
                .Root
                .Elements(TableName)
                .Where(el => (string)el.Attribute(FUserName) == username);

            if (!elements.Any())
            {
                return false;
            }

            return elements.First().Elements(FRole).Select(el => (string)el).Contains(roleName);
        }

        public bool GiveRole(string username, string roleName)
        {
            var elements = this.document
                .Root
                .Elements(TableName)
                .Where(el => (string)el.Attribute(FUserName) == username);

            if (!elements.Any())
            {
                return false;
            }

            XElement roleElement = new XElement(FRole, roleName);

            elements.First().Add(roleElement);
            this.document.Save(this.pathAuthXml);

            return true;
        }

        public bool RevokeRole(string username, string roleName)
        {
            var elements = this.document
                .Root
                .Elements(TableName)
                .Where(el => (string)el.Attribute(FUserName) == username);

            if (!elements.Any())
            {
                return false;
            }

            elements = elements.First().Elements(FRole).Where(el => (string)el == roleName);

            if (!elements.Any())
            {
                return false;
            }

            elements.Remove();
            this.document.Save(this.pathAuthXml);

            return true;
        }

        public ICollection<string> GetUsersInRole(string roleName)
        {
            return this.document
                .Root
                .Elements(TableName)
                .Where(uEl => uEl.Elements(FRole).Select(el => (string)el).Contains(roleName))
                .Select(el => (string)el.Attribute(FUserName))
                .ToArray();
        }

        public bool AddAuth(string username, string hash)
        {
            XElement authElement = new XElement(
                    TableName,
                    new XAttribute(FUserName, username),
                    new XElement(FHash, hash));

            this.document.Root.Add(authElement);
            this.document.Save(this.pathAuthXml);

            return true;
        }

        public string GetHashByUserName(string username)
        {
            var elements = this.document
                .Root
                .Elements(TableName)
                .Where(el => (string)el.Attribute(FUserName) == username);

            if (!elements.Any())
            {
                return null;
            }

            return (string)elements.First().Element(FHash);
        }

        public ICollection<string> GetAllUserNames()
        {
            var elements = this.document
                .Root
                .Elements(TableName);

            if (!elements.Any())
            {
                return null;
            }

            return elements.Select(el => (string)el.Attribute(FUserName)).ToArray();
        }
    }
}
