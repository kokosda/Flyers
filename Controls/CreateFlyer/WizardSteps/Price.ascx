<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Price.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.Price" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers prices">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="item big clear">
                        <div class="radioset">
                            <input type="radio" id="price" name="priceRent" value="PRICE" <%= IsPriceRentChecked("price") ? "checked" : null %>><label for="price">Price</label>
                            <input type="radio" id="rentals" name="priceRent" value="RENT" <%= IsPriceRentChecked("rent") ? "checked" : null %>><label for="rentals">Rent</label>
                        </div>
                    </div>
                    <div class="item big clear">
                    </div>
                    <div class="item small-bs left">
                        <label>Price</label>
                        <input type="text" id="inputPrice" runat="server" data-clientname="Price">
                    </div>
                    <div class="item small-bs">
                        <label>Per</label>
                        <asp:DropDownList ID="ddlPerArea" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="/SF">Per SF</asp:ListItem>
                            <asp:ListItem Value="/Acre">Per Acre</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="item small-bs left">
                        <asp:DropDownList ID="ddlRentPeriod" runat="server">
                            <asp:ListItem Value="/Month">Per Month</asp:ListItem>
                            <asp:ListItem Value="/Week">Per Week</asp:ListItem>
                            <asp:ListItem Value="/Day">Per Day</asp:ListItem>
                            <asp:ListItem Value="/Year">Per Year</asp:ListItem>
                            <asp:ListItem Value="/Hour">Per Hour</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="item submit">
                        <input type="submit" value="Next">
                        <ul>
                            <li><a href="<%= ResolveUrl("~/preview.aspx") %>" class="preview" title="Preview" target="_blank">Preview</a></li>
                            <li><a href="<%= ResolveUrl("~/createflyer.aspx/saveindrafts") %>" class="save" title="Save in drafts">Save in drafts</a></li>
                        </ul>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <div class="flyers-icon"></div>
</div>