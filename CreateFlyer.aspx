<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateFlyer.aspx.cs" Inherits="FlyerMe.CreateFlyer" MasterPageFile="~/MasterPageAccount.master" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flyers">
        <div class="flyers-blocks">
            <div class="block seller">
                <div class="icon"></div>
                <h2>Seller’s agent<br/>flyer</h2>
                Showcase your listing in front of thousands of Agents.
                <a href="<%= ResolveUrl("~/createflyer/sellersagent.aspx") %>" class="choose">Choose</a>
                <a href="<%= ResolveUrl("~/createflyer/sellersagent.aspx") %>" class="linck"></a>
            </div>
            <div class="block custom">
                <div class="icon"></div>
                <h2>Custom<br/>flyer</h2>
                Upload and send your custom design. 
                <a href="<%= ResolveUrl("~/createflyer/custom.aspx") %>" class="choose">Choose</a>
                <a href="<%= ResolveUrl("~/createflyer/custom.aspx") %>" class="linck"></a>
            </div>
            <div class="block buyer">
                <div class="icon"></div>
                <h2>Buyer’s agent<br/>flyer</h2>
                Find the perfect property for your client. 
                <a href="<%= ResolveUrl("~/createflyer/buyersagent.aspx") %>" class="choose">Choose</a>
                <a href="<%= ResolveUrl("~/createflyer/buyersagent.aspx") %>" class="linck"></a>
            </div>
        </div>
    </div>
</asp:Content>