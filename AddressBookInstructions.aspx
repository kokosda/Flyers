<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddressBookInstructions.aspx.cs" 
Inherits="FlyerMe.AddressBookInstructions" Theme="" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="account">
        <form>
            <div class="form-content">
                <fieldset>
                    <legend>Address Book Instructions</legend>
                    <div class="block-delivery">
                        <div class="title">Why you should add <%= clsUtility.SiteBrandName %> contacts to your Address Book...</div>
                        <p>No one likes getting SPAM or unsolicited emails and email filters and SPAM blockers are working overtime to protect you from it, which is wonderful. However, aggressive spam filters can sometimes block legitimate emails — email you really want.</p>
    					<p>Luckily for us all, you can do something about it. By adding <%= GetServiceAccountsMarkup() %> to your email address book, you’re saying to your email software that you want your email from us.</p>
						<p>So add us to your address book to help make sure you get the emails you want. It’s easy! But if you're not sure how to add us to your email address book, read below...</p> 
                    </div>
                    <div class="faq active">
                        <a href="#" class="title">WHAT TO DO IF YOU HAVE AN EMAIL FROM US:<span></span></a>
                        <div class="text">
                        <p>Depending on which email program you use, follow one of these sets of instructions:</p>
                        <div class="top-title">Microsoft Outlook</div>
                            <ul>
                                <li>Open the email message from <%= clsUtility.ProjectName %>.</li>
                                <li>Right-click the name and address in the FROM field.</li>
                                <li>Click "Add Sender to Contacts" in the drop-down dialog box.</li>
                            </ul>
                        <div class="top-title">AOL 9.0</div>
                            <ul>
                                <li>Open the email message from <%= clsUtility.ProjectName %>.</li>
                                <li>Click on the &ldquo;Add Address &rdquo; icon on the right-hand side of the message.</li>
                                <li>Add additional information if you wish, then click on the "Save" button.</li>
                            </ul>
                        <div class="top-title">Yahoo! Mail</div>
                            <ul>
                                <li>Open the email message from <%= clsUtility.ProjectName %>.</li>
                                <li>Click on the "Add to Address Book" link next to the "From Address."</li>
                                <li>On the "Add to Address Book" page, enter optional information into the fields.</li>
                                <li>Click the "Add to Address Book" button.</li>
                            </ul>
                        <div class="top-title">MSN Hotmail</div>
                            <ul>
                                <li>Open the email message from <%= clsUtility.ProjectName %>.</li>
                                <li>Click on the "Save Address" button at the top right of the message.</li>
                                <li>Check the "Add to Contacts" box and click "OK."</li>
                            </ul>
                        <div class="top-title">Google Gmail</div>
                            <ul>
                                <li>Open the email message from <%= clsUtility.ProjectName %>.</li>
                                <li>If you do not see &ldquo;Add sender to Contacts list,&rdquo; then click &ldquo;More options&rdquo;</li>
                                <li>Click &ldquo;Add sender to Contacts list&rdquo;</li>
                            </ul>
 						<div class="top-title">Other</div>
                            <ul>
                                <li>If your email program is not listed, your program should have a &ldquo;Help&rdquo; page of some kind which will point you in the right direction.</li>
                            </ul>
                        </div>
                    </div> 
                    <div class="faq">
                        <a href="#" class="title">WHAT TO DO IF YOU HAVE AN EMAIL FROM US:<span></span></a>
                        <div class="text">
                            <p>Depending on which email program you use, follow one of these sets of instructions:</p>
                            <div class="top-title">Microsoft Outlook</div>
                                <ul>
                                    <li>Open Outlook.</li>
                                    <li>In the toolbar, click File &gt; New &gt; Contact.</li>
                                    <li>Enter the appropriate email address listed above and click &ldquo;Save and Close&rdquo;.</li>
                                    <li>Repeat as needed.</li>
                                </ul> 
 							<div class="top-title">AOL AIM Mail</div>
                                <ul>
                                    <li>Log into AIM Mail.</li>
                                    <li>Click &ldquo;Addresses&rdquo;.</li>
                                    <li>Click &ldquo;Add Contact&rdquo;.</li>
                                    <li>Enter the appropriate email address listed above and click &ldquo;Save&rdquo;.</li>
                                    <li>Repeat as needed.</li>
                                </ul>
 							<div class="top-title">Yahoo! Mail</div>
                                <ul>
                                    <li>Log into AIM Mail.</li>
                                    <li>Click &ldquo;Addresses&rdquo;.</li>
                                    <li>Click &ldquo;Add Contact&rdquo;.</li>
                                    <li>Under &ldquo;Primary Information&rdquo; in the &ldquo;Email&rdquo; field enter the appropriate email address listed above.</li>
                                    <li>Click &ldquo;Save&rdquo;</li>
                                    <li>Click &ldquo;Done&rdquo;</li>
                                    <li>Repeat as needed.</li>
                                </ul>
 							<div class="top-title">MSN Hotmail</div>
                                <ul>
                                    <li>Log into Hotmail.</li>
                                    <li>Click &ldquo;New Contact&rdquo;.</li>
                                    <li>In the &ldquo;Quickname&rdquo; field enter &ldquo;<%= clsUtility.ProjectName %>&rdquo;.</li>
                                    <li>Under &ldquo;Online Addresses&rdquo; in the &ldquo;Email&rdquo; field enter the appropriate email address listed above.</li>
                                    <li>Click &ldquo;Save&rdquo;</li>
                                    <li>Repeat as needed.</li>
                                </ul>
 							<div class="top-title">Google Gmail</div>
                                <ul>
                                    <li>Log into Gmail.</li>
                                    <li>Click &ldquo;Add contact&rdquo; on the left.</li>
                                    <li>Enter the appropriate email address listed above and click the &ldquo;add&rdquo; button.</li>
                                    <li>Follow the instructions provided.</li>
                                    <li>Repeat as needed.</li>
                                </ul>
 							<div class="top-title">Other</div>
                                <ul>
                                    <li>If your email program is not listed, your program should have a &ldquo;Help&rdquo; page of some kind which will point you in the right direction.</li>
                                </ul>
                        </div>
                    </div>
                </fieldset>                    
            </div>
        </form>
    </div>
</asp:Content>