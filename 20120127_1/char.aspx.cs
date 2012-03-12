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
    public partial class _charImg : System.Web.UI.Page
    {
        protected void ShowPage()
        {
            UsersData users = new UsersData(MapPath("."));
            ImagesData images = new ImagesData(MapPath("."));
            var image = new System.Collections.Generic.Dictionary<string, string>();
            if ((string)Request.QueryString["new"] == "true")
            {
                MultiView_Char.SetActiveView(View_NewChar);
                image["imageName"] = "新しい文字";
            }
            else if (Request.QueryString["id"] != null)
            {
                MultiView_Char.SetActiveView(View_ShowChar);
                image = images.FindImageId((string)Request.QueryString["id"]);
                Label_UserName.Text = users.FindUserID(image["userID"])["userName"];
                Image_Char.ImageUrl = string.Format("images/{0}.{1}", image["imageID"], image["imageType"]);
                TextBox_CharName.Text = image["imageName"];
            }
            else
            {
                Response.Redirect("index.aspx");
            }
            if (TextBox_NewCharName.Text == "")
            {
                TextBox_NewCharName.Text = image["imageName"];
            }
            Literal_Title.Text = image["imageName"];
            Literal_HeaderTitle.Text = image["imageName"];
            string userID = (string)Session["userID"];
            Literal_NavTop.Text = PageKits.generateNavTopContent(userID, users.FindUserID(userID)["userName"]);
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
            ShowPage();
        }

        protected void Button_MakeChar_Click(object sender, EventArgs e)
        {
            string userID = (string)Session["userID"];
            ImagesData images = new ImagesData(MapPath("."));
            string imageID = images.AddImage(TextBox_NewCharName.Text, userID, File_NewChar.PostedFile);
            new ActivitiesData(MapPath(".")).AddActivity("makeImage", imageID, userID);
            Response.Redirect("char.aspx?id=" + imageID);
        }

        protected void Button_EditChar_Click(object sender, EventArgs e)
        {
            string imageID = (string)Request.QueryString["id"];
            ImagesData images = new ImagesData(MapPath("."));
            images.RenameImage(imageID, TextBox_CharName.Text);
            new ActivitiesData(MapPath(".")).AddActivity("editImage", imageID, (string)Session["userID"]);
            ShowPage();
        }
    }
}
