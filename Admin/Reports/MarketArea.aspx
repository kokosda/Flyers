<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MarketArea.aspx.cs" Inherits="FlyerMe.Admin.Reports.MarketArea" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
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
                                <label>State<span class="required">*</span></label>
                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnDataBound="ddlState_DataBound" DataSourceID="sdsStates" DataTextField="StateName" DataValueField="StateAbr" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsStates" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>"
                 SelectCommand="SELECT [StateID], [StateName], [StateAbr] FROM [fly_states] ORDER BY [StateName]"></asp:SqlDataSource>
                            </div>
                            <div class="item">
                                <label>Market Area</label>
                                <asp:DropDownList ID="ddlMarketArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMarketArea_SelectedIndexChanged">
                                    <asp:ListItem Text="County" Value="county"></asp:ListItem>
                                    <asp:ListItem Text="Assoication" Value="association"></asp:ListItem>
                                    <asp:ListItem Text="Metropolitan Statistical Area" Value="msa"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <uc:Grid ID="grid" runat="server" PagingDisabled="true" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" />
                    <div class="empty" id="divEmpty" runat="server"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
                    <div class="indent"></div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>