<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="FlyerMe.Error" Theme="" %>
<!doctype html>
<html>
    <head runat="server">
        <title>Error | <%= clsUtility.ProjectName %></title>
	    <meta charset="utf-8" />
	    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
	    <meta http-equiv="X-UA-Compatible" content="IE=8" />
	    <link href="<%= ResolveUrl("~/images/favicon.ico") %>" rel="shortcut icon" />
		<link href="<%= ResolveUrl("~/css/owl.carousel.min.css") %>" rel="stylesheet" type="text/css" />
		<link href="<%= ResolveUrl("~/css/reset.css") %>" rel="stylesheet" type="text/css" />
		<link href="<%= ResolveUrl("~/css/style.css") %>" rel="stylesheet" type="text/css" />
		<link href="<%= ResolveUrl("~/css/adaptive.css") %>" rel="stylesheet" type="text/css" />
    </head>
    <body class="error-page">
        <div class="wrapper">
            <div id="content">
    	        <div class="errorpage">
    		        <div class="icon">
                        <% var statusCodeString = Response.StatusCode.ToString();
                           
                           if (statusCodeString.Length > 2 && statusCodeString[1].Equals('0')) {  %>
                            <%= statusCodeString[0] %><span><%= statusCodeString[1] %></span><%= statusCodeString.Substring(2) %>
                        <% } else { %>
                        <%= statusCodeString %>
                        <% } %>
    		        </div>
    		        <h1><%= ((System.Net.HttpStatusCode)Response.StatusCode).ToString() %></h1>
                    <div class="text"><%= Description %></div>
			        <a href="<%= ResolveUrl("~") %>" class="start">Return to the main page</a>
                    <div class="console" id="divStacktrace" runat="server" visible="false"><asp:Literal ID="ltlStackTrace" runat="server"></asp:Literal></div>
                </div>
            </div>
        </div>
    </body>
</html>