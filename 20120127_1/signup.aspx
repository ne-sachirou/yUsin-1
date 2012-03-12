<%@ Page Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="_20120127_1.register" Title="無題のページ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>サインアップ - ゆーしん</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderTitle" runat="server">
    　サインアップ
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_NavTop" runat="server">
    <a href="signup.aspx">ユーザ登録</a>
    <a href="signin.aspx">ログイン</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <table>
        <tr>
            <th>UserName</th>
            <td>
                <asp:TextBox ID="TextBox_UserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_UserName"
                    runat="server" ErrorMessage="RequiredFieldValidator"
                    ControlToValidate="TextBox_UserName">UserName is required.</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator_UserName" 
                    runat="server" ErrorMessage="RegularExpressionValidator" 
                    ControlToValidate="TextBox_UserName" 
                    ValidationExpression="[-0-9A-Za-z_.]{4,20}">UserName is invalid fromat.</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>UserPassword</th>
            <td>
                <asp:TextBox ID="TextBox_UserPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_UserPassword"
                    runat="server" ErrorMessage="RequiredFieldValidator"
                    ControlToValidate="TextBox_UserPassword">UserPassword is required.</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator_UserPassword"
                    runat="server" ErrorMessage="RegularExpressionValidator"
                    ControlToValidate="TextBox_UserPassword" ValidationExpression="[-_0-9A-Za-z]{8,60}">UserPassword is invalid format.</asp:RegularExpressionValidator>
            </td>
        </tr>
    </table>
    <asp:Button ID="Button_Submit" runat="server" Text="Signup" 
        onclick="Button_Submit_Click" />  
    <div>
        <asp:Label ID="Label_Notify" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>