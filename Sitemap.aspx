<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Sitemap.aspx.cs" 
Inherits="FlyerMe.Sitemap" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  	<div class="text-content">
        <div class="content">
        	<h1><%= clsUtility.ProjectName %> Sitemap</h1>
        	<p>Welcome to the Sitemap of <%= clsUtility.SiteBrandName %>. Please browse the following links to know more about our Website.</p>
			<hr class="sm" />
			<strong>HOME</strong>
            <p><%= clsUtility.SiteBrandName %> offers the most effective and affordable real estate marketing solutions by creating real estate email flyers, personal property websites, free virtual tour and also post property Ads to top syndication engines for real estate listings.</p>
            <a href="<%= RootUrl %>">Go to page</a>
            <hr class="sm" />
			<strong>SEARCH <%= clsUtility.ProjectName.ToUpper() %></strong>
            <p>Search for latest real estate email flyers at <%= clsUtility.SiteBrandName %> with detailed information including pricing and photos.</p>
            <a href="<%= RootUrl %>search.aspx">Go to page</a>
            <hr class="sm" />
			<strong>PRICING</strong>
            <p>At <%= clsUtility.SiteBrandName %> get complete pricing information of real estate email flyers by selecting your marketing area.</p>
            <a href="<%= RootUrl %>prices.aspx">Go to page</a>
            <hr class="sm" />
			<strong>SELLER SAMPLES</strong>
            <p>Find samples of our professionally designed real estate email flyers at <%= clsUtility.SiteBrandName %> with flyer photos.</p>
            <a href="<%= RootUrl %>samples.aspx">Go to page</a>
            <hr class="sm" />
			<strong>BUYER SAMPLES</strong>
            <p>Find samples of our professionally designed real estate email flyers at <%= clsUtility.SiteBrandName %> with flyer photos.</p>
            <a href="<%= RootUrl %>samples.aspx?buyers=true">Go to page</a>
            <hr class="sm" />
			<strong>CREATE FLYER</strong>
            <p>Create your attractive realestate flyers and send to your specified market area.</p>
            <a href="<%= RootUrl %>createflyer.aspx">Go to page</a>
            <hr class="sm" />
			<strong>MARKETING TEST</strong>
            <p>Take the free <%= clsUtility.SiteBrandName %> property marketing test to see if buyers can find your listings.</p>
            <a href="<%= RootUrl %>syndicationtool.aspx">Go to page</a>
            <hr class="sm" />
			<strong>SUBSCRIBE</strong>
            <p>Subscribe to <%= clsUtility.SiteBrandName %> and receive our email flyer campaigns by selecting your marketing area.</p>
            <a href="<%= RootUrl %>subscribe.aspx">Go to page</a>
            <hr class="sm" />
			<strong>FAQ</strong>
            <p>Get all answers to your real estate marketing and real estate email flyers queries at <%= clsUtility.SiteBrandName %>.</p>
            <a href="<%= RootUrl %>faq.aspx">Go to page</a>
            <hr class="sm" />
			<strong>CONTACT US</strong>
            <p>Contact <%= clsUtility.SiteBrandName %> for any assistance regarding real estate marketing, real estate email flyers, listing syndication.</p>
            <a href="<%= RootUrl %>contacts.aspx">Go to page</a>
            <hr class="sm" />
			<strong>WHAT’S NEW</strong>
            <p>At <%= clsUtility.SiteBrandName %> find latest features, easier ways to design and send real estate email flyers.</p>
            <a href="<%= RootUrl %>whatsnew.aspx">Go to page</a>
            <hr class="sm" />
			<strong>PRIVACY & POLICY</strong>
            <p><%= clsUtility.SiteBrandName %> has created this privacy policy in order to disclose our information gathering and dissemination practices.</p>
            <a href="<%= RootUrl %>privacypolicy.aspx">Go to page</a>
            <hr class="sm" />
		</div>
    </div>
</asp:Content>