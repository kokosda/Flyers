<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AB_SendToClients_Footer.aspx.cs" Inherits="FlyerMe.Flyer.MarkUp.AB_SendToClients_Footer" MasterPageFile="" Theme="" ValidateRequest="false" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
    	<td align="center">
        	<table width="648" cellpadding="0" cellspacing="26" border="0" bgcolor="#f4f4f4" style="font-family:Verdana, sans-serif; font-size:14px; line-height:22px">
            	<tr>
                	<td align="center">
                    	<span style="color:#f37b22;"><%= clsUtility.SiteBrandName %></span> Effective Real Estate <span style="color:#f37b22;">Email Flyer</span> Marketing
                        <hr style="border:0; border-bottom:1px solid #dce0e0; margin:17px 2% 15px; float:left; width:96%; background:none;" />
                       	<div>
                        	<a href="<%= RootUrl %>" style="color:#f37b22;">Click here to send your own flyer</a><br />
                            <p style="padding:6px 20px 10px; font-size:12px; width:auto;">You received this email because you are a Real Estate Professional, affiliated with Real Estate or potential homebuyer who made your email address publicly available for the purpose of receiving communications regarding real estate business. This email is sent to you on behalf of a real estate professional or developer and is a business communication. If you'd prefer not to receive email like this from <%= clsUtility.SiteBrandName %> in the future, please <a href="<%= RootUrl %>unsubscribe.aspx?email=<%= Email %>" style="color:#f37b22;">click here to unsubscribe</a>. Alternatively, you may mail your unsubscribe request to UNSUBSCRIBE, <%= clsUtility.SiteBrandName %>, 7669 NW 117th Lane Parkland, FL 33076</p>
                        </div>
                        <div style="color:#aeaeae; font-size:12px;">
                        <%= DateTime.Now.Year %> <%= clsUtility.SiteBrandName %>. All rights reserved
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>