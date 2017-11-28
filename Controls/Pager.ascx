<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pager.ascx.cs" Inherits="FlyerMe.Controls.Pager" %>
<div class="pager">
    <ul id="ulPager" runat="server">
        <li id="liPreviousPage" runat="server"><asp:HyperLink ID="hlPreviousPage" runat="server" CssClass="prev">« previous</asp:HyperLink></li>
        <li id="liPage1" runat="server"><asp:HyperLink ID="hlPage1" runat="server"></asp:HyperLink></li>
        <li id="liPageDotPrevious" runat="server"><asp:HyperLink ID="hlPageDotPrevious" runat="server" class="dot">&#8230;</asp:HyperLink></li>
        <li id="liPage2" runat="server"><asp:HyperLink ID="hlPage2" runat="server"></asp:HyperLink></li>
        <li id="liPage3" runat="server"><asp:HyperLink ID="hlPage3" runat="server"></asp:HyperLink></li>
        <li id="liPage4" runat="server"><asp:HyperLink ID="hlPage4" runat="server"></asp:HyperLink></li>
        <li id="liPage5" runat="server"><asp:HyperLink ID="hlPage5" runat="server"></asp:HyperLink></li>
        <li id="liPage6" runat="server"><asp:HyperLink ID="hlPage6" runat="server"></asp:HyperLink></li>
        <li id="liPage7" runat="server"><asp:HyperLink ID="hlPage7" runat="server"></asp:HyperLink></li>
        <li id="liPageDotNext" runat="server"><asp:HyperLink ID="hlPageDotNext" runat="server" class="dot">&#8230;</asp:HyperLink></li>
        <li id="liLastPage" runat="server"><asp:HyperLink ID="hlLastPage" runat="server"></asp:HyperLink></li>
        <li id="liNexPage" runat="server"><asp:HyperLink ID="hlNextPage" runat="server" CssClass="next">next »</asp:HyperLink></li>
    </ul>
</div>