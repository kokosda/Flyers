<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unsubscribption.aspx.cs" Inherits="FlyerMe.Admin.Reports.Unsubscribption" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
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
                                <label>Unsubscribed From</label>
                                <div class="select-item">
                                    <div class="item left date">
                                        <input type="text" id="inputUnsubscribedFrom" runat="server" data-date readonly />
                                        <a class="date" href=""><span></span>23</a>
                                    </div>
                                    <div class="item right date">
                                        <label>To</label>
                                        <input type="text" id="inputUnsubscribedTo" runat="server" data-date readonly />
                                        <a class="date" href=""><span></span>23</a>
                                    </div>
                                    <input type="submit" class="clear_date" value="&times;"/>
                                </div>
                            </div>
                            <div class="item-submit">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnCommand="btnSearch_Command" />
                            </div>
                        </div>
                    </div>
                    <uc:Grid ID="grid" runat="server" PageSize="10" PageName="admin/reports/unsubscribption.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" ExcelExporterEnabled="true" />
                    <div class="empty" id="divEmpty" runat="server"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
                    <div class="indent"></div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <link href="<%= ResolveUrl("~/css/jquery.ui/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>