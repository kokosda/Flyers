<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="FlyerMe.EditProfile" MasterPageFile="~/MasterPageAccount.master" Theme="" %>
<%@ Register Src="~/Controls/MasterPageAccount/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <uc:UserMenu ID="userMenu" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="message saved" id="divSummarySuccess" runat="server" visible="false">
    <div class="content">
        <div class="icon"></div><asp:Literal ID="ltlSuccessMessage" runat="server"></asp:Literal>
    </div>
</div>
<div class="account">
    <form method="post" enctype="multipart/form-data" runat="server" id="form">
        <div class="form-content">
            <div class="summary-error" id="divSummaryError" runat="server"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
            <fieldset>
                <legend>Profile info</legend>
                <div class="block">
                    <div class="conteiner">
                        <div class="content">
                            <div class="text userpic">
                                Upload your photo. Use one of the following formats: .jpg, .png, .gif.
                            </div>
                            <div class="choice">
                                <input type="file" id="inputFile2MyPhoto" runat="server" accept="image/jpeg,image/png,image/gif" />
                                <a href="#">UPLOAD A FILE FROM<br />YOUR COMPUTER</a>
                            </div>
                        </div>
                    </div>
                    <div class="picture">
                        <div class="image">
                            <div class="table-cell" id="divMyPhoto" runat="server">
                                <input type="file" id="inputFileMyPhoto" runat="server" accept="image/jpeg,image/png,image/gif" />
                                <input type="button" value="delete" class="delete" />
                                <asp:Image ID="imageMyPhoto" runat="server" />
                                <input type="hidden" name="DeleteMyPhoto" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <label>First Name<span class="required">*</span></label>
                    <input type="text" id="inputFirstName" runat="server" data-clientname="FirstName"/>
                </div>
                <div class="item">
                    <label>Middle Initial</label>
                    <input type="text" id="inputMiddleName" runat="server"/>
                </div>
                <div class="item">
                    <label>Last Name<span class="required">*</span></label>
                    <input type="text" id="inputLastName" runat="server" data-clientname="LastName"/>
                </div>
                <div class="item email">
                    <label>Email <span></span></label>
                    <input type="text" id="inputEmail" runat="server" disabled="disabled" />
                </div>
                <div class="item">
                    <label>Title<span class="required">*</span></label>
                    <asp:DropDownList ID="ddlTitle" runat="server" DataTextField="Title" DataValueField="Title" OnDataBound="ddlTitle_DataBound"></asp:DropDownList>
                </div>
                <div class="item">
                    <label>Team Name</label>
                    <input type="text" id="inputTeamName" runat="server"/>
                </div>
            </fieldset>
            <fieldset>
                <legend>Change your password</legend>
                <div class="item">
                    <label>Current Password</label>
                    <input type="password" id="inputCurrentPassword" runat="server" data-clientname="CurrentPassword"/>
                </div>
                <div class="item">
                    <label>New Password</label>
                    <input type="password" id="inputNewPassword" runat="server" data-clientname="NewPassword"/>
                </div>
                <div class="item">
                    <label>Confirm New Password</label>
                    <input type="password" id="inputConfirmNewPassword" runat="server" data-clientname="ConfirmNewPassword"/>
                </div>
                <div class="item">
                    <asp:Button ID="btnChangePassword" runat="server" CssClass="change_password" OnClick="btnChangePassword_Click" Text="CHANGE PASSWORD" />
                </div>
            </fieldset>
            <fieldset>
                <legend>Business info</legend>
                <div class="block">
                    <div class="conteiner">
                        <div class="content">
                            <div class="text">
                                Company logo. We have categorized our database of agents into three main categories i.e. according to County, Realtor Association and Metropolitan Statistical Area (MSA).<br />
                                Use one of the following formats: .jpg, .png, .gif.
                            </div>
                            <div class="choice">
                                <a href="#">UPLOAD A FILE FROM<br />YOUR COMPUTER</a>
                                <input type="file" id="inputFile2MyLogo" runat="server" accept="image/jpeg,image/png,image/gif" />
                            </div>    
                        </div>
                    </div>
                    <div class="picture">
                        <div class="image logo">
                            <div class="table-cell" id="divMyLogo" runat="server">
                                <input type="file" id="inputFileMyLogo" runat="server" accept="image/jpeg,image/png,image/gif" />
                                <input type="button" value="delete" class="delete" />
                                <asp:Image ID="imageMyLogo" runat="server" />
                                <input type="hidden" name="DeleteMyLogo" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <label>Business Address 1<span class="required">*</span></label>
                    <input type="text" id="inputBusinessAddress1" runat="server" data-clientname="BusinessAddress1"/>
                </div>
                <div class="item">
                    <label>Business Address 2</label>
                    <input type="text" id="inputBusinessAddress2" runat="server"/>
                </div>
                <div class="item">
                    <label>Business City<span class="required">*</span></label>
                    <input type="text" id="inputBusinessCity" runat="server" data-clientname="BusinessCity"/>
                </div>
                <div class="item">
                    <label>Business State<span class="required">*</span></label>
                    <asp:DropDownList ID="ddlBusinessState" runat="server" DataTextField="StateName" DataValueField="StateAbr" AutoPostBack="True"  OnSelectedIndexChanged="ddlBusinessState_SelectedIndexChanged" OnDataBound="ddlBusinessState_DataBound"></asp:DropDownList>
                </div>
                <div class="item zip">
                    <label>Business Zip Code<span class="required">*</span></label>
                    <input type="text" id="inputBusinessZipCode" runat="server" data-clientname="BusinessZipCode"/>
                </div>
                <div class="item">
                    <label>Association</label>
                    <asp:DropDownList ID="ddlAssociation" runat="server" DataTextField="association_name" DataValueField="association_id"  AutoPostBack="False" OnDataBound="ddlAssociation_DataBound"></asp:DropDownList>
                </div>
                <div class="item">
                    <label>BRE Number <i class="info" title="All licensed agents in the state of California are required to list their BRE identification number on all information."></i></label>
                    <input type="text" id="inputBre" runat="server"/>
                </div>
                <div class="item">
                    <div class="phone">
                        <label>Business Phone<span class="required">*</span></label>
                        <input type="text" id="inputBusinessPhone" runat="server" data-clientname="BusinessPhone"/>
                    </div>
                    <div class="ext">
                        <label>Ext.</label>
                        <input type="text" id="inputBusinessPhoneExt" runat="server"/>
                    </div>
                </div>
                <div class="item">
                    <label>Home Phone</label>
                    <input type="text" id="inputHomePhone" runat="server" data-clientname="HomePhone"/>
                </div>
                <div class="item">
                    <label>Cell Phone</label>
                    <input type="text" id="inputCellPhone" runat="server" data-clientname="CellPhone"/>
                </div>
                <div class="item">
                    <label>Business Fax</label>
                    <input type="text" id="inputBusinessFax" runat="server" data-clientname="BusinessFax"/>
                </div>
                <div class="item">
                    <label>Website</label>
                    <input type="text" id="inputWebsite" runat="server" data-clientname="Website"/>
                </div>
                <div class="item">
                    <label>Brokerage Name<span class="required">*</span></label>
                    <input type="text" id="inputBrokerageName" runat="server" data-clientname="BrokerageName"/>
                </div>
                <div class="item">
                    <label>Brokerage Address 1<span class="required">*</span></label>
                    <input type="text" id="inputBrokerageAddress1" runat="server" data-clientname="BrokerageAddress1"/>
                </div>
                <div class="item">
                    <label>Brokerage Address 2</label>
                    <input type="text" id="inputBrokerageAddress2" runat="server"/>
                </div>
                <div class="item">
                    <label>Brokerage City<span class="required">*</span></label>
                    <input type="text" id="inputBrokerageCity" runat="server" data-clientname="BrokerageCity"/>
                </div>
                <div class="item">
                    <label>Brokerage State<span class="required">*</span></label>
                    <asp:DropDownList ID="ddlBrokerageState" runat="server" DataTextField="StateName" DataValueField="StateAbr" OnDataBound="ddlBrokerageState_DataBound"></asp:DropDownList>
                </div>
                <div class="item zip">
                    <label>Brokerage Zip Code<span class="required">*</span></label>
                    <input type="text" id="inputBrokerageZipCode" runat="server" data-clientname="BrokerageZipCode"/>
                </div>
                <div class="item">
                    <label>Secondary Email</label>
                    <input type="text" id="inputSecondaryEmail" runat="server" data-clientname="SecondaryEmail"/>
                </div>
                <div class="item" id="divReferredSource" runat="server">
                    <label>Referred Source</label>
                    <asp:DropDownList ID="ddlReferredSource" runat="server">
                        <asp:ListItem Text="" Value="NA"></asp:ListItem>
                        <asp:ListItem Text="Agent" Value="Agent"></asp:ListItem>
                        <asp:ListItem Text="Friend" Value="Friend"></asp:ListItem>
                        <asp:ListItem Text="Realtor.org" Value="Realtor.org"></asp:ListItem>
                        <asp:ListItem Text="Search Engine" Value="Search Engine"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="item" id="divReferredBy" runat="server">
                    <label>Referred By<i class="info" title="Please let us know if someone referred you by entering in their email address."></i></label>
                    <input type="text" id="inputReferredBy" runat="server" />
                </div>
                <div class="item">
                    <label id="labelNewletter" runat="server"></label>
                    <asp:DropDownList ID="ddlNewsletter" runat="server">
                        <asp:ListItem Text="Yes please" Value="Yes" Selected="True" />
                        <asp:ListItem Value="No">No thanks</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </fieldset>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"/>
            <input type="hidden" id="hiddenScrollY" runat="server"/>
        </div>
    </form>
</div>
</asp:Content>