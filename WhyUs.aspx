<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WhyUs.aspx.cs" 
Inherits="FlyerMe.WhyUs" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="text-content">
    <div class="content">
        <h1>Why choose Us?</h1>
        <p>We welcome you to <%= clsUtility.SiteBrandName %>, your one-stop shop for real estate email flyers.</p>

        <p>We specialize in providing email flyer solutions to real estate agents like you. But then, you will say there are so many service providers out there who provide email flyer solution, so what&rsquo;s different about <%= clsUtility.SiteBrandName %>?</p>

        <p>Well, we offer cost-effective solutions. You can avail a <a href="preview.aspx?l=sampleseller" data-clientname="Preview">Seller's Agent Flyer</a> or a <a href="preview.aspx?l=samplebuyer" data-clientname="Preview">Buyer's Agent Flyer</a> for as little as $4.95. We, at <%= clsUtility.SiteBrandName %>, believe that quality service can be provided at reasonable prices and this site is a manifestation of that belief.</p>

        <p>Many of our competitors charge extra for the photographs uploaded by you. Using our template wizard, you can upload up to seven different photographs of your property. And you will not have to shell out extra charges.</p>

        <p><%= clsUtility.SiteBrandName %> allows you to specify the details of your property in a single form. You can upload photographs of your property, complete with details such as location, different kind of rooms etc in a colorful, attractive format replete with bold headings and sub-headings. We have countless color themes matched with snazzy headlines. Choose the one that best matches with your property.</p>

        <p>Worried that you might not possess sufficient technical knowledge? Then, you can lay your worries to rest for <%= clsUtility.SiteBrandName %> utilizes an easy-to-use online flyer wizard. Additionally, the entire process has been explained in an easy-to-understand procedure, which is free of technical jargons.</p>

        <p>Recipients of flyers can further forward your flyer to their clients and that grows your chance to close a deal even more.</p>

        <p>You can also look forward to receiving a complimentary Map page for each flyer created especially for you.</p>
        <h2>How it works?</h2>
        <ol>
          <li><strong>Set up your account:</strong> You have to first create your account using our easy-to-fill <a href="#">sign-up</a> form.</li>
          <li><strong>Create New Flyer:</strong> Get started by selecting the flyer type. We currently offer flyer types for both Seller's Agent and Buyer's Agent. Custom flyer type is also available to send premade flyers.</li>
          <li><strong>Pick Market State:</strong> Select market state that corresponds with the location of your property.</li>
          <li><strong>Build your Market Area:</strong> We have grouped market areas as per County, Realtor's Association and MSA for you to easily choose from.</li>
          <li><strong>Select your Flyer Style:</strong> Pick a suitable headline and layout based on number of photos you like to upload. Plenty of pre-defined templates to choose from.</li>
          <li><strong>Fill in Flyer Details:</strong> Fill in the content for your flyer. The content capture aspects such as flyer title, property highlights, price, description etc.</li>
          <li><strong>Add Property Photos:</strong> Upload up to 5 photographs of your property as well as your photo and logo. No per photo charge here.</li>
          <li><strong>Review &amp; Finish Flyer:</strong> Review the work so far and make necessary change if needed or proceed to checkout.</li>
          <li><strong>Make payment:</strong> Now make a quick payment for the market area and flyer type your selected. We use sophisticated encryption to keep each transaction safe.</li>
          <li><strong>Delivered:</strong> Your flyer will be now queued for delivery to your selected market area.</li>
        </ol>
	</div>
</div>
<div class="get-start">
    <h2>Create and send your beautiful flyer in seconds</h2>
    <a href="<%= ResolveUrl("~/createflyer.aspx") %>" class="start">Get started now</a>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAfterFooter" runat="server">
    <div id="popup_bg" style="display:none;"></div>
    <div id="popup_preview" style="display:none;">
        <div class="content" data-mcs-theme="minimal-dark"></div>
        <a href class="close"></a>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <link href="<%= ResolveClientUrl("~/css/jquery.mcustomscrollbar.css") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%= ResolveUrl("~/js/jquery.mcustomscrollbar.concat.min.js") %>"></script>
    <script type="text/javascript">
        (function ($, w) {
            $(function () {
                $("#content a[data-clientname='Preview']").on("click", function (e) {
                    masterPage.showFlyerPreview(e, $(this).attr("href"));
                });
            });
        })(jQuery, window);
    </script>
</asp:Content>