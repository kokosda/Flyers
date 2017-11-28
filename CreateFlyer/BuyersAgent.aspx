<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuyersAgent.aspx.cs" Inherits="FlyerMe.CreateFlyer.BuyersAgent" MasterPageFile="~/MasterPageAccount.master" Theme="" EnableViewState="false" %>
<%@ Register Src="~/Controls/MasterPageAccount/FlyerMenu.ascx" TagName="FlyerMenu" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <uc:FlyerMenu ID="flyerMenu" runat="server" FlyerTypeDisplayString="Buyer’s agent flyer" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAfterFooter" runat="server">
    <div id="popup_bg" style="display:none;"></div>
    <div id="popup_preview" style="display:none;">
        <div class="content" data-mcs-theme="minimal-dark"></div>
        <a href class="close"></a>
    </div>
</asp:Content>
<asp:Content ID="contentScripts" ContentPlaceHolderID="cphScripts" runat="server">
    <div style="display:none;">
        <link href="<%= ResolveClientUrl("~/css/jquery.ui/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
        <link href="<%= ResolveClientUrl("~/css/jquery.mcustomscrollbar.css") %>" rel="stylesheet" type="text/css" />
        <asp:Literal ID="ltlScripts" runat="server"></asp:Literal>
    </div>
</asp:Content>