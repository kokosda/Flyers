<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactDetails.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.ContactDetails" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers contact">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <h1>Fill in your contact details</h1>
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="item big">
                        <label>Name<span class="required">*</span></label>
                        <input type="text" id="inputName" runat="server" data-clientname="Name"/>
                    </div>
                    <div class="item big">
                        <div class="phone">
                            <label>Phone<span class="required">*</span></label>
                            <input type="text" id="inputPhone" runat="server" data-clientname="Phone"/>
                        </div>
                        <div class="ext">
                            <label>Ext.</label>
                            <input type="text" id="inputExt" runat="server" data-clientname="Ext"/> 
                        </div>
                    </div>
                    <div class="item big">
                        <label>Email<span class="required">*</span></label>
                        <input type="text" id="inputEmail" runat="server" data-clientname="Email"/>
                    </div>
                    <div class="item submit">
                        <input type="submit" value="Next" id="inputNext" runat="server"/>
                        <ul>
                            <li><a href="<%= ResolveUrl("~/preview.aspx") %>" class="preview" title="Preview" target="_blank">Preview</a></li>
                            <li><a href="<%= ResolveUrl("~/createflyer.aspx/saveindrafts") %>" class="save" title="Save in drafts">Save in drafts</a></li>
                        </ul>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <div class="flyers-icon"></div>
</div>