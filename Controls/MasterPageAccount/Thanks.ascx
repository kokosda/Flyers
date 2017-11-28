<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Thanks.ascx.cs" Inherits="FlyerMe.Controls.MasterPageAccount.Thanks" %>
<a href="#" class="close">X</a>
<div class="conteiner">
    <div class="content">
        <div class="icon ok"></div>
        <h1>Create New Flyer Wizard - You’re Done!</h1>
        <div class="text center">
            <div class="fb-like" data-href="https://www.facebook.com/flyermeHQ" data-send="true" data-width="450" data-show-faces="true" data-font="arial"></div>
        </div>
        <hr>
        <div class="text center">Congratulations! Your Flyer has been successfully created and submitted for delivery. It will be automatically syndicated (posted) on popular property sites like Zillow, Trulia, Oodle, Lycos, Vast and more. If you don’t want syndicate your property listing then go to “My Flyers” and click on the “OFF Syndication” link to the right of the flyer.</div>
        <ul class="menus">
            <li><a href="<%= ResolveUrl("~/myflyers.aspx") %>">Send This Flyer To Clients</a></li>
            <li><a href="<%= ResolveUrl("~/createflyer.aspx") %>">Create New Flyer</a></li>
            <li><a href="<%= ResolveUrl("~/myflyers.aspx") %>">Edit My Flyers</a></li>
            <li><a href="<%= ResolveUrl("~/profile.aspx") %>">Edit My Profile</a></li>
        </ul>
        <div class="text center small">
            <strong>We Want To Hear From You!</strong>
            <br>We built this flyer wizard with our users in mind. To be sure we meet your needs as we make enhancements, tell us what you think. <a href="<%= ResolveUrl("~/contacts.aspx") %>">Click here</a> to send us your feedback.
            <br><br>
            <a href="<%= ResolveUrl("~/ensureemailinstructions.aspx") %>">Steps to ensure <%= clsUtility.ProjectName %> emails reach your inbox</a>
        </div>
    </div>
</div>