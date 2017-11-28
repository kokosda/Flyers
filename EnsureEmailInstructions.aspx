<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EnsureEmailInstructions.aspx.cs" 
Inherits="FlyerMe.EnsureEmailInstructions" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="text-content">
    <div class="content">
        <h2>Steps to ensure <%= clsUtility.ProjectName %> emails<br />reach your inbox</h2>
        <p>If you did not receive your FlyerMe confirmation email, customer copy or update, please check your Junk/SPAM folder. Sometimes SPAM filters will filter legitimate email. So, in the future to ensure our emails reach your inbox follow these steps:</p>
    
        <p>Please add <a href="mailto:<%= clsUtility.ContactUsEmail %>"><%= clsUtility.ContactUsEmail %></a>, <%= GetServiceAccountsMarkup() %> to your Safe Senders list in your email client. This action on your part will ensure that our email updates and important messages will get in your inbox safely.</p>
        <p>&nbsp;</p>
        <p>&nbsp;</p>
        <div class="faq">
            <a href="#" class="title">AOL 8.0<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Click the "Add Address" icon</li>
                    <li>Verify the sender's contact information</li>
                    <li>Click "Save"</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">AOL WebMail<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Click on the sender's name and email address</li>
                    <li>Click "Add to Address Book" in the window that appears</li>
                    <li>Click "Save"</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">Hotmail<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Click "Save address" in the toolbar</li>
                    <li>Verify the sender's contact details</li>
                    <li>Click "OK"</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">Yahoo!<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Click the "Add to Address Book" button located on the right, next to the sender's name</li>
                    <li>Verify the sender's contact details</li>
                    <li>Click "Add to Address Book"</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">Gmail<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Click "More Options" in the email header</li>
                    <li>Select "Add Sender to Contacts List"</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">Earthlink<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Click "Add to Address Book" in the email header</li>
                    <li>Use the "Address Book Editor" to verify the sender's contact details</li>
                    <li>Click "Save"</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">Outlook (2000+)<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Click on the Actions menu on the top of your email window</li>
                    <li>Click on "Junk Email"</li>
                    <li>Select "Add Sender's Domain to Safe Senders List"</li>
                </ul>
                    <p><strong>Or follow these steps:</strong></p>
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Right-click the sender's email address</li>
                    <li>Select "Add to Outlook Contacts"</li>
                    <li>Click "Save and close"</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">Outlook Express (6+)<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Left-click the sender icon, or right-click the sender's name</li>
                    <li>Select "Add to Contacts"</li>
                    <li>Click "Save and close"</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">Thunderbird<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Click on the "Message" menu on the top of your email window</li>
                    <li>Choose "Mark"</li>
                    <li>Select "As not Junk"</li>
                </ul>
                    <p><strong>Or Follow These Steps:</strong></p>
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Right-click the sender's email address</li>
                    <li>Select "Add to Address Book"</li>
                    <li>Click "Save and close"</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">MacMail<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Click the sender's email address and select "Open in Address Book"</li>
                    <li>Verify the sender's contact details</li>
                </ul>
            </div>
        </div>
        <div class="faq">
            <a href="#" class="title">Entourage<span></span></a>
            <div class="text">
                <ul>
                    <li>Open the email sent from <%= clsUtility.ProjectName %></li>
                    <li>Right-click the sender's email address</li>
                    <li>Select "Add to Address Book" in the short-cut menu</li>
                    <li>Verify the sender's contact details</li>
                    <li>Click "Save"</li>
                </ul>
            </div>
        </div>                
    </div>
</div>
</asp:Content>