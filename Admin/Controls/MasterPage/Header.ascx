<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="FlyerMe.Admin.Controls.MasterPage.Header" %>
<header>
    <div class="logo">
        <a href="<%= ResolveUrl("~/") %>" title="<%= clsUtility.ProjectName %>"><img src="<%= ResolveUrl("~/admin/images/FlyerMe_logo_color.svg") %>" onerror="this.onerror=null; this.src=<%= ResolveUrl("~/admin/images/FlyerMe_logo_color.png") %>" alt="<%= clsUtility.ProjectName %>" ></a>
    </div>
    <div class="menu">
        <asp:HyperLink ID="hlProfile" NavigateUrl="~/admin/profile.aspx" runat="server" CssClass="user"><%= FirstName %></asp:HyperLink>
        <asp:HyperLink ID="hlReports" NavigateUrl="~/admin/reports.aspx" runat="server" CssClass="user">Reports</asp:HyperLink>
        <asp:HyperLink ID="hlLogOut" NavigateUrl="~/admin/logout.aspx" runat="server" CssClass="logout">Log Out</asp:HyperLink>
    </div>
</header>