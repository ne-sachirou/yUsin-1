<%@ Page Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="signout.aspx.cs" Inherits="_20120127_1.signout" Title="無題のページ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>サインアウト - ゆーしん</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderTitle" runat="server">
    　サインアウト
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_NavTop" runat="server">
    <asp:Literal ID="Literal_NavTop" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <p>本当にサインアウトしますか？</p>
    <p><asp:Button ID="Button1" runat="server" Text="OK" onclick="Button_OK_Click" /></p>
</asp:Content>