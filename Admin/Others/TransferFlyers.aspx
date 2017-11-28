<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransferFlyers.aspx.cs" Inherits="FlyerMe.Admin.Others.TransferFlyers" Theme="" MasterPageFile="~/Admin/MasterPage.master" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form runat="server" method="post" enctype="multipart/form-data">
                    <div class="search" id="divSearch" runat="server">
                        <div class="message warning">Please take a backup of your database before transfer</div>
                        <uc:Message ID="message" runat="server" />
                        <div class="item-box big">
                            <div class="item">
                                <div class="select-item">
                                    <div class="item left label">
                                        <label>Transfer Flyers Older Than</label>
                                    </div>
                                    <div class="item right">
                                         <asp:DropDownList ID="ddlDays" runat="server">
                                            <asp:ListItem Value="120" Text="120"></asp:ListItem>
                                            <asp:ListItem Value="150" Text="150"></asp:ListItem>
                                            <asp:ListItem Value="180" Text="180"></asp:ListItem>
                                            <asp:ListItem Value="210" Text="210"></asp:ListItem>
                                            <asp:ListItem Value="240" Text="240"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="item-submit">
                                <asp:Button ID="btnTransferOrders" runat="server" Text="Transfer Flyers" OnCommand="btnTransferOrders_Command" />
                            </div>
                        </div>
                    </div>
                    <div class="indent"></div>
                    <uc:Grid id="grid" runat="server" PageSize="7" PageName="admin/others/transferflyers.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" TableNews="true" />
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
                $(".item-submit input[type='submit']").on("click", function (e) {
                    return confirm("Are you sure, you want to transfer flyers? Please take a backup of your database before transfer.");
                });
            });
        })(jQuery);
    </script>
</asp:Content>