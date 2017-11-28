<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebsiteUsers.aspx.cs" Inherits="FlyerMe.Admin.Users.WebsiteUsers" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                    <div class="search">
                        <div class="box">
                            <input type="text" id="inputSearch" runat="server" placeholder="Search user by email" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <uc:Grid id="grid" runat="server" PageSize="7" PageName="admin/users/websiteusers.aspx" OnRowEdited="grid_RowEdited" OnActionCellCustomBound="grid_ActionCellCustomBound" OnActionCellCustomCommand="grid_ActionCellCustomCommand" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" />
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>