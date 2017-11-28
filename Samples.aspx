<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Samples.aspx.cs" Inherits="FlyerMe.Samples" MasterPageFile="~/MasterPage.master" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    	<div class="samples-new">
        	<div class="content">
        	    <h1>Our seller sample flyers</h1>
                Samples of various available flyers are displayed below. You can hover the mouse over the small flyer images to see a larger preview of each or click on ‘Preview’ to see the real size sample flyer.
                <ul>
            	    <li><asp:HyperLink ID="hlSellers" runat="server">see our seller flyers</asp:HyperLink></li>
                    <li><asp:HyperLink ID="hlBuyers" runat="server">see our buyer flyers</asp:HyperLink></li>
                </ul>
            </div>
        </div>
        <div class="samples-content">
        	<div class="flyer-diz" id="divFlyerDiz" runat="server">
                <asp:Repeater ID="rptSamples" runat="server" OnItemDataBound="rptSamples_ItemDataBound">
                    <ItemTemplate>
                        <div class="block">
                            <asp:HyperLink ID="hlPreview" runat="server"><asp:Image ID="imageTemplate" runat="server"></asp:Image></asp:HyperLink>
                            <div class="link">
                                <asp:HyperLink ID="hlPreview2" runat="server">Preview</asp:HyperLink>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAfterFooter" runat="server">
    <div id="popup_bg" style="display:none;"></div>
    <div id="popup_preview" style="display:none;">
        <div class="content" data-mcs-theme="minimal-dark"></div>
        <a href class="close"></a>
    </div>
    <link href="<%= ResolveUrl("~/css/jquery.mcustomscrollbar.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>