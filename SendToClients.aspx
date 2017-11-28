<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendToClients.aspx.cs" Inherits="FlyerMe.SendToClients" MasterPageFile="~/MasterPageAccount.master" Theme="" ValidateRequest="false" %>
<%@ Register Src="~/Controls/MasterPageAccount/Popup.ascx" TagPrefix="uc" TagName="Popup" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <div class="flyer-menu">
    	<div class="content">
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="account" id="divAccount" runat="server">
        <form id="form" runat="server" enctype="application/x-www-form-urlencoded">
            <div class="form-content">
                <div class="summary-error" id="divSummaryError" runat="server" visible="false">
                    <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
                </div>
                <fieldset>
                    <legend>Send My Flyer To Clients</legend>
                    <div class="block-text">
                        <div class="title">
                            Send a copy of this flyer to your clients
                        </div>
                        <div class="text">
                            <p>If you like to send a copy of this flyer to your clients, please enter their email addresses below and click "Send To Clients" button.</p>
                            <p><strong>Note:</strong> ONLY send to 10 clients and if you want to send to more, then please save PDF and forward it from your personal email.</p>
                        </div>
                    </div>
                     <div class="block-info">
                        <div class="conteiner">
                            <div class="text">
                                <div class="title" id="divTitle" runat="server"><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></div>
                                <div class="filed" id="divAddress" runat="server"><asp:Literal ID="ltlAddress" runat="server"></asp:Literal></div>
                                <div class="filed" id="divFlyerId" runat="server"><strong>Flyer ID:</strong> <asp:Literal ID="ltlFlyerId" runat="server"></asp:Literal></div>
                                <div class="filed" id="divDeliveryDate" runat="server"><strong>Delivery Date:</strong> <asp:Literal ID="ltlDeliveryDate" runat="server"></asp:Literal></div>
                                <div class="filed" id="divMls" runat="server"><strong>MLS:</strong> <asp:Literal ID="ltlMls" runat="server"></asp:Literal></div>
                                <div class="filed" id="divFlyerType" runat="server"><strong>Flyer Type:</strong> <asp:Literal ID="ltlFlyerType" runat="server"></asp:Literal></div>
                                <div class="filed" id="divStatus" runat="server"><strong>Status:</strong> <asp:Literal ID="ltlStatus" runat="server"></asp:Literal></div>
                                <div class="filed" id="divMarkets" runat="server"><asp:Literal ID="ltlMarkets" runat="server"></asp:Literal></div>
                            </div>
                        </div>
                        <div class="image">
                            <div class="table-cell">
                                <asp:Image ID="imgPhoto1" runat="server" />
                            </div>
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
                        <asp:Button ID="btnSendToClients" runat="server" Text="Send To Clients" OnClick="btnSendToClients_Click" />
                        <asp:HiddenField ID="hfOrderId" runat="server" />
                    </div>
                </fieldset>
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