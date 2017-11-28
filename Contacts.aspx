<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contacts.aspx.cs" 
Inherits="FlyerMe.Contacts" Theme="" Async="true" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="map"></div>
<div class="message" id="divSummary" runat="server" visible="false">
    <div class="content">
        <div class="icon"></div><asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
    </div>
</div>
<div class="text-content">
    <div class="content contact">
        <form method="post" enctype="application/x-www-form-urlencoded" runat="server">
            <div class="form-content">
                <div class="item small-b left">
                    <label>Name<span class="required">*</span></label>
                    <input type="text" id="inputName" runat="server" data-clientname="Name" />
                </div>
                <div class="item small-b">
                    <label>Company (Optional)</label>
                    <input type="text" id="inputCompany" runat="server" />
                </div>
                <div class="item small-b left">
                    <label>State<span class="required">*</span></label>
                    <asp:DropDownList ID="ddlStates" runat="server" DataSourceID="odsStates"
                        DataTextField="StateName" DataValueField="StateAbr" OnDataBound="ddlStates_DataBound">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsStates" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetStates" TypeName="FlyerMe.StatesBLL"></asp:ObjectDataSource>
                </div>
                <div class="item small-b">
                    <label>Email<span class="required">*</span></label>
                    <input type="text" id="inputEmail" runat="server" data-clientname="Email" />
                </div>
                <div class="item small-b left">
                    <label>Phone (Optional)</label>
                    <input type="text" id="inputPhone" runat="server" />
                </div>
                <div class="item small-b">
                    <label>Flyer ID (Optional)</label>
                    <input type="text" id="inputFlyerId" runat="server" />
                </div>
                <div class="item big-b">
                    <label>Message<span class="required">*</span></label>
                    <textarea type="text" id="textareaMessage" runat="server" placeholder="Text your message here..." rows="6" data-clientname="Message" />
                </div>                        
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </div>
        </form>
    </div>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&key=<%= ConfigurationManager.AppSettings["GoogleMapKey"] %>"></script>
</asp:Content>