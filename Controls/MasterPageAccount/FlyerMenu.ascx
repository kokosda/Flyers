<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlyerMenu.ascx.cs" Inherits="FlyerMe.Controls.MasterPageAccount.FlyerMenu" %>
<div class="flyer-menu">
    <div class="content">
        <div class="text"><asp:Literal ID="ltlFlyerTypeFriendlyName" runat="server"></asp:Literal></div>
        <ul>
            <li><a href="<%= ResolveUrl("~/preview.aspx") %>" class="preview" title="Preview" target="_blank">Preview</a></li>
            <li><a href="<%= ResolveUrl("~/createflyer.aspx/saveindrafts") %>" class="save" title="Save in drafts">Save in drafts</a></li>
        </ul>
    </div>
</div>