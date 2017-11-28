<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="FlyerMe.SignUp" MasterPageFile="~/MasterPage.master" Theme="" Async="true" %>
<%@ Import Namespace="FlyerMe" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-content">
        <div class="content signup">
            <div class="soc-image" id="divSocImage" runat="server" visible="false">
                <div class="image">
                    <div class="table-cell">
                        <asp:Image ID="imageAvatar" runat="server" />
                    </div>
                </div>
            </div>
            <h1 id="h1Header" runat="server">Sign In</h1>
            <div class="social" id="divSocial" runat="server">
                <div class="text center">WITH</div>
                <a href="" class="facebook" onclick="(function(e){ masterPage.socksHelper.onFacebookLoginClick(e); })(event)">FACEBOOK</a>
                <a href="" class="linkedin" onclick="(function(e){ masterPage.socksHelper.onLinkedInLoginClick(e); })(event)">LINKEDIN</a>
                <div class="rs"><span>OR</span></div>
            </div>
            <div class="small-content" id="divLogin" runat="server">
                <form method="post" enctype="application/x-www-form-urlencoded" action="login.aspx<%= !String.IsNullOrEmpty(ReturnUrl) ? "?returnurl=" + ReturnUrl : null %>">
           			<div class="form-content">
                        <div class="item small-bs left">
                            <input type="email" placeholder="Email" name="UserName" tabindex="1"/>
                        </div>
                        <div class="item small-bs">
                            <input type="password" placeholder="Password" name="Password" tabindex="2"/>
                        </div>
                        <div class="item-content">
                            <div class="chekbox">
                                <input type="checkbox" id="rememberp" name="Remember"/><label for="rememberp">Remember Me</label>
                            </div>
                            <a href="<%= RootURL.ToUri().UrlToHttps() %>recoverpassword.aspx">Forgot password?</a>
                            <input type="submit" value="Log In"/>
                        </div>
                    </div>
                </form>
            </div>
            <div class="rs" id="divOr" runat="server"><span>OR</span></div>
            <h2>Sign Up</h2>
            <form method="post" enctype="application/x-www-form-urlencoded" action="signup.aspx" runat="server" id="formSignUp">
            	<div class="form-content">
                    <div class="summary-error" id="divSummaryError" runat="server"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
                    <div class="left-colmun">
                        <div class="item" id="divEmail" runat="server">
                            <label>Email<span class="required">*</span></label>
                            <input type="text" id="inputEmail" runat="server" data-clientname="Email" tabindex="3"/>
                        </div>
                        <div class="item" id="divConfirmEmail" runat="server">
                            <label>Confirm Email<span class="required">*</span></label>
                            <input type="text" id="inputEmailConfirm" runat="server" data-clientname="EmailConfirm" tabindex="4"/>
                        </div>
                        <div class="item" id="divPassword" runat="server">
                            <label>Password<span class="required">*</span></label>
                            <input type="password" id="inputPassword" runat="server" data-clientname="Password" tabindex="5"/>
                        </div>
                        <div class="item" id="divConfirmPassword" runat="server">
                            <label>Confirm Password<span class="required">*</span></label>
                            <input type="password" id="inputConfirmPassword" runat="server" data-clientname="ConfirmPassword" tabindex="6"/>
                        </div>
                        <div class="item" id="divFirstName" runat="server">
                            <label>First Name<span class="required">*</span></label>
                            <input type="text" id="inputFirstName" runat="server" data-clientname="FirstName" tabindex="7"/>
                        </div>
                        <div class="item" id="divMiddleName" runat="server">
                            <label>Middle Initial</label>
                            <input type="text" id="inputMiddleName" runat="server" tabindex="8"/>
                        </div>
                        <div class="item" id="divLastName" runat="server">
                            <label>Last Name<span class="required">*</span></label>
                            <input type="text" id="inputLastName" runat="server" data-clientname="LastName" tabindex="9"/>
                        </div>
                        <div class="item">
                            <label>Title<span class="required">*</span></label>
                            <asp:DropDownList ID="ddlTitle" runat="server" OnDataBound="ddlTitle_DataBound" DataTextField="Title" DataValueField="Title" tabindex="10"></asp:DropDownList>
                        </div>
                        <div class="item">
                            <label>Team Name</label>
                            <input type="text" id="inputTeamName" runat="server" tabindex="10"/>
                        </div>
                        <div class="item">
                            <label>Business Address 1<span class="required">*</span></label>
                            <input type="text" id="inputBusinessAddress1" runat="server" data-clientname="BusinessAddress1" tabindex="11"/>
                        </div>
                        <div class="item">
                            <label>Business Address 2</label>
                            <input type="text" id="inputBusinessAddress2" runat="server" tabindex="12"/>
                        </div>
                        <div class="item">
                            <label>Business City<span class="required">*</span></label>
                            <input type="text" id="inputBusinessCity" runat="server" data-clientname="BusinessCity" tabindex="13"/>
                        </div>
                        <div class="item">
                            <label>Business State<span class="required">*</span></label>
                            <asp:DropDownList ID="ddlBusinessState" runat="server" DataTextField="StateName" DataValueField="StateAbr" AutoPostBack="False" OnDataBound="ddlBusinessState_DataBound" TabIndex="14"></asp:DropDownList>
                        </div>
                        <div class="item">
                            <label>Business Zip Code<span class="required">*</span></label>
                            <input type="text" id="inputBusinessZipCode" runat="server" data-clientname="BusinessZipCode" tabindex="15"/>
                        </div>
                        <div class="item">
                            <label>Association</label>
                            <select name="association">
                                <option value="">Select Association...</option>
                            </select>
                        </div>
                        <div class="item">
                            <label>BRE#<i class="info"><div class="tooltip"><strong>BRE Number (CA)</strong> All licensed agents in the state of California are required to list their BRE identification number on all information.</div></i></label>
                            <input type="text" id="inputBre" runat="server" tabindex="17" />
                        </div>
                    </div>
                    <div class="right-colmun">
                        <div class="item">
                            <div class="phone">
                                <label>Business Phone<span class="required">*</span></label>
                                <input type="text" id="inputBusinessPhone" runat="server" data-clientname="BusinessPhone" tabindex="18"/>
                            </div>
                            <div class="ext">
                                <label>Ext.</label>
                                <input type="text" id="inputBusinessPhoneExt" runat="server" tabindex="19"/>
                            </div>
                        </div>
                        <div class="item">
                            <label>Home Phone</label>
                            <input type="text" id="inputHomePhone" runat="server" data-clientname="HomePhone" tabindex="20"/>
                        </div>
                        <div class="item">
                            <label>Cell Phone</label>
                            <input type="text" id="inputCellPhone" runat="server" data-clientname="CellPhone" tabindex="21"/>
                        </div>
                        <div class="item">
                            <label>Business Fax</label>
                            <input type="text" id="inputBusinessFax" runat="server" data-clientname="BusinessFax" tabindex="22"/>
                        </div>
                        <div class="item">
                            <label>Website</label>
                            <input type="text" id="inputWebsite" runat="server" data-clientname="Website" tabindex="24"/>
                        </div>
                        <div class="item">
                            <label>Brokerage Name<span class="required">*</span></label>
                            <input type="text" id="inputBrokerageName" runat="server" data-clientname="BrokerageName" tabindex="25"/>
                        </div>
                        <div class="item">
                            <label>Brokerage Address 1<span class="required">*</span></label>
                            <input type="text" id="inputBrokerageAddress1" runat="server" data-clientname="BrokerageAddress1" tabindex="26"/>
                        </div>
                        <div class="item">
                            <label>Brokerage Address 2</label>
                            <input type="text" id="inputBrokerageAddress2" runat="server" tabindex="27"/>
                        </div>
                        <div class="item">
                            <label>Brokerage City<span class="required">*</span></label>
                            <input type="text" id="inputBrokerageCity" runat="server" data-clientname="BrokerageCity" tabindex="28"/>
                        </div>
                        <div class="item">
                            <label>Brokerage State<span class="required">*</span></label>
                            <asp:DropDownList ID="ddlBrokerageState" runat="server" DataTextField="StateName" DataValueField="StateAbr" OnDataBound="ddlBrokerageState_DataBound" TabIndex="29"></asp:DropDownList>
                        </div>
                        <div class="item">
                            <label>Brokerage Zip Code<span class="required">*</span></label>
                            <input type="text" id="inputBrokerageZipCode" runat="server" data-clientname="BrokerageZipCode" tabindex="30"/>
                        </div>
                        <div class="item">
                            <label>Secondary Email</label>
                            <input type="text" id="inputSecondaryEmail" runat="server" data-clientname="SecondaryEmail" tabindex="31"/>
                        </div>
                        <div class="item">
                            <label>Referred Source</label>
                            <asp:DropDownList ID="ddlReferredSource" runat="server" TabIndex="32">
                                <asp:ListItem Text="" Value="NA"></asp:ListItem>
                                <asp:ListItem Text="Agent" Value="Agent"></asp:ListItem>
                                <asp:ListItem Text="Friend" Value="Friend"></asp:ListItem>
                                <asp:ListItem Text="Realtor.org" Value="Realtor.org"></asp:ListItem>
                                <asp:ListItem Text="Search Engine" Value="Search Engine"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="item">
                            <label>Referred By<i class="info"><div class="tooltip"><strong>Referral Program!</strong>
Please let us know if someone referred you by entering in their email address.</div></i></label>
                            <input type="text" id="inputReferredBy" runat="server" tabindex="33"/>
                        </div>
                        <div class="item">
                            <label id="labelNewletter" runat="server"></label>
                            <asp:DropDownList ID="ddlNewsletter" runat="server" TabIndex="34">
                                <asp:ListItem Text="Yes please" Value="Yes" Selected="True" />
                                <asp:ListItem Value="No">No thanks</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="item-content">
                        <div class="chekbox">
                            <input type="checkbox" id="cbTerm" runat="server" data-clientname="TermsAndConditions" tabindex="35"><asp:Label ID="lblTerm" runat="server" AssociatedControlID="cbTerm">I have read and agree to <%= clsUtility.ProjectName %>'s <a href="<%= RootURL %>termsofuse.aspx" target="_blank">Terms and Conditions</a></asp:Label>
                        </div>
                        <input type="hidden" id="hiddenScrollY" runat="server"/>
                        <asp:Button ID="btnSignUp" runat="server" OnClick="btnSignUp_Click" Text="Sign Up" TabIndex="36"/>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>