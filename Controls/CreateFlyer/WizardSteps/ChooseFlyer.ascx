<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChooseFlyer.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.ChooseFlyer" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers flyer">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="flyer-diz">
                        <asp:Repeater ID="rptSamples" runat="server" OnItemDataBound="rptSamples_ItemDataBound">
                            <ItemTemplate>
                                <div class="block" id="divBlock" runat="server">
                                    <asp:Image ID="imageTemplate" runat="server" />
                                    <div class="buttons">
                                        <a id="aPreview" runat="server" data-clientname="Preview" target="_blank">Preview</a>
                                        <a id="aSelectMe" runat="server" data-clientname="SelectMe">Select Me</a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="item submit">
                        <input type="hidden" id="inputLayout" runat="server" data-clientname="Layout" />
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