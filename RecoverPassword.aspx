<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecoverPassword.aspx.cs" Inherits="FlyerMe.RecoverPassword" MasterPageFile="~/MasterPage.master" Theme="" Async="true" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (!SuccessfullySent) { %>
    <div class="text-content">
        <div class="content form">
            <h1>Recover your password</h1>
            <form method="post" enctype="application/x-www-form-urlencoded" action="recoverpassword.aspx">
           		<div class="form-content">
                    <div class="item center text">
                    Enter your username, or the email address that you used to register. We’ll send you an email with your username and password.
                    </div>
                    <div class="item big">
                        <input type="email" placeholder="Enter your e-mail" name="Email" value="<%= Email %>" class="<%= HasError ? "error" : String.Empty %>"/>
                        <% if (HasError) { %>
                        <label class="error"><%= Message %></label>
                        <% } %>
                    </div>
                    <div class="item submit center">
                        <input type="submit" value="Reset"/>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <% } else { %>
    <div class="sendm">
    	<div class="icon ok"></div>
    	<h1>Your password has been sent to you</h1>
    </div>
    <% } %>
</asp:Content>