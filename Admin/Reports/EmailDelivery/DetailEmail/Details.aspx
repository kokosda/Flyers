<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="FlyerMe.Admin.Reports.EmailDelivery.DetailEmail.Details" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
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
                                <asp:HyperLink ID="hlSendFleyrResults" runat="server" CssClass="button">Send Flyer Results</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                    <uc:Grid ID="grid" runat="server" PageSize="15" PageName="admin/reports/emaildelivery/detailemail/details.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" />
                    <div class="indent"></div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>