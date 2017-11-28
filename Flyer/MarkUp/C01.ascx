<%@ Control Language="C#" AutoEventWireup="true" CodeFile="C01.ascx.cs" Inherits="FlyerMe.Flyer.MarkUp.C01" %>
<%@ Import Namespace="FlyerMe" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
    	<td align="center">
        	<table width="648" cellpadding="0" cellspacing="0" border="0">
            	<tr>
                	<td>
                        <% if (IsFirstPhotoBlockVisible) 
                           {
                               if (IsLinkMarkupBlockVisible)
                               {
                        %>
                           <a href="<%= Link %>" target="_blank">
                               <img src="<%= Helper.GetFullPhotoPath(Flyer.OrderId.Value, 1, Flyer.Photos[0]) %>" style="width:100%;" />
                           </a>
                        <%
                               }
                               else
                               {
                        %>
                               <img src="<%= Helper.GetFullPhotoPath(Flyer.OrderId.Value, 1, Flyer.Photos[0]) %>" style="width:100%;" /> 
                        <%
                               }
                        %>
                        <% } %>
                    </td>
                </tr>
                <% if (IsLinkMarkupBlockVisible) { %>
                <tr>
                    <td>
                        <%= LinkMarkup %>
                    </td>
                </tr>
                <% } %>
            </table>
        </td>
    </tr>
</table>