<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SyndicationTool.aspx.cs" 
Inherits="FlyerMe.SyndicationTool" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-content">
        <div class="content">
            <h1>Make sure buyers can find your property<br />listing where they are looking</h1>
            <p>With nearly every buyer starting their property search online it is critical that you have a strong internet marketing plan.</p>
		    <p>How does your internet marketing plan rate? Take the <%= clsUtility.SiteBrandName %> free internet marketing evaluation to see how easily buyers can find your property listing online.</p>
		</div>
    </div>
    <section id="syndication">
        <div class="content">
            <h2>Listing syndication</h2>
            <ul>
                <span>
                	<li><span><a href="http://www.vast.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n1.png") %>" alt="Vast" /></a></span></li>
                	<li><span><a href="http://www.craigslist.org/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n2.png") %>" alt="Craigslist" /></a></span></li>
                	<li><span><a href="https://www.google.com/retail/merchant-center/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n3.png") %>" alt="GoogleBase" /></a></span></li>
                	<li><span><a href="http://www.ebay.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n4.png") %>" alt="eBay" /></a></span></li>
                </span>
                <span>
                	<li><span><a href="http://www.oodle.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n5.png") %>" alt="Oodle" /></a></span></li>
                	<li><span><a href="http://www.kijiji.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n6.png") %>" alt="Kijiji" /></a></span></li>
                	<li><span><a href="http://www.olx.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n7.png") %>" alt="OLX" /></a></span></li>
                	<li><span><a href="http://www.zillow.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n8.png") %>" alt="Zillow" /></a></span></li>
                </span>
            </ul>
        </div>
    </section>
</asp:Content>