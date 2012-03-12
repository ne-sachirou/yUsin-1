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
    public partial class font : System.Web.UI.Page
    {
        protected void ShowFont()
        {
            string fontID = (string)Request.QueryString["id"];
            FontsData fonts = new FontsData(MapPath("."));
            ImagesData images = new ImagesData(MapPath("."));
            string fontName;
            if ((string)Request.QueryString["new"] == "true")
            {
                fontName = "新しい字体";
                TextBox_FontName.Text = fontName;
                MultiView_Font.SetActiveView(View_MakeFont);
                if (DropDownList_ImageID.Items.Count == 0)
                {
                    foreach (var image in images)
                    {
                        ListItem item = new ListItem();
                        item.Value = image["imageID"];
                        item.Text = image["imageName"];
                        DropDownList_ImageID.Items.Add(item);
                    }
                }
            }
            else if (Request.QueryString["id"] != null)
            {
                var font = fonts.FindFontID(fontID);
                fontName = font["fontName"];
                MultiView_Font.SetActiveView(View_EditFont);
                var charactors = fonts.Charactors(fontID);
                TextBox_Correspondences.Text = "";
                foreach (var charactor in charactors)
                {
                    TextBox_Correspondences.Text += charactor.Key + "," + charactor.Value + Environment.NewLine;
                }
                TextBox_FontName.Text = font["fontName"];
                TextBox_FontProfile.Text = font["fontProfile"];
            }
            else
            {
                Response.Redirect("index.aspx");
                return;
            }
            Literal_Title.Text = fontName;
            Literal_HeaderTitle.Text = fontName;
        }

        protected void ShowFollowing()
        {
            FollowsData follows = new FollowsData(MapPath("."));
            UsersData users = new UsersData(MapPath("."));
            string fontID = (string)Request.QueryString["id"];
            if (follows.IsFollow((string)Session["userID"], "font", fontID))
            {
                Button_Follow.Enabled = false;
                Button_Unfollow.Enabled = true;
            }
            else
            {
                Button_Follow.Enabled = true;
                Button_Unfollow.Enabled = false;
            }
            var followers = follows.FindObjectIDs("font", fontID);
            foreach (var follower in followers)
            {
                Label_Followers.Text += "<div class=\"follows\">" +
                    string.Format("<a href=\"user.aspx?id={0}\">{1}</a>", follower["userID"], users.FindUserID(follower["userID"])["userName"]) +
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
            Literal_NavTop.Text = PageKits.generateNavTopContent(userID, new UsersData(MapPath(".")).FindUserID(userID)["userName"]);
            ShowFont();
            ShowFollowing();
        }

        protected void Button_AddCorrespondence_Click(object sender, EventArgs e)
        {
            TextBox_Correspondences.Text += TextBox_Unicode.Text + "," + DropDownList_ImageID.SelectedItem.Value+ Environment.NewLine;
        }

        protected void Button_Submit_Click(object sender, EventArgs e)
        {
            var charactors = new System.Collections.Generic.Dictionary<string, string>();
            string[] correspondences = TextBox_Correspondences.Text.Split(new char[]{'\r', '\n'});
            foreach (string correspondence in correspondences)
            {
                string[] charactor = correspondence.Split(',');
                if (charactor.Length == 2)
                {
                    charactors[charactor[0]] = charactor[1];
                }
            }
            FontsData fonts = new FontsData(MapPath("."));
            string fontID = fonts.AddFont(TextBox_FontName.Text, TextBox_FontProfile.Text, charactors);
            ActivitiesData activities = new ActivitiesData(MapPath("."));
            activities.AddActivity("makeFont", fontID, (string)Session["userID"]);
        }

        protected void Button_Follow_Click(object sender, EventArgs e)
        {
            string userID = (string)Session["userID"];
            string fontID = (string)Request.QueryString["id"];
            FollowsData follows = new FollowsData(MapPath("."));
            follows.AddFollow(userID, "font", fontID);
            ActivitiesData activities = new ActivitiesData(MapPath("."));
            activities.AddActivity("followFont", fontID, userID);
            ShowFollowing();
        }

        protected void Button_Unfollow_Click(object sender, EventArgs e)
        {
            string userID = (string)Session["userID"];
            string fontID = (string)Request.QueryString["id"];
            FollowsData follows = new FollowsData(MapPath("."));
            follows.RemoveFollow(userID, "font", fontID);
            ActivitiesData activities = new ActivitiesData(MapPath("."));
            activities.AddActivity("followFont", fontID, userID);
            ShowFollowing();
        }
    }
}
