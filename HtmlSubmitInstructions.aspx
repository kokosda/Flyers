<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HtmlSubmitInstructions.aspx.cs" Inherits="FlyerMe.HtmlSubmitInstructions" MasterPageFile="~/MasterPageAccount.master" Theme="" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <div class="flyer-menu">
    	<div class="content">
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="account">
        <form>
            <div class="form-content">
                <fieldset>
                    <legend>HOW DO I POST MY FLYER ON CRAIGSLIST?</legend>
                    <div class="block-post">
                        <div class="title">To create a craigslist post, you need to do the following:</div>
                        <ul>
                            <li>Go to the <a href="http://www.craigslist.org" target="_blank">craigslist homepage</a>.</li>
                            <li>First, you will need to register on craigslist if you don't have an account already. If you do have an account, then login.</li>
                            <li>Select the metro area that best fits your location from the list along the right side of the homepage.</li>
                            <li>Click on the post to classifieds link as shown in the picture below.</li>
                        </ul>
                        <asp:Image runat="server" ImageUrl="~/images/craigslist1.jpg" />
                        <ul>
                            <li>Click on the for housing offered link as shown in the picture below.</li>
                        </ul>
                        <asp:Image runat="server" ImageUrl="~/images/craigslist2.jpg" />
                        <ul>
                            <li>Click on the category that best describes the your selling as shown in the picture below.</li>
                        </ul>
                        <asp:Image runat="server" ImageUrl="~/images/craigslist3.jpg" />
                        <ul>
                            <li>Depending on the metro area you selected, you may be asked to narrow down the location by region and neighborhood.</li>
                            <li>Once you get to the posting information page, switch over to the generate HTML page for your flyer.</li>
                            <li>Copy (Ctrl-c or Cmd-c) the HTML code in the window. If the HTML code is not highlighted, use the Select All command (Ctrl-a or Cmd-a) first.</li>
                            <li>Switch back to the craigslist posting information page.</li>
                            <li>Paste (Ctrl-v) the HTML code into the Posting Description box highlighted below in light blue as shown in the picture below.</li>
                        </ul>
                        <asp:Image runat="server" ImageUrl="~/images/craigslist4.jpg" />
                        <ul>
                            <li>Continue on to the review page to complete your posting.</li>
                        </ul>
                    </div>                        
                </fieldset>                    
            </div>
        </form>
    </div>
</asp:Content>