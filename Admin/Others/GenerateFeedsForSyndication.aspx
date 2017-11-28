<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateFeedsForSyndication.aspx.cs" Inherits="FlyerMe.Admin.Others.GenerateFeedsForSyndication" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="grid" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <uc:Message ID="message" runat="server" />
                <form method="post" enctype="application/x-www-form-urlencoded" runat="server">
                	<div class="indent"></div>
                    <uc:grid ID="grid" runat="server" PagingDisabled="true" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" OnRowCommanded="grid_RowCommanded" />
                    </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>