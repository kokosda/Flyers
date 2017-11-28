<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostToCraigslist.aspx.cs" Inherits="FlyerMe.PostToCraigslist" MasterPageFile="~/MasterPageAccount.master" Theme="" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <div class="flyer-menu">
    	<div class="content">
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="account">
        <form id="form" runat="server" enctype="application/x-www-form-urlencoded" method="post">
            <div class="form-content">
                <fieldset>
                    <legend>My Flyers - Generate HTML</legend>
                    <div class="block-text">
                        <hr />
                        <div class="title">HTML code for your flyer:</div>
                        <div id="editor">
                            <textarea id="textareaMarkup" runat="server"></textarea>
                        </div>
                        <div class="right-chekbox chekbox">
                            <input type="checkbox" id="checkboxIncludeEmail" runat="server"/><asp:Label runat="server" AssociatedControlID="checkboxIncludeEmail">Include Email</asp:Label>
                            <asp:Button ID="btnRegenerateCode" runat="server" CssClass="regenerate" Text="Re-Generate Code" OnClick="btnRegenerateCode_Click" />
                        </div>
                        <p>Craigslist doesn't allow html posting anymore. To post to craigslist please generate JPG by <a id="aConvertToJpg" runat="server">clicking here</a>.</p>
                        <p>You can use the HTML code to enhance your posts on LiveDeal, eBay and other sites that accept HTML.</p>
                        <p>Copy (Ctrl-c or Cmd-c) the HTML code in the window. If the code is not highlighted, use the Select All command (Ctrl-a or Cmd-a) first.</p>
                        <p>If you want to post your flyer on craigslist or any other site which does not allow to include email adderess then uncheck email and re-generate the code.</p>
                    </div>
                    <div class="item submit">
                        
                        <asp:HiddenField ID="hfOrderId" runat="server" />
                    </div>
                    <div class="block-post">
                        For example click following link to get instructions to create a craigslist post using above generated HTML <a href="<%= ResolveUrl("~/htmlsubmitinstructions.aspx") %>">HOW DO I POST MY FLYER ON CRAIGSLIST?</a>
                    </div>
                </fieldset>
                <div class="block-mail">
                    <div class="con">
                        <asp:Literal ID="ltlMarkup" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~/js/ace/ace.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/ace/theme-crimson_editor.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/ace/mode-html.min.js") %>"></script>
    <script type="text/javascript">
        (function ($, w) {
            $(function () {
                var e = ace.edit("editor");

                e.setTheme("ace/theme/crimson_editor");
                e.getSession().setMode("ace/mode/html");
                e.renderer.setShowGutter(false);
                e.setShowPrintMargin(false);
            });
        })(jQuery, window);
    </script>
</asp:Content>