<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Verify.aspx.cs" Inherits="FlyerMe.Verify" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="message" id="divSummary" runat="server">
        <div class="content">
            <div class="icon"></div>
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>