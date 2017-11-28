<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Flyers.aspx.cs" Inherits="FlyerMe.Admin.Flyers" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                    <div class="search" id="divSearch" runat="server">
                        <div class="item-box">
                            <div class="item">
                                <label>Flyer ID</label>
                                <input type="text" id="inputOrderId" runat="server" />
                            </div>
                            <div class="item">
                                <label>Transaction ID</label>
                                <input type="text" id="inputTransactionId" runat="server" />
                            </div>
                            <div class="item">
                                <label>Customer ID</label>
                                <input type="text" id="inputCustomerId" runat="server" />
                            </div>
                        </div>
                        <div class="item-box">
                            <div class="item">
                                <label>Flyer Status</label>
                                <asp:DropDownList ID="ddlOrderStatus" runat="server">
                                    <asp:ListItem Text="All" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Scheduled" Value="Scheduled"></asp:ListItem>
                                    <asp:ListItem Text="Sent" Value="Sent"></asp:ListItem>
                                    <asp:ListItem Text="Queued" Value="Queued"></asp:ListItem>
                                    <asp:ListItem Text="Pending Approval" Value="Pending_Approval"></asp:ListItem>
                                    <asp:ListItem Text="Incomplete" Value="Incomplete"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="item">
                                <label>Delivery From:</label>
                                <div class="select-item">
                                    <div class="item left date">
                                        <input type="text" id="inputDeliveryFrom" runat="server" data-date readonly />
                                        <a class="date" href=""><span></span>23</a>
                                    </div>
                                    <div class="item right date">
                                        <label>To:</label>
                                        <input type="text" id="inputDeliveryTo" runat="server" data-date readonly />
                                        <a class="date" href=""><span></span>23</a>
                                    </div>
                                    <input type="submit" class="clear_date" value="&times;"/>
                                </div>
                            </div>
                            <div class="item">
                                <label>Order From:</label>
                                <div class="select-item">
                                    <div class="item left date">
                                        <input type="text" id="inputOrderFrom" runat="server" data-date readonly />
                                        <a class="date" href=""><span></span>23</a>
                                    </div>
                                    <div class="item right date">
                                        <label>To:</label>
                                        <input type="text" id="inputOrderTo" runat="server" data-date readonly />
                                        <a class="date" href=""><span></span>23</a>
                                    </div>
                                    <input type="submit" class="clear_date" value="&times;"/>
                                </div>
                            </div>
                        </div>
                        <div class="item-submit">
                            <asp:Button ID="btnSearchOrder" runat="server" Text="Search Order" OnClick="btnSearchOrder_Click" />
                        </div>
                    </div>
                    <uc:Grid id="grid" runat="server" PageSize="7" PageName="admin/flyers.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" ExcelExporterEnabled="true" TableFixed="true" />
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <link href="<%= ResolveUrl("~/css/jquery.ui/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>