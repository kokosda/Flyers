<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlyersEmailCount.aspx.cs" Inherits="FlyerMe.Admin.Reports.FlyersEmailCount" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                    <div class="search" id="divSearch" runat="server">
                        <uc:Message ID="message" runat="server" />
                        <div class="item-box">
                            <div class="item">
                                <label>By Flyer ID<span class="required">*</span></label>
                                <input type="text" id="inputOrderId" runat="server" />
                            </div>
                            <div class="item-submit">
                                <asp:Button ID="btnSearchByFlyerId" runat="server" OnCommand="btnSearchByFlyerId_Command" Text="Search" />
                            </div>
                        </div>
                        <div class="item-box">
                            <div class="item">
                                <label>By Status</label>
                                <div class="select-item">
                                    <div class="item left">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem Text="Pending Approval" Value="Pending_Approval"></asp:ListItem>
                                            <asp:ListItem Text="Scheduled" Value="Scheduled" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Queued" Value="Queued"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="item right">
                                        <label>&nbsp;</label>
                                        <asp:DropDownList ID="ddlDay" runat="server">    
                                            <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                            <asp:ListItem Text="Till Today" Value="Today" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="item-submit">
                            	<asp:Button ID="btnSearchByStatus" runat="server" OnCommand="btnSearchByStatus_Command" Text="Search" />
                            </div>
                        </div>
                    </div>
                    <uc:Grid ID="grid" runat="server" PageSize="10" PageName="admin/reports/flyersemailcount.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" />
                    <div class="empty" id="divEmpty" runat="server"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
                    <div class="indent"></div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>