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
    public class FollowsData : DatasData
    {
        private static int ColumnNumber_UserID = 0;
        private static int ColumnNumber_TargetType = 1;
        private static int ColumnNumber_ObjectID = 2;

        public FollowsData(string currntPath)
            : base(currntPath)
        {
            DatasFileName = "follows.csv";
        }

        protected override Dictionary<string, string> GenerateDictionary(string[] data)
        {
            return new Dictionary<string, string>
            {
                {"userID", data[ColumnNumber_UserID]},
                {"targetType", data[ColumnNumber_TargetType]},
                {"objectID", data[ColumnNumber_ObjectID]}
            };
        }

        /// <summary>
        /// Find records by user id.
        /// </summary>
        /// <param name="userID">Follow subject user id</param>
        /// <returns>Finded records.</returns>
        public Dictionary<string, string>[] FindUserIDs(string userID)
        {
            return FindRecords(delegate(Dictionary<string, string> record)
            {
                return record["userID"] == userID;
            });
        }

        /// <summary>
        /// Find records by target object.
        /// </summary>
        /// <param name="targetType">user|font</param>
        /// <param name="objectID">Followed object is.</param>
        /// <returns>Finded records.</returns>
        public Dictionary<string, string>[] FindObjectIDs(string targetType, string objectID)
        {
            return FindRecords(delegate(Dictionary<string, string> record)
            {
                return record["targetType"] == targetType && record["objectID"] == objectID;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID">Follow subject.</param>
        /// <param name="targetType">user|font</param>
        /// <param name="objectID">Target object id.</param>
        /// <returns>The subject follows the target or not.</returns>
        public bool IsFollow(string userID, string targetType, string objectID)
        {
            return FindRecords(delegate(Dictionary<string, string> record)
            {
                return record["userID"] == userID &&
                    record["targetType"] == targetType &&
                    record["objectID"] == objectID;
            }).Length > 0;
        }

        /// <summary>
        /// Follow a user or a font.
        /// </summary>
        /// <param name="userID">Follow subject.</param>
        /// <param name="targetType">user|font</param>
        /// <param name="objectID">Target object ID.</param>
        public void AddFollow(string userID, string targetType, string objectID)
        {
            AddRecord(new string[]
            {
                userID,
                targetType,
                objectID
            });
        }

        /// <summary>
        /// Unfollow a user or a font.
        /// </summary>
        /// <param name="userID">Unfollow subject.</param>
        /// <param name="targetType">user|font</param>
        /// <param name="objectID">Target object id.</param>
        public void RemoveFollow(string userID, string targetType, string objectID)
        {
            RemoveRecords(delegate(Dictionary<string, string> record)
            {
                return record["userID"] == userID &&
                    record["targetType"] == targetType &&
                    record["objectID"] == objectID;
            });
        }

        /// <summary>
        /// When a user signdown, you shold remove his follows data.
        /// </summary>
        /// <param name="userID">Singdowned user id.</param>
        public void RemoveUserID(string userID)
        {
            RemoveRecords(delegate(Dictionary<string, string> record)
            {
                return record["userID"] == userID ||
                    (record["targetType"] == "user" && record["objectID"] == userID);
            });
        }
    }
}
