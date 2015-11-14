namespace Employees.DAL.Xml
{
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

            int maxId = (!elements.Any()) ? 0 : elements.Max(t => (int)t.Attribute(FId));

            XElement awardElement = new XElement(
                TableName,
                new XAttribute(FId, ++maxId),
                new XElement(FTitle, award.Title));

            this.document.Root.Add(awardElement);
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

        private Award ElementToAward(XElement element)
        {
            int id = (int)element.Attribute(FId);
            string title = (string)element.Element(FTitle);

            return new Award(id, title);
        }
    }
}
