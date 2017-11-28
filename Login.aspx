<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="FlyerMe.LogIn" MasterPageFile="~/MasterPage.master" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-content">
        <div class="content form">
            <h1>Log In</h1>
            <div class="social">
                <div class="text center">WITH</div>
                <a href="#" class="facebook" onclick="(function(e){ masterPage.socksHelper.onFacebookLoginClick(e); })(event)">FACEBOOK</a>
            	<a href="#" class="linkedin" onclick="(function(e){ masterPage.socksHelper.onLinkedInLoginClick(e); })(event)">LINKEDIN</a>
                <div class="rs"><span>OR</span></div>
            </div>
            <form method="post" enctype="application/x-www-form-urlencoded">
                <div class="form-content">
                    <div class="summary-error" id="divSummaryError" runat="server" visible="false"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
                    <div class="item big">
                        <label>Email</label>
                        <input type="email" name="UserName" value="<%= UserName %>" />
                    </div>
                    <div class="item big">
                        <label>Password</label>
                        <input type="password" name="Password" />
                    </div>
                    <div class="chekbox big">
                        <input type="checkbox" id="rememberp" name="Remember" <%= RememberIsChecked ? "checked" : String.Empty %>/><label for="rememberp">Remember Me</label>
                    </div>
                    <div class="item submit center">
                        <input type="submit" value="Log In" />
                    </div>
                </div>
            </form>
            <ul class="links">
                <li><a href="<%= RootURL %>recoverpassword.aspx">Forgot password?</a></li>
                <li><a href="<%= RootURL %>signup.aspx">Not a member yet?</a></li>
            </ul>
        </div>
    </div>
</asp:Content>