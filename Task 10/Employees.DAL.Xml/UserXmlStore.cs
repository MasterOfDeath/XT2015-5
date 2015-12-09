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
        private const string TableName = "user";
        private const string FId = "id";
        private const string FName = "name";
        private const string FBirthDay = "birth_day";
        private const string AwardTableName = "award";
        private const string FOwner = "owner";

        private XDocument document;
        private string pathUserXml = ConfigurationManager.AppSettings["pathUserXml"];
        private string dirUserAvatars = ConfigurationManager.AppSettings["dirUserAvatars"];
        private string pathAwardXml = ConfigurationManager.AppSettings["pathAwardXml"];

        public UserXmlStore()
        {
            if (!File.Exists(this.pathUserXml))
            {
                new XDocument(new XElement("head"))
                    .Save(this.pathUserXml);
            }
            
            this.document = XDocument.Load(this.pathUserXml);
        }

        public bool AddUser(User user)
        {
            if (user.Id < 0)
            {
                return false;
            }

            var elements = this.document
                .Root
                .Elements(TableName);

            if (user.Id == 0)
            {
                int maxId = (!elements.Any()) ? 0 : elements.Max(t => (int)t.Attribute(FId));
                XElement userElement = new XElement(
                    TableName,
                    new XAttribute(FId, ++maxId),
                    new XElement(FName, user.Name),
                    new XElement(FBirthDay, user.BirthDay));

                this.document.Root.Add(userElement);
            }
            else
            {
                elements = elements.Where(el => (int)el.Attribute(FId) == user.Id);

                if (!elements.Any())
                {
                    return false;
                }

                elements.First().SetElementValue(FName, user.Name);
                elements.First().SetElementValue(FBirthDay, user.BirthDay);
            }

            this.document.Save(this.pathUserXml);
            
            return true;
        }
        
        public bool DeleteUser(User user)
        {
            IEnumerable<XElement> elements = this.document
                .Root
                .Elements(TableName)
                .Where(el => (int)el.Attribute(FId) == user.Id);

            if (!elements.Any())
            {
                return false;
            }
            
            elements.First().Remove();
            this.document.Save(this.pathUserXml);

            // Remove dependences in awards
            this.RemoveUserIdElements(XDocument.Load(this.pathAwardXml), user.Id);

            this.RemoveAvatars(user.Id);
            
            return true;
        }

        public IEnumerable<User> ListAllUsers()
        {
            return this.document
                .Root
                .Elements(TableName)
                .Select(el => this.ElementToUser(el))
                .ToList();
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

        public IEnumerable<User> ListUsersByAwardId(int awardId)
        {
            var awardDoc = XDocument.Load(this.pathAwardXml);
            var element = awardDoc
                .Root
                .Elements(AwardTableName)
                .First(el => (int)el.Attribute(FId) == awardId);

            var users = element.Elements(FOwner).Select(el => this.GetUserById((int)el));

            return users.ToList();
        }

        public bool SaveAvatar(int userId, byte[] imageArray, string imageType)
        {
            var dirSepar = Path.DirectorySeparatorChar;

            File.WriteAllBytes(
                dirUserAvatars + dirSepar + userId.ToString(), imageArray);

            File.WriteAllText(
                dirUserAvatars + dirSepar + userId.ToString() + ".mime", imageType);

            return true;
        }

        public Tuple<byte[], string> GetAvatar(int userId)
        {
            var dirSepar = Path.DirectorySeparatorChar;
            var imgFile = new FileInfo(dirUserAvatars + dirSepar + userId.ToString());
            var mimeFile = new FileInfo(dirUserAvatars + dirSepar + userId.ToString() + ".mime");

            if (imgFile.Attributes.HasFlag(FileAttributes.Directory) ||
                mimeFile.Attributes.HasFlag(FileAttributes.Directory) ||
                !imgFile.Exists ||
                !mimeFile.Exists)
            {
                return null;
            }

            var imageType = File.ReadAllText(mimeFile.FullName);
            var imageArray = File.ReadAllBytes(imgFile.FullName);

            return new Tuple<byte[], string>(imageArray, imageType);
        }

        private User ElementToUser(XElement element)
        {
            int id = (int)element.Attribute(FId);
            string name = (string)element.Element(FName);
            DateTime birthDay = Convert.ToDateTime((string)element.Element(FBirthDay));

            User user = new User(id, name, birthDay);

            return user;
        }

        private void RemoveUserIdElements(XDocument document, int userId)
        {
            var elements = document
                .Root
                .Elements(AwardTableName)
                .SelectMany(awEls => awEls.Elements(FOwner).Where(el => (int)el == userId));

            if (elements.Any())
            {
                elements.Remove();
                document.Save(this.pathAwardXml);
            }
        }

        private void RemoveAvatars(int userId)
        {
            var dirSepar = Path.DirectorySeparatorChar;
            var imgFile = new FileInfo(dirUserAvatars + dirSepar + userId.ToString());
            var mimeFile = new FileInfo(dirUserAvatars + dirSepar + userId.ToString() + ".mime");

            if (!imgFile.Attributes.HasFlag(FileAttributes.Directory) ||
                imgFile.Exists)
            {
                imgFile.Delete();
            }

            if (!imgFile.Attributes.HasFlag(FileAttributes.Directory) ||
                mimeFile.Exists)
            {
                mimeFile.Delete();
            }
        }
    }
}
