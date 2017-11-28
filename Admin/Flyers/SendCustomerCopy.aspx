<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendCustomerCopy.aspx.cs" Inherits="FlyerMe.Admin.Flyers.SendCustomerCopy" Theme="" MasterPageFile="~/Admin/MasterPagePopup.master" Async="true" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <uc:Message ID="message" runat="server" />
    <form method="post" enctype="application/x-www-form-urlencoded" runat="server">	
        <div class="content">
		    <div class="item">
			    <label>User Email ID</label>
                <div class="test-input"><asp:Literal ID="ltlUserEmailId" runat="server"></asp:Literal></div>
		    </div>
		    <div class="item">
			    <label>Subject</label>
                <div class="test-input"><asp:Literal ID="ltlSubject" runat="server"></asp:Literal></div>
		    </div>
		    <div class="item">
			    <label>Copy(Email)</label>
                <input type="text" id="inputCopyEmail" runat="server" />
		    </div>
		    <div class="item text">
			    <label>Note</label>
                <textarea id="textareaNote" runat="server"></textarea>
		    </div>
        </div>
        <div class="item-actions">
            <asp:Button runat="server" OnCommand="save_Command" CssClass="save" Text="Send customer copy" />
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