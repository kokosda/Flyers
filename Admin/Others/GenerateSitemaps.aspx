<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateSitemaps.aspx.cs" Inherits="FlyerMe.Admin.Others.GenerateSitemaps" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <uc:Message ID="message" runat="server" />
                <form method="post" enctype="application/x-www-form-urlencoded" runat="server">
                    <div class="add-agents">
						<div class="item-box">
							<div class="item">
                                <div class="select-item">
                                    <div class="item left">
                                        <asp:Button ID="btnGoogleSitemap" runat="server" Text="Google Sitemap" OnCommand="btnGoogleSitemap_Command" />
                                    </div>
                                    <div class="item right">
                                        <asp:HyperLink ID="hlGoogleSitemapViewFeed" runat="server" NavigateUrl="~/xmlfeeds/sitemap.xml" Target="_blank" CssClass="button">View feed</asp:HyperLink>
                                    </div>
                                </div>
							</div>
                            <div class="item">
                                <div class="select-item">
                                    <div class="item left">
                                        <asp:Button ID="btnYahooSitemap" runat="server" Text="Yahoo Sitemap" OnCommand="btnYahooSitemap_Command" />
                                    </div>
                                    <div class="item right">
                                        <asp:HyperLink ID="hlYahooSitemapViewFeed" runat="server" NavigateUrl="~/xmlfeeds/urllist.txt" Target="_blank" CssClass="button">View feed</asp:HyperLink>
                                    </div>
                                </div>
							</div>
                        </div>
                    </div>
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>