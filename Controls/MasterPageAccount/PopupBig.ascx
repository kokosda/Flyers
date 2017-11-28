<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupBig.ascx.cs" Inherits="FlyerMe.Controls.MasterPageAccount.Popup" %>
<%@ Register Src="~/Controls/MasterPageAccount/Thanks.ascx" TagName="Thanks" TagPrefix="uc" %>
<div id="popup_bg" style="display:none;">
</div>
<div id="popup_big" style="display:none;">
    <div id="popup_content">
        <div id="thanks_popup_content">
            <uc:Thanks ID="Thanks" runat="server" />
        </div>
    </div>
</div>