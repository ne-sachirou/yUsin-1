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
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessionID"] != null)
            {
                Response.Redirect("signout.aspx");
            }
        }

        protected void Button_Submit_Click(object sender, EventArgs e)
        {
            UsersData users = new UsersData(MapPath("."));
            try
            {
                users.AddUser(TextBox_UserName.Text, TextBox_UserPassword.Text);
                TextBox_UserPassword.Text = "";
                Response.Redirect("index.aspx");
            }
            catch (InvalidOperationException err)
            {
                Label_Notify.Text = "Button_Submit_Click()/users.AddUser(): " + err.Message;
                TextBox_UserPassword.Text = "";
            }
        }
    }
}
