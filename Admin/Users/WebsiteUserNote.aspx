<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebsiteUserNote.aspx.cs" Inherits="FlyerMe.Admin.Users.WebsiteUserNote" Theme="" MasterPageFile="~/Admin/MasterPagePopup.master" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <uc:Message ID="message" runat="server"></uc:Message>
	<form method="post" enctype="multipart/form-data" runat="server">
        <div class="contnet">
			<div class="item text">
				<label>Note</label>
                <textarea id="textareaNote" runat="server"></textarea>
			</div>
			<div class="item-actions">
				<asp:Button runat="server" OnCommand="save_Command" CssClass="save" Text="Save"></asp:Button>
                <asp:Button runat="server" CssClass="close" Text="Close" />
			</div>
        </div>
	</form>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript">
        (function ($, w) {
            $(function () {
                $("input.close").off().on("click", function () {
                    w.close();
                });
            });
        }(jQuery, window));
    </script>
</asp:Content>