<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Unsubscribe.aspx.cs" Inherits="FlyerMe.Unsubscribe" Async="true" %>
<%@ Register Src="~/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:Message ID="message" runat="server" />
    <div class="text-content" id="divContentUnsubscribe" runat="server">
        <div class="content subscribe">
            <h1>Unsubscribe</h1>
            <p>You may choose to stop receiving Real Estate Email Flyer Campaigns and other email communications from <%= clsUtility.SiteBrandName %>. Simply fill in the form below and click on "Unsubsrcibe" button.</p>
            <p class="red"><strong>Note:</strong> Please enter your information accurately. Make sure you provide exact email address where you receive our communications.</p>
            <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                <div class="form-content">
                    <div class="item">
                        <label>Email<span class="required">*</span></label>
                        <input type="text" id="inputEmail" runat="server" data-clientname="Email" />
                    </div>
                    <asp:Button ID="btnUnsubscribe" runat="server" Text="Unsubscribe" OnClick="btnUnsubscribe_Click" />
	            </div>
            </form>
            <p>&nbsp;</p>
            <p>Your privacy is taken seriously. If you have received email from us after <strong>48 hours</strong> of unsubscribing, please forward the message to <strong><%= clsUtility.AbuseEmail %></strong>. It will be investigated.</p>
	    </div>
    </div>
    <div class="sendm" id="divUnsubscibeByIdAndSecretPreview" runat="server" visible="false">
    	<h1>Almost there</h1>
        <div class="text">
            <p>You are about to stop receiving Real Estate Email Flyer Campaigns and other email communications from <%= clsUtility.SiteBrandName %>.</p>
            <p>Your privacy is taken seriously. If you have received email from us after <strong>48 hours</strong> of unsubscribing, please forward the message to <strong><%= clsUtility.AbuseEmail %></strong>. It will be investigated.</p>
            <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                <p><asp:LinkButton ID="lbUnsubscribeByIdAndSecret" runat="server" OnCommand="lbUnsubscribeByIdAndSecret_Command">Unsubscribe</asp:LinkButton></p>
            </form>
        </div>
    </div>
    <div class="sendm" id="divSubscibeByIdAndSecret" runat="server" visible="false">
    	<div class="icon ok"></div>
    	<h1>Done</h1>
        <div class="text">
            <p>You have been successfully unsubscribed. Please allow 48 hours for this to take effect. Thank you.</p>
            <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                <p><asp:LinkButton ID="lbSubscribeByIdAndSecret" runat="server" OnCommand="lbSubscribeByIdAndSecret_Command">Recover subscribtion</asp:LinkButton></p>
            </form>
        </div>
    </div>
</asp:Content>