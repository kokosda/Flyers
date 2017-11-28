<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogIn.ascx.cs" Inherits="FlyerMe.Controls.MasterPage.LogIn" %>
<%@ Import Namespace="FlyerMe" %>
<a href="#" class="close">X</a>
<h1>Log In</h1>
<div class="social">
    <div class="text center">WITH</div>
    <a href="#" class="facebook" onclick="(function(e){ masterPage.socksHelper.onFacebookLoginClick(e); })(event)">FACEBOOK</a>
    <a href="#" class="linkedin" onclick="(function(e){ masterPage.socksHelper.onLinkedInLoginClick(e); })(event)">LINKEDIN</a>
    <div class="rs"><span>OR</span></div>
</div>
<form method="post" enctype="application/x-www-form-urlencoded" action="<%= RootURL.ToUri().UrlToHttps() %>login.aspx">
    <div class="form-content">
        <div class="item big">
            <label>Email</label>
            <input type="email" name="UserName" />
        </div>
        <div class="item big">
            <label>Password</label>
            <input type="password" name="Password" />
        </div>
        <div class="chekbox big">
            <input type="checkbox" id="remember" name="Remember" checked><label for="remember">Remember Me</label>
        </div>
        <div class="item submit center">
            <input type="submit" value="Log In"  />
        </div>
    </div>
</form>
<ul class="links">
    <li><a href="<%= RootURL %>recoverpassword.aspx" class="recoverpassword">Forgot password?</a></li>
    <li><a href="<%= RootURL %>signup.aspx" class="signup">Not a member yet?</a></li>
</ul>