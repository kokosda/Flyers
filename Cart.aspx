<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="FlyerMe.Cart" MasterPageFile="~/MasterPageAccount.master" Theme="" Async="true" %>
<%@ Register Src="~/Controls/MasterPageAccount/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc" %>
<%@ Import Namespace="FlyerMe" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <uc:UserMenu ID="userMenu" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="message" id="divCartSummary" runat="server" visible="false">
        <div class="content">
            <div class="icon"></div><asp:Literal ID="ltlCartSummary" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="account">
        <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form" action="cart.aspx">
            <div class="form-content">
                <% if (IsEmpty) { %>
                <fieldset>
                    <legend>Your cart is empty</legend>
                    <div class="total center">Click Create Flyer button to add something to your cart.</div>
					<div class="center">
                        <asp:HyperLink runat="server" NavigateUrl="~/createflyer.aspx" CssClass="start">Create Flyer</asp:HyperLink>
					</div>
                </fieldset>
                <% } else { %>
                <fieldset>
                    <legend>Transaction History</legend>
                    <table class="history">
                        <thead>
                            <th>&nbsp;</th>
                            <th>FlyerID</th>
                            <th>Flyer Title</th>
                            <th>Total</th>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptTransactionHistory" runat="server" OnItemDataBound="RptTransactionHistory_ItemDataBound">
                                <ItemTemplate>
                                    <tr id="tr" runat="server">
                                        <td><input type="checkbox" id="checkbox" runat="server"/><asp:Label runat="server" AssociatedControlID="checkbox"></asp:Label></td>
                                        <td><asp:Literal ID="ltlOrder" runat="server"></asp:Literal></td>
                                        <td><asp:Literal ID="ltlFlyer" runat="server"></asp:Literal></td>
                                        <td><asp:Literal ID="ltlTotal" runat="server"></asp:Literal></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <div class="item">
                        <input type="hidden" name="OrderIds" value=""/>
                    </div>
                    <% if (rptTransactionHistory.Items.Count > 5) { %>
                    <a href="#fulltransactionhistory" class="more">View More</a>
                    <% } %>
                </fieldset>
                <fieldset>
                    <legend><a name="purchasesummary"></a>Purchase Summary</legend>
                    <table class="sumary">
                        <thead>
                            <th>&nbsp;</th>
                            <th>&nbsp;</th>
                            <th>&nbsp;</th>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <div class="total">                        
                        <div class="sub" id="divTaxRate" runat="server">
                            Tax (<label><asp:Literal ID="ltlTaxRate" runat="server"></asp:Literal></label>): <strong>$0.00</strong>
                        </div>                        
                        <div class="sub" id="divDiscount" runat="server" style="display:none;">
                            Discount: <strong>$0.00</strong>
                        </div>                        
                        <div class="sub" id="divSubTotal">
                            Sub-total: <strong>$0.00</strong>
                        </div>
                        <div class="total">
                            Total Price: <strong>$0.00</strong>
                            <input type="hidden" name="TotalPrice" value="0.00" />
                        </div>
                    </div>
                    <div class="coupon">
                        Have any coupon number or promotional claim codes? Enter them here:
                        <div class="item apply">
                            <input type="text" name="PromoCode"/><input type="submit" value="Apply" data-getdiscountrateurl="<%= ResolveUrl("~/cart.aspx/getdiscountrate") %>"/>
                            <label class="error" id="labelCouponMessage" style="display:none;"></label>
                            <input type="hidden" name="PromoCodeIsApplied" value="false" />
                        </div>
                    </div>
                </fieldset>
                <input type="submit" value="Free of Charge!" style="display:none;" id="inputMakePayment" />
                <% } %>
            </div>
        </form>
    </div>
    <link href="<%= ResolveUrl("~/css/jquery.mcustomscrollbar.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript">
        window.stripeCom = {
            src: 'https://checkout.stripe.com/checkout.js',
            cssClass: 'stripe-button',
            'data-key': '<%= clsUtility.StripePublishableApiKey %>',
            'data-image': '<%= ResolveUrl("~/images/flyermy_logo_icon.png") %>',
            'data-name': '<%= clsUtility.SiteBrandName %>',
            'data-description': null,
            'data-amount': null,
            'data-locale': 'en',
            'data-billing-address': true,
            'data-email': '<%= User.Identity.Name %>',
            'data-currency': 'USD',
            'data-zip-code': true
        };
    </script>
</asp:Content>