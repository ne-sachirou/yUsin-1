using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
    public partial class _Default : System.Web.UI.Page
    {
        protected void ShowActivities()
        {
            ActivitiesData activities = new ActivitiesData(MapPath("."));
            FollowsData follows = new FollowsData(MapPath("."));
            UsersData users = new UsersData(MapPath("."));
            FontsData fonts = new FontsData(MapPath("."));
            ImagesData images = new ImagesData(MapPath("."));
            var myFollowings = follows.FindUserIDs((string)Session["userID"]);
            List<Dictionary<string, string>> shownActivities = new List<Dictionary<string, string>>();
            foreach (var following in myFollowings)
            {
                Dictionary<string, string>[] thisActivities;
                switch (following["targetType"])
                {
                    case "font":
                        thisActivities = activities.FindObjectIDs("font", following["objectID"]);
                        break;
                    case "user":
                        thisActivities = activities.FindUserIDs(following["objectID"]);
                        break;
                    default:
                        thisActivities = new Dictionary<string, string>[0];
                        break;
                }
                foreach (var thisActivity in thisActivities)
                {
                    shownActivities.Add(thisActivity);
                }
            }
            Literal_Activities.Text = "";
            foreach (Dictionary<string, string> activity in shownActivities.ToArray().Reverse())
            {
                string targetUri;
                string targetName;
                string actionName;
                switch (activity["activityType"])
                {
                    case "makeImage":
                        targetName = "文字 " + images.FindImageId(activity["objectID"])["imageName"];
                        targetUri = "char.aspx?id=" + activity["objectID"];
                        actionName = "作成";
                        break;
                    case "editImage":
                        targetName = "文字 " + images.FindImageId(activity["objectID"])["imageName"];
                        targetUri = "char.aspx?id=" + activity["objectID"];
                        actionName = "編集";
                        break;
                    case "makeFont":
                        targetName = "字体 " + fonts.FindFontID(activity["objectID"])["fontName"];
                        targetUri = "font.aspx?id=" + activity["objectID"];
                        actionName = "作成";
                        break;
                    case "editFont":
                        targetName = "字体 " + fonts.FindFontID(activity["objectID"])["fontName"];
                        targetUri = "font.aspx?id=" + activity["objectID"];
                        actionName = "編集";
                        break;
                    case "makeSentence":
                        targetName = "例文";
                        targetUri = "sentence.aspx?id=" + activity["objectID"];
                        actionName = "作成";
                        break;
                    case "followFont":
                        targetName = "字体 " + fonts.FindFontID(activity["objectID"])["fontName"];
                        targetUri = "font.aspx?id=" + activity["objectID"];
                        actionName = "認知";
                        break;
                    case "followUser":
                        targetName = "賢者 " + users.FindUserID(activity["objectID"])["userName"];
                        targetUri = "user.aspx?id=" + activity["objectID"];
                        actionName = "認知";
                        break;
                    default:
                        targetName = "UMA";
                        targetUri = "index.aspx";
                        actionName = "UFO";
                        break;
                }
                string userID = activity["userID"];
                string userName = users.FindUserID(userID)["userName"];
                if (activity["activityType"] == "makeSentence")
                {
                    var sentence = new SentencesData(MapPath(".")).FindSentenceID(activity["objectID"]);
                    Literal_Activities.Text +=
                        "<a href=\"" + targetUri + "\">" +
                        PageKits.generateFontedContent(sentence["sentence"], sentence["fontID"]) +
                        "</a>";
                }
                Literal_Activities.Text +=
                    "<div class=\"activity\">" +
                    "<a href=\"user.aspx?id=" + userID + "\">" + userName + "さん</a>が" +
                    "<a href=\"" + targetUri + "\">" + targetName + "</a>を" +
                    actionName + "しました。" +
                    "<span class=\"activityDate\">" + activity["activityDate"] + "</span>" +
                    "</div>";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessionID"] == null)
            {
                MultiView_HeaderTitle.SetActiveView(View_HeaderTitle_Default);
                MultiView_NavTop.SetActiveView(View_NavTop_Default);
                MultiView_Main.SetActiveView(View_Main_Default);
                Literal_Title.Text = PageKits.SiteName;
            }
            else
            {
                MultiView_HeaderTitle.SetActiveView(View_HeaderTitle_User);
                MultiView_NavTop.SetActiveView(View_NavTop_User);
                MultiView_Main.SetActiveView(View_Main_User);
                UsersData users = new UsersData(MapPath("."));
                SessionsData sessions = new SessionsData(MapPath("."));
                string userID = (string)Session["userID"];
                if (!sessions.IsValidSession((string)Session["sessionID"], userID))
                {
                    Response.Redirect("signout.aspx?signout=true");
                    return;
                }
                string userName = users.FindUserID(userID)["userName"];
                Literal_Title.Text = userName + " - " + PageKits.SiteName;
                Literal_NavTop.Text = PageKits.generateNavTopContent(userID, userName);
                ShowActivities();
            }
        }
    }
}
