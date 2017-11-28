<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="FlyerMe.Admin.ProfileAdmin" Theme="" MasterPageFile="~/Admin/MasterPage.master" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form method="post" enctype="application/x-www-form-urlencoded" runat="server">
					<div class="back add">
                        <asp:HyperLink NavigateUrl="~/admin/logout.aspx" runat="server">exit the profile</asp:HyperLink>
					</div>
                    <uc:Message id="message" runat="server" />
                    <div class="add-agents admin">
                        <div class="item-box">
                            <asp:HiddenField ID="hfUserId" runat="server" />
                            <div class="item">
                                <label>Login (Email)<span class="required">*</span></label>
                                <input type="email" id="inputLogin" runat="server" required />
                            </div>
                            <div class="item">
                                <label>New Password<span class="required">*</span></label>
                                <input type="password" id="inputNewPassword" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Confirm Password<span class="required">*</span></label>
                                <input type="password" id="inputConfirmPassword" runat="server" required />
                            </div>
                        </div>
                        <div class="item-box">                                
                        </div>
                        <div class="item-submit">
                            <asp:Button ID="btnSaveProfile" runat="server" Text="Save Profile" OnCommand="btnSaveProfile_Command" />
                        </div>
                    </div>
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>