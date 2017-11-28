<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddComment.aspx.cs" Inherits="FlyerMe.Admin.Flyers.AddComment" Theme="" MasterPageFile="~/Admin/MasterPagePopup.master" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <uc:Message ID="message" runat="server" />
    <form method="post" enctype="application/x-www-form-urlencoded" runat="server">	
        <div class="content">
		    <div class="item text">
                <textarea id="textareaComment" runat="server"></textarea>
		    </div>
            <div class="item">
                <div class="test-input">Last modified on: <asp:Literal ID="ltlLastModifiedOn" runat="server">No Comments Yet!</asp:Literal></div>
			</div>
        </div>
        <div class="item-actions">
            <asp:Button runat="server" OnCommand="save_Command" CssClass="save" Text="Save comment" />
            <asp:Button runat="server" CssClass="close" Text="Close" />
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