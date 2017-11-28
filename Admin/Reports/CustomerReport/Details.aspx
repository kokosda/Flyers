<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="FlyerMe.Admin.Reports.CustomerReport.Details" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                    <div class="indent"></div>
                    <uc:Message ID="message" runat="server" />
                    <uc:Grid ID="grid" runat="server" PageSize="15" PageName="admin/reports/customerreport/details.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" ExcelExporterEnabled="true" />
                    <div class="indent"></div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>