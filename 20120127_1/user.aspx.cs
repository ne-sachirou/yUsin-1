using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace _20120127_1
{
    public partial class user : System.Web.UI.Page
    {
        protected void ShowActivities(string userID)
        {
            Literal_Activities.Text = "";
            ActivitiesData activities = new ActivitiesData(MapPath("."));
            ImagesData images = new ImagesData(MapPath("."));
            FontsData fonts = new FontsData(MapPath("."));
            UsersData users = new UsersData(MapPath("."));
            var myActivities = activities.FindUserIDs(userID);
            foreach (var activity in myActivities.Reverse())
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
                    "<a href=\"" + targetUri + "\">" + targetName + "</a>を" +
                    actionName + "しました。" +
                    "<span class=\"activityDate\">" + activity["activityDate"] + "</span>" +
                    "</div>";
            }
        }

        protected void ShowFollowingStatus()
        {
            FollowsData follows = new FollowsData(MapPath("."));
            if ((string)Session["userID"] == (string)Request.QueryString["id"])
            {
                Button_Follow.Visible = false;
                Button_Unfollow.Visible = false;
                return;
            }
            if (follows.IsFollow((string)Session["userID"], "user", (string)Request.QueryString["id"]))
            {
                Button_Follow.Enabled = false;
                Button_Unfollow.Enabled = true;
            }
            else
            {
                Button_Follow.Enabled = true;
                Button_Unfollow.Enabled = false;
            }
        }

        protected void ShowFollows()
        {
            FollowsData follows = new FollowsData(MapPath("."));
            UsersData users = new UsersData(MapPath("."));
            string targetUserID = (string)Request.QueryString["id"];
            var hisFollowings = follows.FindUserIDs(targetUserID);
            foreach (var hisFollowing in hisFollowings)
            {
                string followingUserID = hisFollowing["objectID"];
                Label_Followings.Text += "<div class=\"follows\">" +
                    string.Format("<a href=\"user.aspx?id={0}\">{1}</a>", followingUserID, users.FindUserID(followingUserID)["userName"]) +
                    "</div>";
            }
            var hisFollowers = follows.FindObjectIDs("user", targetUserID);
            foreach (var hisFollower in hisFollowers)
            {
                string followerUserID = hisFollower["userID"];
                Label_Followers.Text += "<div class=\"follows\">" +
                    string.Format("<a href=\"user.aspx?id={0}\">{1}</a>", followerUserID, users.FindUserID(followerUserID)["userName"]) +
                    "</div>";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionsData sessions = new SessionsData(MapPath("."));
            string userID = (string)Session["userID"];
            if (!sessions.IsValidSession((string)Session["sessionID"], userID))
            {
                Response.Redirect("signout.aspx?signout=true");
                return;
            }
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("index.aspx");
            }
            string targetUserID = (string)Request.QueryString["id"];
            UsersData users = new UsersData(MapPath("."));
            string targetUserName = users.FindUserID(targetUserID)["userName"];
            Literal_NavTop.Text = PageKits.generateNavTopContent(userID, users.FindUserID(userID)["userName"]);
            Literal_Title.Text = targetUserName;
            Literal_HeaderTitle.Text = targetUserName;
            ShowFollowingStatus();
            ShowFollows();
            ShowActivities(targetUserID);
        }

        protected void Button_Follow_Click(object sender, EventArgs e)
        {
            string userID = (string)Session["userID"];
            string targetUserID = (string)Request.QueryString["id"];
            FollowsData follows = new FollowsData(MapPath("."));
            follows.AddFollow(userID, "user", targetUserID);
            ActivitiesData activities = new ActivitiesData(MapPath("."));
            activities.AddActivity("followUser", targetUserID, userID);
            ShowFollowingStatus();
        }

        protected void Button_Unfollow_Click(object sender, EventArgs e)
        {
            string userID = (string)Session["userID"];
            string targetUserID = (string)Request.QueryString["id"];
            FollowsData follows = new FollowsData(MapPath("."));
            follows.RemoveFollow(userID, "user", targetUserID);
            ActivitiesData activities = new ActivitiesData(MapPath("."));
            activities.AddActivity("followUser", targetUserID, userID);
            ShowFollowingStatus();
        }
    }
}
