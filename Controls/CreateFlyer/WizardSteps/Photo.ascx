<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Photo.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.Photo" %>
<%@ Register Src="~/Controls/CreateFlyer/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<div class="flyers amenities">
    <div class="flyers-content left">
        <uc:LeftMenu ID="leftMenu" runat="server" />
        <div class="right">
            <h1>Add photos and logo</h1>
            <form method="post" enctype="multipart/form-data" runat="server" id="form">
            	<div class="form-content">
                    <div class="files logo">
                        <asp:Panel id="panelMyPhoto" runat="server" CssClass="file-one">
                            <div class="image"></div>
                            <input type="file" id="inputMyPhoto" runat="server" accept="image/jpeg,image/png,image/gif">
                            <div class="text">Add My Photo<br><a href="">browse</a></div>
                            <input type="button" class="delete"/>
                            <input type="hidden" name="DeleteMyPhoto" />
                            <asp:Image ID="imageMyPhoto" runat="server" CssClass="preview" />
                        </asp:Panel>
                        <asp:Panel id="panelMyLogo" runat="server" CssClass="file-one">
                            <div class="image"></div>
                            <input type="file" id="inputMyLogo" runat="server" accept="image/jpeg,image/png,image/gif">
                            <div class="text">Add My Logo<br><a href="">browse</a></div>
                            <input type="button" class="delete"/>
                            <input type="hidden" name="DeleteMyLogo" />
                            <asp:Image ID="imageMyLogo" runat="server" CssClass="preview" />
                        </asp:Panel>
                    </div>
                    <h2>Add photos</h2>
                    <div class="files">
                        <div class="file-one <%= imagePhoto1.Visible ? "change" : null %>">
                            <div class="image"></div>
                            <input type="file" id="inputPhoto1" runat="server" accept="image/jpeg,image/png,image/gif">
                            <div class="text">Add Photo 1<br><a href="">browse</a></div>
                            <input type="button" value="delete" class="delete"/>
                            <input type="hidden" name="DeletePhoto1" />
                            <asp:Image ID="imagePhoto1" runat="server" CssClass="preview" />
                        </div>
                        <div class="file-one <%= imagePhoto2.Visible ? "change" : null %>">
                            <div class="image"></div>
                            <input type="file" id="inputPhoto2" runat="server" accept="image/jpeg,image/png,image/gif">
                            <div class="text">Add Photo 2<br><a href="">browse</a></div>
                            <input type="button" value="delete" class="delete"/>
                            <input type="hidden" name="DeletePhoto2" />
                            <asp:Image ID="imagePhoto2" runat="server" CssClass="preview" />
                        </div>
                        <div class="file-one <%= imagePhoto3.Visible ? "change" : null %>">
                            <div class="image"></div>
                            <input type="file" id="inputPhoto3" runat="server" accept="image/jpeg,image/png,image/gif">
                            <div class="text">Add Photo 3<br><a href="">browse</a></div>
                            <input type="button" value="delete" class="delete"/>
                            <input type="hidden" name="DeletePhoto3" />
                            <asp:Image ID="imagePhoto3" runat="server" CssClass="preview" />
                        </div>
                        <div class="file-one <%= imagePhoto4.Visible ? "change" : null %>">
                            <div class="image"></div>
                            <input type="file" id="inputPhoto4" runat="server" accept="image/jpeg,image/png,image/gif">
                            <div class="text">Add Photo 4<br><a href="">browse</a></div>
                            <input type="button" value="delete" class="delete"/>
                            <input type="hidden" name="DeletePhoto4" />
                            <asp:Image ID="imagePhoto4" runat="server" CssClass="preview" />
                        </div>
                        <div class="file-one <%= imagePhoto5.Visible ? "change" : null %>">
                            <div class="image"></div>
                            <input type="file" id="inputPhoto5" runat="server" accept="image/jpeg,image/png,image/gif">
                            <div class="text">Add Photo 5<br><a href="">browse</a></div>
                            <input type="button" value="delete" class="delete"/>
                            <input type="hidden" name="DeletePhoto5" />
                            <asp:Image ID="imagePhoto5" runat="server" CssClass="preview" />
                        </div>
                        <div class="item submit">
                            <input type="submit" value="Next"/>
                            <ul>
                                <li><a href="<%= ResolveUrl("~/preview.aspx") %>" class="preview" title="Preview" target="_blank">Preview</a></li>
                                <li><a href="<%= ResolveUrl("~/createflyer.aspx/saveindrafts") %>" class="save" title="Save in drafts">Save in drafts</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <div class="flyers-icon"></div>
</div>