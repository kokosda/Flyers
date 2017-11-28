<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PriceRange.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.PriceRange" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers prices">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <h1>Choose your price</h1>
            <form method="post" enctype="application/x-www-form-urlencoded" runat="server" id="form">
            	<div class="form-content">
                    <div class="item big">
                        <label>Price range</label>
                        <div class="item small-bs left invisible">
                            <input id="min" runat="server" data-clientname="PriceMin" type="text">
                        </div>
                        <div class="item small-bs invisible">
                            <input id="max" runat="server" data-clientname="PriceMax" type="text">
                        </div>
                        <div class="slider-range">
                            <div id="slider-range" class="clear"></div>
                        </div>
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