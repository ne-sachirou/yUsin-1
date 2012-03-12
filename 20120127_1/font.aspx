<%@ Page Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="font.aspx.cs" Inherits="_20120127_1.font" Title="無題のページ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        <asp:Literal ID="Literal_Title" runat="server"></asp:Literal> :字体 - ゆーしん
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderTitle" runat="server">
    <asp:Literal ID="Literal_HeaderTitle" runat="server"></asp:Literal> :字体
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_NavTop" runat="server">
    <asp:Literal ID="Literal_NavTop" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <p>字体名<asp:TextBox ID="TextBox_FontName" runat="server"></asp:TextBox></p>
    <p>字体説明<asp:TextBox ID="TextBox_FontProfile" runat="server" Height="50px" 
            TextMode="MultiLine" Width="200px"></asp:TextBox></p>
    <asp:Label ID="Label_Followers" runat="server" Text="Label" BorderColor="Red" 
        BorderStyle="Dotted" BorderWidth="1px"></asp:Label>
    <div class="clear"></div>
    
    <asp:MultiView ID="MultiView_Font" runat="server">
        <asp:View ID="View_MakeFont" runat="server">
            <asp:TextBox ID="TextBox_Unicode" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Unicode" 
                runat="server" ErrorMessage="RegularExpressionValidator" 
                ControlToValidate="TextBox_Unicode" ValidationExpression=".">一度に一文字だけ指定して下さい</asp:RegularExpressionValidator>
            へ、文字を
            <asp:DropDownList ID="DropDownList_ImageID" runat="server">
            </asp:DropDownList>
            として
            <asp:Button ID="Button_AddCorrespondence" runat="server" Text="追加" 
                onclick="Button_AddCorrespondence_Click" />
        </asp:View>
        
        <asp:View ID="View_EditFont" runat="server">
            <asp:Button ID="Button_Follow" runat="server" Text="認知" 
                onclick="Button_Follow_Click" />
            <asp:Button ID="Button_Unfollow" runat="server" Text="否認" 
                onclick="Button_Unfollow_Click" />
        </asp:View>
    </asp:MultiView>
    <p><asp:TextBox ID="TextBox_Correspondences" runat="server" Height="150px" 
            TextMode="MultiLine" Width="600px"></asp:TextBox></p>
    <asp:Button ID="Button_Submit" runat="server" Text="編集終了"
        onclick="Button_Submit_Click" />
</asp:Content>
