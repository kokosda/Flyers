<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Offer.aspx.cs" Inherits="FlyerMe.Admin.Discounts.Offer" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
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
                                <label>Type</label>
                                <asp:DropDownList ID="ddlDiscountType" runat="server" DataSourceID="sdsDiscountType" DataTextField="DiscountType" DataValueField="pkDiscountTypeID"></asp:DropDownList>
                                <asp:SqlDataSource ID="sdsDiscountType" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>"
                                    SelectCommand="SELECT [pkDiscountTypeID], [DiscountType] FROM [fly_tblDiscountType]">
                                </asp:SqlDataSource>
                            </div>
                            <div class="item">
                                <label>In</label>
                                <asp:DropDownList ID="ddlIn" runat="server">
                                    <asp:ListItem Value="False">Value, $</asp:ListItem>
                                    <asp:ListItem Value="True">Percentage, %</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="item">
                                <label>&nbsp;</label>
                                <div class="chekbox small right">
                                    <input type="checkbox" id="inputActive" runat="server" checked />
                                    <asp:Label runat="server" AssociatedControlID="inputActive">Active</asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="item-box">
                            <div class="item">
                                <label>Discount<span class="required">*</span></label>
                                <input type="text" id="inputDiscount" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Code<span class="required">*</span></label>
                                <input type="text" id="inputCode" runat="server" required />
                            </div>
                        </div>
                        <div class="item-submit">
                            <asp:Button ID="btnAddOffer" runat="server" Text="Add Offer" OnCommand="btnAddOffer_Command" />
                        </div>
                    </div>
                    <uc:Grid id="grid" runat="server" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" OnRowDeleted="grid_RowDeleted" PagingDisabled="true" />
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript">
        (function ($) {
            $(function () {
                $(".del .close").on("click", function (e) {
                    return confirm("Are you sure, you want to delete this discount offer?");
                });
            });
        })(jQuery);
    </script>
</asp:Content>