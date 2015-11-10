﻿namespace Employees.DAL.Xml
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
        private IAwardStore awardStore;
        private string pathUserXml = ConfigurationManager.AppSettings["pathUserXml"];
        
        public UserXmlStore()
        {
            if (!File.Exists(this.pathUserXml))
            {
                new XDocument(new XElement("head"))
                    .Save(this.pathUserXml);
            }
            
            this.document = new XDocument();
            this.document = XDocument.Load(this.pathUserXml);

            this.awardStore = AwardXmlStore.Instance;
        }

        public bool AddUser(User user)
        {
            var elements = this.document
                .Root
                .Elements(User.TableName);

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
            this.document.Save(this.pathUserXml);
            
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
            this.document.Save(this.pathUserXml);
            
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

            if (this.awardStore.Awards.Count > 0)
            {
                foreach (var el in element.Elements(User.FHasAward))
                {
                    var awardId = (int)el;

                    var award = this.awardStore.GetAwardById(awardId);

                    if (award == null)
                    {
                        throw new ArgumentException("Incorrect state of data source");
                    }
                    else
                    {
                        user.AddAward(award);
                    }
                }
            }
            
            return user;
        }

        public User GetUserById(int userId)
        {
            var elements = this.ListAllUsers().Where(user => user.Id == userId);

            if (elements.Count() == 0)
            {
                return null;
            }

            return elements.First();
        }

        public bool RewardUser(User user, Award award)
        {
            var elements = this.document
                .Root
                .Elements(User.TableName)
                .Where(el => (int)el.Attribute(User.FId) == user.Id);

            if (elements.Count() == 0)
            {
                return false;
            }

            elements.First().Add(new XElement(User.FHasAward, award.Id));
            this.document.Save(this.pathUserXml);

            return true;
        }

        public bool PullOffAward(User user, Award award)
        {
            var userEls = this.document
                .Root
                .Elements(User.TableName)
                .Where(el => (int)el.Attribute(User.FId) == user.Id);

            if (userEls.Count() == 0)
            {
                return false;
            }

            var awardEls = userEls
                .First()
                .Elements(User.FHasAward)
                .Where(el => Convert.ToInt32(el.Value) == award.Id);

            if (awardEls.Count() == 0)
            {
                return false;
            }

            awardEls.First().Remove();
            this.document.Save(this.pathUserXml);

            return true;
        }
    }
}