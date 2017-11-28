<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Message.ascx.cs" Inherits="FlyerMe.Controls.Message" %>
<div class="message" id="divMessage" runat="server" visible="false">
    <div class="content">
        <div class="icon"></div><asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
    </div>
</div>