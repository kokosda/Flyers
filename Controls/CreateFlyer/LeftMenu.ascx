<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftMenu.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.LeftMenu" %>
<div class="left-menu">
    <ul>
        <% foreach (var item in Items) { %>
       <li><a href="<%= RootURL + BaseAbsoluteUrl + "?step=" + item.Step %>" class="item <%= item.CssClass %>" data-step="<%= item.Step %>"><%= item.Text %>
           <% if (item.IsRequired) { %>
           <span class="required">*</span>
           <% } %>
           </a></li>
        <% } %>
    </ul>
</div>