<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="FlyerMe.Controls.MasterPage.Menu" %>
<ul class="menu">
    <li runat="server"><asp:HyperLink ID="hlCreateFlyer" runat="server" ToolTip="Create Flyer" Text="Create Flyer"></asp:HyperLink></li>
    <li runat="server"><asp:HyperLink ID="hlPrices" runat="server" ToolTip="Prices" Text="Prices"></asp:HyperLink></li>
    <li runat="server"><asp:HyperLink ID="hlSamples" runat="server" ToolTip="Samples" Text="Samples"></asp:HyperLink></li>
    <li runat="server"><asp:HyperLink ID="hlProduct" runat="server" ToolTip="Product" Text="Product"></asp:HyperLink></li>
    <li runat="server"><asp:HyperLink ID="hlSearch" runat="server" ToolTip="Search flyers" Text="Search"></asp:HyperLink></li>
</ul>