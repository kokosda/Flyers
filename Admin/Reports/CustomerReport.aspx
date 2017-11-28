<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerReport.aspx.cs" Inherits="FlyerMe.Admin.Reports.CustomerReport" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
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
                                <label>Flyer Created From<span class="required">*</span></label>
                                <div class="select-item">
                                    <div class="item left date">
                                        <input type="text" id="inputFlyerCreatedFrom" runat="server" data-date readonly required />
                                        <a class="date" href=""><span></span>23</a>
                                    </div>
                                    <div class="item right date">
                                        <label>To<span class="required">*</span></label>
                                        <input type="text" id="inputFlyerCreatedTo" runat="server" data-date readonly required />
                                        <a class="date" href=""><span></span>23</a>
                                    </div>
                                    <input type="submit" class="clear_date" value="&times;"/>
                                </div>
                            </div>
                            <div class="item-submit">
                                <asp:Button runat="server" Text="Generate Report" OnCommand="btnGenerateReport_Command" />
                            </div>
                        </div>
                    </div>
                    <uc:Grid ID="grid" runat="server" PageSize="10" PageName="admin/reports/customerreport.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" ExcelExporterEnabled="true" />
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