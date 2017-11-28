<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="FlyerMe.Controls.MasterPageLanding.Header" %>
<%@ Register Src="~/Controls/MasterPage/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc" %>
<%@ Register Src="~/Controls/MasterPage/Menu.ascx" TagName="Menu" TagPrefix="uc" %>
<header>
	<div class="header">
		<div class="logo">
			<a href="<%= RootURL %>" title="<%= clsUtility.ProjectName %>"><img src="<%= ResolveUrl(String.Format("~/images/{0}_logo.svg", clsUtility.ProjectName)) %>" alt="<%= clsUtility.ProjectName %>" /></a>
		</div>
        <div class="logo-fixed">
            <a href="<%= RootURL %>" title="<%= clsUtility.ProjectName %>"><img src="<%= ResolveUrl(String.Format("~/images/{0}_logo_color.svg", clsUtility.ProjectName)) %>" onerror="this.onerror=null; this.src=<%= ResolveUrl(String.Format("~/images/{0}_logo.png", clsUtility.ProjectName)) %>" alt="<%= clsUtility.ProjectName %>"></a>
        </div>
		<nav>
			<a href="#" class="mobile-menu"><span class="line-1"></span><span class="line-2"></span><span class="line-3"></span></a>
			<div class="bg"></div>
			<div class="slide-menu">
				<uc:UserMenu ID="UserMenu" runat="server" />
				<uc:Menu ID="Menu" runat="server" />
			</div>
		</nav>
	</div>
</header>