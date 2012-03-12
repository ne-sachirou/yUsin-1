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
using System.Collections;
using System.Collections.Generic;

namespace _20120127_1
{
    sealed public class SessionsData : DatasData
    {
        private static int ColumnNumber_SessionID = 0;
        private static int ColumnNumber_UserID = 1;
        private static int ColumnNumber_ExpirationDate = 2;

        public SessionsData(string currentPath)
            : base(currentPath)
        {
            DatasFileName = "sessions.csv";
        }

        protected override Dictionary<string, string> GenerateDictionary(string[] data)
        {
            return new Dictionary<string, string>
            {
                {"sessionID", data[ColumnNumber_SessionID]},
                {"userID", data[ColumnNumber_UserID]},
                {"expirationDate", data[ColumnNumber_ExpirationDate]}
            };
        }

        public Dictionary<string, string> FindSessionID(string sessionID)
        {
            return this.FindRecord(sessionID, ColumnNumber_SessionID);
        }

        public Dictionary<string, string> FindUserID(string userID)
        {
            return this.FindRecord(userID, ColumnNumber_UserID);
        }

        public bool IsValidSession(string sessionID, string userID)
        {
            var session = this.FindSessionID(sessionID);
            string storedSessionID;
            session.TryGetValue("userID", out storedSessionID);
            return storedSessionID != null &&
                session["userID"] == userID &&
                DateTime.Parse(session["expirationDate"]) - DateTime.Now > new TimeSpan(0);
        }

        public string[] FindExpiratedSessions()
        {
            List<string> sessions = new List<string>();
            foreach (var record in this)
            {
                if (DateTime.Parse(record["expirationDate"]) - DateTime.Now > new TimeSpan(0))
                {
                    sessions.Add(record["sessionID"]);
                }
            }
            return sessions.ToArray();
        }

        public void AddSession(string userID)
        {
            AddRecord(new string[]
            {
                System.Guid.NewGuid().ToString(),
                userID,
                (DateTime.Now + new TimeSpan(0, 30, 0)).ToString()
            });
        }

        public void AddSession(string userID, string sessionID)
        {
            AddRecord(new string[]
            {
                sessionID,
                userID,
                (DateTime.Now + new TimeSpan(0, 30, 0)).ToString()
            });
        }

        public void RemoveSession(string sessionID)
        {
            RemoveRecord(sessionID, ColumnNumber_SessionID);
        }

        public void RemoveUserIDSessions(string userID)
        {
            RemoveRecords(delegate(string[] record)
            {
                return record[ColumnNumber_UserID] == userID;
            });
        }

        public void RemoveExpiratedSessois()
        {
            RemoveRecords(delegate(string[] record)
            {
                return DateTime.Parse(record[ColumnNumber_ExpirationDate]) - DateTime.Now > new TimeSpan(0);
            });
        }
    }
}
