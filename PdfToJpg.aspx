<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PdfToJpg.aspx.cs" Inherits="FlyerMe.PdfToJpg" MasterPageFile="~/MasterPageAccount.master" Theme="" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <div class="flyer-menu">
    	<div class="content">
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flyers upload">
        <div class="flyers-content">
            <div class="right">
                <h1>PDF To Image Converter</h1>
                <form id="form" runat="server" enctype="multipart/form-data">
                    <div class="form-content">
                        <div class="summary-error">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </div>
                        <div class="item big add upload">
                            <input type="file" id="fileUpload" runat="server" accept="application/pdf" />
                            <div class="file-text"></div><div class="file-submit">Browse</div>
                        </div>
                        <div class="description">
                            Chose pdf document to convert
                            <br />&nbsp;
                        </div>
                        <div class="item big">
                            <label>Page number</label>
                            <asp:TextBox ID="txtPageNo" Text="1" runat="server"></asp:TextBox>
                        </div>
                        <div class="description">
                            You can specify the page number<br />if your PDF file have multiple pages.
                            <br />&nbsp;
                        </div>
                        <div class="item submit">
                            <asp:Button ID="btnConvert" Text="Convert" runat="server" OnClick="btnConvert_Click" />
                        </div>                                                
                        <div id="divMessage" runat="server" visible="false" class="item big">
                            <strong style="font-size:14px;">Right click on the picture shown below and select "Save Picture As" option to save the converted image to your computer. Then close this browser tab and continue on Step 1 (flyer content) and load the converted image by clicking on the "Browse". If you have any problems with the conversion/upload, please feel free to contact us at <a href="<%= clsUtility.ContactUsEmail %>"><%= clsUtility.ContactUsEmail %></a>
                            </strong>
                            <img id="imgFile" runat="server" style="max-width:512px;" /><br />
                            <a id="aImageText" runat="server" target="_blank">To See Full Size Of Converted Picture Click Here</a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <div class="flyers-icon"></div>
    </div>
</asp:Content>