<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Description.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.Description" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers description">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <form method="get" enctype="application/x-www-form-urlencoded" action="<%= ResolveClientUrl("~/createflyer.aspx/getpropertydata") %>" <%= !IsZillowBlockVisible ? "style='display:none;'" : null %>>
            	<div class="form-content">
                    <div class="zillow-block">
                        <div class="grey-right-button">
                            <input type="submit" class="zillowinput" value="Get property data" />
                            <div class="bg"></div>
                        </div>
                        <a href="http://zillow.com" class="zillow" target="_blank"><img src="<%= ResolveClientUrl("~/images/zillow.png") %>" alt="Zillow" /></a>
                    </div>
                    <input type="hidden" name="address" value="<%= Flyer.StreetAddress %>" />
                    <input type="hidden" name="city" value="<%= Flyer.City %>" />
                    <input type="hidden" name="state" value="<%= Flyer.State %>" />
                    <input type="hidden" name="zipCode" value="<%= Flyer.ZipCode %>" />
                </div>
            </form>
            <h1>Fill in your property description</h1>
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="item small-bs left">
                        <label>MLS number<i class="info"><div class="tooltip">Enter MLS number of the property. This is an optional field.</div></i></label>
                        <input type="text" id="inputMlsNumber" runat="server" data-clientname="MlsNumber"/>
                    </div>
                    <div class="item small-bs">
                        <label>Year built</label>
                        <input type="text" id="inputYearBuilt" runat="server" data-clientname="YearBuilt"/>
                    </div>
                    <div class="item small-bs left">
                        <label>Subdivision<i class="info"><div class="tooltip">Provide more property details using these custom fields. Enter label (like Bedrooms, Sq. Footage, Pets etc.) in the first box and then its corressponding value (like 4, 3500, Allowed etc.) to the box on right.</div></i></label>
                        <input type="text" id="inputSubdivision" runat="server" data-clientname="Subdivision"/>
                    </div>
                    <div class="item small-bs">
                        <label>HOA</label>
                        <input type="text" id="inputHoa" runat="server" data-clientname="Hoa"/>
                    </div>
                    <hr />
                    <div class="item small-bs left">
                        <label>Bedrooms</label>
                        <asp:DropDownList ID="ddlBedrooms" runat="server" data-clientname="Bedrooms"></asp:DropDownList>
                    </div>
                    <div class="item small-bs">
                        <label>Bathrooms</label>
                        <asp:DropDownList ID="ddlBathrooms" runat="server" data-clientname="Bathrooms"></asp:DropDownList>
                    </div>
                    <div class="item small-bs left">
                        <label>Lot size</label>
                        <input type="text" id="inputLotSize" runat="server" data-clientname="LotSize"/>
                    </div>
                    <div class="item small-bs">
                        <label>Sqft</label>
                        <input type="text" id="inputSqft" runat="server" data-clientname="Sqft">
                    </div>
                    <div class="item small-bs left">
                        <label>Floors</label>
                        <asp:DropDownList ID="ddlFloors" runat="server" data-clientname="Floors"></asp:DropDownList>
                    </div>
                    <div class="item small-bs">
                        <label>Parking</label>
                        <asp:DropDownList ID="ddlParking" runat="server" data-clientname="Parking"></asp:DropDownList>
                    </div>
                    <div class="item big" id="divOpenHouses" runat="server">
                        <label>Open Houses</label>
                        <input type="text" id="inputOpenHouses" runat="server" data-clientname="OpenHouses" />
                    </div>
                    <div class="item big">
                        <label>Description<i class="info"><div class="tooltip">Provide a short & snappy description of your property here. Do not cover topics like number of rooms etc as we have provided custom fields for the same below.</div></i></label>
                        <textarea rows="2" id="textareaDescription" runat="server" data-clientname="Description"></textarea>
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