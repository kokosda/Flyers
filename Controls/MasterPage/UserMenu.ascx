<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserMenu.ascx.cs" Inherits="FlyerMe.Controls.MasterPage.UserMenu" %>
<% if (Request.IsAuthenticated) { %>
	<ul class="user-login-menu">
		<li><a href="<%= RootURL %>profile.aspx" class="user" title="<%= VisibleUsername %>"><%= VisibleUsername %> 
             <div class="avatar">
                <div class="image">
                    <div class="table-cell">
                        <asp:Image ID="imgUserPicture" runat="server" />
                    </div>
                </div>
            </div>
		    </a>
			<ul>
				<li><a href="<%= RootURL %>myflyers.aspx" title="My flyers">My flyers<span id="spanIncompleteFlyers" runat="server"></span></a></li>
				<li><a href="<%= RootURL %>profile.aspx" title="Edit profile">Edit profile</a></li>
				<li><a href="<%= RootURL %>cart.aspx" title="Cart">Cart</a></li>
				<li><a href="<%= RootURL %>signout.aspx" class="out" title="Sign out">Sign out</a></li>
			</ul>
		</li>
	</ul>
<% } %>
<% else { %>
	<ul class="user-anonym-menu">
		<li><a href="<%= RootURL %>login.aspx" class="login" title="Log In">Log In</a></li>
		<li><a href="<%= RootURL %>signup.aspx" class="signup" title="Sign Up Free">Sign Up Free</a></li>
	</ul>
<% } %>