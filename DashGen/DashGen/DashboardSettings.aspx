﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardSettings.aspx.cs" Inherits="DashGen.DashboardSettings" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 60%;
        }

        .auto-style2 {
        }

        .auto-style3 {
            width:20%;
        }
        .column2 {
            width: 80%;
        }
    </style>

    <script type="text/javascript">
        function ChangeBackground(image) {
            $('.buyButton_new').css('background-image', 'url(images/image)');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table class="auto-style1">
                <%--                <tr>
                    <td class="auto-style3">
                        <dx:ASPxLabel ID="lblBoardLastIntegralTime" runat="server" Text="Son kaç dk. detaylı gösterilsin" ForeColor="White" Visible="False">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtBoardDataLastIntegralTime" runat="server" Width="170px" Visible="False">
                            <MaskSettings Mask="<0..99999999>"/>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>--%><%--                <tr>
                    <td class="auto-style3">
                        <dx:ASPxLabel ID="lblDataIntervalCount" runat="server" Text="Data gösterim sıklığı" ForeColor="White" Visible="False">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtBoardDataIntervalCount" runat="server" Width="170px" Visible="False">
                            <MaskSettings Mask="<0..99999999>"/>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td class="auto-style3">
                        <dx:ASPxLabel ID="lblDashboardSettingList" runat="server" Text="Dashboard Setting List" ForeColor="White">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3" colspan="2">
                        <dx:ASPxGridView ID="grdDashSettings" runat="server" AutoGenerateColumns="False" DataSourceID="dsDashboardSettings" KeyFieldName="Id" OnRowInserted="ASPxGridView2_RowInserted" OnRowUpdated="ASPxGridView2_RowUpdated">
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0">
                                    <EditButton Visible="True" Text="Düzenle">
                                    </EditButton>
                                    <NewButton Visible="True" Text="Yeni">
                                    </NewButton>
                                    <DeleteButton Visible="True" Text="Sil">
                                    </DeleteButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="Id" ReadOnly="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MaxExpectedWaitingTime" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MaxWaitedQueueCount" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MaxLongerWaitTime" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AcdNos" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SkillNos" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MailFormat" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MailReceipts" VisibleIndex="8">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MailSendingRepatInMinutes" VisibleIndex="9">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="BoardDataRangeInHours" VisibleIndex="10">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="BoardDataLastIntegralTime" VisibleIndex="11">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="BoardDataIntervalCount" VisibleIndex="12">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="ModifiedTime" VisibleIndex="13">
                                </dx:GridViewDataDateColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <asp:EntityDataSource ID="dsDashboardSettings" runat="server" ConnectionString="name=GenpaDashboardEntities" DefaultContainerName="GenpaDashboardEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="DashboardSettings" EntityTypeFilter="DashboardSettings">
                        </asp:EntityDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <table class="auto-style1">
                            <tr>
                                <td>Ekran Geçiş Süresi</td>
                                <td>
                                    <asp:TextBox ID="txtScreenPassTime" runat="server"></asp:TextBox>
                                </td>
                                <td>Ekran Geçilebilir mi</td>
                                <td>
                                    <asp:CheckBox ID="chkPassAllowed" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Gün Başlangıcı</td>
                                <td>
                                    <dx:ASPxTimeEdit ID="aspxtimeStart" runat="server">
                                    </dx:ASPxTimeEdit>
                                </td>
                                <td>Gün Ortası</td>
                                <td>
                                    <dx:ASPxTimeEdit ID="aspxtimeAfter" runat="server">
                                    </dx:ASPxTimeEdit>
                                </td>
                                <td>Gün Sonu</td>
                                <td>
                                    <dx:ASPxTimeEdit ID="aspxtimeEnd" runat="server">
                                    </dx:ASPxTimeEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:FileUpload ID="FileUpload1" runat="server" BackColor="White"/>
                        <dx:ASPxButton ID="btnChangeImage" runat="server" OnClick="btnChangeImage_Click" Text="Kaydet">
                        </dx:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <dx:ASPxLabel ID="lblMailSettings" runat="server" Text="Mail ayarları" ForeColor="White">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="2">
                        <dx:ASPxGridView ID="grdMailSettings" runat="server" AutoGenerateColumns="False" DataSourceID="dsMailSettings" KeyFieldName="Id" OnRowInserted="ASPxGridView1_RowInserted" OnRowUpdated="ASPxGridView1_RowUpdated">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <EditButton Text="Düzenle" Visible="true">
                                    </EditButton>
                                    <NewButton Text="Yeni" Visible="false">
                                    </NewButton>
                                    <DeleteButton Text="Sil" Visible="false">
                                    </DeleteButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="Id" ReadOnly="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Host" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Port" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn FieldName="SSL" VisibleIndex="4">
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="UseDefaultCredental" VisibleIndex="5">
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn FieldName="CredentalEmail" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CredentalPassword" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Priority" VisibleIndex="8">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="ModifyDate" VisibleIndex="9" Visible="false">
                                </dx:GridViewDataDateColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <asp:EntityDataSource ID="dsMailSettings" runat="server" ConnectionString="name=GenpaDashboardEntities" DefaultContainerName="GenpaDashboardEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="MailSettings">
                        </asp:EntityDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="2">
                        <dx:ASPxLabel ID="lblUsers" runat="server" Text="Kullanıcılar" ForeColor="White">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="2">
                        <dx:ASPxGridView ID="grdUsers" runat="server" AutoGenerateColumns="False" DataSourceID="dsUsers" KeyFieldName="Id" OnLoad="grdUsers_Load" OnRowInserted="grdUsers_RowInserted" OnRowUpdated="grdUsers_RowUpdated">
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0">
                                    <EditButton Visible="True" Text="Düzenle">
                                    </EditButton>
                                    <NewButton Visible="True" Text="Yeni">
                                    </NewButton>
                                    <DeleteButton Visible="True" Text="Sil">
                                    </DeleteButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="Id" ReadOnly="True" VisibleIndex="1" Caption="Id">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Caption="Ad">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Surname" VisibleIndex="3" Caption="Soyad">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Username" VisibleIndex="4" Caption="Kullanıcı">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Password" VisibleIndex="5" PropertiesTextEdit-Password="true" Caption="Şifre">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <asp:EntityDataSource ID="dsUsers" runat="server" ConnectionString="name=GenpaDashboardEntities" DefaultContainerName="GenpaDashboardEntities" EnableInsert="true" EnableDelete="True" EnableFlattening="False" EnableUpdate="True" EntitySetName="Users">
                        </asp:EntityDataSource>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
