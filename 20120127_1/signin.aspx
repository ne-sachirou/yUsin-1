<%@ Page Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="signin.aspx.cs" Inherits="_20120127_1.login" Title="無題のページ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>サインイン - ゆーしん</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderTitle" runat="server">
    サインイン
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_NavTop" runat="server">
    <asp:Literal ID="Literal_NavTop" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <asp:MultiView ID="MultiView_Main" runat="server">
        <asp:View ID="View_MainDefault" runat="server">
                <table>
                <tr>
                    <th>UserName</th>
                    <td>
                        <asp:TextBox ID="TextBox_UserName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_UseName" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBox_UserName">
                            UserName is required.</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_UserName"
                            runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="TextBox_UserName"
                            ValidationExpression="[-0-9A-Za-z_.]{4,20}">UserName is invalid format.</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <th>UserPassword</th>
                    <td>
                        <asp:TextBox ID="TextBox_UserPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_UserPassword" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBox_UserPassword">
                            UserPassword is required.</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_UserPassword" runat="server"
                            ErrorMessage="RegularExpressionValidator" ControlToValidate="TextBox_UserPassword"
                            ValidationExpression="[-_0-9A-Za-z]{8,60}">UserPassword is invalid format.</asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
            <asp:Button ID="Button_Submit" runat="server" Text="Signin" onclick="Button_Submit_Click" />
            <div>
                <asp:Label ID="Label_Notify" runat="server" Text=""></asp:Label>
            </div>
        </asp:View>
        
        <asp:View ID="View_MainUser" runat="server">
            <p>既にサインインしています。</p>
            <p><a href="signout.aspx">サインアウト</a></p>
        </asp:View>
    </asp:MultiView>
</asp:Content>