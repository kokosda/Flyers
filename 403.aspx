<%@ Page Language="C#" AutoEventWireup="true" CodeFile="403.aspx.cs" Inherits="FlyerMe.Error403" Theme="" %>
<!doctype html>
<html>
    <head runat="server">
	    <meta charset="utf-8" />
	    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
	    <meta http-equiv="X-UA-Compatible" content="IE=8" />
	    <link href="<%= ResolveUrl("~/images/favicon.ico") %>" rel="shortcut icon" />
	    <title>FlyerMe</title>
		<link href="<%= ResolveUrl("~/css/owl.carousel.min.css") %>" rel="stylesheet" type="text/css" />
		<link href="<%= ResolveUrl("~/css/reset.css") %>" rel="stylesheet" type="text/css" />
		<link href="<%= ResolveUrl("~/css/style.css") %>" rel="stylesheet" type="text/css" />
		<link href="<%= ResolveUrl("~/css/adaptive.css") %>" rel="stylesheet" type="text/css" />
    </head>
    <body class="error-page">
        <div class="wrapper">
            <div id="content">
    	        <div class="errorpage">
    		        <div class="icon">4<span>0</span>3</div>
    		        <h1>Forbidden: access is denied.</h1>
                    <div class="text">You do not have permission to view this directory or page using the credentials that you supplied.</div>
			        <a href="<%= ResolveUrl("~") %>" class="start">Return to the main page</a>
                </div>
            </div>
        </div>
    </body>
</html>