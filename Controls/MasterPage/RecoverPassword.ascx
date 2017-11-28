<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecoverPassword.ascx.cs" Inherits="FlyerMe.Controls.MasterPage.RecoverPassword" %>
<%@ Import Namespace="FlyerMe" %>
<a href="#" class="close">X</a>
<h1>Recover your password</h1>
<form method="post" enctype="application/x-www-form-urlencoded" action="<%= RootURL.ToUri().UrlToHttps() %>recoverpassword.aspx">
    <div class="form-content">
        <div class="item center text">
            Enter your username, or the email address that you used to register. We’ll send you an email with your username and password.
        </div>
        <div class="item big">
            <input type="email" placeholder="Enter your e-mail" name="Email">
        </div>
        <div class="item submit center">
            <input type="submit" value="Reset">
        </div>
    </div>
</form>