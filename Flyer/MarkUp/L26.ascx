﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="L26.ascx.cs" Inherits="FlyerMe.Flyer.MarkUp.L26" %>
<%@ Register Src="~/Flyer/MarkUp/L20X_PropertyFeaturesBlock.ascx" TagPrefix="uc" TagName="PropertyFeatures" %>
<%@ Import Namespace="FlyerMe" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
    	<td align="center">
			<link href='https://fonts.googleapis.com/css?family=Lato:400,700,900&subset=latin,latin-ext' rel='stylesheet' type='text/css' />
        	<table width="648" cellpadding="0" cellspacing="0" border="0" style="font-family: 'Lato', sans-serif;">
            	<tr>
                	<td style=" text-align:right; overflow:hidden; line-height:32px; font-size:14px; letter-spacing:0.1px;">
                    	<table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td width="37%" bgcolor="#232323" style="padding:16px 10px 12px 48px;">
                                <a href="<%= RootUrl %>" style="display:block; float:left"><img src="<%= EmailImageUrl %>images/FlyerMe_logo_blue.png" /></a>
                            </td>
                            <td  width="18%" bgcolor="#232323" style="padding:16px 0px 12px 0px;">
                                <a href="<%= ViewPdfUrl %>" style="color:#f37b22;">View PDF</a>  
                            </td>
                            <td  width="25%" align="center"  bgcolor="#232323" style="padding:16px 0px 12px 0px;">
                                <a href="<%= SendToClientsUrl %>" style="color:#f37b22;">Send to clients</a>
                            </td>
                            <td   width="124"  bgcolor="#232323" style="padding:16px 47px 12px 0;">
                                 <div><!--[if mso]>
  <v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" href="<%= ViewOnlineUrl %>" style="height:32px;v-text-anchor:middle;width:116px;" arcsize="50%" stroke="f" fillcolor="#f37b22">
    <w:anchorlock/>
    <center>
  <![endif]-->
      <a href="<%= ViewOnlineUrl %>"
style="background-color:#f37b22;border-radius:16px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:13px;font-weight:bold;line-height:32px;text-align:center;text-decoration:none;width:116px;-webkit-text-size-adjust:none;">View online</a>
  <!--[if mso]>
    </center>
  </v:roundrect>
<![endif]--></div>
                            </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                <% if (IsFirstPhotoBlockVisible) { %>
                <tr>
                	<td>
                   	<img src="<%= FirstPhotoUrl %>" style="max-width:648px; float:left"/></td>
                </tr>
                <% } %>
                <% if (IsMlsPriceBlockVisible) { %>
                <tr>
                	<td bgcolor="#f37b22" style="width:100%; height:28px; padding:18px 0 16px; margin:-2px 0 0 0;  float:left; font-size:17px; color:#838383; color:#ffffff; line-height:20px; letter-spacing:0.6px;">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        	<tr>
                            <% if (Flyer.MlsNumber.HasText()) { %>
                            <td width="50%" align="center" valign="bottom" style="color:#ffffff;"><img src="<%= EmailImageUrl %>images/icons_1.png" style="vertical-align:text-bottom" />MLS: <strong><%= Flyer.MlsNumber %></strong></td>
                            <% } %>
                            <% if (Price.HasText()) { %>
                            <td width="50%" align="center" valign="bottom" style="border-left:1px solid #d3d7d7; color:#ffffff;"><img src="<%= EmailImageUrl %>images/icons_2.png"  style="vertical-align:text-bottom"  />Price: <strong><%= Price %></strong></td>
                            <% } %>
                        </tr>
                        </table>
                    </td>
                </tr>
                <% } %>
                <% if (IsFlyerTitleBlockVisible) { %>
                <tr>
                    <td style="padding:41px 0px 36px; font-size:36px; line-height:48px; font-weight:600; text-align:center;letter-spacing:0.6px; width:100%; float:left; color:#ffffff;" bgcolor="#09509e">
                        <%= Flyer.FlyerTitle %>
                    </td>
                </tr>
                <% } %>
                <% if (IsPropertyDescriptionBlockVisible) { %>
                <tr>
                	<td background="<%= EmailImageUrl %>images/bg_images_00_4.png" bgcolor="#eef4f4" width="100%" height="171" style="font-size:15px; line-height:36px; letter-spacing:0.6px; color:#ffffff;" valign="middle">
  <!--[if gte mso 9]>
  <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:171px;">
    <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_images_00_4.png" color="#eef4f4" />
    <v:textbox inset="0,0,0,0">
  <![endif]-->
                         
                        <table cellpadding="0" cellspacing="0" border="0">
                            <% if (Flyer.Bedrooms.HasText() || Flyer.Subdivision.HasText()) { %>
                        	<tr>
                                <% if (Flyer.Bedrooms.HasText()) { %>
                            	<td  valign="bottom" style="padding:8px 40px 0px 50px;color:#ffffff;"><img src="<%= EmailImageUrl %>images/icons_3.png" /> <span>Bedrooms:</span><strong><%= Flyer.Bedrooms %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:8px 40px 0px 50px;color:#ffffff;"></td>
                                <% } %>
                                <% if (Flyer.Subdivision.HasText()) { %>
                            	<td  valign="bottom" style="padding:8px 40px 0px 50px;color:#ffffff;"><img src="<%= EmailImageUrl %>images/icons_6.png" /> <span>Subdivision:</span><strong><%= Flyer.Subdivision %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:8px 40px 0px 50px;color:#ffffff;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% if (Flyer.Bathrooms.HasText() || Flyer.YearBuilt.HasText()) { %>
                            <tr>
                                <% if (Flyer.Bathrooms.HasText()) { %>
                            	<td valign="bottom" style="padding:0px 40px 0px 50px;color:#ffffff;"><img src="<%= EmailImageUrl %>images/icons_4.png" /> <span>Full Baths:</span><strong><%= Flyer.Bathrooms %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:0px 40px 0px 50px;color:#ffffff;"></td>
                                <% } %>
                                <% if (Flyer.YearBuilt.HasText()) { %>
                            	<td valign="bottom" style="padding:0px 40px 0px 50px;color:#ffffff;"><img src="<%= EmailImageUrl %>images/icons_7.png" /> <span>Year Built:</span><strong><%= Flyer.YearBuilt %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:0px 40px 0px 50px;color:#ffffff;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% if (Flyer.LotSize.HasText() || Flyer.Sqft.HasText()) { %>
                        	<tr>
                                <% if (Flyer.LotSize.HasText()) { %>
                            	<td valign="bottom" style="padding:0px 40px 0px 50px;color:#ffffff;"><img src="<%= EmailImageUrl %>images/icons_5.png" /> <span>Lot Size:</span><strong><%= Flyer.LotSize %></strong></td>
                                <% } %>
                            	<td valign="bottom" style="padding:0px 40px 0px 50px;color:#ffffff;"><img src="<%= EmailImageUrl %>images/icons_8.png" /> <span>Sqft:</span><strong><%= Flyer.Sqft %></strong></td>
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
                <% if (IsSecondPhotoBlockVisible || IsThirdPhotoBlockVisible) { %>
                <tr>
                    <td>
                    	<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
                                <% if (IsSecondPhotoBlockVisible) { %>
								<td <%= ShouldColspan(0, 1) ? "colspan='2'" : null %>>
									<img src="<%= GetSecondaryPhotoUrl(0, 1) %>" style="float:left;" />
								</td>
                                <% } %>
                                <% if (IsThirdPhotoBlockVisible) { %>
								<td <%= ShouldColspan(1, 1) ? "colspan='2'" : null %>>
									<img src="<%= GetSecondaryPhotoUrl(1, 1) %>" style="float:left;" />
								</td>
                                <% } %>
							</tr>
                        </table>
                    </td>
                </tr>
                <% } %>
                <% if (IsPropertyDescriptionBlockVisible) { %>
                <tr>
                	<td style="padding:28px 0 51px 10px;">
                        <p style="font-size:36px; line-height:26px; letter-spacing:0.1px; text-align:center; font-weight:600; width:auto; color:#09509e;">Property Features</p>
                            <table cellpadding="0" cellspacing="0" width="100%"  style="padding:2px 0 0 0px; font-size:14px; line-height:16px; color:#838383;">
                            	<uc:PropertyFeatures ID="propertyFeatures" runat="server" />
                            </table>
                    </td>
                </tr>
                <% } %>
                <% if (IsFourthPhotoBlockVisible || IsFifthPhotoBlockVisible) { %>
                <tr>
                	<td>
                    	
                    	<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
                                <% if (IsFourthPhotoBlockVisible) { %>
								<td <%= ShouldColspan(2, 2) ? "colspan='2'" : null %>>
									<img src="<%= GetSecondaryPhotoUrl(2, 2) %>" style="float:left;" />
								</td>
                                <% } %>
                                <% if (IsFifthPhotoBlockVisible) { %>
								<td <%= ShouldColspan(3, 2) ? "colspan='2'" : null %>>
									<img src="<%= GetSecondaryPhotoUrl(3, 2) %>" style="float:left;" />
								</td>
                                <% } %>
							</tr>
                        </table>
                    </td>
                </tr>
                <% } %>
                <% if (IsOpenHousesBlockVisible) { %>
                <tr>
                     <td bgcolor="#ff7c0c">
								<table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center" style="font-size:36px; line-height:26px; letter-spacing:0.1px; font-weight:600; color:#ffffff; padding:33px 0 0; ">
                                    Open Houses
                                </td>
                             </tr>
                             <tr>
                                <td align="center" style="padding:22px 8% 0; color:#ffffff;">
									<%= Flyer.OpenHouses %>
                                </td>
                           </tr> 
                      	</table>
                    </td>
                </tr> 
                <% } %>
                <% if (IsDescriptionBlockVisible) { %>
                <tr>
                <td background="<%= EmailImageUrl %>images/07_flyerUs_bg_im_01.png" bgcolor="#f37b22" height="233" width="100%" style=" color:#ffffff; font-size:15px; line-height:26px; letter-spacing:0.6px;" valign="top">
                      <!--[if gte mso 9]>
                      <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:233px;">
                        <v:fill type="tile" src="<%= EmailImageUrl %>images/07_flyerUs_bg_im_01.png" color="#eef4f4" />
                        <v:textbox inset="0,0,0,0">
                      <![endif]-->
                      	<table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center" style="font-size:36px; line-height:26px; letter-spacing:0.1px; font-weight:600; color:#ffffff; padding:31px 0 0; margin:0;">
                                    Property Description
                                </td>
                             </tr>
                             <tr>
                                <td align="center" style="padding:32px 8%;color:#ffffff;">
                                   <%= Description %>
                                </td>
                           </tr> 
                      	</table>
                      <!--[if gte mso 9]>
                        </v:textbox>
                      </v:rect>
                      <![endif]-->
                </td>
                </tr>
                <% } %>
                <% if (IsAddressBlockVisible) { %>
                <tr>
                    <td height="352" bgcolor="#ecf4f4">
                    <a href="<%= Flyer.MapLink %>" style="text-decoration:none;" target="_blank"> 
                    <table cellpadding="0" cellspacing="0" width="100%" height="352" >
                    	<tr>
                            <td background="<%= GoogleMapImageLink %>" bgcolor="#ecf4f4" width="648" height="352" valign="top">
                              <!--[if gte mso 9]>
                              <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:352px;">
                                <v:fill type="tile" src="<%= GoogleMapImageLink %>" color="#ecf4f4" />
                                <v:textbox inset="0,0,0,0">
                              <![endif]-->
                            <table cellpadding="0" cellspacing="0">
                            	<tr>
                                	<td height="68px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td width="48"></td>
                                    <td background="<%= EmailImageUrl %>images/address_blue.png" bgcolor="#09509e" style="border-radius:50%; -webkit-border-radius:50%; -moz-border-radius:50%;" width="199" height="199" valign="midle">
                                        <table cellpadding="0" cellspacing="0" width="199" height="199" style="font-size:14px; line-height:15px; letter-spacing:0.6px; text-align:center; color:#ffffff;">
                                            <tr>
                                                <td valign="bottom" style="padding:0 40px; color:#ffffff;">
                                                    <%= Flyer.AptSuiteBldg %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="31" align="center">
                                                    <hr style="width:40px; margin:0 auto; background:#ffffff; border:0; height:1px;  float:none;" /> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" style="padding:0 30px;color:#ffffff;">
                                                    <%= Address %>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                              <!--[if gte mso 9]>
                                </v:textbox>
                              </v:rect>
                              <![endif]-->
                            </td>
                        </tr>
                        </table>
                        </a>
                    </td>
                </tr>
                <% } %>
                <% if (IsContactsBlockVisible) { %>
                <tr>
                    <td style="padding:47px 0 41px; font-size:14px; line-height:22px; color:#8d8d8d; letter-spacing:0.6px;">
                    	<table cellpadding="0" cellspacing="0" width="100%">
                        	<tr>
                            	<td  width="26%" align="right"><img src="<%= AvatarUrl %>"/></td>
                                <td width="40%"  style="border-right:1px solid #e8e8e8; padding-left:24px">
                                	<strong style="font-size:20px; line-height:22px; font-weight:600; color:#000000;"><%= Flyer.Name %></strong><br>
                                    <%= BrokerageName %><br>
                                    Phone: <%= Phone %><br>
                                    <% if (MobilePhone.HasText()) { %>
                                    Mobile: <%= MobilePhone %><br>
                                    <% } %>
                                    <a href="mailto:<%= Flyer.Email %>" style="color:#09509e;"><%= Flyer.Email %></a>
                                </td>
                                <td width="34%" ><img src="<%= CompanyLogoUrl %>" /></td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
                <% } %>
                <tr>
                	<td style="padding:16px 48px 12px 49px; text-align:right; overflow:hidden;; font-size:14px; letter-spacing:0.1px;" bgcolor="#232323">
                    	<table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td width="40%">
                                <a href="<%= RootUrl %>" style="display:block; float:left"><img src="<%= EmailImageUrl %>images/FlyerMe_logo_blue.png" /></a>
                            </td>
							<td  width="35%" style="color:#ffffff;">
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