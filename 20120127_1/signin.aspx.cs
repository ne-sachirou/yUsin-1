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

namespace _20120127_1
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessionID"] != null)
            {
                Response.Redirect("index.aspx");
                //MultiView_Main.SetActiveView(View_MainUser);
                //string userID = (string)Session["userID"];
                //string userName = (new UsersData(MapPath("."))).FindUserID(userID)["userName"];
                //Literal_NavTop.Text = PageKits.generateNavTopContent(userName);
            }
            else
            {
                MultiView_Main.SetActiveView(View_MainDefault);
                Literal_NavTop.Text = "<a href=\"signup.aspx\">ユーザ登録</a><a href=\"signin.aspx\">ログイン</a>";
            }
        }

        protected void Button_Submit_Click(object sender, EventArgs e)
        {
            UsersData users = new UsersData(MapPath("."));
            SessionsData sessions = new SessionsData(MapPath("."));
            string userID = string.Empty;
            try
            {
                userID = users.AuthorizeUser(TextBox_UserName.Text, TextBox_UserPassword.Text);
                sessions.AddSession(userID, Session.SessionID);
                Session["sessionID"] = Session.SessionID;
                Session["userID"] = userID;
                Response.Redirect("index.aspx");
            }
            catch (InvalidOperationException err)
            {
                Label_Notify.Text = "Button_Submit_Click()/user.AuthorizeUzer(): " + err.Message;
                TextBox_UserPassword.Text = "";
                return;
            }
        }
    }
}
