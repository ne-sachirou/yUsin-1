<%@ Page Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="char.aspx.cs" Inherits="_20120127_1._charImg" Title="無題のページ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        <asp:Literal ID="Literal_Title" runat="server"></asp:Literal> :文字 - ゆーしん
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderTitle" runat="server">
    <asp:Literal ID="Literal_HeaderTitle" runat="server"></asp:Literal> :文字
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_NavTop" runat="server">
    <asp:Literal ID="Literal_NavTop" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <asp:MultiView ID="MultiView_Char" runat="server">
        <asp:View ID="View_NewChar" runat="server">
            <div id="drawing"></div>
            <table>
                <tr><th>文字名</th><td><asp:TextBox ID="TextBox_NewCharName" runat="server"></asp:TextBox></td></tr>
                <tr><th>文字画像(400x400px)</th><td><input id="File_NewChar" type="file" runat="server" /></td></tr>
            </table>
            <asp:Button ID="Button_MakeChar" runat="server" Text="作成" 
                onclick="Button_MakeChar_Click" />
        </asp:View>
        
        <asp:View ID="View_ShowChar" runat="server">
            <table>
                <tr><th>文字名</th><td><asp:TextBox ID="TextBox_CharName" runat="server"></asp:TextBox></td></tr>
                <tr><th>投稿ユーザ</th><td><asp:Label ID="Label_UserName" runat="server" Text=""></asp:Label></td></tr>
                <tr><th>文字画像</th><td><asp:Image ID="Image_Char" runat="server" /></td></tr>
            </table>
            <asp:Button ID="Button_EditChar" runat="server" Text="編集終了" 
                onclick="Button_EditChar_Click" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
