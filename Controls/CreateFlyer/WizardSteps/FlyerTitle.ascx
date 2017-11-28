<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlyerTitle.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.FlyerTitle" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers fill">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <h1>Fill the titles</h1>
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="item big">
                        <label>Flyer title<span class="required">*</span> <i class="info"><div class="tooltip">Enter an appealing and descriptive heading for your flyer here.</div></i></label>
                        <input type="text" id="inputFlyerTitle" runat="server" data-clientname="FlyerTitle">
                    </div>
                    <div class="item big">
                        <label>Email subject<span class="required">*</span> <i class="info"><div class="tooltip">Enter a catchy text here. Your flyer will be emailed to other agents or clients. The text specified by you in this field will be used as a subject line for delivering the mail.</div></i></label>
                        <input type="text" id="inputEmailSubject" runat="server" data-clientname="EmailSubject">
                    </div>
                    <div class="item submit">
                        <input type="submit" id="inputNext" runat="server" value="Next">
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