<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlterUsername.aspx.cs" Inherits="FlyerMe.Admin.AlterUsername" Theme="" MasterPageFile="~/Admin/MasterPage.master" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <uc:Message id="message" runat="server" />
                <form method="post" enctype="application/x-www-form-urlencoded" runat="server">
                    <div class="add-agents admin">
                        <div class="item-box">
                            <div class="item">
                                <label>Old Username<span class="required">*</span></label>
                                <input type="text" id="inputOldUsername" runat="server" required />
                            </div>
                            <div class="item">
                                <label>New Username<span class="required">*</span></label>
                                <input type="text" id="inputNewUsername" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Confirm Username<span class="required">*</span></label>
                                <input type="text" id="inputConfirmUsername" runat="server" required />
                            </div>
                        </div>
                        <div class="item-submit">
                            <asp:Button ID="btnSubmit" runat="server" OnCommand="btnSubmit_Command" Text="Submit" />
                        </div>
                    </div>
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>