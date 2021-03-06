<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DesiredLocation.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.DesiredLocation" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers location">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <h1>Fill in the desired property location</h1>
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="item big">
                        <textarea rows="4" id="textareaDesiredLocation" runat="server" data-clientname="DesiredLocation" placeholder="Describe your customerís location requirements"></textarea>
                    </div>
                    <div class="item submit">
                        <input type="submit" value="Next">
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