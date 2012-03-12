<%@ Page Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="_20120127_1._Default" Title="無題のページ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        <asp:Literal ID="Literal_Title" runat="server"></asp:Literal>
    </title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderTitle" runat="server">
    <asp:MultiView ID="MultiView_HeaderTitle" runat="server">
        <asp:View ID="View_HeaderTitle_Default" runat="server">
            <link rel="stylesheet" href="publicIndex.css" type="text/css" />
            <a href="index.aspx">
                <canvas class="barnner">
                    <img src="barnner.png" alt="ゆーしん（yusin）" />
                </canvas>
            </a>
        </asp:View>
        
        <asp:View ID="View_HeaderTitle_User" runat="server">
            <link rel="stylesheet" href="userProfile.css" type="text/css" />
            最近のアクティビティ
        </asp:View>
    </asp:MultiView>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_NavTop" runat="server">
    <asp:MultiView ID="MultiView_NavTop" runat="server">
        <asp:View ID="View_NavTop_Default" runat="server">
            <a href="signup.aspx">ユーザ登録</a>
            <a href="signin.aspx">ログイン</a>
        </asp:View>
        
        <asp:View ID="View_NavTop_User" runat="server">
            <asp:Literal ID="Literal_NavTop" runat="server"></asp:Literal>
        </asp:View>
    </asp:MultiView>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <asp:MultiView ID="MultiView_Main" runat="server">
        <asp:View ID="View_Main_Default" runat="server">
            <p><a class="buttonYellow" href="signup.aspx">ユーザ登録</a></p>
            <p><a class="buttonYellow" href="signin.aspx">ログイン</a></p>
            <p><strong>文字を作る</strong></p>
            <p><strong>文字を見る</strong></p>
            <p><img src="barnner2.png" alt="yUsin" /></p>
            <p>「ゆーしん」は、ユーラル語で「言葉」を意味する"yusin"からきています。</p>
        </asp:View>
        
        <asp:View ID="View_Main_User" runat="server">
            <asp:Literal ID="Literal_Activities" runat="server"></asp:Literal>
        </asp:View>
    </asp:MultiView>
</asp:Content>
