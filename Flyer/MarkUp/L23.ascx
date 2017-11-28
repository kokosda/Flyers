<%@ Control Language="C#" AutoEventWireup="true" CodeFile="L23.ascx.cs" Inherits="FlyerMe.Flyer.MarkUp.L23" %>
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
                                <td width="37%" bgcolor="#ffffff" style="padding:16px 10px 12px 48px;">
                                    <a href="<%= RootUrl %>" style="display:block; float:left"><img src="<%= EmailImageUrl %>images/FlyerMe_logo.png" /></a>
                                </td>
                                <td  width="18%" bgcolor="#ffa400" style="padding:16px 0px 12px 0px;">
                                    <a href="<%= ViewPdfUrl %>" style="color:#ffffff;">View PDF</a>  
                                </td>
                                <td  width="25%" align="center"  bgcolor="#ffa400" style="padding:16px 0px 12px 0px;">
                                    <a href="<%= SendToClientsUrl %>" style="color:#ffffff;">Send to clients</a>
                                </td>
                                <td   width="124"  bgcolor="#ffa400" style="padding:0 47px 0 0;">
                                     <div><!--[if mso]>
      <v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" href="<%= ViewOnlineUrl %>" style="height:28px;v-text-anchor:middle;width:116px;" arcsize="58%" strokecolor="#ffffff" fillcolor="#ffa400">
        <w:anchorlock/>
        <center style="color:#ffffff;font-family:sans-serif;font-size:13px;font-weight:bold;">View online</center>
      </v:roundrect>
    <![endif]--><a href="<%= ViewOnlineUrl %>"
    style="background-color:#ffa400;border:2px solid #ffffff;border-radius:16px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:13px;font-weight:bold;line-height:28px;text-align:center;text-decoration:none;width:116px;-webkit-text-size-adjust:none;mso-hide:all;">View online</a></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <% if (IsFirstPhotoBlockVisible) { %>
                <tr>
                	<td>
                    	<img src="<%= FirstPhotoUrl %>" style="max-width:648px;"/>
                    </td>
                </tr>
                <% } %>
                <% if (IsFlyerTitleBlockVisible) { %>
                <tr>
                	<td style="padding:56px 0px 56px; font-size:36px; line-height:48px; font-weight:600; text-align:center;letter-spacing:0.6px; color:#09509e;">
                        <%= Flyer.FlyerTitle %>
                    </td>
                </tr>
                <% } %>
                <% if (IsMlsPriceBlockVisible || IsSecondaryPhotosBlockVisible) { %>
                <tr>
                     <td>                        
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                              <tbody>
                                <tr>
                                <% if (IsMlsPriceBlockVisible || IsSecondPhotoBlockVisible) { %>
                                  <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <% if (IsMlsPriceBlockVisible) { %>
                                        <tr>
                                        <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td background="<%= EmailImageUrl %>images/bg_images_1.png" bgcolor="#f37b22" width="324" height="92" valign="top">
                                      <!--[if gte mso 9]>
                                      <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:324px;height:92px;">
                                        <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_images_1.png" color="#f37b22" />
                                        <v:textbox inset="0,0,0,0">
                                      <![endif]-->
                                                	<table cellpadding="0" cellspacing="0" border="0" width="100%"  style="color:#ffffff;">
                                            			<tr>
                                                            <% if (Flyer.MlsNumber.HasText()) { %>
                                                			<td width="50%" valign="bottom" style="padding:20px 0 5px 50px;"><img src="<%= EmailImageUrl %>images/icons_1.png" style="vertical-align:text-bottom" />MLS: <strong><%= Flyer.MlsNumber %></strong></td>
                                                            <% } else { %>
                                                            <td width="50%" valign="bottom" style="padding:20px 0 5px 50px;"></td>
                                                            <% } %>
                                                         </tr>
                                                         <tr>
                                                             <% if (Price.HasText()) { %>
                                                			<td width="50%" valign="bottom" style=" padding:5px 0 20px 50px;"><img src="<%= EmailImageUrl %>images/icons_2.png"  style="vertical-align:text-bottom"  />Price: <strong><%= Price %></strong></td>
                                                             <% } %>
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
                                            <% } %>
                                            <% if (IsSecondPhotoBlockVisible) { %>
                                            <tr>
                                                <td>
                        							<img src="<%= GetSecondaryPhotoUrl(0) %>" style="float:left;" />
                                                </td>
                                            </tr>
                                            <% } %>
                                        </table>
                                </td>
                                <% } %>
                                <% if (IsPropertyDescriptionBlockVisible) { %>
                                  <td background="<%= EmailImageUrl %>images/bg_images_2.png" bgcolor="#09509e" width="324" height="289" valign="top">
                                  <!--[if gte mso 9]>
                                  <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:324px;height:289px;">
                                    <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_images_2.png" color="#09509e" />
                                    <v:textbox inset="0,0,0,0">
                                  <![endif]-->
                                   <table cellpadding="0" cellspacing="0" border="0" style="color:#ffffff;">
                                       <% if (Flyer.Bedrooms.HasText()) { %>
                                        <tr>
                                            <td  valign="bottom" style="padding:24px 0 8px 77px;"><img src="<%= EmailImageUrl %>images/icons_3.png" />&nbsp;&nbsp; <span>Bedrooms:</span> <strong><%= Flyer.Bedrooms %></strong></td>
                                         </tr>
                                       <% } %>
                                       <% if (Flyer.Bathrooms.HasText()) { %>
                                         <tr>
                                            <td valign="bottom" style="padding:8px 0 8px 77px;"><img src="<%= EmailImageUrl %>images/icons_4.png" />&nbsp;&nbsp; <span>Full Baths:</span> <strong><%= Flyer.Bathrooms %></strong></td>
                                         </tr>
                                       <% } %>
                                       <% if (Flyer.LotSize.HasText()) { %>
                                         <tr>
                                            <td valign="bottom" style="padding:8px 0 8px 77px;"><img src="<%= EmailImageUrl %>images/icons_5.png" />&nbsp;&nbsp; <span>Lot Size:</span> <strong><%= Flyer.LotSize %></strong></td>
                                         </tr>
                                       <% } %>
                                       <% if (Flyer.Subdivision.HasText()) { %>
                                         <tr>
                                            <td  valign="bottom" style="padding:8px 0 8px 77px;"><img src="<%= EmailImageUrl %>images/icons_6.png" />&nbsp;&nbsp; <span>Subdivision:</span> <strong><%= Flyer.Subdivision %></strong></td>
                                         </tr>
                                       <% } %>
                                       <% if (Flyer.YearBuilt.HasText()) { %>
                                         <tr>
                                            <td valign="bottom" style="padding:8px 0 8px 77px;"><img src="<%= EmailImageUrl %>images/icons_7.png" />&nbsp;&nbsp; <span>Year Built:</span> <strong><%= Flyer.YearBuilt %></strong></td>
                                         </tr>
                                       <% } %>
                                       <% if (Flyer.Sqft.HasText()) { %>
                                         <tr>
                                            <td valign="bottom" style="padding:8px 0 8px 77px;"><img src="<%= EmailImageUrl %>images/icons_8.png" />&nbsp;&nbsp; <span>Sqft:</span> <strong><%= Flyer.Sqft %></strong></td>
                                        </tr>
                                       <% } %>
                                    </table>
                                         <!--[if gte mso 9]>
                                        </v:textbox>
                                      </v:rect>
                                      <![endif]-->
                                    </td>
                                <% } %>
                                </tr>
                                <% if (IsThirdPhotoBlockVisible || IsFourthPhotoBlockVisible) { %>
                                <tr>
                                    <% if (IsThirdPhotoBlockVisible) { %>
                                  <td <%= ShouldColspan(1) ? "colspan='2'" : null %>>
                        				<img src="<%= GetSecondaryPhotoUrl(1) %>" style="float:left;" /></td>
                                    <% } %>
                                    <% if (IsFourthPhotoBlockVisible) { %>
                                  <td <%= ShouldColspan(2) ? "colspan='2'" : null %>>
                        				<img src="<%= GetSecondaryPhotoUrl(2) %>" style="float:left;" /></td>
                                    <% } %>
                                </tr>
                                <% if (IsFifthPhotoBlockVisible) { %>
                                <tr>
                                  <td <%= ShouldColspan(3) ? "colspan='2'" : null %>>
                        				<img src="<%= GetSecondaryPhotoUrl(3) %>" style="float:left;" /></td>
                                </tr>
                                    <% } %>
                                <% } %>
                              </tbody>
                            </table>
                    </td>
                </tr>
                <% } %>
                <% if (IsPropertyFeaturesBlockVisible) { %>
                <tr>
                	<td style="padding:48px 0 60px 10px; width:100%; float:left;">
                        <p style="font-size:36px; line-height:26px; letter-spacing:0.1px; text-align:center; font-weight:600; width:auto;">Property Features</p>
                            <table cellpadding="0" cellspacing="0" width="100%"  style="padding:42px 0 0 0px; font-size:14px; line-height:16px;">
                            	<uc:PropertyFeatures ID="propertyFeatures" runat="server" />
                            </table>
                    </td>
                </tr>
                <% } %>
                <% if (IsOpenHousesBlockVisible) { %>
                <tr>
                     <td bgcolor="#09509e">
								<table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center" style="font-size:36px; line-height:26px; letter-spacing:0.1px; font-weight:600; color:#ffffff; padding:33px 0 0; ">
                                    Open Houses
                                </td>
                             </tr>
                             <tr>
                                <td align="center" style="padding:22px 8% 0;color:#ffffff; ">
									<%= Flyer.OpenHouses %>
                                </td>
                           </tr> 
                      	</table>
                    </td>
                </tr> 
                <% } %>
                <% if (IsDescriptionBlockVisible) { %>
                <tr>
                	<td background="<%= EmailImageUrl %>images/bg_images_3.png" bgcolor="#09509e" width="648" height="273" valign="top" style="text-align:center; color:#ffffff; font-size:15px; line-height:26px; letter-spacing:0.6px;">
                      <!--[if gte mso 9]>
                      <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:273px;">
                        <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_images_3.png" color="#09509e" />
                        <v:textbox inset="0,0,0,0">
                      <![endif]-->
                    	<table cellpadding="0" cellspacing="0" width="100%" style="color:#ffffff;">
                            <tr>
                           		<td align="center" style="font-size:36px; line-height:26px; letter-spacing:0.1px; font-weight:600;color:#ffffff; padding:79px 0 0 0;">Property Description</td>
                            </tr>
                            <tr>
                            	<td align="center" style="padding:32px 8%;color:#ffffff;"><%= Description %></td>
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
                                    <td bgcolor="#f37b22" style="border-radius:50%; -webkit-border-radius:50%; -moz-border-radius:50%;" width="199" height="199" valign="midle">
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
                	<td style="text-align:right; overflow:hidden; line-height:32px; font-size:14px; letter-spacing:0.1px; border-top:1px solid #e5e5e5;">
                    	<table cellpadding="0" cellspacing="0" border="0" >
                        <tr>
                           <td width="37%" bgcolor="#ffffff" style="padding:16px 10px 12px 48px;">
                                <a href="<%= RootUrl %>" style="display:block; float:left"><img src="<%= EmailImageUrl %>images/FlyerMe_logo.png" /></a>
                            </td>
							<td  width="35%" bgcolor="#ffa400" style="color:#ffffff;" style="padding:16px 0px 12px 0px;">
                                Create your own flyer
                            </td>
                           <td width="224" bgcolor="#ffa400" style="padding:16px 47px 12px 0;">
                                <div><!--[if mso]>
  <v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" href="<%= CreateFlyerUrl %>" style="height:28px;v-text-anchor:middle;width:116px;" arcsize="58%" strokecolor="#ffffff" fillcolor="#ffa400">
    <w:anchorlock/>
    <center style="color:#ffffff;font-family:sans-serif;font-size:13px;font-weight:bold;">NOW</center>
  </v:roundrect>
<![endif]--><a href="<%= CreateFlyerUrl %>"
style="background-color:#ffa400;border:2px solid #ffffff;border-radius:16px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:13px;font-weight:bold;line-height:28px;text-align:center;text-decoration:none;width:116px;-webkit-text-size-adjust:none;mso-hide:all;">NOW</a>
  <!--[if mso]>
    </center>
  </v:roundrect>
<![endif]--></div>
                            </td>
            </table>
        </td>
                </tr>
            </table>
        </td>
    </tr>
</table>