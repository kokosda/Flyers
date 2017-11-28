<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlockedEmails.aspx.cs" Inherits="FlyerMe.Admin.Others.BlockedEmails" Theme="" MasterPageFile="~/Admin/MasterPage.master" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form runat="server" method="post" enctype="multipart/form-data">
                    <div class="search" id="divSearch" runat="server">
                        <uc:Message ID="message" runat="server" />
                        <div class="item-box">
                            <div class="item">
                                <label>Email<span class="required">*</span></label>
                                <input type="text" id="inputEmail" runat="server" required />
                            </div>
                        </div>
                        <div class="item-submit">
                            <asp:Button ID="btnAddBlockedEmail" runat="server" Text="Add Blocked Email" OnCommand="btnAddBlockedEmail_Command" />
                        </div>
                    </div>
                    <uc:Grid id="grid" runat="server" PageSize="7" PageName="admin/others/bademails.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" OnRowEdited="grid_RowEdited" OnRowDeleted="grid_RowDeleted" TableNews="true" />
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
                    return confirm("Are you sure, you want to delete this blocked email?");
                });
            });
        })(jQuery);
    </script>
</asp:Content>