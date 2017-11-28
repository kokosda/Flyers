<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Location.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.Location" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers location">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <h1>Fill in your property location</h1>
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="item big">
                        <label>Street address</label>
                        <input type="text" id="inputStreetAddress" runat="server" placeholder="House name/number + street/road" data-clientname="StreetAddress"/>
                    </div>
                    <div class="item big">
                        <label>Apt, Suite, Bldg.</label>
                        <input type="text" id="inputAptSuiteBldg" runat="server" placeholder="Apt., suite, building acces code" data-clientname="AptSuiteBldg">
                    </div>
                    <div class="item small-bs left">
                        <label>City</label>
                        <input type="text" id="inputCity" runat="server" data-clientname="City">
                    </div>
                    <div class="item small-bs">
                        <label>State</label>
                        <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateAbr"></asp:DropDownList>
                    </div>
                    <div class="item small-bs left">
                        <label>Zip code</label>
                        <input type="text" id="inputZipCode" runat="server" data-clientname="ZipCode">
                    </div>
                    <div class="item small-bs chekbox" style="display:none;">
                        <input type="checkbox" id="inputAddMap" runat="server" data-clientname="AddMap"><asp:Label runat="server" AssociatedControlID="inputAddMap">Add google map link</asp:Label>
                        <input type="hidden" id="inputMapLink" runat="server" data-clientname="MapLink" />
                    </div>
                    <div class="item submit">
                        <input type="submit" id="inputNext" runat="server" value="Next">
                        <ul>
                            <li><a href="<%= ResolveUrl("~/preview.aspx") %>" class="preview" title="Preview" target="_blank">Preview</a></li>
                            <li><a href="<%= ResolveUrl("~/createflyer.aspx/saveindrafts") %>" class="save" title="Save in drafts">Save in drafts</a></li>
                        </ul>
                    </div>
                    <hr style="display:none;"/>
                    <div id="map" style="display:none;"></div>
                </div>
            </form>
        </div>
    </div>
    <div class="flyers-icon"></div>
</div>