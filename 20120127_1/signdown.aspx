<%@ Page Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="signdown.aspx.cs" Inherits="_20120127_1.signdown" Title="無題のページ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>サインダウン - ゆーしん</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderTitle" runat="server">
    　アカウント削除
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_NavTop" runat="server">
    <asp:Literal ID="Literal_NavTop" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <p>本当にアカウントを削除しますか？</p>
    <p><asp:HiddenField ID="HiddenField_Token" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Yes" onclick="Button_OK_Click" />
        <asp:Button ID="Button2" runat="server" Text="Cansel" onclick="Button_NO_Click" 
                style="height: 26px" /></p>
</asp:Content>