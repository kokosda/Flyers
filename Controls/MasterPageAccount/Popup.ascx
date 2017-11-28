<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Popup.ascx.cs" Inherits="FlyerMe.Controls.MasterPageAccount.Popup" %>
<%@ Register Src="~/Controls/MasterPageAccount/GenericProgress.ascx" TagName="GenericProgress" TagPrefix="uc" %>
<%@ Register Src="~/Controls/MasterPageAccount/GenericConfirmation.ascx" TagName="GenericConfirmation" TagPrefix="uc" %>
<div id="popup_bg" style="display:none;">
</div>
<div id="popup" style="display:none;">
    <div id="popup_content">
        <div id="genericprogress_popup_content">
            <uc:GenericProgress ID="GenericProgress" runat="server" />
        </div>
        <div id="genericconfirmation_popup_content">
            <uc:GenericConfirmation ID="GenericConfirmation" runat="server" />
        </div>
    </div>
</div>