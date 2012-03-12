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
    public partial class sentence : System.Web.UI.Page
    {
        protected void ShowSentence()
        {
            SentencesData sentences = new SentencesData(MapPath("."));
            if ((string)Request.QueryString["new"] == "true")
            {
                Literal_Title.Text = "新しい例文";
                Literal_HeaderTitle.Text = "新しい例文";
                MultiView_Sentence.SetActiveView(View_NewSentence);
                FontsData fonts = new FontsData(MapPath("."));
                foreach (var font in fonts)
                {
                    ListItem item = new ListItem();
                    item.Value = font["fontID"];
                    item.Text = font["fontName"];
                    DropDownList_Fonts.Items.Add(item);
                }
            }
            else if (Request.QueryString["id"] != null)
            {
                var sentence = sentences.FindSentenceID((string)Request.QueryString["id"]);
                string titleSentence;
                if (sentence["sentence"].Length > 10)
                {
                    titleSentence = sentence["sentence"].Substring(0, 10) + "…";
                }
                else
                {
                    titleSentence = sentence["sentence"];
                }
                Literal_Title.Text = sentence["sentence"];
                Literal_HeaderTitle.Text = sentence["sentence"];
                MultiView_Sentence.SetActiveView(View_ShowSentence);
                Literal_OrgSentence.Text = sentence["sentence"];
                Literal_FontedSentence.Text = PageKits.generateFontedContent(sentence["sentence"], sentence["fontID"]);
                Literal_FontedFontName.Text = new FontsData(MapPath(".")).FindFontID(sentence["fontID"])["fontName"];
            }
            else
            {
                Response.Redirect("index.aspx");
                return;
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
            ShowSentence();
        }

        protected void Button_Submit_Click(object sender, EventArgs e)
        {
            string userID = (string)Session["userID"];
            SentencesData sentences = new SentencesData(MapPath("."));
            string sentenceID = sentences.AddSentence(TextBox_Sentence.Text, DropDownList_Fonts.SelectedItem.Value, userID);
            ActivitiesData activities = new ActivitiesData(MapPath("."));
            activities.AddActivity("makeSentence", sentenceID, userID);
            Response.Redirect("sentence.aspx?id=" + sentenceID);
        }
    }
}
