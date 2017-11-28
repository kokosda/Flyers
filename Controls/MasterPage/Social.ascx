<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Social.ascx.cs" Inherits="FlyerMe.Controls.MasterPage.Social" %>
<%@ Import Namespace="FlyerMe" %>
<%if (!Page.User.Identity.IsAuthenticated) { %>
<script type="text/javascript">
    window.socksHelper_Options = {
        FacebookAppId: "<%= ConfigurationManager.AppSettings["FacebookAppId"] %>",
        LinkedInAppId: "<%= ConfigurationManager.AppSettings["LinkedInAppId"] %>",
        LinkedInAuthenticationState: "<%= LinkedInAuthenticationState %>",
        LinkedInRedirectUri: "<%= (RootUrl + "login.aspx").ToUri().UrlToHttps().ToString().ToLower() %>",
        LinkedInUseRedirect: <%= (!Request.Url.IsHttps()).ToString().ToLower() %>,
        LinkedInAuthorizationUrl: "<%= ConfigurationManager.AppSettings["LinkedInAuthorizationUrl"] %>",
        LoginUrl: "<%= ResolveUrl("~/login.aspx") %>",
        SignUpUrl: "<%= ResolveUrl("~/signup.aspx") %>"
    };
</script>
<script type="text/javascript" src="//platform.linkedin.com/in.js">
    api_key: <%= ConfigurationManager.AppSettings["LinkedInAppId"] %>
    authorize: false
<% if (Request.Url.IsHttps()) { %>
    credentials_cookie: true
<% } %>
</script>
<% } %>