<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="FlyerMe.Admin.Controls.Flyers.Edit" %>
<%@ Import Namespace="FlyerMe" %>
<%@ Import Namespace="FlyerMe.SpecialExtensions" %>
<form method="post" runat="server" enctype="application/x-www-form-urlencoded">
    <div class="back save cancel">
        <asp:LinkButton ID="lbSaveAndBackToModifyTop" runat="server" OnCommand="save_Command" CommandArgument='<%# Eval("order_id") %>' CssClass="save"><span></span>Save and back to modify flyer</asp:LinkButton>
        <asp:HyperLink runat="server" NavigateUrl='<%# "~/admin/flyers/details.aspx?orderid=" + Eval("order_id") %>' CssClass="cancel">Cancel</asp:HyperLink>
    </div>
    <div class="texts">
        <div class="box-item">
            <div class="item">
                <label>Flyer ID</label>
                <div class="test-input"><%# Eval("order_id") %></div>
            </div>
            <div class="item">
                <label>Customer Email</label>
                <input type="text" value="<%# Eval("customer_id") %>" name="customer_id" />
            </div>
            <div class="item">
                <label>Type</label>
                <asp:DropDownList ID="ddlTypes" runat="server" SelectedValue='<%# (Eval("type") as String).Capitalize() %>'>
                    <asp:ListItem Value="Seller" Text="Seller"></asp:ListItem>
                    <asp:ListItem Value="Buyer" Text="Buyer"></asp:ListItem>
                    <asp:ListItem Value="Custom" Text="Custom"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="item">
                <label>Market State</label>
                <input type="text" value="<%# Eval("market_state") %>" name="market_state" />
            </div>
            <div class="item">
                <label>Market County</label>
                <input type="text" value="<%# Eval("market_county") %>" name="market_county" />
            </div>
            <div class="item">
                <label>Market Association</label>
                <input type="text" value="<%# Eval("market_association") %>" name="market_association" />
            </div>
            <div class="item">
                <label>Market MSA</label>
                <input type="text" value="<%# Eval("market_msa") %>" name="market_msa" />
            </div>
            <div class="item">
                <label>Total Price</label>
                <input type="text" value="<%# Eval("tota_price") %>" name="tota_price" />
            </div>
            <div class="item">
                <label>Invoice Tax %</label>
                <input type="text" value="<%# Eval("invoice_tax") %>" name="invoice_tax" />
            </div>
            <div class="item">
                <label>Trancaction ID</label>
                <input type="text" value="<%# Eval("invoice_transaction_id") %>" name="invoice_transaction_id" />
            </div>
            <div class="item">
                <label>Status</label>
                <asp:DropDownList ID="ddlStatus" runat="server" SelectedValue='<%# (Eval("status") as String).Capitalize() %>'>
                    <asp:ListItem Value="Scheduled">Scheduled</asp:ListItem>
                    <asp:ListItem Value="Sent">Sent</asp:ListItem>
                    <asp:ListItem Value="Queued">Queued</asp:ListItem>
                    <asp:ListItem Value="Pending_Approval">Pending Approval</asp:ListItem>
                    <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="item">
                <label>Layout</label>
                <input type="text" value="<%# Eval("layout") %>" name="layout" />
            </div>
            <div class="item">
                <label>Delivery Date</label>
                <input type="text" value="<%# Eval("delivery_date").GetDateStringFromObject() %>" name="delivery_date" data-date readonly /><a class="date" href=""><span></span>23</a>
            </div>
            <div class="item">
                <label>MLS Number</label>
                <input type="text" value="<%# Eval("mls_number") %>" name="mls_number" />
            </div>
        </div>
        <div class="box-item">
            <div class="item">
                <label>Email Subject</label>
                <input type="text" value="<%# Eval("email_subject") %>" name="email_subject" />
            </div>
            <div class="item">
                <label>Title</label>
                <input type="text" value="<%# Eval("title").GetTrimmedStringFromObject() %>" name="title" />
            </div>
            <div class="item">
                <label>Property Category</label>
                <asp:DropDownList ID="ddlPropertyCategory" AppendDataBoundItems="true" runat="server" SelectedValue='<%# Eval("fk_PropertyCategory") %>' DataSourceID="sdsPropertyCategory" DataTextField="Category" DataValueField="CategoryID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsPropertyCategory" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT 0  as [CategoryID], '' as [Category] Union SELECT [CategoryID], [Category] FROM [fly_PropertyCategory]"></asp:SqlDataSource>
            </div>
            <div class="item">
                <label>Property Type</label>
                <asp:DropDownList ID="ddlPropertyType" AppendDataBoundItems="true" runat="server" SelectedValue='<%# Eval("fk_PropertyType") %>' DataSourceID="sdsPropertyType" DataTextField="PropertyType" DataValueField="PropertyTypeID"></asp:DropDownList>
                <asp:SqlDataSource ID="sdsPropertyType" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>"
                SelectCommand="SELECT 0 as [PropertyTypeID], '' as [PropertyType] Union SELECT [PropertyTypeID], [PropertyType] FROM [fly_PropertyType]"></asp:SqlDataSource>
            </div>
            <div class="item">
                <label>Property Address</label>
                <input type="text" value="<%# Eval("prop_address1").GetTrimmedStringFromObject() %>" name="prop_address1" />
            </div>
            <div class="item">
                <label>Apt, Suite, Bldg.</label>
                <input type="text" value="<%# Eval("AptSuiteBldg") %>" name="AptSuiteBldg" />
            </div>
            <div class="item">
                <label>Property City</label>
                <input type="text" value="<%# Eval("prop_city").GetTrimmedStringFromObject() %>" name="prop_city" />
            </div>
            <div class="item">
                <label>Property State</label>
                <input type="text" value="<%# Eval("prop_state") %>" name="prop_state" />
            </div>
            <div class="item">
                <label>Property Zip Code</label>
                <input type="text" value="<%# Eval("prop_zipcode") %>" name="prop_zipcode" />
            </div>
            <div class="item">
                <label>Open Houses</label>
                <input type="text" value="<%# Eval("OpenHouses") %>" name="OpenHouses" />
            </div>
            <div class="item text">
                <label>Property Description</label>
                <input type="text" value="<%# Eval("prop_desc") %>" name="prop_desc" />
            </div>
            <div class="item">
                <label>Property Price</label>
                <input type="text" value="<%# Eval("prop_price") %>" name="prop_price" />
            </div>
        </div>
        <div class="item text big">
            <label>Markup</label>
            <div id="editor">
            </div>
            <textarea name="markup" style="display:none;"><%# Eval("markup") %></textarea>
        </div>
    </div>
    <div class="back save cancel">
	    <asp:LinkButton ID="lbSaveAndBackToModifyBottom" runat="server" OnCommand="save_Command" CommandArgument='<%# Eval("order_id") %>' CssClass="save"><span></span>Save and back to modify flyer</asp:LinkButton>
        <a href="#" class="cancel">Cancel</a>
    </div>
</form>