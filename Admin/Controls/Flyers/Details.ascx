<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Details.ascx.cs" Inherits="FlyerMe.Admin.Controls.Flyers.Details" %>
<%@ Import Namespace="FlyerMe" %>
<%@ Import Namespace="FlyerMe.SpecialExtensions" %>
<form method="post" runat="server" enctype="application/x-www-form-urlencoded">
    <div class="back">
        <asp:HyperLink ID="hlBackToManageOrderTop" runat="server"><span></span>Back to Flyers</asp:HyperLink>
    </div>
    <div class="block-menu">
        <asp:HyperLink ID="hlSendCustomerCopyTop" runat="server" Target="_blank" CssClass="send"><span></span>Send customer copy</asp:HyperLink>
        <ul>
            <li><a href="" id="aAddCommentsTop" runat="server" class="addcoments"><span></span>Add comments</a></li>
            <li><asp:HyperLink runat="server" CssClass="edit" NavigateUrl='<%# "~/admin/flyers/details.aspx?orderid=" + Eval("order_id") + "&edit=true" %>'><span></span>Edit</asp:HyperLink></li>
            <li><asp:LinkButton runat="server" OnCommand="delete_Command" CommandArgument='<%# Eval("order_id") %>' CssClass="delete"><span></span>Delete</asp:LinkButton></li>
        </ul>
    </div>
    <div class="message" id="divMessage" runat="server" visible="false"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
    <div class="texts">
        <div class="box-item">
            <div class="item">
                <div class="label">Flyer ID</div><div class="content"><%# Eval("order_id") %></div>
            </div>
            <div class="item">
                <div class="label">Customer Email</div><div class="content"><%# Eval("customer_id") %></div>
            </div>
            <div class="item">
                <div class="label">Type</div><div class="content"><%# Eval("type") %></div>
            </div>
            <div class="item">
                <div class="label">Market State</div><div class="content"><%# Eval("market_state") %></div>
            </div>
            <div class="item">
                <div class="label">Market County</div><div class="content"><%# Eval("market_county") %></div>
            </div>
            <div class="item">
                <div class="label">Market Association</div><div class="content"><%# Eval("market_association") %></div>
            </div>
            <div class="item">
                <div class="label">Market MSA</div><div class="content"><%# Eval("market_msa") %></div>
            </div>
            <div class="item">
                <div class="label">Total Price</div><div class="content"><%# Eval("tota_price").GetPriceStringFromObject() %></div>
            </div>
            <div class="item">
                <div class="label">Invoice Tax %</div><div class="content"><%# Eval("invoice_tax").GetDecimalStringFromObject("0.00") %></div>
            </div>
            <div class="item">
                <div class="label">Trancaction ID</div><div class="content"><%# Eval("invoice_transaction_id") %></div>
            </div>
            <div class="item">
                <div class="label">Status</div><div class="content"><asp:Literal ID="ltlStatus" runat="server" Text='<%# (Eval("Status") as String).Capitalize() %>'></asp:Literal></div>
            </div>
            <div class="item">
                <div class="label">Layout</div><div class="content"><%# Eval("layout") %></div>
            </div>
            <div class="item">
                <div class="label">Delivery Date</div><div class="content"><%# Eval("delivery_date").GetDateStringFromObject() %></div>
            </div>
            <div class="item">
                <div class="label">MLS Number</div><div class="content"><%# Eval("mls_number") %></div>
            </div>
        </div>
        <div class="box-item">
            <div class="item">
                <div class="label">Email Subject</div><div class="content"><%# Eval("email_subject") %></div>
            </div>
            <div class="item">
                <div class="label">Title</div><div class="content"><%# Eval("title") %></div>
            </div>
            <div class="item">
                <div class="label">Property Category</div><div class="content"><%# Eval("Category") %></div>
            </div>
            <div class="item">
                <div class="label">Property Type</div><div class="content"><%# Eval("PropertyType") %></div>
            </div>
            <div class="item">
                <div class="label">Property Address</div><div class="content"><%# Eval("prop_address1") %></div>
            </div>
            <div class="item">
                <div class="label">Apt, Suite, Bldg.</div><div class="content"><%# Eval("AptSuiteBldg") %></div>
            </div>
            <div class="item">
                <div class="label">Property City</div><div class="content"><%# Eval("prop_city") %></div>
            </div>
            <div class="item">
                <div class="label">Property State</div><div class="content"><%# Eval("prop_state") %></div>
            </div>
            <div class="item">
                <div class="label">Property Zip Code</div><div class="content"><%# Eval("prop_zipcode") %></div>
            </div>
            <div class="item">
                <div class="label">Open Houses</div><div class="content"><%# Eval("OpenHouses") %></div>
            </div>
            <div class="item text">
                <div class="label">Property Description</div><div class="content"><%# Eval("prop_desc") %></div>
            </div>
            <div class="item">
                <div class="label">Property Price</div><div class="content"><%# Eval("prop_price") %></div>
            </div>
        </div>
        <div class="item-submit">
            <asp:Button ID="btnApproveFlyer" runat="server" OnCommand="approveFlyer_Command" CommandArgument='<%# Eval("order_id") %>' Text="Approve Flyer" />
            <asp:Button ID="btnRescheduleFlyer" runat="server" OnCommand="rescheduleFlyer_Command" CommandArgument='<%# Eval("order_id") %>' Text="Reschedule Flyer" />
        </div>
    </div>
    <div class="flyercontent" runat="server" visible='<%# (Eval("markup") as String).HasText() %>'>
        <%# Eval("markup") %>
    </div>
    <div class="block-menu">
        <asp:HyperLink ID="hlSendCustomerCopyBottom" runat="server" Target="_blank" CssClass="send"><span></span>Send customer copy</asp:HyperLink>
        <ul>
            <li><a href="" id="aAddCommentsBottom" runat="server" class="addcoments"><span></span>Add comments</a></li>
            <li><asp:HyperLink runat="server" CssClass="edit" NavigateUrl='<%# "~/admin/flyers/details.aspx?orderid=" + Eval("order_id") + "&edit=true" %>'><span></span>Edit</asp:HyperLink></li>
            <li><asp:LinkButton runat="server" OnCommand="delete_Command" CommandArgument='<%# Eval("order_id") %>' CssClass="delete"><span></span>Delete</asp:LinkButton></li>
        </ul>
    </div>
    <div class="back">
        <asp:HyperLink ID="hlBackToManageOrderBottom" runat="server"><span></span>Back to Flyers</asp:HyperLink>
    </div>
</form>