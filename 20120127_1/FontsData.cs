using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml;

namespace _20120127_1
{
    public class FontsData : DatasData
    {
        private static int ColumnNumber_FontID = 0;
        private static int ColumnNumber_FontName = 1;
        private static int ColumnNumber_FontProfile = 2;

        public FontsData(string currentPath)
            : base(currentPath)
        {
            DatasFileName = "fonts.csv";
        }

        public string FontsDir
        {
            get
            {
                return CurrentPath + "\\fonts\\";
            }
        }

        protected override Dictionary<string, string> GenerateDictionary(string[] data)
        {
            return new Dictionary<string, string>
            {
                {"fontID", data[ColumnNumber_FontID]},
                {"fontName", data[ColumnNumber_FontName]},
                {"fontProfile", UnescapeForCsv(data[ColumnNumber_FontProfile])}
            };
        }

        public Dictionary<string, string> FindFontID(string fontID)
        {
            return FindRecord(fontID, ColumnNumber_FontID);
        }

        public Dictionary<string, string> Charactors(string fontID)
        {
            Dictionary<string, string> font = FindFontID(fontID);
            XmlDocument fontXml = new XmlDocument();
            fontXml.Load(FontsDir + fontID + ".xml");
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (XmlElement charactorNode in fontXml.GetElementsByTagName("charactor"))
            {
                result[charactorNode.GetAttribute("unicode")] = charactorNode.GetAttribute("imageID");
            }
            return result;
        }

        /// <summary>
        /// Create font data as an XmlDocument Object.
        /// Xml structure is following.
        /// %font
        ///   %fontName Font name
        ///   %fontProfile Font profile
        ///   %charactors
        ///     %charactor{:unicode, :imageID}+
        /// </summary>
        /// <param name="fontName">Short font name.</param>
        /// <param name="fontProfile">Long font description.</param>
        /// <param name="charactors">Correspondence of {:unicode=>:imageID}.</param>
        /// <returns>Created font XmlDocument Object.</returns>
        private XmlDocument CreateFontXml(string fontName, string fontProfile, Dictionary<string, string> charactors)
        {
            /*
             * <?xml version="1.0" encoding="UTF-8"?>
               <font>
                 <fontName>Example Font</fontName>
                 <fontProfile>This is an example font data.</fontProfile>
                 <charactors>
                   <charactor imageID="some guid" unicode="E" />
                   <charactor imageID="some guid" unicode="x" />
                 </charactors>
               </font>
             */
            XmlDocument fontXml = new XmlDocument();
            XmlElement fontNode = fontXml.CreateElement("font");

            XmlElement fontNameNode = fontXml.CreateElement("fontName");
            fontNameNode.AppendChild(fontXml.CreateTextNode(fontName));
            fontNode.AppendChild(fontNameNode);

            XmlElement fontProfileNode = fontXml.CreateElement("fontProfile");
            fontProfileNode.AppendChild(fontXml.CreateTextNode(fontProfile));
            fontNode.AppendChild(fontProfileNode);

            XmlElement charactorsNode = fontXml.CreateElement("charactors");
            foreach (KeyValuePair<string, string> charactor in charactors)
            {
                XmlElement charactorNode = fontXml.CreateElement("charactor");
                charactorNode.SetAttribute("unicode", charactor.Key);
                charactorNode.SetAttribute("imageID", charactor.Value);
                charactorsNode.AppendChild(charactorNode);
            }
            fontNode.AppendChild(charactorsNode);

            fontXml.AppendChild(fontNode);
            return fontXml;
        }

        public string AddFont(string fontName, string fontProfile, Dictionary<string, string> charactors)
        {
            XmlDocument fontXml = CreateFontXml(fontName, fontProfile, charactors);
            string fontID = System.Guid.NewGuid().ToString();
            fontXml.Save(FontsDir + fontID + ".xml");
            AddRecord(new string[]
            {
                fontID,
                EscapeForCsv(fontName),
                EscapeForCsv(fontProfile)
            });
            return fontID;
        }

        public void RemoveFont(string fontID)
        {
            System.IO.File.Delete(FontsDir + fontID + ".xml");
            RemoveRecord(fontID, ColumnNumber_FontID);
        }

        public void EditFont(string fontID, string fontName, string fontProfile, Dictionary<string, string> charactors)
        {
            RemoveFont(fontID);
            XmlDocument fontXml = CreateFontXml(fontName, fontProfile, charactors);
            fontXml.Save(FontsDir + fontID + ".xml");
            AddRecord(new string[]
            {
                fontID,
                EscapeForCsv(fontName),
                EscapeForCsv(fontProfile)
            });
        }
    }
}
