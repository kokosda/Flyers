<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailWithSearch.aspx.cs" Inherits="FlyerMe.Admin.Reports.EmailDelivery.EmailWithSearch" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                    <div class="search">
                        <uc:Message ID="message" runat="server" />
                        <div class="item-box">
                            <div class="item">
                                <label>Flyer ID</label>
                                <input type="text" id="inputOrderId" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Email</label>
                                <input type="text" id="inputEmail" runat="server" />
                            </div>
                            <div class="item-submit">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnCommand="btnSearch_Command" />
                            </div>
                        </div>
                    </div>
                    <uc:Grid ID="grid" runat="server" PageSize="50" PageName="admin/reports/emaildelivery/emailwithsearch.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" />
                    <div class="empty" id="divEmpty" runat="server"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
                    <div class="indent"></div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>