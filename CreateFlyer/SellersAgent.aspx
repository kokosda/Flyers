<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellersAgent.aspx.cs" Inherits="FlyerMe.CreateFlyer.SellersAgent" MasterPageFile="~/MasterPageAccount.master" Theme="" EnableViewState="false" %>
<%@ Register Src="~/Controls/MasterPageAccount/FlyerMenu.ascx" TagName="FlyerMenu" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <uc:FlyerMenu ID="flyerMenu" runat="server" FlyerTypeDisplayString="Seller’s agent flyer" />
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
        <link href="<%= ResolveUrl("~/css/jquery.ui/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
        <link href="<%= ResolveUrl("~/css/jquery.mcustomscrollbar.css") %>" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=<%= ConfigurationManager.AppSettings["GoogleMapKey"] %>"></script>
        <asp:Literal ID="ltlScripts" runat="server"></asp:Literal>
    </div>
</asp:Content>