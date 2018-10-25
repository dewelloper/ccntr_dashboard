<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DashGen.Login" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/Login.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="centerHeadline">
            <strong>Genpa Dashboard Access</strong>
        </div>
        <div class="center">
            <table class="auto-style1">
                <tr>
                    <td class="floatRight">
                        <dx:ASPxLabel ID="lblUserName" runat="server" Text="Kullanıcı Adı:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtUsername" runat="server" Width="170px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="floatRight">
                        <dx:ASPxLabel ID="lblPassword" runat="server" Text="Şifre">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPassword" runat="server" Width="170px" Password="true">
                        </dx:ASPxTextBox>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="txtLogging" runat="server" Text="Giriş" OnClick="txtLogging_Click">
                        </dx:ASPxButton>
                        <%--<asp:ImageButton ID="imgButtonSettings" Style="float: right;" runat="server" ImageUrl="images/Settings.png" Width="40px" Height="40px" OnClick="imgButtonSettings_Click" />--%>
                        <dx:ASPxLabel ID="lblError" runat="server" Width="350px">
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>

        </div>
    </form>


</body>
</html>
