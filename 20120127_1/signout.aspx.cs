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
    public partial class signout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Request.QueryString["signout"] == "true")
            {
                PageKits.ForceSignOut(MapPath("."), Session);
                Response.Redirect("index.aspx");
            }
            string userID = (string)Session["userID"];
            Literal_NavTop.Text = PageKits.generateNavTopContent(userID, new UsersData(MapPath(".")).FindUserID(userID)["userName"]);
        }

        protected void Button_OK_Click(object sender, EventArgs e)
        {
            PageKits.ForceSignOut(MapPath("."), Session);
            Response.Redirect("index.aspx");
        }
    }
}
