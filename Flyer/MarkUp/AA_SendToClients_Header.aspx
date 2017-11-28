<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AA_SendToClients_Header.aspx.cs" Inherits="FlyerMe.Flyer.MarkUp.AA_SendToClients_Header" MasterPageFile="" Theme="" ValidateRequest="false" %>
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
                <tr>
                	<td>
                        <% if (!String.IsNullOrEmpty(CustomerName)) { %>
                    	<strong><%= CustomerName %></strong> has sent this flyer which may be of interest to you.<br />
                        <% } %>
                        <% if (!String.IsNullOrEmpty(CustomerName)) { %>
                        <strong><%= CustomerName %>'s Message:</strong>
                        <% } %>
                        <% if (!String.IsNullOrEmpty(Message)) { %>
                        <%= Message.Replace("\n", "<br />") %>
                        <% } %>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>