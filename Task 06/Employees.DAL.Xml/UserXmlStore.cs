namespace Employees.DAL.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Employees.DAL.Contract;
    using Employees.Entites;
    
    public class UserXmlStore : IUserStore
    {
        private XDocument document;
        private string pathXml = ConfigurationManager.AppSettings["pathXml"];
        
        public UserXmlStore()
        {
            if (!File.Exists(this.pathXml))
            {
                new XDocument(new XElement("head"))
                    .Save(this.pathXml);
            }
            
            this.document = new XDocument();
            this.document = XDocument.Load(this.pathXml);
        }
        
        public bool Add(User user)
        {
            var elements = this.document.Root.Elements(User.TableName);
            
            int maxId = (!elements.Any()) ? 0 : elements.Max(t => Convert.ToInt32(t.Attribute(User.FId).Value));
            
            XElement userElement = new XElement(
                User.TableName,
                new XAttribute(User.FId, ++maxId),
                new XElement(User.FName, user.Name),
                new XElement(User.FBirthDay, user.BirthDay));
            
            this.document.Root.Add(userElement);
            this.document.Save(this.pathXml);
            
            return true;
        }
        
        public bool Delete(int id)
        {
            IEnumerable<XElement> elements = this.document
                .Descendants(User.TableName)
                .Where(el => el.Attribute(User.FId).Value == id.ToString());

            if (!elements.Any())
            {
                return false;
            }
            
            elements.First().Remove();
            this.document.Save(this.pathXml);
            
            return true;
        }

        public IEnumerable<User> ListAll()
        {
            return this.document
                .Descendants(User.TableName)
                .Select(el => this.ElementToUser(el))
                .ToList();
        }
        
        private User ElementToUser(XElement element)
        {
            int id = Convert.ToInt32(element.Attribute(User.FId).Value);
            string name = element.Element(User.FName).Value;
            DateTime birthDay = Convert.ToDateTime(element.Element(User.FBirthDay).Value);
            
            return new User(id, name, birthDay);
        }
    }
}
