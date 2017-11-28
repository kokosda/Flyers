<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArchivedDelivery.aspx.cs" Inherits="FlyerMe.Admin.Reports.ArchivedDelivery" Theme="" MasterPageFile="~/Admin/MasterPage.master" Async="true" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <uc:Message ID="message" runat="server" />
                <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                    <div class="add-agents">
                        <div class="item-box">
                            <div class="item">
                                <label>Flyer ID<span class="required">*</span></label>
                                <input type="text" id="inputOrderId" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Emails count<span class="required">*</span></label>
                                <input type="text" id="inputEmailAddressesCount" runat="server" value="50" required />
                            </div>
                            <div class="item">
                                <label>File</label>
                                <div class="tekst-input">
                                    <asp:HyperLink ID="hlDownloadFile" runat="server" Visible="false">Download File</asp:HyperLink>
                                    <asp:Button ID="btnGetOrderDetails" runat="server" Text="Get Order Details" OnClick="btnGetOrderDetails_Click" />
                                </div>
                            </div>
                            <div class="item">
                                <label>User Email</label>
                                <input type="text" id="inputUserEmail" runat="server" />
                            </div>
                            <div class="item">
                                <label>Subject</label>
                                <input type="text" id="inputSubject" runat="server" />
                            </div>
                            <div class="item">
                                <label>Mail Body</label>
                                <textarea id="textareaMailBody" runat="server"></textarea>
                            </div>
                        </div>
                        <div class="item-submit" id="divSendFlyerResults" runat="server" visible="false">
                            <asp:Button ID="btnSendFlyerResults" runat="server" Text="Send Flyer Results" OnClick="btnSendFlyerResults_Click" />
                        </div>
                    </div>
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>