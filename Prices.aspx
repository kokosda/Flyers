<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prices.aspx.cs" Inherits="FlyerMe.Prices" MasterPageFile="~/MasterPage.master" Theme="" %>
<%@ Register Src="~/Controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ OutputCache Duration="300" Location="ServerAndClient" VaryByParam="*" %>
<%@ Import Namespace="FlyerMe" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pricing">
        <div class="content">
            <h1>Select your Market & Flyer Type</h1>
            Our database of agents is organized into three main categories - according to County, Realtor Association and Metropolitan Statistical Area (MSA). Market area refers to these categories. Choose the market area that best fits your listing and your goals.
        </div>
    </div>
    <div class="select-state">
        <form runat="server" action="prices.aspx" method="get" enctype="application/x-www-form-urlencoded">
            <div class="form-content">
                <div class="item">
                    <label>Select State</label>
                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="false"></asp:DropDownList>
                </div>
                <div class="radios">
                    <div class="radio horizontally">
                        <div class="title">select market area:</div>
                        <input type="radio" id="radioCounty" name="market" value="county" <%= IsMarketChecked("county") ? "checked" : String.Empty %> /><label for="radioCounty">By County</label>
                        <input type="radio" id="radioAssociation" name="market" value="association" <%= IsMarketChecked("association") ? "checked" : String.Empty %> /><label for="radioAssociation">By Association</label>
                        <input type="radio" id="radioMsa" name="market" value="msa" <%= IsMarketChecked("msa") ? "checked" : String.Empty %> /><label for="radioMsa">By Metropolitan Statistical Area (MSA)</label>
                    </div>
                    <div class="radio horizontally">
                        <div class="title">select flyer type:</div>
                        <input type="radio" id="radioSeller" name="flyer" value="seller" <%= IsFlyerChecked("seller") ? "checked" : String.Empty %>/><label for="radioSeller">Seller’s Agent Flyer</label>
                        <input type="radio" id="radioBuyer" name="flyer" value="buyer" <%= IsFlyerChecked("buyer") ? "checked" : String.Empty %>/><label for="radioBuyer">Buyer’s Agent Flyer</label>
                        <input type="radio" id="radioCustom" name="flyer" value="custom" <%= IsFlyerChecked("custom") ? "checked" : String.Empty %>/><label for="radioCustom">PreMade Custom Flyer</label>
                    </div>
                </div>
            </div>
            <input type="hidden" id="hiddenPrices" name="prices" value="<%= GetFilterPrices() %>" class="data"/>
            <input type="hidden" id="hiddenPageNumber" value="<%= pager.PageNumber %>"/>
            <input type="hidden" id="hiddenPageSize" value="<%= pager.PageSize %>"/>
        </form>
    </div>
    <div class="price">
        <div class="content">
            <form action="<%= RootUrl %>createflyer.aspx" method="get" enctype="application/x-www-form-urlencoded">
           	    <div class="form-content">
                    <div class="tables">
                        <table>
                            <thead>
                                <th>County</th>
                                <th>List size</th>
                                <th>Price</th>
                            </thead>
                            <tbody>
                                <% for (var i = 0; i < PricesList.Count; i++ )
                                   { %>
                                <tr>
                                    <td><input type="checkbox" id="cb_<%= i %>" <%= IsPriceChecked(i) ? "checked" : null %> /><label for="cb_<%= i %>"><%= PricesList[i].market %></label></td>
                                    <td><%= PricesList[i].listsize.FormatCount() %></td>
                                    <td><%= PricesList[i].price.FormatPrice() %></td>
                                </tr>
                                <% } %>
                            </tbody>
                        </table>
                        <uc:Pager ID="pager" runat="server" PageName="prices.aspx" />
                    </div>
                    <div class="create">
                        <div class="icon"></div>
                        <ul>
                            <% foreach(var i in GetSelectedPricesList()) { %>
                            <li>
                                <div class="title"><%= i.Value.market %></div>
                                <div class="cost"><%= i.Value.listsize.FormatCount() %> | <%= i.Value.price.FormatPrice() %></div>
                            </li>
                            <% } %>
                        </ul>
                        <div class="total">
                            <div class="block size">
                                <div class="strong"><%= GetTotalListSize().FormatCount() %></div>
                                Total list size
                            </div>
                            <div class="block amount">
                                <div class="strong"><%= GetTotalAmount().FormatPrice() %></div>
                                Total price
                            </div>
                        </div>
                        <input type="submit" value="Create Flyer" <%= GetSelectedPricesList().Count == 0 ? "disabled" : null %>/>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>