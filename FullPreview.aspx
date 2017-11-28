<%@ Page Language="C#" Theme="" AutoEventWireup="true" CodeFile="FullPreview.aspx.cs" Inherits="FlyerMe.FullPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Image Preview | Real Estate Email Flyers | <%= clsUtility.ProjectName %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><strong style="font-size:20px;">Right click on the picture shown below and select save as option to save the converted image to your computer.</strong></div>
    <div>
        <img src="<%=RootURL%>pdf/<%=Request.QueryString["imgPath"]%>" border="1" alt="" />
    </div>
    </form>
</body>
</html>
