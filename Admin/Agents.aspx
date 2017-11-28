<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Agents.aspx.cs" Inherits="FlyerMe.Admin.Agents" Theme="" MasterPageFile="~/Admin/MasterPage.master" EnableEventValidation="false" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form runat="server" method="post" enctype="application/x-www-form-urlencoded" data-marketsurl="">
                    <div class="back add">
                        <asp:HyperLink NavigateUrl="~/admin/agents/addagentemail.aspx" runat="server"><span></span>Add New agent email</asp:HyperLink>
					</div>
                    <div class="search" id="divSearch" runat="server">
                        <uc:Message ID="message" runat="server" />
                        <div class="item-box">
                            <div class="item">
                                <label>Email</label>
                                <input type="text" id="inputEmail" runat="server" />
                            </div>
                            <div class="item">
                                <label>First Name</label>
                                <input type="text" id="inputFirstName" runat="server" />
                            </div>
                            <div class="item">
                                <label>Middle Name</label>
                                <input type="text" id="inputMiddleName" runat="server" />
                            </div>
                            <div class="item">
                                <label>Last Name</label>
                                <input type="text" id="inputLastName" runat="server" />
                            </div>
                        </div>
                        <div class="item-box">
                            <div class="item">
                                <label>State</label>
                                <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" DataSourceID="sdsStates" DataTextField="StateName" DataValueField="StateAbr" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="[Select State]"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsStates" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT [StateName], [StateAbr] FROM [fly_states] order by [StateName]">
                                </asp:SqlDataSource>
                            </div>
                            <div class="item">
                                <label>County</label>
                                <asp:DropDownList ID="ddlCounty" runat="server" AppendDataBoundItems="true" DataTextField="market" DataValueField="market">
                                    <asp:ListItem Value="" Text="[Select County]"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCounty" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT DISTINCT [market] FROM [fly_county_listsize] where state=@state order by market">
                                    <SelectParameters>
                                        <asp:QueryStringParameter QueryStringField="state" Name="state" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="item">
                                <label>Association</label>
                                <asp:DropDownList ID="ddlAssociation" AppendDataBoundItems="true" runat="server" DataTextField="market" DataValueField="market">
                                    <asp:ListItem Value="" Text="[Select Association]"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsAssociation" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT DISTINCT [market] FROM [fly_association_listsize] where state=@state order by [market]">
                                    <SelectParameters>
                                        <asp:QueryStringParameter QueryStringField="state" Name="state" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="item">
                                <label>MSA</label>
                                <asp:DropDownList ID="ddlMsa" AppendDataBoundItems="true" runat="server" DataTextField="market" DataValueField="market">
                                    <asp:ListItem Value="" Text="[Select MSA]"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsMsa" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" 
                                    SelectCommand="SELECT DISTINCT [market] FROM [fly_msa_listsize] where state=@state order by [market]">
                                    <SelectParameters>
                                        <asp:QueryStringParameter QueryStringField="state" Name="state" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="item-submit">
                            <asp:Button ID="btnSearch" runat="server" Text="Search Agents" OnCommand="btnSearch_Command" />
                        </div>
                    </div>
                    <div class="item choice">
                        <asp:DropDownList ID="ddlLastRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLastRecords_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="All Records"></asp:ListItem>
                            <asp:ListItem Value="50" Text="Last Records: 50" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="100" Text="Last Records: 100"></asp:ListItem>
                            <asp:ListItem Value="1000" Text="Last Records: 1000"></asp:ListItem>
                            <asp:ListItem Value="10000" Text="Last Records: 10000"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="item choice">
                        <asp:DropDownList ID="ddlRecordsPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRecordsPerPage_SelectedIndexChanged">
                            <asp:ListItem Value="10" Text="Records Per Page: 10" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="20" Text="Records Per Page: 20"></asp:ListItem>
                            <asp:ListItem Value="50" Text="Records Per Page: 50"></asp:ListItem>
                            <asp:ListItem Value="100" Text="Records Per Page: 100"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <uc:Grid id="grid" runat="server" PageSize="10" PageName="admin/agents.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" OnRowEditing="grid_RowEditing" OnRowEdited="grid_RowEdited" OnRowDeleted="grid_RowDeleted" ExcelExporterEnabled="true" TableFixed="true" />
                    <input type="hidden" id="hiddenScrollY" runat="server"/>
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <link href="<%= ResolveUrl("~/css/jquery.ui/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        (function ($) {
            $(function () {
                $(".del .close").on("click", function (e) {
                    return confirm("Are you sure, you want to delete this agent email?");
                });
            });
        })(jQuery);
    </script>
</asp:Content>