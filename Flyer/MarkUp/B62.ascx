<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B62.ascx.cs" Inherits="FlyerMe.Flyer.MarkUp.B62" %>
<%@ Register Src="~/Flyer/MarkUp/L20X_PropertyFeaturesBlock_Buyers.ascx" TagPrefix="uc" TagName="PropertyFeatures" %>
<%@ Import Namespace="FlyerMe" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
    	<td align="center">
			<link href='https://fonts.googleapis.com/css?family=Lato:400,700,900&subset=latin,latin-ext' rel='stylesheet' type='text/css' />
        	<table width="648" cellpadding="0" cellspacing="0" border="0" style="font-family: 'Lato', sans-serif;">
            	<tr>
                	<td style="padding:16px 10px 0px 63px; text-align:right; overflow:hidden; line-height:32px; font-size:14px; letter-spacing:0.1px;">
                    	<table cellpadding="0" cellspacing="0" border="0" style=" margin:0 38px; padding-bottom:12px">
                        <tr>
                            <td width="40%">
                                <a href="<%= RootUrl %>" style="display:block; float:left"><img src="<%= EmailImageUrl %>images/<%= clsUtility.ProjectName %>_logo.png" /></a>
                            </td>
                            <td  width="15%">
                                <a href="<%= ViewPdfUrl %>" style="color:#09509e;">View PDF</a>  
                            </td>
                            <td  width="25%" align="center">
                                <a href="<%= SendToClientsUrl %>" style="color:#09509e;">Send to clients</a>
                            </td>
                            <td   width="124">
                                 <div><!--[if mso]>
  <v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" href="<%= ViewOnlineUrl %>" style="height:32px;v-text-anchor:middle;width:120px;" arcsize="50%" stroke="f" fillcolor="#f37b22">
    <w:anchorlock/>
    <center>
  <![endif]-->
      <a href="<%= ViewOnlineUrl %>"
style="background-color:#f37b22;border-radius:16px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:13px;font-weight:bold;line-height:32px;text-align:center;text-decoration:none;width:120px;-webkit-text-size-adjust:none;">View online</a>
  <!--[if mso]>
    </center>
  </v:roundrect>
<![endif]--></div>
                            </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                <% if (IsFlyerTitleBlockVisible || IsDescriptionBlockVisible) { %>
                <tr>
                	<td background="<%= EmailImageUrl %>images/08_bg_im_01.png" bgcolor="#ff7c0c" width="648" height="384" valign="top">
                  <!--[if gte mso 9]>
                  <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:384px;">
                    <v:fill type="tile" src="<%= EmailImageUrl %>images/08_bg_im_01.png" color="#ff7c0c" />
                    <v:textbox inset="0,0,0,0">
                  <![endif]-->
                  <table cellpadding="0" cellspacing="0" width="100%" style="color:#ffffff">
                      <% if (IsFlyerTitleBlockVisible) { %>
                  <tr>
                   <td style="font-size:36px; text-align:center; padding:55px 0 0 0;">Buyer’s agent has potential<br>client for your property</td>
                   </tr>
                      <% } %>
                      <% if (IsDescriptionBlockVisible) { %>
                   <tr>
                    <td style="text-align:center; padding:50px 90px 45px; font-size:15px; line-height:24px; width:auto;">
                        <%= Description %>
                    </td>
                     </tr>
                      <% } %>
                     </table>
                    <!--[if gte mso 9]>
                        </v:textbox>
                      </v:rect>
                      <![endif]-->
                    </td>
                </tr>
                <% } %>
                <% if (IsMlsPriceBlockVisible || Flyer.Hoa.HasText() || IsPropertyDescriptionBlockVisible) { %>
                <tr>
                	<td>
                    <table cellpadding="0" cellspacing="0" border="0" style="color:#09509e;">
                            <% if (Flyer.MlsNumber.HasText() || Flyer.Hoa.HasText()) { %>
                        	<tr>
                                <% if (Flyer.MlsNumber.HasText()) { %>
                            	<td  valign="bottom" style="padding:65px 0px 8px 63px;" width="48%"><img src="<%= EmailImageUrl %>images/icons_blue_1.png" /> <span>MLS:</span> <strong style="color:#000000;"><%= Flyer.MlsNumber %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:65px 0px 8px 63px;" width="48%"></td>
                                <% } %>
                                <% if (Flyer.Hoa.HasText()) { %>
                           	    <td  valign="bottom" style="padding:65px 0px 8px 63px;"><img src="<%= EmailImageUrl %>images/icons_blue_9.png" /> <span>HOA:</span> <strong style="color:#000000;"><%= Flyer.Hoa %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:65px 0px 8px 63px;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% if (Flyer.Bedrooms.HasText() || Flyer.Subdivision.HasText()) { %>
                        	<tr>
                                <% if (Flyer.Bedrooms.HasText()) { %>
                            	<td  valign="bottom" style="padding:8px 0px 8px 63px;" width="48%"><img src="<%= EmailImageUrl %>images/icons_blue_3.png" /> <span>Bedrooms:</span> <strong style="color:#000000;"><%= Flyer.Bedrooms %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:8px 0px 8px 63px;" width="48%"></td>
                                <% } %>
                                <% if (Flyer.Subdivision.HasText()) { %>
                            	<td  valign="bottom" style="padding:8px 0px 8px 63px;"><img src="<%= EmailImageUrl %>images/icons_blue_6.png" /> <span>Subdivision:</span> <strong style="color:#000000;"><%= Flyer.Subdivision %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:8px 0px 8px 63px;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% if (Flyer.Bathrooms.HasText() || Flyer.YearBuilt.HasText()) { %>
                            <tr>
                                <% if (Flyer.Bathrooms.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 0px 8px 63px;" width="48%"><img src="<%= EmailImageUrl %>images/icons_blue_4.png" /> <span>Full Baths:</span> <strong style="color:#000000;"><%= Flyer.Bathrooms %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 0px 8px 63px;" width="48%"></td>
                                <% } %>
                                <% if (Flyer.YearBuilt.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 0px 8px 63px;"><img src="<%= EmailImageUrl %>images/icons_blue_7.png" /> <span>Year Built:</span> <strong style="color:#000000;"><%= Flyer.YearBuilt %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 0px 8px 63px;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% if (Flyer.LotSize.HasText() || Flyer.Sqft.HasText()) { %>
                        	<tr>
                                <% if (Flyer.LotSize.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 0px 55px 63px;" width="48%"><img src="<%= EmailImageUrl %>images/icons_blue_5.png" /> <span>Lot Size:</span> <strong style="color:#000000;"><%= Flyer.LotSize %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 0px 55px 63px;" width="48%"></td>
                                <% } %>
                                <% if (Flyer.Sqft.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 0px 55px 63px;"><img src="<%= EmailImageUrl %>images/icons_blue_8.png" /> <span>Sqft:</span> <strong style="color:#000000;"><%= Flyer.Sqft %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 0px 55px 63px;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                        </table>
                        </td>
                </tr>
                <% } %>
                <% if (IsPriceRangeBlockVisible || IsContactsBlockVisible || IsLocationBlockVisible) { %>
                <tr>
                	<td align="center">
                        <% if (IsPriceRangeBlockVisible) { %>
                    	<table cellpadding="0" cellspacing="0" width="372" style="border:2px solid #09509e;border-radius:19px; -webkit-border-radius:19px; -moz-border-radius:19px;font-weight:900; color:#000000;">
                            <tr>
                                <td style="padding:10px 0;" align="center">
                                    <img src="<%= EmailImageUrl %>images/icons_blue_2.png" />  Price: <%= PriceRange %>
                                </td>
                            </tr>
                        </table>
                        <% } %>
                        <% if (IsContactsBlockVisible || IsLocationBlockVisible) { %>
                        <table cellpadding="0" cellspacing="0" width="100%" style="font-size:15px;  line-height:26px; letter-spacing:0.6px; color:#09509e; padding:50px 0 0px 0;" >
                            <% if (IsContactsBlockVisible) { %>
                        	<tr>
                            <td>
                            <table cellpadding="0" cellspacing="0" width="100%" style="font-size:15px;  line-height:26px; letter-spacing:0.6px; color:#09509e;">
                        	<tr>
                            	<td style="padding:0 0 0 70px;" valign="bottom"><img src="<%= EmailImageUrl %>images/icons_blue_10.png" />Phone: <strong style=" color:#000000;"><%= Phone %></strong>
                                </td>
                            	<td valign="bottom"><img src="<%= EmailImageUrl %>images/icons_blue_11.png" />Email: <strong style="color:#000000;"><a href="mailto:<%= Flyer.Email %>"><%= Flyer.Email %></a></strong>
                                </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                            <% } %>
                            <% if (IsLocationBlockVisible) { %>
                        	<tr>
                            	<td style="padding:20px 0 55px 70px; line-height:26px;" valign="bottom">
                                <span style="display:block; padding:0 3px 25px 0;float:left;"><img src="<%= EmailImageUrl %>images/icons_blue_12.png" />Location:</span><span style="color:#000000;"><%= Flyer.Location.Replace("\n", "<br />") %></span>
                                </td>
                            </tr>
                            <% } %>
                        </table>
                        <% } %>
                    </td>
                </tr>
                <% } %>
                <% if (IsPropertyFeaturesBlockVisible) { %>
                <tr>
                	<td background="<%= EmailImageUrl %>images/08_bg_im_02.png" bgcolor="#ff7c0c" width="648" height="560" valign="top" align="center">
                      <!--[if gte mso 9]>
                      <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:560px;">
                        <v:fill type="tile" src="<%= EmailImageUrl %>images/08_bg_im_02.png" color="#ff7c0c" />
                        <v:textbox inset="0,0,0,0">
                      <![endif]-->
                      <p>&nbsp;</p>
                    	 <p style="font-size:36px; line-height:26px; letter-spacing:0.1px; text-align:center; font-weight:600; width:auto; color:#ffffff;">Property Features</p>
                            <table cellpadding="0" cellspacing="0" width="90%"  style="font-size:14px; line-height:16px; color:#ffffff;  line-height:26px; margin:0 auto">
                            	<uc:PropertyFeatures ID="propertyFeatures" runat="server" />
                            </table>
                      <!--[if gte mso 9]>
                        </v:textbox>
                      </v:rect>
                      <![endif]-->
                    </td>
                </tr>
                <% } %>
                <tr>
                	<td style="padding:16px 10px 0px 10px; text-align:right; overflow:hidden; line-height:32px; font-size:14px; letter-spacing:0.1px; border-top:1px solid #e5e5e5;">
                    	<table cellpadding="0" cellspacing="0" border="0" style=" margin:0 38px; padding-bottom:12px">
                        <tr>
                            <td width="40%">
                                <a href="<%= RootUrl %>" style="display:block; float:left"><img src="<%= EmailImageUrl %>images/<%= clsUtility.ProjectName %>_logo.png" /></a>
                            </td>
 						    <td  width="35%">
                                Create your own flyer
                            </td>
                           <td width="224">
                                 <div><!--[if mso]>
  <v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" href="<%= CreateFlyerUrl %>" style="height:32px;v-text-anchor:middle;width:120px;" arcsize="50%" stroke="f" fillcolor="#f37b22">
    <w:anchorlock/>
    <center>
  <![endif]-->
      <a href="<%= CreateFlyerUrl %>"
style="background-color:#f37b22;border-radius:16px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:13px;font-weight:bold;line-height:32px;text-align:center;text-decoration:none;width:120px;-webkit-text-size-adjust:none;">NOW</a>
  <!--[if mso]>
    </center>
  </v:roundrect>
<![endif]--></div>
                            </td>
                </tr>
            </table>
        </td>
                </tr>
            </table>
        </td>
    </tr>
</table>