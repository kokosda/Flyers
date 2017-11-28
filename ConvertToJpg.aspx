<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConvertToJpg.aspx.cs" Inherits="FlyerMe.ConvertToJpg" MasterPageFile="~/MasterPageAccount.master" Theme="" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <div class="flyer-menu">
    	<div class="content">
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="convertm">
    	<div class="icon convert"></div>
    	<h1>Right click on image below and save to your computer</h1>
        <div class="text"><strong>Note:</strong> Please wait for 2 minutes if image doesn't appeare below and referesh the page again. Still image doesn't appeare? Please contact <a href="mailto:<%= clsUtility.ContactUsEmail %>"><%= clsUtility.ContactUsEmail %></a>.
        </div>
        <asp:Image ID="imgOrder" runat="server" />
    </div>
</asp:Content>