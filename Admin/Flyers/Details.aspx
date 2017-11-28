<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="FlyerMe.Admin.Flyers.Details" Theme="" MasterPageFile="~/Admin/MasterPage.master" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/Flyers/Details.ascx" TagPrefix="uc" TagName="Details" %>
<%@ Register Src="~/Admin/Controls/Flyers/Edit.ascx" TagPrefix="uc" TagName="Edit" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <asp:Repeater ID="rpt" runat="server" DataSourceID="sds" OnItemDataBound="rpt_ItemDataBound">
                    <ItemTemplate>
                        <uc:Details ID="details" runat="server" Visible="false" />
                        <uc:Edit ID="edit" runat="server" Visible="false" />
                    </ItemTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="sds" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>"
                    DeleteCommand="DELETE FROM [fly_order] WHERE [order_id] = @order_id" 
                    SelectCommand="SELECT [order_id], [customer_id], [type], [market_state], [market_county], [market_association],  [market_msa], [tota_price], [invoice_tax], [invoice_transaction_id], [status],  [headline], [layout], [delivery_date], [mls_number], [email_subject],  [theme], [title], [sub_title], [prop_address1], [prop_address2], [prop_city], [prop_state], [prop_zipcode], [prop_desc], [prop_price], [use_mls_logo], [use_housing_logo],  [markup], fk_PropertyCategory, fk_PropertyType, Category, PropertyType, AptSuiteBldg, OpenHouses FROM  [fly_order] left outer join fly_PropertyCategory  on fly_order.fk_PropertyCategory=fly_PropertyCategory.CategoryID left outer join fly_PropertyType  on fly_order.fk_PropertyType=fly_PropertyType.PropertyTypeID WHERE ([order_id] = @order_id)" 
                    UpdateCommand="UPDATE [fly_order] SET [customer_id] = @customer_id, [type] = @type, [market_state] = @market_state, [market_county] = @market_county, [market_association]=@market_association,  [market_msa]=@market_msa ,[tota_price] = @tota_price, [invoice_tax] = @invoice_tax, [invoice_transaction_id] = @invoice_transaction_id, [status] = @status, [headline] = @headline, [layout] = @layout, [delivery_date] = @delivery_date, [mls_number] = @mls_number, [email_subject] = @email_subject, [theme] = @theme, [title] = @title, [sub_title] = @sub_title, [prop_address1] = @prop_address1, [prop_address2] = @prop_address2, [prop_city] = @prop_city, [prop_state] = @prop_state, [prop_zipcode] = @prop_zipcode, [prop_desc] = @prop_desc, [prop_price] = @prop_price, [use_mls_logo] = @use_mls_logo, [use_housing_logo] = @use_housing_logo, [markup] = @markup, fk_PropertyCategory=@fk_PropertyCategory, fk_PropertyType=@fk_PropertyType, AptSuiteBldg = @AptSuiteBldg, OpenHouses = @OpenHouses WHERE [order_id] = @order_id">
                    <DeleteParameters>
                        <asp:Parameter Name="order_id" Type="Int64" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="customer_id" Type="String" />
                        <asp:Parameter Name="type" Type="String" />
                        <asp:Parameter Name="market_state" Type="String" />
                        <asp:Parameter Name="market_county" Type="String" />
                        <asp:Parameter Name="market_association" Type="String" />
                        <asp:Parameter Name="market_msa" Type="String" />
                        <asp:Parameter Name="tota_price" Type="Decimal" DefaultValue="0M" />
                        <asp:Parameter Name="invoice_tax" Type="Decimal" DefaultValue="0M" />
                        <asp:Parameter Name="invoice_transaction_id" Type="String" />
                        <asp:Parameter Name="status" Type="String" />
                        <asp:Parameter Name="headline" Type="String" />
                        <asp:Parameter Name="layout" Type="String" />
                        <asp:Parameter Name="delivery_date" Type="DateTime" />
                        <asp:Parameter Name="mls_number" Type="String" />
                        <asp:Parameter Name="email_subject" Type="String" />
                        <asp:Parameter Name="theme" Type="String" />
                        <asp:Parameter Name="title" Type="String" />
                        <asp:Parameter Name="sub_title" Type="String" />
                        <asp:Parameter Name="prop_address1" Type="String" />
                        <asp:Parameter Name="prop_address2" Type="String" />
                        <asp:Parameter Name="prop_city" Type="String" />
                        <asp:Parameter Name="prop_state" Type="String" />
                        <asp:Parameter Name="prop_zipcode" Type="String" />
                        <asp:Parameter Name="prop_desc" Type="String" />
                        <asp:Parameter Name="prop_price" Type="String" />
                        <asp:Parameter Name="use_mls_logo" Type="Boolean" DefaultValue="True" />
                        <asp:Parameter Name="use_housing_logo" Type="Boolean" DefaultValue="True" />
                        <asp:Parameter Name="markup" Type="String" />
                        <asp:Parameter Name="fk_PropertyCategory" />
                        <asp:Parameter Name="fk_PropertyType" />
                        <asp:Parameter Name="AptSuiteBldg" Type="String" />
                        <asp:Parameter Name="OpenHouses" Type="String" />
                        <asp:Parameter Name="order_id" Type="Int64" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter Name="order_id" QueryStringField="orderid" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <link href="<%= ResolveUrl("~/css/jquery.ui/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>