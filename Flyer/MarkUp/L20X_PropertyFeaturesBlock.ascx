<%@ Control Language="C#" AutoEventWireup="true" CodeFile="L20X_PropertyFeaturesBlock.ascx.cs" Inherits="FlyerMe.Flyer.MarkUp.L20X_PropertyFeaturesBlock" %>
<%@ Import Namespace="FlyerMe" %>
<%  var openTr = false;
    var closeTr = false;
    var length = PropertyFeatures.Length % 4 == 0 ? PropertyFeatures.Length : PropertyFeatures.Length + (4 - (PropertyFeatures.Length % 4));
    var dataSource = new String[length];

    Array.Copy(PropertyFeatures, dataSource, PropertyFeatures.Length);

    for (var i = 0; i < length; i++)
    {
        openTr = (i % 4) == 0;
        closeTr = ((i + 1) % 4) == 0;

        if (openTr)
        { %>
<tr>
     <% } %>
        <% if (dataSource[i].HasText()) { %>
    <td style="padding:7px 2% 7px 0"><img src="<%= EmailImageUrl %>images/bulet_gray.png" /> <%= dataSource[i] %></td>
        <% } else { %>
    <td style="padding:7px 2% 7px 0"></td>
        <% } %>
     <% 
        if (closeTr)
        { %>
</tr>
     <% }
    } 
%>