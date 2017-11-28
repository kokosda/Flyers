<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationsStatus.aspx.cs" Inherits="FlyerMe.Admin.Reports.EmailDelivery.ApplicationsStatus" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <div class="indent"></div>
                <uc:Message ID="message" runat="server" />
                <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                    <uc:Grid ID="grid" runat="server" PagingDisabled="true" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" />
                    <div class="indent"></div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>