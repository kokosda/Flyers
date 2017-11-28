<%@ Control Language="C#" AutoEventWireup="true" CodeFile="L20X_PropertyFeaturesBlock_Buyers.ascx.cs" Inherits="FlyerMe.Flyer.MarkUp.L20X_PropertyFeaturesBlock_Buyers" %>
<%@ Import Namespace="FlyerMe" %>
<%  var openTr = false;
    var closeTr = false;
    var length = PropertyFeatures.Length % 3 == 0 ? PropertyFeatures.Length : PropertyFeatures.Length + (3 - (PropertyFeatures.Length % 3));
    var dataSource = new String[length];

    Array.Copy(PropertyFeatures, dataSource, PropertyFeatures.Length);

    for (var i = 0; i < length; i++)
    {
        openTr = (i % 3) == 0;
        closeTr = ((i + 1) % 3) == 0;

        if (openTr)
        { %>
<tr>
     <% } %>
        <% if (dataSource[i].HasText()) { %>
    <td style="padding:7px 0 7px 2%" width="31%"><img src="<%= EmailImageUrl %>images/bulet_04.png" /> <%= dataSource[i] %></td>
        <% } else { %>
    <td style="padding:7px 0 7px 2%" width="31%"></td>
        <% } %>
     <% 
        if (closeTr)
        { %>
</tr>
     <% }
    } 
%>