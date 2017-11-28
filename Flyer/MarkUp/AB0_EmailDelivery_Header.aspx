<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AB0_EmailDelivery_Header.aspx.cs" Inherits="FlyerMe.Flyer.MarkUp.AB0_EmailDelivery_Header" MasterPageFile="" Theme="" ValidateRequest="false" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
    	<td align="center">
        	<table width="648" cellpadding="0" cellspacing="17" border="0" bgcolor="#f4f4f4" style="font-family:Verdana, sans-serif; font-size:12px; line-height:24px">
            	<tr>
                	<td align="center">
                    	Trouble viewing this email? <a href="<%= RootUrl %>showflyer.aspx?orderid=<%= OrderId %>" style="color:#f37b22;" target="_blank">View it online</a><br />
                        To ensure deliverability, add <%= GetServiceAccountsMarkup() %> to your address book. <a href="<%= RootUrl %>addressbookinstructions.aspx" style="color:#f37b22;">Learn how</a>
                    </td>
                </tr>
                <% if (ShowSenderAgentIntroduction) { %>
                <tr>
                	<td>
                        I am <%= SenderAgentFullname %> a real estate professional based in <%= SenderAgentCounty %>, <%= SenderAgentState %> <%= SenderAgentBrokerageName %>. I am currently marketing the following property that may be of interest to you:
                    </td>
                </tr>
                <% } %>
            </table>
        </td>
    </tr>
</table>