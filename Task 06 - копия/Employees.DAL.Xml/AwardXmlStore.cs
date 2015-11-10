
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

    class AwardXmlStore : IAwardStore
    {
        private string pathXml = ConfigurationManager.AppSettings["pathAwardXml"];
        private IDictionary<int, string> awards;
        private XDocument document;

        public AwardXmlStore()
        {
            if (!File.Exists(this.pathXml))
            {
                new XDocument(new XElement("head"))
                    .Save(this.pathXml);
            }

            this.document = new XDocument();
            this.document = XDocument.Load(this.pathXml);

            this.awards = this.ListAllAwards();
        }

        public IDictionary<int, string> Awards
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
            throw new NotImplementedException();
        }

        public IDictionary<int, string> ListAllAwards()
        {
            var resultDic = new Dictionary<int, string>();

            IEnumerable<XElement> elements = this.document.Root.Elements(Award.TableName);

            if (elements != null)
            {
                foreach (var el in elements)
                {
                    var key = (int)el.Attribute(Award.FId);
                    var value = (string)el.Element(Award.FTitle);
                    resultDic.Add(key, value);
                }
            }

            return resultDic;
        }
    }
}
