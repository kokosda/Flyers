<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Amenities.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.Amenities" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers amenities">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <h1>Common amenities</h1>
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="chekbox colmun">
                         <input type="checkbox" id="checkboxCheckAll"><label for="checkboxCheckAll">Check all</label>
                    </div>
                    <hr />
                    <div class="chekbox colmun" data-checkall>
                        <input type="checkbox" id="checkboxCentralAc" runat="server" data-clientname="CentralAc"><asp:Label runat="server" AssociatedControlID="checkboxCentralAc">Central A/C</asp:Label>
                        <input type="checkbox" id="checkboxCentralHeat" runat="server" data-clientname="CentralHeat"><asp:Label runat="server" AssociatedControlID="checkboxCentralHeat">Central heat</asp:Label>
                        <input type="checkbox" id="checkboxFireplace" runat="server" data-clientname="Fireplace"><asp:Label runat="server" AssociatedControlID="checkboxFireplace">Fireplace</asp:Label>
                        <input type="checkbox" id="checkboxHighVaultedCeiling" runat="server" data-clientname="HighVaultedCeiling"><asp:Label runat="server" AssociatedControlID="checkboxHighVaultedCeiling">High/Vaulted ceiling</asp:Label>
                        <input type="checkbox" id="checkboxWalkInCloset" runat="server" data-clientname="WalkInCloset"><asp:Label runat="server" AssociatedControlID="checkboxWalkInCloset">Walk-in closet</asp:Label>
                        <input type="checkbox" id="checkboxHardwoodFloor" runat="server" data-clientname="HardwoodFloor"><asp:Label runat="server" AssociatedControlID="checkboxHardwoodFloor">Hardwood floor</asp:Label>
                        <input type="checkbox" id="checkboxTileFloor" runat="server" data-clientname="TileFloor"><asp:Label runat="server" AssociatedControlID="checkboxTileFloor">Tile floor</asp:Label>
                        <input type="checkbox" id="checkboxFamilyRoom" runat="server" data-clientname="FamilyRoom"><asp:Label runat="server" AssociatedControlID="checkboxFamilyRoom">Family room</asp:Label>
                        <input type="checkbox" id="checkboxLivingRoom" runat="server" data-clientname="LivingRoom"><asp:Label runat="server" AssociatedControlID="checkboxLivingRoom">Living room</asp:Label>
                        <input type="checkbox" id="checkboxBonusRecRoom" runat="server" data-clientname="BonusRecRoom"><asp:Label runat="server" AssociatedControlID="checkboxBonusRecRoom">Bonus/Rec room</asp:Label>
                        <input type="checkbox" id="checkboxOfficeDen" runat="server" data-clientname="OfficeDen"><asp:Label runat="server" AssociatedControlID="checkboxOfficeDen">Office/Den</asp:Label>
                        <input type="checkbox" id="checkboxDiningRoom" runat="server" data-clientname="DiningRoom"><asp:Label runat="server" AssociatedControlID="checkboxDiningRoom">Dining room</asp:Label>
                        <input type="checkbox" id="checkboxBreakfastNook" runat="server" data-clientname="BreakfastNook"><asp:Label runat="server" AssociatedControlID="checkboxBreakfastNook">Breakfast nook</asp:Label>
                        <input type="checkbox" id="checkboxDishwasher" runat="server" data-clientname="Dishwasher"><asp:Label runat="server" AssociatedControlID="checkboxDishwasher">Dishwasher</asp:Label>
                        <input type="checkbox" id="checkboxRefrigerator" runat="server" data-clientname="Refrigerator"><asp:Label runat="server" AssociatedControlID="checkboxRefrigerator">Refrigerator</asp:Label>
                        <input type="checkbox" id="checkboxAttic" runat="server" data-clientname="Attic"><asp:Label runat="server" AssociatedControlID="checkboxAttic">Attic</asp:Label>
                        <input type="checkbox" id="checboxGraniteCountertop" runat="server" data-clientname="GraniteCountertop"><asp:Label runat="server" AssociatedControlID="checboxGraniteCountertop">Granite countertop</asp:Label>
                        <input type="checkbox" id="checkboxMicrowave" runat="server" data-clientname="Microwave"><asp:Label runat="server" AssociatedControlID="checkboxMicrowave">Microwave</asp:Label>
                        <input type="checkbox" id="checkboxStainlessSteelAppliances" runat="server" data-clientname="StainlessSteelAppliances"><asp:Label runat="server" AssociatedControlID="checkboxStainlessSteelAppliances">Stainless steel appliances</asp:Label>
                        <input type="checkbox" id="checkboxStoveOven" runat="server" data-clientname="StoveOven"><asp:Label runat="server" AssociatedControlID="checkboxStoveOven">Stove/Oven</asp:Label>
                    </div>
                    <hr>
                    <h2>Additional amenities</h2>
                    <div class="chekbox colmun" data-checkall>
                        <input type="checkbox" id="checkboxBasement" runat="server" data-clientname="Basement"><asp:Label runat="server" AssociatedControlID="checkboxBasement">Basement</asp:Label>
                        <input type="checkbox" id="checkboxWasher" runat="server" data-clientname="Washer"><asp:Label runat="server" AssociatedControlID="checkboxWasher">Washer</asp:Label>
                        <input type="checkbox" id="checkboxDryer" runat="server" data-clientname="Dryer"><asp:Label runat="server" AssociatedControlID="checkboxDryer">Dryer</asp:Label>
                        <input type="checkbox" id="checkboxLaundryAreaInside" runat="server" data-clientname="LaundryAreaInside"><asp:Label runat="server" AssociatedControlID="checkboxLaundryAreaInside">Laundry area - inside</asp:Label>
                        <input type="checkbox" id="checkboxLaundryAreaGarage" runat="server" data-clientname="LaundryAreaGarage"><asp:Label runat="server" AssociatedControlID="checkboxLaundryAreaGarage">Laundry area - garage</asp:Label>
                        <input type="checkbox" id="checkboxBalconyDeckPatio" runat="server" data-clientname="BalconyDeckPatio"><asp:Label runat="server" AssociatedControlID="checkboxBalconyDeckPatio">Balcony, Deck, or Patio</asp:Label>
                        <input type="checkbox" id="checkboxYard" runat="server" data-clientname="Yard"><asp:Label runat="server" AssociatedControlID="checkboxYard">Yard</asp:Label>
                        <input type="checkbox" id="checkboxSwimmingPool" runat="server" data-clientname="SwimmingPool"><asp:Label runat="server" AssociatedControlID="checkboxSwimmingPool">Swimming pool</asp:Label>
                        <input type="checkbox" id="checkboxJacuzziWhirlpool" runat="server" data-clientname="JacuzziWhirlpool"><asp:Label runat="server" AssociatedControlID="checkboxJacuzziWhirlpool">Jacuzzi/ Whirlpool</asp:Label>
                        <input type="checkbox" id="checkboxSauna" runat="server" data-clientname="Sauna"><asp:Label runat="server" AssociatedControlID="checkboxSauna">Sauna</asp:Label>
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