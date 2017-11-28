<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Faq.aspx.cs" 
Inherits="FlyerMe.Faq" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="text-content">
    <div class="content">
        <h2>About <%= clsUtility.ProjectName %></h2>
        <div class="faq">
            <a href="#" class="title">How does <%= clsUtility.ProjectName %> work?<span></span></a>
            <div class="text"><p><a href="whyus.aspx">Please see here</a></p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">How do I get started?<span></span></a>
            <div class="text"><p>It's easy. First create your account <asp:HyperLink runat="server" NavigateUrl="~/signup.aspx">here</asp:HyperLink>. There is no mebership fee or contract. Once having the account, log in and use 'Create New Flyer' tab or link and follow instructions to create and send email flyers. Do not forget to add your photo and logo to complete your profile. See our pricing <asp:HyperLink runat="server" NavigateUrl="~/prices.aspx">here</asp:HyperLink>.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">How can I contact customer support?<span></span></a>
            <div class="text"><p>The <a href="contacts.aspx">Contact Us</a> link can be found on the bottom of any page within <%= clsUtility.SiteBrandName %>. Please use our online contact form to send us an inquiry, or email <a href="mailto:<%= clsUtility.ContactUsEmail %>"><%= clsUtility.ContactUsEmail %></a>.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">Do you charge a membership fee?<span></span></a>
            <div class="text"><p>No! There is no membership fee or contract or commitment. You can open an account with us free of charge. You can create and review flyers without paying a dime. All your flyers created will be saved and you pay only when you decide to send the flyer out. See our <a href="prices.aspx">pricing</a> here. Get started <a href="signup.aspx">here</a>.</p></div>
        </div>
    
        <h2><%= clsUtility.ProjectName %></h2>
        <div class="faq">
            <a href="#" class="title">What is an Email Flyer?<span></span></a>
            <div class="text"><p>Email flyer is a form of Internet advertising. Email flyers allow you to harness the power of the Internet. You can reach a large number of target audiences in a cost-effective way and in a short duration. Email flyer is an e-pamphlet wherein you can list out your property details in an attractive format and can upload photographs of the same.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">How do I create an email flyer?<span></span></a>
            <div class="text"><p>It’s easy. You first create your account <asp:HyperLink runat="server" NavigateUrl="~/signup.aspx">here</asp:HyperLink>. After that, you can proceed to create your email flyer. We have created plenty of templates so that you can select the one that best suits your requirements. Email flyers can be created using our sophisticated yet user-friendly flyer wizard. Additionally, we have also provided the instructions and necessary help that you might require while creating your flyer. You just need to provide the market area, market state, property details and photographs and your flyer is created and made ready for delivery in a jiffy.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">How much does each email flyer cost?<span></span></a>
            <div class="text"><p>Our e-mail flyers are available at rock bottom prices. Our <a href="preview.aspx?l=sampleseller" data-clientname="Preview">Seller's Flyer Agent</a> as well as <a href="preview.aspx?l=samplebuyer" data-clientname="Preview">Buyer's Agent Flyers</a> starts from as low as $4.95 whether you choose to distribute it to real estate agents in a particular county, association or MSA. For more details, visit our pricing page.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">What cost of flyer includes?<span></span></a>
            <div class="text"><p>Cost of flyer shown on our pricing page includes flyer and its delivery to the given market area. There is no charge for creating flyers. You pay when you decide to send the flyer out.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">What is a Seller's Agent Flyer?<span></span></a>
            <div class="text"><p>You should select ‘Seller’s Agent Flyers’, if you are representing a client who is selling his property. In other words, this flyer is for a real estate agent who is representing a seller of a particular property. This flyer is designed to provide with all the necessary information that an agent or client might require before taking any concrete decision concerning presenting or buying a property.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">What is a Buyer's Agent Flyer?<span></span></a>
            <div class="text"><p>This Flyer is designed for agents who represent buyer in a real estate transaction. It provides necessary details that a buyer is seeking in his property so that other agents become aware of the requirement and recommend a suitable property. Very effective medium for buyer’s agents.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">What is a Custom Flyer?<span></span></a>
            <div class="text"><p>This Flyer is designed to send premade custom flyers. Premade custom flyer can be uploaded as image. Suitable for both Seller and Buyer's agent.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">How do I edit my flyer?<span></span></a>
            <div class="text"><p>Login to your account. Go to <strong>'My Flyers'</strong> tab and locate your flyer to edit. If already logged in, click on <strong>'My Flyers'</strong> link on the top-right corner of any page and then go to <strong>'My Flyers'</strong> tab to edit your flyers.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">How do I add or edit my personal photo and company logo?<span></span></a>
            <div class="text"><p>Login to your account. Go to 'Profile' tab and manage your personal photo and company logo. If already logged in, click on 'My Flyers' link on the top-right corner of any page and then go to 'Profile' tab to edit your personal photo and company logo.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">What is a Market Area?<span></span></a>
            <div class="text"><p>We have classified agent's email list into three categories viz. as per County, Association and MSA. Each category is referred as Market Area. For example, selecting ‘County’ as your market area will let you choose multiple counties to send your email flyer, however, all destination counties must be within the same state. Number of agents or list size in each selected market area is listed on our pricing page.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">What is the significance of Market Area?<span></span></a>
            <div class="text"><p>When you select a market area, your flyer will be delivered to all the agents in our database under that particular area. Please note that any given email flyer can be blocked by ISPs or spam filters and we cannot guarantee that everyone in our database will actually receive the email flyer.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">What is the generate HTML option under My Flyers?<span></span></a>
            <div class="text"><p>This is a feature where you can generate the HTML code of your flyer and post it to any website that accepts postings in a HTML format including Craigslist and Ebay among others. Simply cut the generated HTML code (highlight the generated code or press ctrl + A and then press ctrl + c) and paste (ctrl + v) in the appropriate place on the target website.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">What if I am having trouble creating my flyer?<span></span></a>
            <div class="text"><p>We are always here to help you and guide you in the process of making flyers. Feel free to email us at <%= clsUtility.ContactUsEmail %>, or call us toll-free at 866-817-5437 for assistance. Note that communicating via email with us is and it actually may mean a shorter response time.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">What if I couldn't complete the flyer in one time? Can I access it later?<span></span></a>
            <div class="text"><p>Absolutely! While creating a flyer just click on ‘Save Flyer. I’ll finish it later’ button and the flyer gets saved. Just log back in and you can easily access your flyers from 'My Flyers' page to make changes or complete it.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">I've done my own custom flyer. Can you send it for me?<span></span></a>
            <div class="text"><p>Sure! You can use our 'Custom Flyer' template and upload your premade flyer as image.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">How can we print these email flyers?<span></span></a>
            <div class="text"><p>Our email flyers can be saved or printed as PDF. Anyone viewing the flyer can save or print flyer as PDF using "Print PDF" link on it.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">For how long a flyer is available to view and print?<span></span></a>
            <div class="text"><p>The flyers once sent out are stored in our system for a 60-day period, during which you can view them or take a print out. If needed, the same flyer can be re-ordered from 'My Flyers' page.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">How many property photos can be added to a flyer? Is there a per photo charge?<span></span></a>
            <div class="text"><p>Our Seller’s Agent Flyer allow you to upload up to seven photographs of your property. We do not charge per photo.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">Can I forward the flyer to my clients?<span></span></a>
            <div class="text"><p>Absolutely! We deliver your email flyer using our agent's email list. Besides this you can also send the flyer to your clients. You can either type their email addresses or quickly import them from your current addressbook (Outlook, Yahoo, Hotmail, AOl etc.).</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">Why do I need to sign-up upon clicking 'Send to Clients’ link on a flyer?<span></span></a>
            <div class="text"><p>Whenever a flyer is forwarded using ‘Send to Clients’, your name will be appearing under ‘Contact Person’. Hence, we request you to provide us with your complete business profile including your photo and company logo.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">When are the flyers actually sent out?<span></span></a>
            <div class="text"><p>On scheduled day all flyers are sent out within 24 - 48 hours. In most cases within few hours.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">Are the flyers sent in the body of an email or as an attachment?<span></span></a>
            <div class="text"><p>Our flyers are sent out as HTML emails (as body of the email) and are very email friendly.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">Can I reuse a flyer I've already created?<span></span></a>
            <div class="text"><p>Absolutely! Once logged in, on 'My Flyers' page locate the flyer you like to reuse and click on 'Resend as New Flyer'. This will give you an exact copy of the flyer you already created. Now you can edit this flyer to send it as a new flyer.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">Can I simply upload an image of a flyer I already have created?<span></span></a>
            <div class="text"><p>Sure! You can use our 'Custom Flyer' template and upload your premade flyer as image.</p></div>
        </div>
        <h2>Other</h2>
        <div class="faq">
            <a href="#" class="title">Can I use your service to promote my business or recruit agents?<span></span></a>
            <div class="text"><p>You can use our service to promote your business if you offer products or sevices that can help realtors grow their business or serve their clients well. Yes we do send out recruiting flyers. Contact Us for further details.</p></div>
        </div>
        <div class="faq">
            <a href="#" class="title">How can I ensure <%= clsUtility.ProjectName %> emails reach my inbox?<span></span></a>
            <div class="text"><p>To ensuring <%= clsUtility.ProjectName %> emails reach your inbox please <asp:HyperLink runat="server" NavigateUrl="~/ensureemailinstructions.aspx">click here</asp:HyperLink>.</p></div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAfterFooter" runat="server">
    <div id="popup_bg" style="display:none;"></div>
    <div id="popup_preview" style="display:none;">
        <div class="content" data-mcs-theme="minimal-dark"></div>
        <a href class="close"></a>
    </div>
    <link href="<%= ResolveClientUrl("~/css/jquery.mcustomscrollbar.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
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