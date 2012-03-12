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
    sealed public class UsersData : DatasData
    {
        private static int ColmnNumber_UserID = 0;
        private static int ColmnNumber_UserName = 1;
        private static int ColmnNumber_UserPassword = 2;

        public UsersData(string currentPath)
            : base(currentPath)
        {
            DatasFileName = "users.csv";
        }

        protected override Dictionary<string, string> GenerateDictionary(string[] data)
        {
            return new Dictionary<string, string>
            {
                {"userID", data[ColmnNumber_UserID]},
                {"userName", data[ColmnNumber_UserName]},
                {"userPassword", data[ColmnNumber_UserPassword]}
            };
        }

        public Dictionary<string, string> FindUserID(string userID)
        {
            return FindRecord(userID, ColmnNumber_UserID);
        }

        public Dictionary<string, string> FindUserName(string userName)
        {
            return FindRecord(userName, ColmnNumber_UserName);
        }

        public string AuthorizeUser(string userName, string userPassword)
        {
            var record = FindUserName(userName);
            string userID = string.Empty;
            if (!record.TryGetValue("userID", out userID))
            {
                throw new InvalidOperationException("No userName found.");
            }
            if (!record["userPassword"].Equals(ComputeHashString(userPassword)))
            {
                throw new InvalidOperationException("Invalid userPassword.<br />");
            }
            return userID;
        }

        public void AddUser(string userName, string userPassword)
        {
            var record = FindUserName(userName);
            string userID = string.Empty;
            if (record.TryGetValue("userID", out userID))
            {
                throw new InvalidOperationException("userName already exists.");
            }
            AddRecord(new string[]
            {
                System.Guid.NewGuid().ToString(),
                base.EscapeForCsv(userName),
                base.ComputeHashString(userPassword)
            });
        }

        public void RemoveUser(string userID)
        {
            RemoveRecord(userID, ColmnNumber_UserID);
        }
    }
}
