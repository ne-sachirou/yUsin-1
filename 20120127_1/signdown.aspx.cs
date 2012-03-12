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
    public partial class signdown : System.Web.UI.Page
    {
        protected void SignDown()
        {
            if (HiddenField_Token.Value != (string)Session["sessinID"])
            {
                throw new InvalidOperationException("Cross-Site Request Forgery (CSRF)");
            }
            SessionsData sessions = new SessionsData(MapPath("."));
            UsersData users = new UsersData(MapPath("."));
            if (!sessions.IsValidSession((string)Session["sessionID"], (string)Session["userID"]))
            {
                throw new InvalidOperationException("The session is invalid.");
            }
            sessions.RemoveSession((string)Session["sessionID"]);
            users.RemoveUser((string)Session["userID"]);
            Session.Abandon();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HiddenField_Token.Value = (string)Session["sessionID"];
            string userID = (string)Session["userID"];
            Literal_NavTop.Text = PageKits.generateNavTopContent(userID, new UsersData(MapPath(".")).FindUserID(userID)["userName"]);
        }

        protected void Button_OK_Click(object sender, EventArgs e)
        {
            try
            {
                SignDown();
                Response.Redirect("index.aspx");
            }
            catch (InvalidOperationException)
            {
                Response.Redirect("signout.aspx?signout=true");
            }
        }

        protected void Button_NO_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}
