<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecoverPasswordEmail.aspx.cs" Inherits="FlyerMe.EmailTemplates.RecoverPasswordEmail"  MasterPageFile="" Theme="" ValidateRequest="false" %>
<!doctype>
<html>
    <head>
        <title><%= clsUtility.ProjectName %></title>
    </head>
<body style="padding:0; margin:0;">
    <table width="100%" height="100%" cellpadding="0" cellspacing="24" border="0" bgcolor="#f9f9f9">
	<tr>
    	<td align="center" valign="top">
            <table width="100%" cellpadding="0" cellspacing="0" border="0" >
                <tr>
                    <td align="center" style="padding-bottom:24px;">
                        <img src="<%= EmailImageUrl %>images/FlyerMe_biglogo.png" />
                    </td>
                </tr>
            </table>
            <table width="600" cellpadding="0" cellspacing="34" border="0" bgcolor="#ffffff">
                <tr>
                    <td>
                        <table  width="100%" cellpadding="0" cellspacing="0" border="0" >
                            <tr>
                                <td align="center" valign="middle" style="padding-bottom:27px; border-bottom:1px solid #dedede;font-size:14px; line-height:24px;font-family: Verdana, sans-serif; color:#222222;">
                                    We have sent you this message in response to your request<br /> for password assistance.
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" style="padding:27px 0; border-bottom:1px solid #dedede; font-size:14px; line-height:24px;font-family: Verdana, sans-serif;color:#222222;">
                                The current login information for your <%= clsUtility.ProjectName %> account is:
                                <p>Login ID: <strong><%= Login %></strong> &nbsp;&nbsp;&nbsp;&nbsp; Password: <strong><%= Password %></strong></p>
                                When logging in, please make sure you enter your entire email address, <%= Login %>.
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" style=" padding:27px 0 0; font-size:14px; line-height:24px;font-family: Verdana, sans-serif;">
                                    If you get directed back to the Log In screen after attempting to log in with the correct email address and password, this may be a result of the security settings on your computer. Please make sure that your computer is set to accept cookies so you may login to <a href="<%= clsUtility.SiteHttpWwwRootUrl %>" style="color:#f46a02;"><%= clsUtility.SiteBrandName %></a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table><br>
            <table width="600" cellpadding="0" cellspacing="34" border="0" bgcolor="#ffffff" >
                <tr>
                    <td>
                        <table  width="100%" cellpadding="0" cellspacing="0" border="0" >
                            <tr>
                                <td align="center" valign="middle" style="font-size:14px; line-height:24px;font-family: Verdana, sans-serif;color:#222222;">
                                   <p>If you need additional assistance, contact Customer Services at <br />(866) 441-5164, or by email at <a href="mailto:<%= clsUtility.ContactUsEmail %>" style="color:#f46a02;"><%= clsUtility.ContactUsEmail %></a></p>
                                   <p style="padding-top:10px;"><strong>Thank you for choosing <a href="#" style="color:#f46a02;"><%= clsUtility.SiteBrandName %></a></strong></p>
                                   
                                   <p>See you online! <br />The <%= clsUtility.SiteBrandName %> Team</p>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</body>
</html>
