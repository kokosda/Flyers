<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="FlyerMe.Admin.Controls.MasterPagePopup.Header" %>
<header>
    <div class="logo">
        <a href="/" title="<%= clsUtility.ProjectName %>"><img src="<%= ResolveUrl("~/admin/images/FlyerMe_logo_color.svg") %>" onerror="this.onerror=null; this.src=<%= ResolveUrl("~/admin/images/FlyerMe_logo_color.png") %>" alt="<%= clsUtility.ProjectName %>" ></a>
    </div>
</header>