<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserReferrals.aspx.cs" Inherits="FlyerMe.Admin.Users.UserReferrals" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <div class="indent"></div>
                <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                    <uc:Grid id="grid" runat="server" PageSize="10" PageName="admin/users/userreferrals.aspx" OnRowEdited="grid_RowEdited"  OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" />
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>