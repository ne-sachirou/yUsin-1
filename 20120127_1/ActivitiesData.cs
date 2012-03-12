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
    sealed public class ActivitiesData : DatasData
    {
        private static int ColunmNumber_AvtivityID = 0;
        private static int ColunmNumber_ActivityType = 1;
        private static int ColunmNumber_ObjectID = 2;
        private static int ColumnNumber_UserID = 3;
        private static int ColunmNumber_AvtivityDate = 4;

        public ActivitiesData(string currentpath)
            : base(currentpath)
        {
            DatasFileName = "activities.csv";
        }

        protected override Dictionary<string, string> GenerateDictionary(string[] data)
        {
            return new Dictionary<string, string>
            {
                {"activityID", data[ColunmNumber_AvtivityID]},
                {"activityType", data[ColunmNumber_ActivityType]},
                {"objectID", data[ColunmNumber_ObjectID]},
                {"userID", data[ColumnNumber_UserID]},
                {"activityDate", data[ColunmNumber_AvtivityDate]}
            };
        }

        public void AddActivity(string activityType, string objectID, string userID)
        {
            AddRecord(new string[]
            {
                System.Guid.NewGuid().ToString(),
                activityType,
                objectID,
                userID,
                DateTime.Now.ToString()
            });
        }

        public Dictionary<string, string>[] FindObjectIDs(string activityType, string objectID)
        {
            return FindRecords(delegate(Dictionary<string, string> record)
            {
                return record["activityType"] == activityType && record["objectID"] == objectID;
            });
        }

        public Dictionary<string, string>[] FindUserIDs(string userID)
        {
            return FindRecords(delegate(Dictionary<string, string> record)
            {
                return record["userID"] == userID;
            });
        }
    }
}
