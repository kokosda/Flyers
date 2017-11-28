<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomersByEsp.aspx.cs" Inherits="FlyerMe.Admin.Reports.CustomersByEsp" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
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
                                <label>ESP</label>
                                <input type="text" id="inputEsp" runat="server" />
                            </div>
                            <div class="item">
                                <label>State</label>
                                <asp:DropDownList ID="ddlState" runat="server" DataSourceID="sdsStates" DataTextField="StateName" DataValueField="StateAbr" OnDataBound="ddlState_DataBound"></asp:DropDownList>
                                <asp:SqlDataSource ID="sdsStates" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>"
                 SelectCommand="SELECT [StateID], [StateName], [StateAbr] FROM [fly_states] ORDER BY [StateName]"></asp:SqlDataSource>
                            </div>
                            <div class="item-submit">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnCommand="btnSearch_Command" />
                            </div>
                        </div>
                    </div>
                    <uc:Grid ID="grid" runat="server" PageSize="10" PageName="admin/reports/customersbyesp.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" />
                    <div class="empty" id="divEmpty" runat="server"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
                    <div class="indent"></div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>