<%@ Page Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="sentence.aspx.cs" Inherits="_20120127_1.sentence" Title="無題のページ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        <asp:Literal ID="Literal_Title" runat="server"></asp:Literal> :例文 - ゆーしん
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderTitle" runat="server">
    <asp:Literal ID="Literal_HeaderTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_NavTop" runat="server">
    <asp:Literal ID="Literal_NavTop" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <asp:MultiView ID="MultiView_Sentence" runat="server">
        <asp:View ID="View_NewSentence" runat="server">
            <asp:DropDownList ID="DropDownList_Fonts" runat="server">
            </asp:DropDownList>
            <asp:TextBox ID="TextBox_Sentence" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox>
            <asp:Button ID="Button_Submit" runat="server" Text="作成" 
                onclick="Button_Submit_Click" />
        </asp:View>
        
        <asp:View ID="View_ShowSentence" runat="server">
            <asp:Literal ID="Literal_FontName" runat="server"></asp:Literal>
            <table>
                <tr>
                    <th>元文</th>
                    <td><asp:Literal ID="Literal_OrgSentence" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th>字体</th>
                    <td><asp:Literal ID="Literal_FontedFontName" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th>字体文</th>
                    <td><asp:Literal ID="Literal_FontedSentence" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
