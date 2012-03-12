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

namespace _20120127_1
{
    public class PageKits : System.Web.UI.Page
    {
        public static string SiteName
        {
            get
            {
                return "ゆーしん";
            }
        }

        public static void ForceSignOut(string currentPath, System.Web.SessionState.HttpSessionState session)
        {
            SessionsData sessions = new SessionsData(currentPath);
            sessions.RemoveSession((string)session["sessionID"]);
            session.Abandon();
        }

        public static string generateNavTopContent(string userID, string userName)
        {
            return "<a href=\"index.aspx\">ホーム</a>" +
                "<a href=\"signout.aspx?signout=true\">ログアウト</a>" +
                string.Format("<a href=\"user.aspx?id={0}\">{1}</a>", userID, userName) + 
                "<span class=\"buttonBlue\" id=\"makeNewActivity\"></span>";
        }

        public static string generateFontedContent(string sentence, string fontID)
        {
            return "<span class=\"fonted\" data-fontID=\"" + fontID + "\">" + sentence + "</span>";
        }
    }
}
