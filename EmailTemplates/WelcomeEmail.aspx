<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WelcomeEmail.aspx.cs" Inherits="FlyerMe.EmailTemplates.WelcomeEmail"  MasterPageFile="" Theme="" ValidateRequest="false" %>
<%@ Import Namespace="FlyerMe" %>
<!doctype>
<html>
    <head>
        <title><%= clsUtility.ProjectName %></title>
    </head>
    <body style="margin:0px; padding:0px;">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td align="center">
                    <link href='https://fonts.googleapis.com/css?family=Lato:400,700,900&subset=latin,latin-ext' rel='stylesheet' type='text/css' />
                    <table width="709" cellpadding="0" cellspacing="0" border="0" style="font-family: 'Lato', sans-serif;">
                        <tr>
                            <td align="center" height="137" valign="midle">
                                <a href="<%= RootUrl %>"><img src="<%= EmailImageUrl %>images/mail/logo.png"></a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
								<table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td valign="midle" height="50" style="padding:76px 40px 50px; color:#09509e; font-weight:400; font-size:18px;line-height:32px;  letter-spacing:0.5px;">
                                            <strong style="font-weight:900;">Dear <%= Fullname %></strong><br />
											Congratulations! You have successfully registered. Thanks so much <br />
											for joining us. You’re on your way to putting your listing in front of <br />
											thousands of realtors.

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="midle" align="center" style=" color:#ff7c0c; font-weight:400; font-size:18px;line-height:32px;  letter-spacing:0.5px;">
											Bellow are your login details<br>
											<strong style="font-weight:900;">Login ID:</strong> <span style=" text-decoration:none; color:#ff7c0c; "><%= Login %></span> &nbsp;&nbsp;&nbsp; <strong style="font-weight:900;">Password:</strong> <%= Password %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="midle" align="center" style=" padding-top:42px; color:#09509e; font-weight:900; font-size:18px;line-height:32px;  letter-spacing:0.5px;">
											Go ahead, give <%= clsUtility.ProjectName %> a try, its really easy to get started!
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="midle" align="center" style=" padding:39px 0;">
											<div><!--[if mso]>
											  <v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" href="<%= RootUrl %>createflyer.aspx" style="height:45px;v-text-anchor:middle;width:208px;" arcsize="50%" stroke="f" fillcolor="#f37b22">
												<w:anchorlock/>
												<center>
											  <![endif]-->
												  <a href="<%= RootUrl %>createflyer.aspx"
											style="background-color:#f37b22;border-radius:22px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:18px;font-weight:bold;line-height:45px;text-align:center;text-decoration:none;width:208px;-webkit-text-size-adjust:none;">Get started now</a>
											  <!--[if mso]>
												</center>
											  </v:roundrect>
											<![endif]--></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td background="<%= EmailImageUrl %>images/register_blue_bottom.png" bgcolor="#09509e" width="709" height="350" valign="top">
                              <!--[if gte mso 9]>
                              <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:709px;height:350px;">
                                <v:fill type="tile" src="<%= EmailImageUrl %>images/register_blue_bottom.png" color="#ff7c0c" />
                                <v:textbox inset="0,0,0,0">
                              <![endif]-->
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td valign="midle" height="50" align="center" style="padding-top:55px; color:#ffffff; font-weight:400; font-size:18px;line-height:32px;">
                                            We appreciate your business and look forward to<br />
											helping you grow you client base. Have any <br />
											questions? Just shoot us an email! We’re always <br />
											here to help. <a href="mailto:<%= clsUtility.ContactUsEmail %>" style="color:#ffffff;"><%= clsUtility.ContactUsEmail %></a>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="midle" align="center" style="padding-top:50px; color:#ffffff; font-weight:700; font-size:18px;">
											Cheerfully yours,<br />
											The <%= clsUtility.ProjectName %> Team
                                        </td>
                                    </tr>
                                </table>
                            <!--[if gte mso 9]>
                                </v:textbox>
                              </v:rect>
                              <![endif]-->
                            </td>
                        </tr>
                        <tr>
                            <td background="<%= EmailImageUrl %>images/register_bottom.png" bgcolor="#ff7c0c" width="709" height="231" valign="top">
                              <!--[if gte mso 9]>
                              <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:709px;height:231px;">
                                <v:fill type="tile" src="<%= EmailImageUrl %>images/register_bottom.png" color="#ff7c0c" />
                                <v:textbox inset="0,0,0,0">
                              <![endif]-->
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td valign="midle" height="50" align="center" style="padding-top:65px; color:#000000; font-weight:400; font-size:12px;">
                                            Note: To ensure deliverability, please add <%= GetServiceAccountsMarkup() %><br />
											to your address book. Look here for instructions:<br />
											<a href="<%= RootUrl %>addressbookinstructions.aspx" style=" color:#ffffff; "><%= RootUrl %>addressbookinstructions.aspx</a><br />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="midle" align="center" style="padding-top:10px; color:#000000; font-weight:400; font-size:12px;">
                                           
											This is a computer generated e-mail so please don't reply to this e-mail.<br />
											Any queries can be sent to <a href="mailto:<%= clsUtility.ContactUsEmail %>" style=" text-decoration:none; color:#ffffff; "><%= clsUtility.ContactUsEmail %></a>
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
