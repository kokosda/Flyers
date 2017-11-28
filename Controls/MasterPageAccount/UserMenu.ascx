<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserMenu.ascx.cs" Inherits="FlyerMe.Controls.MasterPageAccount.UserMenu" %>
<div class="user-menu">
	<ul>
		<li><a href="<%= RootURL %>myflyers.aspx" 
			<% if (Page.ToString().IndexOf("myflyers", StringComparison.OrdinalIgnoreCase) >= 0) { %> 
				class="active" 
			<% } %> 
			title="My Flyers">My Flyers</a></li>
		<li><a href="<%= RootURL %>profile.aspx"
			<% if (Page.ToString().IndexOf("profile", StringComparison.OrdinalIgnoreCase) >= 0) { %> 
				class="active"
			<% } %> 
			 title="Profile">Profile</a></li>
		<li><a href="<%= RootURL %>cart.aspx" 
			<% if (Page.ToString().IndexOf("cart", StringComparison.OrdinalIgnoreCase) >= 0) { %>
				class="active" 
			<% } %> 
			title="Cart">Cart</a></li>
	</ul>
</div>