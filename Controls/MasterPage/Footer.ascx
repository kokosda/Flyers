<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="FlyerMe.Controls.MasterPage.Footer" %>
<%@ Register Src="~/Controls/MasterPage/Menu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Import Namespace="FlyerMe" %>
<footer>
    <div class="footer">
        <div class="content">
            <div class="copyright">
            <div class="logo"></div>
            © <%= DateTime.Now.Year %> <%= clsUtility.ProjectName %><br>All rights reserved
            </div>
            <uc:Menu ID="Menu" runat="server" />
            <ul class="menu2">
                <li><a href="http://flyerme.ghost.io/" title="Blog" target="_blank">Blog</a></li>
                <li><asp:HyperLink ID="hlPrivacyPolicy" runat="server" ToolTip="Privacy & Policy" Text="Privacy & Policy"></asp:HyperLink></li>
                <li><asp:HyperLink ID="hlFaq" runat="server" ToolTip="FAQ" Text="FAQ"></asp:HyperLink></li>
                <li><asp:HyperLink ID="hlContacts" runat="server" ToolTip="Contacts" Text="Contacts"></asp:HyperLink></li>
                <li><asp:HyperLink ID="hlTermsAndConditions" runat="server" ToolTip="Terms and Conditions" Text="Terms and Conditions"></asp:HyperLink></li>
                <li><asp:HyperLink ID="hlAntiSpamPolicy" runat="server" ToolTip="Anti-Spam Policy" Text="Anti-Spam Policy"></asp:HyperLink></li>
                <li><asp:HyperLink ID="hlSitemap" runat="server" ToolTip="Sitemap" Text="Sitemap"></asp:HyperLink></li>
            </ul>
            <div class="social">
                <h2>FOLLOW US</h2>
                <ul>
                    <li><a href="https://www.facebook.com/flyermeHQ" target="_blank" title="Facebook" class="facebook">Facebook</a></li>
                    <li><a href=" https://twitter.com/FlyerMeHQ" target="_blank" title="Twitter" class="twitter">Twitter</a></li>
                    <li><a href="https://www.instagram.com/flyermehq/" target="_blank" title="Instagram" class="instagram">Instagram</a></li>
                    <li><a href="https://www.pinterest.com/flyerme/" target="_blank" title="Pinterest" class="pinterest">Pinterest</a></li>
                    <li><a href="https://www.linkedin.com/company/flyerme" target="_blank" title="LinkedIn" class="linkedIn">LinkedIn</a></li>
                    <!--<li><a href="#" target="_blank" title="Google+" class="google">Google+</a></li>-->
                    <!--<li><a href="#" target="_blank" title="RSS" class="rss">RSS</a></li>-->
                </ul>
            </div>
            <div class="subscrible">
                <h2>Like Real Estate Marketing? </h2>
                <div class="text">Subscribe to our newsletter for real estate marketing news, promotions, tutorials, and more.</div>
                <form method="get" enctype="application/x-www-form-urlencoded" action="<%= RootURL.ToUri().UrlToHttps() %>subscribe.aspx">
           			<div class="form-content">
                        <input type="text" class="form-text" placeholder="E-mail" name="email" />
                        <input type="submit" class="submit" value="GO" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</footer>