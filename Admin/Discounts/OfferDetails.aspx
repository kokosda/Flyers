<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfferDetails.aspx.cs" Inherits="FlyerMe.Admin.Discounts.OfferDetails" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
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
                                <label>In</label>
                                <asp:DropDownList ID="ddlIn" runat="server">
                                    <asp:ListItem Value="False">Value, $</asp:ListItem>
                                    <asp:ListItem Value="True">Percentage, %</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="item">
                                <label>Email<span class="required">*</span></label>
                                <input type="text" id="inputEmail" runat="server" required />
                            </div>
                        </div>
                        <div class="item-box">
                            <div class="item">
                                <label>Discount<span class="required">*</span></label>
                                <input type="text" id="inputDiscount" runat="server" />
                            </div>
                            <div class="item">
                                <label>Code<span class="required">*</span></label>
                                <input type="text" id="inputCode" runat="server" required />
                            </div>                             
                        </div>
                        <div class="item-submit">
                            <asp:Button ID="btnAddOfferDiscountDetails" runat="server" Text="Add Offer Discount Details" OnCommand="btnAddOfferDiscountDetails_Command" />
                        </div>
                    </div>
                    <uc:Grid id="grid" runat="server" PageSize="7" PageName="admin/discounts/offerdetails.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" OnRowEditing="grid_RowEditing" OnRowEdited="grid_RowEdited" OnRowEditCanceled="grid_RowEditCanceled" OnRowDeleted="grid_RowDeleted" />
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
                    return confirm("Are you sure, you want to delete this discount code?");
                });
            });
        })(jQuery);
    </script>
</asp:Content>