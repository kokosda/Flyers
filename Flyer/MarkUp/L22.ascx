<%@ Control Language="C#" AutoEventWireup="true" CodeFile="L22.ascx.cs" Inherits="FlyerMe.Flyer.MarkUp.L22" %>
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
                                <td width="37%" bgcolor="#09509e" style="padding:16px 10px 12px 48px;">
                                    <a href="<%= RootUrl %>" style="display:block; float:left"><img src="<%= EmailImageUrl %>images/FlyerMe_logo_blue.png" /></a>
                                </td>
                                <td  width="18%" bgcolor="#f37b22" style="padding:16px 0px 12px 0px;">
                                    <a href="<%= ViewPdfUrl %>" style="color:#ffffff;">View PDF</a>  
                                </td>
                                <td  width="25%" align="center"  bgcolor="#f37b22" style="padding:16px 0px 12px 0px;">
                                    <a href="<%= SendToClientsUrl %>" style="color:#ffffff;">Send to clients</a>
                                </td>
                                <td   width="124"  bgcolor="#f37b22" style="padding:16px 47px 12px 0;">
                                     <div><!--[if mso]>
      <v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" href="<%= ViewOnlineUrl %>" style="height:28px;v-text-anchor:middle;width:116px;" arcsize="58%" strokecolor="#ffffff" fillcolor="#f37b22">
        <w:anchorlock/>
        <center style="color:#ffffff;font-family:sans-serif;font-size:13px;font-weight:bold;">View online</center>
      </v:roundrect>
    <![endif]--><a href="<%= ViewOnlineUrl %>"
    style="background-color:#f37b22;border:2px solid #ffffff;border-radius:16px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:13px;font-weight:bold;line-height:28px;text-align:center;text-decoration:none;width:116px;-webkit-text-size-adjust:none;mso-hide:all;">View online</a></div>
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
                <% if (IsMlsPriceBlockVisible || IsFlyerTitleBlockVisible || IsDescriptionBlockVisible) { %>
                <tr>
                	<td background="<%= EmailImageUrl %>images/bg_image_02_01.png" bgcolor="#ffffff" width="648" height="393" valign="top" style="padding:33px 0px 0">
                          <!--[if gte mso 9]>
                          <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:426px;">
                            <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_image_02_01.png" color="#ffffff" />
                            <v:textbox inset="0,0,0,0">
                          <![endif]-->
                    	<table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <% if (IsMlsPriceBlockVisible) { %>
                        	<tr>
                            	<td align="center">
                                    <table cellpadding="0" cellspacing="0" border="0" width="478" style="border-radius:23px; -webkit-border-radius:23px; -moz-border-radius:23px; border:1px solid #e5e5e5">
                                        <tr>
                                            <% if (Flyer.MlsNumber.HasText()) { %>
                                            <td width="50%" align="center" valign="bottom" style="padding:10px 0;"><img src="<%= EmailImageUrl %>images/icons_orang_1.png" style="vertical-align:text-bottom" />MLS: <strong style="color:#000000;"><%= Flyer.MlsNumber %></strong></td>
                                            <% } %>
                                            <% if (Price.HasText()) { %>
                                            <td width="50%" align="center" valign="bottom" style="border-left:1px solid #d3d7d7; padding:10px 0;"><img src="<%= EmailImageUrl %>images/icons_orang_2.png"  style="vertical-align:text-bottom"  />Price: <strong style="color:#000000;"><%= Price %></strong></td>
                                            <% } %>
                                    </tr>
                                </table>
                          	</td>
                           </tr>
                            <% } %>
                        <% if (IsFlyerTitleBlockVisible) { %>
                           <tr>
                        <td style="padding:48px 0px 48px; font-size:36px; line-height:48px; font-weight:600; text-align:center;letter-spacing:0.6px; color:#f37b22">
                            <%= Flyer.FlyerTitle %>
                        </td>
                        </tr>
                        <% } %>
                        <% if (IsPropertyDescriptionBlockVisible) { %>
                        <tr>
                        <td>
                        <table cellpadding="0" cellspacing="0" border="0" style="color:#f37b22;">
                            <% if (Flyer.Bedrooms.HasText() || Flyer.Subdivision.HasText()) { %>
                        	<tr>
                                <% if (Flyer.Bedrooms.HasText()) { %>
                            	<td  valign="bottom" style="padding:8px 60px 8px 60px;"><img src="<%= EmailImageUrl %>images/icons_orang_3.png" /> <span>Bedrooms:</span> <strong style="color:#000000;"><%= Flyer.Bedrooms %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:8px 60px 8px 60px;"></td>
                                <% } %>
                                <% if (Flyer.Subdivision.HasText()) { %>
                            	<td  valign="bottom" style="padding:8px 60px 8px 60px;"><img src="<%= EmailImageUrl %>images/icons_orang_6.png" /> <span>Subdivision:</span> <strong style="color:#000000;"><%= Flyer.Subdivision %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:8px 60px 8px 60px;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% if (Flyer.Bathrooms.HasText() || Flyer.YearBuilt.HasText()) { %>
                            <tr>
                                <% if (Flyer.Bathrooms.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 60px 8px 60px;"><img src="<%= EmailImageUrl %>images/icons_orang_4.png" /> <span>Full Baths:</span> <strong style="color:#000000;"><%= Flyer.Bathrooms %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 60px 8px 60px;"></td>
                                <% } %>
                                <% if (Flyer.YearBuilt.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 60px 8px 60px;"><img src="<%= EmailImageUrl %>images/icons_orang_7.png" /> <span>Year Built:</span> <strong style="color:#000000;"><%= Flyer.YearBuilt %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 60px 8px 60px;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% if (Flyer.LotSize.HasText() || Flyer.Sqft.HasText()) { %>
                        	<tr>
                                <% if (Flyer.LotSize.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 60px 48px 60px;"><img src="<%= EmailImageUrl %>images/icons_orang_5.png" /> <span>Lot Size:</span> <strong style="color:#000000;"><%= Flyer.LotSize %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 60px 48px 60px;"></td>
                                <% } %>
                                <% if (Flyer.Sqft.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 60px 48px 60px;"><img src="<%= EmailImageUrl %>images/icons_orang_8.png" /> <span>Sqft:</span> <strong style="color:#000000;"><%= Flyer.Sqft %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 60px 48px 60px;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                        </table>
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
                <% if (IsSecondaryPhotosBlockVisible) { %>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <% var openTr = false;
                               var closeTr = false;
                                
                               for (var i = 0; i < SecondaryPhotos.Count; i++)
                               {
                                   openTr = i % 2 == 0;
                                   closeTr = (i + 1) % 2 == 0;

                                   if (openTr)
                                   { %>
                            <tr>
                                <% } %>
                                <td <%= ShouldColSpanTd(i) ? "colspan='2'" : null %>>
                                    <img src="<%= GetSecondaryPhotoUrl(i) %>" style="float:left;" />
                                </td>
                                   <%
                                   if (closeTr)
                                   {
                                       %>
                            </tr>
                                <% } %> 
                            <% } %>
                        </table>
                    </td>
                </tr>
                <% } %>
                <% if (IsPropertyFeaturesBlockVisible || IsPropertyDescriptionBlockVisible) { %>
                <tr>
                    <td background="<%= EmailImageUrl %>images/bg_image_02_2.png" bgcolor="#ffffff" width="648" height="529" valign="top" style="padding:48px 0 0;">
                                              <!--[if gte mso 9]>
                      <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:576px;">
                        <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_image_02_2.png" color="#ffffff" />
                        <v:textbox inset="0,0,0,0">
                      <![endif]-->
                    <% if (IsPropertyFeaturesBlockVisible) { %>
                        <p style="font-size:36px; line-height:26px; letter-spacing:0.1px; text-align:center; font-weight:600; width:auto; color:#09509e;">Property Features</p>
                            <table cellpadding="0" cellspacing="0" width="100%"  style="padding:42px 0 0 0px; font-size:14px; line-height:16px;">
                            	<uc:PropertyFeatures ID="propertyFeatures" runat="server" />
                            </table>
                    <% } %>
                    <% if (IsOpenHousesBlockVisible) { %>
                    <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center" style="font-size:36px; line-height:26px; letter-spacing:0.1px; font-weight:600; color:#09509e; padding:63px 0 0; ">
                                    Open Houses
                                </td>
                             </tr>
                             <tr>
                                <td align="center" style="padding:22px 8% 0;">
									<%= Flyer.OpenHouses %>
                                </td>
                           </tr> 
                    </table>
                    <% } %>
                    <% if (IsDescriptionBlockVisible) { %>
                            <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center" style="font-size:36px; line-height:26px; letter-spacing:0.1px; font-weight:600; color:#09509e; padding:31px 0 0;">
                                    Property Description
                                </td>
                             </tr>
                             <tr>
                                <td align="center" style="padding:32px 8%;line-height:26px;">
                                    <%= Description %>
                                </td>
                           </tr> 
                      	</table>
                    <% } %>
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
                	<td style="text-align:right; overflow:hidden; line-height:32px; font-size:14px; letter-spacing:0.1px; border-top:1px solid #e5e5e5;">
                    	<table cellpadding="0" cellspacing="0" border="0" >
                            <tr>
                               <td width="37%" bgcolor="#09509e" style="padding:16px 10px 12px 48px;">
                                    <a href="<%= RootUrl %>" style="display:block; float:left"><img src="<%= EmailImageUrl %>images/FlyerMe_logo_blue.png" /></a>
                                </td>
							    <td  width="35%" bgcolor="#f37b22" style="color:#ffffff; padding:16px 0px 12px 0px;">
                                    Create your own flyer
                                </td>
                               <td width="224" bgcolor="#f37b22" style="padding:16px 47px 12px 0;">
                                    <div><!--[if mso]>
      <v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" href="<%= CreateFlyerUrl %>" style="height:28px;v-text-anchor:middle;width:116px;" arcsize="58%" strokecolor="#ffffff" fillcolor="#f37b22">
        <w:anchorlock/>
        <center style="color:#ffffff;font-family:sans-serif;font-size:13px;font-weight:bold;">NOW</center>
      </v:roundrect>
    <![endif]--><a href="<%= CreateFlyerUrl %>"
    style="background-color:#f37b22;border:2px solid #ffffff;border-radius:16px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:13px;font-weight:bold;line-height:28px;text-align:center;text-decoration:none;width:116px;-webkit-text-size-adjust:none;mso-hide:all;">NOW</a>
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