<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelfPromotionEmail.aspx.cs" Inherits="FlyerMe.EmailTemplates.SelfPromotionEmail"  MasterPageFile="" Theme="" ValidateRequest="false" %>
<!doctype>
<html>
    <head>
        <title><%= clsUtility.ProjectName %></title>
    </head>
<body style="padding:0; margin:0;">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center">
                <link href='https://fonts.googleapis.com/css?family=Lato:400,700,900&subset=latin,latin-ext' rel='stylesheet' type='text/css' />
                <table width="648" cellpadding="0" cellspacing="0" border="0" style="font-family: 'Lato', sans-serif;">
                    <tr>
                        <td align="center" height="137" valign="midle">
                            <a href="<%= RootUrl %>">
                                <img src="<%= EmailImageUrl %>images/mail/logo.png">
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="216" valign="midle" bgcolor="#10549b" style="color:#ffffff; font-size:28px;">
                            The smartest way to put your <br />listing in front of thousands of <br />realtors, fast.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td rowspan="3">
                                        <img src="<%= EmailImageUrl %>images/mail/1.jpg" style="max-width:408px; float:left;"/>
                                    </td>
                                    <td><img src="<%= EmailImageUrl %>images/mail/2.jpg" style="max-width:240px; float:left;"/></td>
                                </tr>
                                <tr>
                                    <td><img src="<%= EmailImageUrl %>images/mail/3.jpg" style="max-width:240px; float:left;"/></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td background="<%= EmailImageUrl %>images/bg_images_04_email.png" bgcolor="#ff7c0c" width="648" height="357" valign="top">
                            <!--[if gte mso 9]>
                            <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:357px;">
                            <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_images_04_email.png" color="#ff7c0c" />
                            <v:textbox inset="0,0,0,0">
                            <![endif]-->
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td valign="midle" height="95" align="center" style="padding-top:40px; color:#ffffff; font-weight:600; font-size:24px;">
                                        Create and send your beautiful <br />e-mail flyer in seconds. 
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="midle" height="195" align="center" style=" color:#ffffff; font-weight:400; font-size:38px;">
                                        Exclusive launch offer: <br/>
                                        <div style="font-weight:600;">FLYERME20 save 20% <br/>
                                        off your first order</div>
                                    </td>
                                </tr>
                            </table>
                        <!--[if gte mso 9]>
                            </v:textbox>
                            </v:rect>
                            <![endif]-->
                        </td>
                    </tr><tr>
                        <td background="<%= EmailImageUrl %>images/bg_images_mail.png" bgcolor="#eef4f4" width="648" height="306" valign="top">
                            <!--[if gte mso 9]>
                            <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:306px;">
                            <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_images_mail.png" color="#050505" />
                            <v:textbox inset="0,0,0,0">
                            <![endif]-->
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td valign="midle" height="95" align="center" style="padding-top:50px; color:#050505; font-size:28px; letter-spacing:1px">
                                        Send your flyer to <strong style="font-weight:900;">thousands</strong><br /> agents with buyers instantly! 
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="midle" height="105" widyh="648" align="center">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td width="167"></td>
                                                <td width="314" height="56" bgcolor="#f37b22" align="center" style="color:#ffffff; border-radius:28px; -webkit-border-radius:28px; -moz-border-radius:28px;">
                                                            <a href="<%= RootUrl %>"
                                                    style=" color:#ffffff;font-size:24px;font-weight:bold;line-height:56px;text-align:center;text-decoration:none; -webkit-text-size-adjust:none;">Get started now</a>
                                                </td>
                                                <td width="167"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="midle" height="50" align="center" style=" color:#9b9b9b; font-size:14px; letter-spacing:1px">
                                        <a href="<%= RootUrl %>" style="color:#9b9b9b;">www.<%= clsUtility.SiteBrandName %></a>
                                    </td>
                                </tr>
                            </table>
                        <!--[if gte mso 9]>
                            </v:textbox>
                            </v:rect>
                            <![endif]-->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
