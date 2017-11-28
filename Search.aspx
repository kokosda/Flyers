<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" MasterPageFile="~/MasterPage.master" Theme="" %>
<%@ Register Src="~/Controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divSearchForm" runat="server" class="search-form">
        <div class="content">
            <h1>Search</h1>
            <form method="get" enctype="application/x-www-form-urlencoded" action="search.aspx">
           		<div class="form-content">
                    <div class="item litl addres">
                        <input type="text" placeholder="Address or Street" name="address" value="<%= Request.QueryString["address"] %>"/>
                    </div>
                    <div class="item litl city">
                        <input type="text" placeholder="City" name="city" value="<%= Request.QueryString["city"] %>"/>
                    </div>
                    <div class="item litl state">
                        <input type="text" placeholder="State" name="state" value="<%= Request.QueryString["state"] %>"/>
                    </div>
                    <div class="item litl zip">
                        <input type="text" placeholder="ZIP" name="zip" value="<%= Request.QueryString["zip"] %>"/>
                    </div>
                    <input type="submit" value="GO"/>
                    <input id="inpClearer" runat="server" type="submit" class="clear" value="Clear"/>
                </div>
            </form>
        </div>
    </div>
    <div class="search-content">
            <asp:Repeater ID="rptFlyers" runat="Server" OnItemDataBound="rptFlyers_ItemDataBound">
                <ItemTemplate>
                    <div class="block">
                        <div class="image">
                            <div class="table-cell">
                                <asp:HyperLink ID="hlShowFlyer" runat="server" Target="_blank"><asp:Image ID="imgPhoto1" runat="server"/></asp:HyperLink>
                            </div>
                        </div>
                        <div class="table">
                            <div class="table-cell">
                                <span class="title"><asp:Literal ID="ltlAddress" runat="server"></asp:Literal></span>
                                <span class="prices"><asp:Literal ID="ltlPrice" runat="server"></asp:Literal></span>
                            </div>
                        </div>
                        <ul class="characteristic">
                	        <li class="icon bedrooms"><span>Bedrooms:</span><strong><asp:Literal ID="ltlBedrooms" runat="server"></asp:Literal></strong></li>
                	        <li class="icon full_baths"><span>Full Baths:</span><strong><asp:Literal ID="ltlFullBaths" runat="server"></asp:Literal></strong></li>
                	        <li class="icon year_built"><span>Year Built:</span><strong><asp:Literal ID="ltlYearBuilt" runat="server"></asp:Literal></strong></li>
                	        <li class="icon sqft"><span>Sqft:</span><strong><asp:Literal ID="ltlSquareFeet" runat="server"></asp:Literal></strong></li>
                        </ul>
                   </div>  
                </ItemTemplate>
            </asp:Repeater>
            <uc:Pager ID="pager" runat="server" PageName="search.aspx" />
        </div>
</asp:Content>