<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Popup.ascx.cs" Inherits="FlyerMe.Controls.MasterPage.Popup" %>
<%@ Register Src="~/Controls/MasterPage/LogIn.ascx" TagName="LogIn" TagPrefix="uc" %>
<%@ Register Src="~/Controls/MasterPage/RecoverPassword.ascx" TagName="RecoverPassword" TagPrefix="uc" %>
<div id="popup_bg" style="display:none;">
</div>
<div id="popup" style="display:none;">
    <div id="popup_content">
        <div id="login_popup_content">
            <uc:LogIn ID="LogIn" runat="server" />
        </div>
        <div id="recoverpassword_popup_content">
            <uc:RecoverPassword ID="RecoverPassword" runat="server" />
        </div>
    </div>
</div>