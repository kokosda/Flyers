<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyFlyers.aspx.cs" Inherits="FlyerMe.MyFlyers" MasterPageFile="~/MasterPageAccount.master" Theme="" %>
<%@ Register Src="~/Controls/MasterPageAccount/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <uc:UserMenu ID="userMenu" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="account-flyers">
        <ul class="flyers-menu">
            <li><asp:HyperLink ID="hlAll" runat="server" NavigateUrl="~/myflyers.aspx" Text="All"></asp:HyperLink></li>
            <li><asp:HyperLink ID="hlSellers" runat="server" NavigateUrl="~/myflyers.aspx?type=seller" Text="Seller’s"></asp:HyperLink></li>
            <li><asp:HyperLink ID="hlBuyers" runat="server" NavigateUrl="~/myflyers.aspx?type=buyer" Text="Buyer's"></asp:HyperLink></li>
            <li><asp:HyperLink ID="hlCustom" runat="server" NavigateUrl="~/myflyers.aspx?type=custom" Text="Custom"></asp:HyperLink></li>
        </ul>
        <div class="content">
            <form runat="server">
                <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptOrders_ItemDataBound">
                    <ItemTemplate>
                        <div class="block">
                            <div class="conteiner">
                                <div class="text">
                                    <div class="sel"><asp:Literal ID="ltlType" runat="server"></asp:Literal></div>
                                    <div class="status" id="divStatus" runat="server"><asp:Literal ID="ltlStatus" runat="server"></asp:Literal></div>
                                    <div class="title" id="divTitle" runat="server"><asp:HyperLink ID="hlTitle" runat="server"></asp:HyperLink></div>
                                    <div class="price" id="divPrice" runat="server"><asp:Literal ID="ltlPrice" runat="server"></asp:Literal></div>
                                    <div class="filed" id="divAddress" runat="server"><asp:Literal ID="ltlAddress" runat="server"></asp:Literal></div>
                                    <div class="filed"><strong>Flyer ID:</strong> <asp:Literal ID="ltlFlyerId" runat="server"></asp:Literal></div>
                                    <div class="filed" id="divDeliveryDate" runat="server"><strong>Delivery Date:</strong> <asp:Literal ID="ltlDeliveryDate" runat="server"></asp:Literal></div>
                                    <div class="filed" id="divMls" runat="server"><strong>MLS:</strong> <asp:Literal ID="ltlMls" runat="server"></asp:Literal></div>
                                    <div class="filed" id="divMarket" runat="server"><asp:Literal ID="ltlMarket" runat="server"></asp:Literal></div>
                                    <div class="filed link" id="divShowFlyerUrl" runat="server"><asp:Literal ID="ltlShowFlyerUrl" runat="server"></asp:Literal></div>
                                </div>
                            </div>
                            <div class="image">
                                <div class="table-cell">
                    	            <asp:Image ID="imgPhoto1" runat="server" />
                                </div>
                            </div>
                            <div class="links">
                                <ul>
                                    <li><asp:HyperLink ID="hlSendThisFlyerToClients" runat="server" Text="Send this flyer to clients"></asp:HyperLink></li>
                                    <li><asp:LinkButton ID="lbEditAndResend" runat="server" OnClick="lbEditAndResend_Click" Text="Edit and Resend as New Flyer"></asp:LinkButton></li>
                                    <li><asp:HyperLink ID="hlPostToCraigslist" runat="server" Text="Post to Craiqslist"></asp:HyperLink></li>
                                    <li><asp:HyperLink ID="hlDeliveryReport" runat="server" Text="Delivery Report"></asp:HyperLink></li>
                                    <li><asp:LinkButton ID="lbTurnSyndication" runat="server" OnClick="lbTurnSyndication_Click">Turn Syndication <asp:Literal ID="ltlTurnSyndication" runat="server"></asp:Literal></asp:LinkButton></li>
                                </ul>
                                <ul>
                                    <li><asp:HyperLink ID="hlViewThisFlyer" runat="server" Text="View this flyer"></asp:HyperLink></li>
                                    <li><asp:HyperLink ID="hlEditThisFlyer" runat="server" Text="Edit this flyer"></asp:HyperLink></li>
                                    <li><asp:LinkButton ID="lbCreateCopy" runat="server" Text="Create a copy" OnClick="lbCreateCopy_Click"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbDeleteThisFlyer" runat="server" Text="Delete this flyer" OnClick="lbDeleteThisFlyer_Click"></asp:LinkButton></li>
                                </ul>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="pager">
                    <uc:Pager ID="pager" runat="server" PageName="myflyers.aspx" />
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAfterFooter" runat="server">
    <div id="popup_bg" style="display:none;"></div>
    <div id="popup_preview" style="display:none;">
        <div class="content" data-mcs-theme="minimal-dark"></div>
        <a href class="close"></a>
    </div>
    <link href="<%= ResolveClientUrl("~/css/jquery.mcustomscrollbar.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>