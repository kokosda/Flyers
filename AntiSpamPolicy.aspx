<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AntiSpamPolicy.aspx.cs" 
Inherits="FlyerMe.AntiSpamPolicy" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="text-content">
    <div class="content">
      	<h1>Our Anti-Spam Policy</h1>
        <p>Unsolicited Commercial Email (UCE) is a violation of <%= clsUtility.ProjectName %>'s terms of service. If you know of a violation, or believe that you have received UCE from <%= clsUtility.ProjectName %>, please notify us immediately at <%= clsUtility.AbuseEmail %>. </p>

        <p><%= clsUtility.SiteBrandName %> employs strict anti-spam policies and procedures and enforces them rigorously.</p>

        <div class="faq">
            <a href="#" class="title">For ISPs and other Mail Administrators<span></span></a>
            <div class="text">
            <p><strong><%= clsUtility.SiteBrandName %>'s Anti-Spam Practices </strong></p>
            <p><%= clsUtility.ProjectName %> invests significant resources to ensure that no email coming from <%= clsUtility.ProjectName %> is spam. What does <%= clsUtility.ProjectName %> do to prevent misuse of our service? <%= clsUtility.ProjectName %> employs a very strict anti-spam policy and only allows customers who comply with permission guidelines to use our service. If we determine one of our customers is in violation of our permission policies, we disable their <%= clsUtility.ProjectName %> account immediately.</p>
            <p>To support our policies <%= clsUtility.ProjectName %> includes a sophisticated anti-spam detection system and implements strong abuse prevention practices. These include the following:</p>
            <p><strong>List Verification:</strong> The <%= clsUtility.ProjectName %> Customer Support team verifies all large lists. The list verification process includes a complete review of the list source, list age, collection methodology, confirmation practices, etc. New customer lists are tested before any volume mailings. Any list with questionable permission practices is rejected (or must first be fully-confirmed). </p>
            <p><strong>Global Unsubscribe list:</strong> <%= clsUtility.ProjectName %> maintains a global unsubscribe list that is checked against all imported and supplied emails that are managed by our system. This allows a consumer to contact us and globally opt-out from any customer who may be using the <%= clsUtility.ProjectName %> system. This assures that once someone requests removal that they can never be re-added or receive additional mailings through the <%= clsUtility.ProjectName %> system.</p>
            <p><strong>List Inspection</strong>: In addition to the list verification process, all lists are screened for known spamming characteristics. <%= clsUtility.ProjectName %> continues to invest in our list inspection methods. These methods are continuously revised and kept up-to-date with changing spam practices.</p>
            <p><strong>Verification of From and Reply-to Ownership:</strong> <%= clsUtility.ProjectName %> verifies the ownership of all email addresses used as From or Reply-to addresses in campaigns sent from <%= clsUtility.ProjectName %>. This prevents any spoofing of sender identity.</p>
            <p><strong>Abuse Desk:</strong> Our abuse email box is watched closely and all abuse complaints are processed promptly. Our system automatically tabulates all reported abuse complaints and associates them with a sender and a campaign. If a sender receives an unacceptable percentage of complaints, their account is disabled and the abuse team follows up with the customer directly.</p>
            <p><strong>ISP and Blacklist Relations:</strong> <%= clsUtility.ProjectName %> has standing relationships and ongoing dialogues with many of the leading ISPs and Blacklists. These include the sharing of information regarding policies, practices and issues. If you are an ISP, mail administrator or blacklist owner and would like to get in touch with us, please email <%= clsUtility.AbuseEmail %>.</p>
            <p><strong>Net Abuse Sightings and other online sources:</strong> <%= clsUtility.ProjectName %> actively monitors various websites that report spam and abuse for references to <%= clsUtility.ProjectName %> and takes appropriate action whenever appropriate. </p>
            <p>The <%= clsUtility.ProjectName %> Team is always looking for more vehicles for education and prevention to make sure all of our customers are great permission-based emailers. Please send any suggestions for enhancements or improvements to <a href="mailto:<%= clsUtility.AbuseEmail %>"><%= clsUtility.AbuseEmail %></a></p>
            </div>
        </div>
        <p>We employ a very strict anti-spam policy and only allow customers who comply with permission guidelines to use our service.</p>
                
        <div class="faq">
            <a href="#" class="title"><%= clsUtility.SiteBrandName %>'s Anti-Spam Policy<span></span></a>
            <div class="text">
            <p><strong><%= clsUtility.SiteBrandName %>'s Anti-Spam Policy</strong></p>
            <p><%= clsUtility.ProjectName %> has a no tolerance spam policy. <%= clsUtility.ProjectName %>'s customer support actively monitors large import lists and emails going to a large number of subscribers. Any customer found to be using <%= clsUtility.ProjectName %> for spam will be immediately cut-off from use of the product. If you know of or suspect any violators, please notify us immediately at <a href="mailto:<%= clsUtility.AbuseEmail %>"><%= clsUtility.AbuseEmail %></a>. </p>
            <p>Every email contains a mandatory unsubscribe link. <%= clsUtility.ProjectName %> will terminate the customer's account If they try and remove this link. </p>
            <p><strong>What is Spam? </strong></p>
            <p>Spam is unsolicited email also known as UCE (Unsolicited Commercial Email). By sending email to only those who have requested to receive it, you are following accepted permission-based email guidelines. </p>
            <p>What constitutes a Preexisting business relationship? </p>
            <p>The recipient of your email has made a purchase, requested information, responded to a questionnaire or a survey, or had offline contact with you. </p>
            <p><strong>What constitutes consent? </strong></p>
            <p>The recipient of your email has been clearly and fully notified of the collection and use of his email address and has consented prior to such collection and use. This is often called informed consent.</p>
            <p><strong>Isn't there a law against sending Spam? </strong></p>
            <p>The federal anti-spam law went into effect on January 1st, 2004 and preempts all state laws. While this new law will not stop spam, it does make most spam illegal and ultimately less attractive to spammers. The law is specific about requirements to send commercial email and empowers the federal government to enforce the law. The penalties can include a fine and/or imprisonment for up to 5 years.</p>
            <p><strong>How <%= clsUtility.ProjectName %> protects you from sending spam </strong></p>
            <p><%= clsUtility.ProjectName %> is a permission-based email-marketing tool that follows the strictest permission-based philosophies:</p>
            <ul>
                <li>Communication - By accepting our license agreement you have agreed to use only permission-based lists and never to sell or rent your lists.</li>
                <li>Unsubscribe - Every email generated from <%= clsUtility.ProjectName %> contains an unsubscribe link which allows your subscribers to opt-out of future email campaigns and automatically updates your subscriber lists to avoid the chance of sending unwanted emails to visitors who have unsubscribed.</li>
                <li>Identification - Your email header information is correct because it is pre-set for you by <%= clsUtility.ProjectName %>. Your email campaign's "From" address is verified and already accurately identifies you as the sender.</li>
                <li>Contact Information - all of your emails are pre-filled with your contact information including your physical address.</li>
            </ul>
            <p><strong>How to protect yourself from Spam: Take the Spam Test</strong></p>
            <ul>
                <li>Are you importing a purchased list of ANY kind?</li>
                <li>Are you sending to non-specific addresses such as:</li>
                <li>sales@domain.com, business@domain.com, webmaster@domain.com, info@domain.com, or other general addresses.</li>
                <li>Are you sending to distribution lists or mailing lists which send indirectly to a variety of email addresses?</li>
                <li>Are you mailing to anyone who has not explicitly agreed to join your mailing list?</li>
                <li>Have you falsified your originating address or transmission path information?</li>
                <li>Have you used a third party email address or domain name without their permission?</li>
                <li>Does your email's subject line contain false or misleading information?</li>
                <li>Does your email fail to provide a working link to unsubscribe?</li>
                <li>Are you failing to process any unsubscribe requests that come to you via a reply to your email within 10 days or the request?</li>
            </ul>
            <p>If you have answered YES to ANY of the above questions you will likely be labeled a SPAMMER.</p>
            </div>
        </div>
    </div>
</div>
</asp:Content>