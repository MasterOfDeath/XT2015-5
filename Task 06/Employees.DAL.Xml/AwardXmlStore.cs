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
        private string pathAwardXml = ConfigurationManager.AppSettings["pathAwardXml"];
        private XDocument document;

        private AwardXmlStore()
        {
            if (!File.Exists(this.pathAwardXml))
            {
                new XDocument(new XElement("head"))
                    .Save(this.pathAwardXml);
            }

            this.document = new XDocument();
            this.document = XDocument.Load(this.pathAwardXml);
        }

        public static AwardXmlStore Instance { get; } = new AwardXmlStore();

        public int AddAward(Award award)
        {
            var elements = this.document
                .Root
                .Elements(Award.TableName);

            int maxId = (!elements.Any()) ? 0 : elements.Max(t => (int)t.Attribute(Award.FId));

            XElement awardElement = new XElement(
                Award.TableName,
                new XAttribute(Award.FId, ++maxId),
                new XElement(Award.FTitle, award.Title));

            this.document.Root.Add(awardElement);
            this.document.Save(this.pathAwardXml);

            return maxId;
        }

        public ICollection<Award> ListAllAwards()
        {
            return this.document
                .Root
                .Elements(Award.TableName)
                .Select(el => this.ElementToAward(el))
                .ToList();
        }

        public Award GetAwardByTitle(string titleStr)
        {
            var els = this.document
                .Root
                .Elements(Award.TableName)
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
                .Elements(Award.TableName)
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
                .Elements(Award.TableName)
                .Where(awEls => awEls.Elements(Award.FOwner).Select(el => (int)el).Contains(userId))
                .Select(el => this.ElementToAward(el));

            return els;
        }

        public bool PresentAward(int userId, int awardId)
        {
            var elements = this.document
                .Root
                .Elements(Award.TableName)
                .Where(el => (int)el.Attribute(Award.FId) == awardId);

            if (elements.Count() == 0)
            {
                return false;
            }

            elements.First().Add(new XElement(Award.FOwner, userId));
            this.document.Save(this.pathAwardXml);

            return true;
        }

        public bool PullOffAward(int userId, int awardId)
        {
            var awardEls = this.document
                .Root
                .Elements(Award.TableName)
                .Where(el => (int)el.Attribute(Award.FId) == awardId);

            if (awardEls.Count() == 0)
            {
                return false;
            }

            var userEls = awardEls
                .First()
                .Elements(Award.FOwner)
                .Where(el => (int)el == userId);

            if (userEls.Count() == 0)
            {
                return false;
            }

            userEls.First().Remove();
            this.document.Save(this.pathAwardXml);

            return true;
        }

        internal void RemoveUserIdElements(int userId)
        {
            var elements = this.document
                .Root
                .Elements(Award.TableName)
                .SelectMany(awEls => awEls.Elements(Award.FOwner).Where(el => (int)el == userId));

            if (elements.Any())
            {
                elements.Remove();
                this.document.Save(this.pathAwardXml);
            }
        }

        private Award ElementToAward(XElement element)
        {
            int id = (int)element.Attribute(Award.FId);
            string title = (string)element.Element(Award.FTitle);

            return new Award(id, title);
        }
    }
}
