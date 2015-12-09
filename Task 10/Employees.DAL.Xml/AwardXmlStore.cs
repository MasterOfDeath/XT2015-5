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

    public class AwardXmlStore : IAwardStore
    {
        private const string TableName = "award";
        private const string FId = "id";
        private const string FTitle = "title";
        private const string FOwner = "owner";

        private string pathAwardXml = ConfigurationManager.AppSettings["pathAwardXml"];
        private string dirAwardAvatars = ConfigurationManager.AppSettings["dirAwardAvatars"];
        private XDocument document;

        public AwardXmlStore()
        {
            if (!File.Exists(this.pathAwardXml))
            {
                new XDocument(new XElement("head"))
                    .Save(this.pathAwardXml);
            }

            this.document = XDocument.Load(this.pathAwardXml);
        }

        public int AddAward(Award award)
        {
            var elements = this.document
                .Root
                .Elements(TableName);

            int maxId = award.Id;

            if (award.Id == 0)
            {
                maxId = (!elements.Any()) ? 0 : elements.Max(t => (int)t.Attribute(FId));

                XElement awardElement = new XElement(
                    TableName,
                    new XAttribute(FId, ++maxId),
                    new XElement(FTitle, award.Title));

                this.document.Root.Add(awardElement);
            }
            else
            {
                elements = elements.Where(el => (int)el.Attribute(FId) == award.Id);

                if (!elements.Any())
                {
                    return 0;
                }
                
                elements.First().SetElementValue(FTitle, award.Title);
            }
            
            this.document.Save(this.pathAwardXml);

            return maxId;
        }

        public ICollection<Award> ListAllAwards()
        {
            return this.document
                .Root
                .Elements(TableName)
                .Select(el => this.ElementToAward(el))
                .ToList();
        }

        public Award GetAwardByTitle(string titleStr)
        {
            var els = this.document
                .Root
                .Elements(TableName)
                .Select(el => this.ElementToAward(el))
                .Where(award => award.Title.ToLower() == titleStr.ToLower());

            if (els.Count() == 0)
            {
                return null;
            }

            return els.First();
        }

        public Award GetAwardById(int id)
        {
            var els = this.document
                .Root
                .Elements(TableName)
                .Select(el => this.ElementToAward(el))
                .Where(award => award.Id == id);

            if (els.Count() == 0)
            {
                return null;
            }

            return els.First();
        }

        public IEnumerable<Award> ListAwardsByUserId(int userId)
        {
            var els = this.document
                .Root
                .Elements(TableName)
                .Where(awEls => awEls.Elements(FOwner).Select(el => (int)el).Contains(userId))
                .Select(el => this.ElementToAward(el));

            return els;
        }

        public bool PresentAward(int userId, int awardId)
        {
            var elements = this.document
                .Root
                .Elements(TableName)
                .Where(el => (int)el.Attribute(FId) == awardId);

            if (elements.Count() == 0)
            {
                return false;
            }

            elements.First().Add(new XElement(FOwner, userId));
            this.document.Save(this.pathAwardXml);

            return true;
        }

        public bool PullOffAward(int userId, int awardId)
        {
            var awardEls = this.document
                .Root
                .Elements(TableName)
                .Where(el => (int)el.Attribute(FId) == awardId);

            if (awardEls.Count() == 0)
            {
                return false;
            }

            var userEls = awardEls
                .First()
                .Elements(FOwner)
                .Where(el => (int)el == userId);

            if (userEls.Count() == 0)
            {
                return false;
            }

            userEls.First().Remove();
            this.document.Save(this.pathAwardXml);

            return true;
        }

        public bool DeleteAward(int awardId)
        {
            IEnumerable<XElement> elements = this.document
                .Root
                .Elements(TableName)
                .Where(el => (int)el.Attribute(FId) == awardId);

            if (!elements.Any())
            {
                return false;
            }

            elements.First().Remove();
            this.document.Save(this.pathAwardXml);

            this.RemoveAvatars(awardId);

            return true;
        }

        public bool SaveAvatar(int awardId, byte[] imageArray, string imageType)
        {
            var dirSepar = Path.DirectorySeparatorChar;

            File.WriteAllBytes(
                dirAwardAvatars + dirSepar + awardId.ToString(), imageArray);

            File.WriteAllText(
                dirAwardAvatars + dirSepar + awardId.ToString() + ".mime", imageType);

            return true;
        }

        public Tuple<byte[], string> GetAvatar(int awardId)
        {
            var dirSepar = Path.DirectorySeparatorChar;
            var imgFile = new FileInfo(dirAwardAvatars + dirSepar + awardId.ToString());
            var mimeFile = new FileInfo(dirAwardAvatars + dirSepar + awardId.ToString() + ".mime");

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

        private Award ElementToAward(XElement element)
        {
            int id = (int)element.Attribute(FId);
            string title = (string)element.Element(FTitle);

            return new Award(id, title);
        }

        private void RemoveAvatars(int awardId)
        {
            var dirSepar = Path.DirectorySeparatorChar;
            var imgFile = new FileInfo(dirAwardAvatars + dirSepar + awardId.ToString());
            var mimeFile = new FileInfo(dirAwardAvatars + dirSepar + awardId.ToString() + ".mime");

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
