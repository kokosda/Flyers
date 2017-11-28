<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFlyer.ascx.cs" Inherits="FlyerMe.Controls.CreateFlyer.WizardSteps.UploadFlyer" %>
<div class="flyers upload">
    <div class="flyers-content">
        <div class="right">
            <h1>Upload your flyer</h1>
            <form method="post" enctype="multipart/form-data" runat="server" id="form">
            	<div class="form-content">
                    <div class="summary-error" id="divSummaryError" runat="server" visible="false"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
                    <div class="item big add upload">
                        <input type="file" runat="server" id="inputFile" accept="image/jpeg,image/png,image/tiff,image/gif" data-clientname="File">
                        <div class="file-text"></div><div class="file-submit">Browse</div>
                    </div>
                    <div class="description">
                        <p>You can upload most of the commonly known image <br>formats such as JPEG (.jpg file), BMP (.bmp file), <br>GIF (.gif file), TIFF (.tif file), PNG (.png file)</p>
                        <p>If your flyer is in a PDF format then please click to convert it to an uploadable image(.JPG) format <a href="<%= ResolveClientUrl("~/pdftojpg.aspx") %>" target="_blank">Convert PDF To Image</a></p>
                        <p>&nbsp;</p>
                    </div>
                    <div class="item small-bs ">
                        <label>Delivery Date<span class="required">*</span></label>
                        <input type="text" id="inputDeliveryDate" runat="server" data-clientname="DeliveryDate" placeholder="Ñhoose date" readonly>
                        <a class="date" href=""><span></span>23</a>
                    </div>
                    <div class="item big-bs">
                        <label>Email Subject Line<span class="required">*</span></label>
                        <input type="text" id="inputEmailSubject" runat="server" data-clientname="EmailSubject">
                    </div>
                    <div class="description">
                        <p>Your Flyer will be emailed to other agents or clients. The text specified by you in Email Subject Line will be used as a subject line for delivering the mail. Ex: ‘Spacious & Luxury Oceanfront Condo’</p>
                    </div>
                    <div class="item big-bs">
                        <label>Link (e-mail or URL)<i class="info"><div class="tooltip">We will hyperlink your Flyer to your website (URL) or an e-mail address.</div></i></label>
                        <input type="text" id="inputLink" runat="server" data-clientname="Link">
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