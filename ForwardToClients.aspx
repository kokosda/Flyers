<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForwardToClients.aspx.cs" Inherits="FlyerMe.ForwardToClients" MasterPageFile="~/MasterPageAccount.master" Theme="" ValidateRequest="false" Async="true" %>
<%@ Register Src="~/Controls/MasterPageAccount/Popup.ascx" TagPrefix="uc" TagName="Popup" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <div class="flyer-menu">
    	<div class="content">
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="account" id="divAccount" runat="server">
        <form id="form" runat="server" enctype="application/x-www-form-urlencoded" method="post">
            <div class="form-content">
                <div class="summary-error" id="divSummaryError" runat="server" visible="false">
                    <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
                </div>
                <fieldset>
                    <legend>Send My Flyer To Clients</legend>
                    <div class="block-text">
                        <div class="title">
                            Forward a copy of this flyer to your clients
                        </div>
                        <div class="text">
                            <p>If you like to forward a copy of this flyer to your clients, please enter or import their email addresses below and click "Forward To Clients" button. Flyer will be sent to first 10 email addresses. </p>
                            <p><strong>Note:</strong> ONLY send to 10 clients and if you want to send to more, then please save PDF and forward it from your personal email.</p>
                        </div>
                    </div>
                    <div class="item">
                        <label>Email Subject<span class="required">*</span></label>
                        <input type="text" id="inputEmailSubject" runat="server" data-clientname="EmailSubject" />
                    </div>
                    <div class="item">
                        <label>Email Addresses<span class="required">*</span></label>
                        <div class="filed">
                            <textarea id="textareaEmailAddresses" runat="server" class="plaxo-target" data-clientname="EmailAddresses"></textarea>
                            <div class="description">
                                <strong>Note:</strong> Use commas to separate multiple email addresses.<br />Maximum: 10.
                            </div>
                            <input type="submit" value="Add from my address book" class="add_address_book plaxo-invoker" />
                        </div>
                    </div>
                    <div class="item">
                        <label>Message</label>
                        <div class="filed">
                            <textarea id="textareaMessage" runat="server"></textarea>
                        </div>
                    </div>
                    <div class="item submit_client">
                        <asp:Button ID="btnForwardToClients" runat="server" Text="Forward To Clients" OnClick="btnForwardToClients_Click" />
                        <asp:HiddenField ID="hfOrderId" runat="server" />
                    </div>
                </fieldset>
                <div class="block-mail">
                    <div class="con">
                        <asp:Literal ID="ltlMarkup" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <div id="divSuccess" runat="server" visible="false" class="sendm">
        <div class="icon ok"></div>
    	<h1><asp:Literal ID="ltlSuccessMessage" runat="server"></asp:Literal></h1>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAfterFooter" runat="server">
    <uc:Popup ID="popup" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript" src="http://www.plaxo.com/css/m/js/util.js"></script>
    <script type="text/javascript" src="http://www.plaxo.com/css/m/js/basic.js"></script>
    <script type="text/javascript" src="http://www.plaxo.com/css/m/js/abc_launcher.js"></script>
</asp:Content>