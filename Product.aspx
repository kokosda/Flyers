<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="FlyerMe.Product" MasterPageFile="~/MasterPage.master" Theme="" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="product-real">
        <div class="content">
            <img src="images/ipod.png" alt="" />
            <div class="text">
                <h1>Real Estate Email Flyers</h1>
                <%= clsUtility.SiteBrandName %> helps motivated real estate agents like you reclaim your time and results by connecting you to your target audience quickly. Available to you 24/7 and 365 days of the year, you can design an effective, beautiful flyer in minutes. 
            </div>
        </div>
    </div>
    <div class="product-blocks">
        <div class="content">
            <div class="block block-1">
                <div class="icon"></div>
                <h3>Creative Design</h3>
                Clean and creative designs highlight the very best features of your property.
            </div>
            <div class="block block-2">
                <div class="icon"></div>
                <h3>Excellent Customer Service</h3>
                Our email marketing services are available to you 24/7. We exist to help you succeed.
            </div>
            <div class="block block-3">
                <div class="icon"></div>
                <h3>Get Results Quickly</h3>
                Create a beautiful flyer and put your listing in front of its ideal audience in minutes.
            </div>
        </div>
    </div>
    <div class="product-work">
        <div class="content">
            <h2>How does it work?</h2>
            <div class="block n1 odd">
                <div class="conteiner">
                    <div class="content">
                        <h6>Sign up</h6>
            Using our secure sign-up form, create your account.
                    </div>
                </div>
                <div class="icon"></div>
            </div>
            <div class="block n2 even">
                <div class="conteiner">
                    <div class="content">
                        <h6>Choose flyer</h6>
            Select your favorite design from the wide selection of beautiful <br>
            flyer templates, or upload your own custom design.   
                    </div>
                </div>
                <div class="icon"></div>
            </div>
            <div class="block n3 odd">
                <div class="conteiner">
                    <div class="content">
                        <h6>Fill in the property info</h6>
            Write a description of the space, amenities, area or neighborhood and the property’s availability. Be sure to include your property’s best features and unique selling points. 
                    </div>
                </div>
                <div class="icon"></div>
            </div>
            <div class="block n4 even">
                <div class="conteiner">
                    <div class="content">
                        <h6>Send it</h6>
            A step by step process guides you through selecting the area where your flyer will be delivered. Click “send.” Congratulations, your flyer has been sent!   
                    </div>
                </div>
                <div class="icon"></div>
            </div>
        </div>
    </div>
    <div class="toch">
        <h2>Get in touch</h2>
        Stay on top of the latest real estate marketing trends, product promotions, tutorials, and more.
        <form method="get" enctype="application/x-www-form-urlencoded" action="<%= RootURL %>subscribe.aspx">
           	<div class="form-content">
                <div class="subscribe">
                    <input type="text" name="email" />
                    <input type="submit" value="Subscribe Now" />
                </div>
            </div>
        </form>
    </div>
</asp:Content>