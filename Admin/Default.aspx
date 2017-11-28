<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="FlyerMe.Admin.Default" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <ul class="index-menu">
                    <li><a href="<%= ResolveUrl("~/admin/users/websiteusers.aspx") %>" class="icons user"><span></span>Users</a></li>
                    <li><a href="<%= ResolveUrl("~/admin/flyers.aspx") %>" class="icons orders"><span></span>Flyers</a></li>
                    <li><a href="<%= ResolveUrl("~/admin/agents.aspx") %>" class="icons agents"><span></span>Agents</a></li>
                    <li><a href="<%= ResolveUrl("~/admin/discounts/promo.aspx") %>" class="icons discount"><span></span>Discounts</a></li>
                    <!--<li><a href="#" class="icons bad_emails"><span></span>Bad Emails</a></li>-->
                    <li><a href="<%= ResolveUrl("~/admin/others/generatefeedsforsyndication.aspx") %>" class="icons others"><span></span>Others</a></li>
                    <!--<li><a href="#" class="icons apps_status"><span></span>Apps Status</a></li>-->
                </ul>
            </div>
        </div>
    </div>
</asp:Content>