<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PropertyType.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.PropertyType" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers type">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <h1>Fill in your property type</h1>
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="item big">
                        <label>Offer type</label>
                        <div class="radioset">
                            <input type="radio" id="inputRealEstate" name="offer" value="real estate" <%= IsOfferChecked("real estate") ? "checked" : null %>><label for="inputRealEstate">Real estate</label>
                            <input type="radio" id="inputRentals" name="offer" value="rentals" <%= IsOfferChecked("rentals") ? "checked" : null %>><label for="inputRentals">Rentals</label>
                        </div>
                    </div>
                    <div class="item big">
                        <label>Property type</label>
                        <div class="radioset">
                            <input type="radio" id="inputResidential" name="property" value="residential" <%= IsPropertyTypeChecked("residential") ? "checked" : null %>><label for="inputResidential">Residential</label>
                            <input type="radio" id="inputCommercial" name="property" value="commercial" <%= IsPropertyTypeChecked("commercial") ? "checked" : null %>><label for="inputCommercial">Commercial</label>
                        </div>
                    </div>
                    <div class="item small-bs clear">
                        <label>Residential type</label>
                        <asp:DropDownList ID="ddlResidentialType" runat="Server" DataTextField="PropertyType" DataValueField="PropertyTypeID"></asp:DropDownList>
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