<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Scripts.ascx.cs" Inherits="FlyerMe.Controls.MasterPage.Scripts" %>
<%: System.Web.Optimization.Scripts.Render(BundleName) %>
<!--[if IE]>
	<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<% if (RenderAnalytics) { %>
<script type="text/javascript">
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-75702707-3', 'auto');
  ga('send', 'pageview');
</script>
<% } %>
<% if (RenderChat) { %>
<script>
    window.intercomSettings = {
        app_id: "<%= clsUtility.IntercomIoApiKey %>",
        <% if (Page.User.Identity.IsAuthenticated) { %>
        name: "<%= VisibleUsername %>",
        email: "<%= Page.User.Identity.Name %>",
        created_at: <%= CreationDateUnix.ToString() %>
        <% } %>
    };
</script>
<script>(function(){var w=window;var ic=w.Intercom;if(typeof ic==="function"){ic('reattach_activator');ic('update',intercomSettings);}else{var d=document;var i=function(){i.c(arguments)};i.q=[];i.c=function(args){i.q.push(args)};w.Intercom=i;function l(){var s=d.createElement('script');s.type='text/javascript';s.async=true;s.src='https://widget.intercom.io/widget/<%= clsUtility.IntercomIoApiKey %>';var x=d.getElementsByTagName('script')[0];x.parentNode.insertBefore(s,x);}if(w.attachEvent){w.attachEvent('onload',l);}else{w.addEventListener('load',l,false);}}})()</script>
<% } %>