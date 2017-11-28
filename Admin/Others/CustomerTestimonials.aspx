<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerTestimonials.aspx.cs" Inherits="FlyerMe.Admin.Others.CustomerTestimonials" Theme="" MasterPageFile="~/Admin/MasterPage.master" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/Grid/Grid.ascx" TagPrefix="uc" TagName="Grid" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <form runat="server" method="post" enctype="multipart/form-data">
                    <div class="search" id="divSearch" runat="server">
                        <uc:Message ID="message" runat="server" />
                        <div class="item-box">
                            <div class="item">
                                <label>First Name<span class="required">*</span></label>
                                <input type="text" id="inputFirstName" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Last Name<span class="required">*</span></label>
                                <input type="text" id="inputLastName" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Company<span class="required">*</span></label>
                                <input type="text" id="inputCompany" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Position<span class="required">*</span></label>
                                <input type="text" id="inputPosition" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Active</label>
                                <div class="chekbox">
                                    <input type="checkbox" id="inputActive" runat="server" checked />
                                    <asp:Label runat="server" AssociatedControlID="inputActive"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="item-box">
                            <div class="item">
                                <label>Message<span class="required">*</span></label>
                                <textarea type="text" id="textareaMessage" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Image Path</label>
                                <input type="text" id="inputImagePath" runat="server" />
                            </div>
                            <div class="item upload">
                                <label>&nbsp;</label>
                                <input type="file" id="inputImage" runat="server" accept="image/jpeg,image/png,image/gif" />
                                <div class="file-text"></div>
                                <div class="file-submit">Browse</div>
                            </div>
                            <div class="item">
                                <label>Submitted Date<span class="required">*</span></label>
                                <input type="text" id="inputSubmittedDate" runat="server" required readonly data-date />
                                <a class="date" href=""><span></span>23</a>
                            </div>
                        </div>
                        <div class="item-submit">
                            <asp:Button ID="btnAddTestimonial" runat="server" Text="Add Testimonial" OnCommand="btnAddTestimonial_Command" />
                        </div>
                    </div>
                    <uc:Grid id="grid" runat="server" PageSize="7" PageName="admin/others/customertestimonials.aspx" OnRowHeadBinding="grid_RowHeadBinding" OnRowDataBinding="grid_RowDataBinding" OnRowEditing="grid_RowEditing" OnRowEditCanceled="grid_RowEditCanceled" OnRowEdited="grid_RowEdited" OnRowDeleted="grid_RowDeleted" TableNews="true" />
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <link href="<%= ResolveUrl("~/css/jquery.ui/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>