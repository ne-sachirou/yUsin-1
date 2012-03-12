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

namespace _20120127_1
{
    public class SentencesData : DatasData
    {
        private static int ColumnNumber_SentenceID = 0;
        private static int ColumnNumber_Sentence = 1;
        private static int ColumnNumber_FontID = 2;
        private static int ColumnNumber_UserId = 3;

        public SentencesData(string currentPath)
            : base(currentPath)
        {
            DatasFileName = "sentences.csv";
        }

        protected override Dictionary<string, string> GenerateDictionary(string[] data)
        {
            return new Dictionary<string, string>
            {
                {"sentenceID", data[ColumnNumber_SentenceID]},
                {"sentence", UnescapeForCsv(data[ColumnNumber_Sentence])},
                {"fontID", data[ColumnNumber_FontID]},
                {"userID", data[ColumnNumber_UserId]}
            };
        }

        public Dictionary<string, string> FindSentenceID(string sentenceID)
        {
            return FindRecord(sentenceID, ColumnNumber_SentenceID);
        }

        public Dictionary<string, string>[] FindFontID(string fontID)
        {
            return FindRecords(delegate(Dictionary<string, string> record)
            {
                return record["fontID"] == fontID;
            });
        }

        public Dictionary<string, string>[] FindUserID(string userID)
        {
            return FindRecords(delegate(Dictionary<string, string> record)
            {
                return record["userID"] == userID;
            });
        }

        public string AddSentence(string sentence, string fontID, string userID)
        {
            string sentenceID = System.Guid.NewGuid().ToString();
            AddRecord(new string[]
            {
                sentenceID,
                EscapeForCsv(sentence),
                fontID,
                userID
            });
            return sentenceID;
        }
    }
}
