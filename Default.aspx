<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="FlyerMe.Default" MasterPageFile="~/MasterPageLanding.master" Theme="" %>
<%@ Import Namespace="FlyerMe" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="top">
        <div class="content">
            <h2>Create and send your beautiful e-mail flyer in seconds</h2>
            <a href="<%= RootURL %>createflyer.aspx" class="start">Get started now</a>
        </div>
    </section>
    <section id="work">
        <div class="content">
            <h2>How does it work?</h2>
            <div class="block b1">
                <div class="icon"></div>
                <h3>Choose Your<br/> Market Area</h3>
                <div class="text">Our step by step process helps you select the area where your flyer will be delivered.</div>
            </div>     
            <div class="block b2">
                <div class="icon"></div>
                <h3>Pick Your<br/> Flyer</h3>
                <div class="text">Select your favorite flyer template or upload your own custom design.</div>
            </div> 
            <div class="block b3">
                <div class="icon"></div>
                <h3>Fill in the<br/> Property Info</h3>
                <div class="text">Describe your property and upload photos to the flyer.</div>
            </div> 
            <div class="block b4">
                <div class="icon"></div>
                <h3> Review, Approve<br/> and Deliver</h3>
                <div class="text">View the proof, make your edits and then click “send.” </div>
            </div>            
        </div>
    </section>
    <section id="marketing">
        <div class="content-m">
            <div class="content">
                <h2>Real estate<br/>marketing<br/>made simple</h2>
                <a href="<%= RootURL %>createflyer.aspx" class="start">Get started now</a>
            </div>
            <div class="video">
				<video poster="<%= RootURL %>images/<%= clsUtility.ProjectNameInLowerCase %>_video.jpg" controls="controls" border="10"> 
					<source src="<%= RootURL %>images/<%= clsUtility.ProjectNameInLowerCase %>.mp4" type="video/mp4"> 
					<p>Your browser does not support the video tag.</p>
				</video> 
			</div>
        </div>
    </section>
    <section id="divRecent" runat="server" class="recent">
        <div class="content-m">
            <div class="content">
                <h2>Recent Flyers</h2>
                    <asp:Repeater ID="rRecentFlyers" runat="Server" OnItemDataBound="rRecentFlyers_ItemDataBound">
                        <ItemTemplate>
                            <div class="block">
                                <div class="image">
                                    <a href="<%= RootURL %>ShowFlyer.aspx?oid=<%#DataBinder.Eval(Container.DataItem,"order_id")%>">
                                        <img src="<%# DataBinder.Eval(Container.DataItem,"Photo1").ToString().Trim() != "" ? ("Order/" +DataBinder.Eval(Container.DataItem,"order_id") + "/Photo1/" + DataBinder.Eval(Container.DataItem,"Photo1").ToString()) : "images/no-photo-front.jpg"%>" alt="<%#DataBinder.Eval(Container.DataItem, "prop_address1")%>" />
                                    </a>
                                </div>
                                <div class="table">
                                    <div class="table-cell">
                                        <span class="title"><%#DataBinder.Eval(Container.DataItem,"prop_address1")%> <%#DataBinder.Eval(Container.DataItem,"prop_city")%>, <%#DataBinder.Eval(Container.DataItem,"prop_state")%></span>
                                        <asp:Label ID="lblPrice" runat="server" CssClass="prices"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
            <a href="<%= RootURL %>search.aspx" class="start">View all Flyers</a>
            </div>
        </div>
    </section>
    <section id="subscribers">
        <div class="content">
            <h2>Send instantly to <%= 1000000.FormatCount() %><br/>subscribers anytime, anywhere</h2>
            <a href="<%= RootURL %>createflyer.aspx" class="start">Get started now</a>
        </div>
    </section>
    <section id="customers">
        <div class="content">
            <h2>Our customers are saying</h2>
            <ul>
                <asp:Repeater ID="rTestimonials" runat="Server" OnItemDataBound="rTestimonials_ItemDataBound">
                    <ItemTemplate>
                        <li>
                            <div class="conteiner">
                                <div class="text">
                			        <h3><asp:Literal ID="ltlFullname" runat="server"></asp:Literal></h3>
                                    <div class="spec"><asp:Literal ID="ltlCompany" runat="server"></asp:Literal></div>
                                    <div class="t"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></div>
                                </div>
                            </div>
                            <div class="image"><asp:Image ID="imgUser" runat="server" /></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </section>
    <section id="syndication">
        <div class="content">
            <h2>Listing syndication</h2>
            <ul>
                <span>
                	<li><span><a href="http://www.vast.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n1.png") %>" alt="Vast" /></a></span></li>
                	<li><span><a href="http://www.craigslist.org/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n2.png") %>" alt="Craigslist" /></a></span></li>
                	<li><span><a href="https://www.google.com/retail/merchant-center/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n3.png") %>" alt="GoogleBase" /></a></span></li>
                	<li><span><a href="http://www.ebay.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n4.png") %>" alt="eBay" /></a></span></li>
                </span>
                <span>
                	<li><span><a href="http://www.oodle.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n5.png") %>" alt="Oodle" /></a></span></li>
                	<li><span><a href="http://www.kijiji.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n6.png") %>" alt="Kijiji" /></a></span></li>
                	<li><span><a href="http://www.olx.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n7.png") %>" alt="OLX" /></a></span></li>
                	<li><span><a href="http://www.zillow.com/" target="_blank"><img src="<%= ResolveUrl("~/images/logos/n8.png") %>" alt="Zillow" /></a></span></li>
                </span>
            </ul>
        </div>
    </section>
    <section id="text">
        <div class="content">
        <p><%= clsUtility.ProjectName %>’s mission is to help motivated real estate agents grow their business by giving them the best digital marketing tools possible, available to them 24 hours per day, 7 days per week.</p>

    <p>We believe that providing quick and easy access to a pre-qualified audience of buyers and sellers is the best way to help a motivated real estate agent like you reach your goals. We accomplish this by helping you send well-designed flyers of your listing to the target market of your choice. It’s real estate marketing on your terms, your time frame, and your budget. </p>      
        </div>
    </section>
</asp:Content>