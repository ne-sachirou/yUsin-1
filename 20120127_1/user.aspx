<%@ Page Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="_20120127_1.user" Title="無題のページ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        <asp:Literal ID="Literal_Title" runat="server"></asp:Literal> - ゆーしん
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderTitle" runat="server">
    <asp:Literal ID="Literal_HeaderTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_NavTop" runat="server">
    <asp:Literal ID="Literal_NavTop" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <h2>認知</h2>
    <asp:Label ID="Label_Followings" runat="server" Text="" BorderColor="Red" 
        BorderStyle="Dotted" BorderWidth="1px"></asp:Label>
    <div class="clear"></div>
    
    <h2>被認知</h2>
    <asp:Label ID="Label_Followers" runat="server" Text="" BorderColor="Red" 
        BorderStyle="Dotted" BorderWidth="1px"></asp:Label>
    <div class="clear"></div>
    
    <div>
        <asp:Button ID="Button_Follow" runat="server" Text="認知"
            onclick="Button_Follow_Click" />
        <asp:Button ID="Button_Unfollow" runat="server" Text="否認"
            onclick="Button_Unfollow_Click" />
    </div>
    
    <asp:Literal ID="Literal_Activities" runat="server"></asp:Literal>
</asp:Content>
