<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectMarketArea.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.SelectMarketArea" %>
<%@ Register Src="~/Controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Import Namespace="FlyerMe" %>
<div class="flyers no-icon">
    <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
        <div class="form-content">
            <div class="flyers-content">
                <div class="right">
                    <h1>Select your market area</h1>
                    <div class="item big clear">
                    </div>
                    <div class="item small-bs left">
                        <label>State</label>
                        <asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList>
                    </div>
                    <div class="item small-bs ">
                        <label>Choose delivery date<span class="required">*</span> </label>
                        <input type="text" id="inputDeliveryDate" runat="server" data-clientname="DeliveryDate" placeholder="Ñhoose date">
                        <a href="" class="date"><span></span>23</a>
                    </div>
                    <div class="item big clear">
                        <label>Market</label>
                        <div class="radioset">
                            <input type="radio" id="radioCounty" name="market" value="county" <%= IsMarketChecked("county") ? "checked" : String.Empty %> /><label for="radioCounty">County</label>
                            <input type="radio" id="radioAssociation" name="market" value="association" <%= IsMarketChecked("association") ? "checked" : String.Empty %> /><label for="radioAssociation">Association</label>
                            <input type="radio" id="radioMsa" name="market" value="msa" <%= IsMarketChecked("msa") ? "checked" : String.Empty %> /><label for="radioMsa">MSA</label>
                        </div>
                    </div>
                    <div class="price">
                        <div class="tables">
                            <table>
                                <thead>
                                    <th>County</th>
                                    <th>List size</th>
                                    <th>Price</th>
                                </thead>
                                <tbody>
                                    <% for (var i = 0; i < PricesList.Count; i++ ) { %>
                                    <tr>
                                        <td><input type="checkbox" id="cb_<%= i %>" <%= IsPriceChecked(i) ? "checked" : null %> /><label for="cb_<%= i %>"><%= PricesList[i].market %></label></td>
                                        <td><%= PricesList[i].listsize.FormatCount() %></td>
                                        <td><%= PricesList[i].price.FormatPrice() %></td>
                                    </tr>
                                    <% } %>
                                </tbody>
                            </table>
                            <uc:Pager ID="pager" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="create price">
                <div class="icon"></div>
                <table>
                    <tbody>
                        <% foreach(var i in GetSelectedPricesList()) { %>
                        <tr>
                            <td><%= i.Value.market %></td>
                            <td><%= i.Value.listsize.FormatCount() %></td>
                            <td><%= i.Value.price.FormatPrice() %></td>
                        </tr>
                        <% } %>
                    </tbody>
                </table>
                <div class="total">
                    <div class="block size">
                        Total list size
                        <div class="strong"><%= GetTotalListSize().FormatCount() %></div>
                    </div>
                    <div class="block amount">
                        Total price
                        <div class="strong"><%= GetTotalAmount().FormatPrice() %></div>
                    </div>
                </div>
                <input type="submit" value="Checkout" <%= IsCheckoutDisabled() ? "disabled" : null %>>
                <input type="hidden" id="hiddenPrices" name="prices" value="<%= GetFilterPrices() %>" class="data"/>
                <input type="hidden" id="hiddenPageNumber" value="<%= pager.PageNumber %>"/>
                <input type="hidden" id="hiddenPageSize" value="<%= pager.PageSize %>"/>
            </div>
        </div>
    </form>
</div>