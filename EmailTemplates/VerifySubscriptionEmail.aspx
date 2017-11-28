<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerifySubscriptionEmail.aspx.cs" Inherits="FlyerMe.EmailTemplates.VerifySubscriptionEmail"  MasterPageFile="" Theme="" ValidateRequest="false" %>
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
                                            <strong>Congratulations! You are just one step away from<br />becoming part of the <%= clsUtility.SiteBrandName %> community.</strong><br />
                                   	        <p>Please click on the link below to confirm subscription. <br /><a href="<%= VerificationLink %>" style="color:#f46a02;"><%= VerificationLink %></a></p>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="middle" style=" padding:27px 0 0; font-size:14px; line-height:24px;font-family: Verdana, sans-serif;">
                                    
                                        <p>If you don't see the link, or if you click on the link and it appears broken,<br /> please copy and paste the URL into a new browser window.</p>
                                    
                                           <p>See you online! <br />The <%= clsUtility.SiteBrandName %> Team</p>
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
                                           <p>Note: To ensure deliverability, please add <%= GetServiceAccountsMarkup() %> to your address book. Look here for instructions:<br>
                                           <a href="<%= clsUtility.SiteHttpWwwRootUrl %>/addressbookinstructions.aspx" style="color:#f46a02;"><%= clsUtility.SiteHttpWwwRootUrl %>/addressbookinstructions.aspx</a></p>
                                           <p style="padding-top:10px;">Please don't reply to this computer generated e-mail. Any queries<br> can be sent to <a href="mailto:<%= clsUtility.ContactUsEmail %>" style="color:#f46a02;"><%= clsUtility.ContactUsEmail %></a>.
        </p>
                                   
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