<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowFlyer.aspx.cs" Inherits="FlyerMe.ShowFlyer"  MasterPageFile="~/MasterPage.master" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="show-flyer" id="divShowFlyer" runat="server">
            <asp:Literal ID="ltlFlyerMarkup" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>