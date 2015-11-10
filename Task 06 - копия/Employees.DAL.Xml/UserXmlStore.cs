namespace Employees.DAL.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Employees.DAL.Contract;
    using Employees.Entites;
    
    public class UserXmlStore : IUserStore
    {
        private XDocument document;
        // private IDictionary<int, string> awards;
        private IAwardStore awardStore;
        private string pathXml = ConfigurationManager.AppSettings["pathUserXml"];
        
        public UserXmlStore()
        {
            if (!File.Exists(this.pathXml))
            {
                new XDocument(new XElement("head"))
                    .Save(this.pathXml);
            }
            
            this.document = new XDocument();
            this.document = XDocument.Load(this.pathXml);

            this.awardStore = new AwardXmlStore();

            //this.awards = this.awardStore.ListAllAwards();

            //var us = new User(5, "Test", DateTime.Now);
            //us.AddAward(new Award(2, "Prise 1"));
            //us.AddAward(new Award(3, "Prise 2"));
            //us.AddAward(new Award(1, "Prise 3"));

            //this.AddUser(us);
        }

        public bool AddUser(User user)
        {
            var elements = this.document.
                Root.Elements(User.TableName);

            int maxId = (!elements.Any()) ? 0 : elements.Max(t => (int)t.Attribute(User.FId));

            XElement userElement = new XElement(
                User.TableName,
                new XAttribute(User.FId, ++maxId),
                new XElement(User.FName, user.Name),
                new XElement(User.FBirthDay, user.BirthDay));

            foreach (var award in user.Awards)
            {
                userElement.Add(new XElement(User.FHasAward, award.Id));
            }
            
            this.document.Root.Add(userElement);
            this.document.Save(this.pathXml);
            
            return true;
        }
        
        public bool DeleteUser(int id)
        {
            IEnumerable<XElement> elements = this.document
                .Root
                .Elements(User.TableName)
                .Where(el => el.Attribute(User.FId).Value == id.ToString());

            if (!elements.Any())
            {
                return false;
            }
            
            elements.First().Remove();
            this.document.Save(this.pathXml);
            
            return true;
        }

        public IEnumerable<User> ListAllUsers()
        {
            return this.document
                .Root
                .Elements(User.TableName)
                .Select(el => this.ElementToUser(el))
                .ToList();
        }
        
        private User ElementToUser(XElement element)
        {
            int id = (int)element.Attribute(User.FId);
            string name = (string)element.Element(User.FName);
            DateTime birthDay = Convert.ToDateTime((string)element.Element(User.FBirthDay));

            User user = new User(id, name, birthDay);

            if (this.awardStore.Awards?.Count > 0)
            {
                foreach (var el in element.Elements(User.FHasAward))
                {
                    var awardId = (int)el;

                    if (!this.awardStore.Awards.ContainsKey(awardId))
                    {
                        throw new ArgumentException("Incorrect state of data source");
                    }
                    else
                    {
                        var awardTitle = this.awardStore.Awards[awardId];
                        user.AddAward(new Award(awardId, awardTitle));
                    }
                }
            }
            
            return user;
        }
    }
}
