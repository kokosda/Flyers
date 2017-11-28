<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="FlyerMe.Admin.LogIn" Theme="" %>
<%@ Register Src="~/Controls/MasterPage/Scripts.ascx" TagName="Scripts" TagPrefix="uc" %>
<!doctype html>
<html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
	    <link href="<%= ResolveUrl("~/images/favicon.ico") %>" rel="shortcut icon">
        <title><%= clsUtility.ProjectName %></title>
        <link href="<%= ResolveUrl("~/admin/css/reset.css") %>" rel="stylesheet" type="text/css">
        <link href="<%= ResolveUrl("~/admin/css/style.css") %>" rel="stylesheet" type="text/css">
        <link href="<%= ResolveUrl("~/admin/css/adaptive.css") %>" rel="stylesheet" type="text/css">
    </head>
    <body class="login">
        <div class="wrapper">
	        <div class="loginform">
    	        <div class="logo">
                    <asp:HyperLink runat="server" NavigateUrl="~/">
    		            <img src="<%= AdminSiteRootUrl %>images/FlyerMe_logo.svg" onerror="this.onerror=null; this.src=<%= AdminSiteRootUrl %>images/FlyerMe_logo.png" alt="<%= clsUtility.ProjectName %>">
                    </asp:HyperLink>
                </div>
    	        <form method="post" runat="server" enctype="application/x-www-form-urlencoded" id="form">
        	        <input type="text" id="inputLogin" runat="server" placeholder="Login" />
                    <input type="password" id="inputPassword" runat="server" placeholder="Password" />
                    <div class="error_message" id="divSummaryError" runat="server" visible="false">
                        <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
                    </div>
                    <asp:Button ID="btnEnter" runat="server" Text="Enter" OnClick="btnEnter_Click" />
                </form>
            </div>
        </div>
        <uc:Scripts ID="scripts" runat="server" BundleName="~/bundles/scripts/admin/login" />
    </body>
</html>