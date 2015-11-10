namespace Employees.DAL.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Employees.DAL.Contract;
    using Entites;

    public class AwardXmlStore : IAwardStore
    {
        private string pathAwardXml = ConfigurationManager.AppSettings["pathAwardXml"];
        internal ICollection<Award> awards;
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

            this.awards = this.ListAllAwards();
        }

        public static AwardXmlStore Instance { get; } = new AwardXmlStore();

        public ICollection<Award> Awards
        {
            get
            {
                return this.awards;
            }

            private set
            {
                this.awards = value;
            }
        }

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

            // Refresh public Awards list
            this.Awards = this.ListAllAwards();

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
            IEnumerable<Award> aw = this.awards
                .Where(award => award.Title.ToLower() == titleStr.ToLower());

            if (aw.Count() == 0)
            {
                return null;
            }

            return aw.First();
        }

        public Award GetAwardById(int id)
        {
            IEnumerable<Award> aw = this.Awards.Where(award => award.Id == id);

            if (aw.Count() == 0)
            {
                return null;
            }

            return aw.First();
        }

        private Award ElementToAward(XElement element)
        {
            int id = (int)element.Attribute(Award.FId);
            string title = (string)element.Element(Award.FTitle);

            return new Award(id, title);
        }
    }
}
