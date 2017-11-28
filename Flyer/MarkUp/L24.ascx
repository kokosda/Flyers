<%@ Control Language="C#" AutoEventWireup="true" CodeFile="L24.ascx.cs" Inherits="FlyerMe.Flyer.MarkUp.L24" %>
<%@ Register Src="~/Flyer/MarkUp/L20X_PropertyFeaturesBlock.ascx" TagPrefix="uc" TagName="PropertyFeatures" %>
<%@ Import Namespace="FlyerMe" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
    	<td align="center">
			<link href="https://fonts.googleapis.com/css?family=Lato:400,700,900&subset=latin,latin-ext" rel="stylesheet" type="text/css" />
        	<table width="648" cellpadding="0" cellspacing="0" border="0" style="font-family: 'Lato', sans-serif;">
            	<tr>
                	<td style="padding:16px 10px 0px 10px; text-align:right; overflow:hidden; line-height:32px; font-size:14px; letter-spacing:0.1px;">
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
                <% if (IsFirstPhotoBlockVisible) { %>
                <tr>
                	<td>
                    	<img src="<%= FirstPhotoUrl %>" style="max-width:648px; margin-bottom:-33px;"/>
                        <% if (IsSecondPhotoBlockVisible || IsThirdPhotoBlockVisible) { %>
                        <table cellpadding="0" cellspacing="0" width="100%">
                        	<tr>
                            	<td align="right" <%= ShouldColspan(0, 1) ? "colspan='2'" : null %>><img src="<%= GetSecondaryPhotoUrl(0, 1) %>" style="display:inline-block;" /></td>
                                <td align="left" <%= ShouldColspan(1, 1) ? "colspan='2'" : null %>><img src="<%= GetSecondaryPhotoUrl(1, 1) %>" style="display:inline-block;" /></td>
                            </tr>
                        </table>
                        <% } %>
                    </td>
                </tr>
                <% } %>
                <% if (IsFlyerTitleBlockVisible) { %>
                <tr>
                	<td style="padding:66px 0px 53px; font-size:36px; line-height:48px; font-weight:600; text-align:center;letter-spacing:0.6px; width:100%; color:#f37b22; float:left;">
                        <%= Flyer.FlyerTitle %>
                    </td>
                </tr>
                <% } %>
                <% if (IsPropertyDescriptionBlockVisible) { %>
                <tr>
                     <td background="<%= EmailImageUrl %>images/bg_images_04_1.png" bgcolor="#ffffff" width="648" height="169" valign="top">
                      <!--[if gte mso 9]>
                      <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:169px;">
                        <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_images_04_1.png" color="#ffffff" />
                        <v:textbox inset="0,0,0,0">
                      <![endif]-->
                        
                        <table cellpadding="0" cellspacing="0" border="0" style="color:#ffffff;">
                            <% if (Flyer.Bathrooms.HasText() || Flyer.Subdivision.HasText()) { %>
                        	<tr>
                                <% if (Flyer.Bedrooms.HasText()) { %>
                            	<td  valign="bottom" style="padding:28px 45px 8px 109px;"><img src="<%= EmailImageUrl %>images/icons_3.png" /> <span>Bedrooms:</span> <strong><%= Flyer.Bedrooms %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:28px 45px 8px 109px;"></td>
                                <% } %>
                                <% if (Flyer.Subdivision.HasText()) { %>
                            	<td  valign="bottom" style="padding:28px 45px 8px 45px;"><img src="<%= EmailImageUrl %>images/icons_6.png" /> <span>Subdivision:</span> <strong><%= Flyer.Subdivision %></strong></td>
                                <% } else { %>
                                <td  valign="bottom" style="padding:28px 45px 8px 45px;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% if (Flyer.Bathrooms.HasText() || Flyer.YearBuilt.HasText()) { %>
                            <tr>
                                <% if (Flyer.Bathrooms.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 45px 8px 109px;"><img src="<%= EmailImageUrl %>images/icons_4.png" /> <span>Full Baths:</span> <strong><%= Flyer.Bathrooms %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 45px 8px 109px;"></td>
                                <% } %>
                                <% if (Flyer.YearBuilt.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 45px 8px 45px;"><img src="<%= EmailImageUrl %>images/icons_7.png" /> <span>Year Built:</span> <strong><%= Flyer.YearBuilt %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 45px 8px 45px;"></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% if (Flyer.LotSize.HasText() || Flyer.Sqft.HasText()) { %>
                        	<tr>
                                <% if (Flyer.LotSize.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 45px 8px 109px;"><img src="<%= EmailImageUrl %>images/icons_5.png" /> <span>Lot Size:</span> <strong><%= Flyer.LotSize %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 45px 8px 109px;"></td>
                                <% } %>
                                <% if (Flyer.Sqft.HasText()) { %>
                            	<td valign="bottom" style="padding:8px 45px 8px 45px;"><img src="<%= EmailImageUrl %>images/icons_8.png" /> <span>Sqft:</span> <strong><%= Flyer.Sqft %></strong></td>
                                <% } else { %>
                                <td valign="bottom" style="padding:8px 45px 8px 45px;"></td>
                                <% } %>
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
                <% if (IsMlsPriceBlockVisible || IsPropertyFeaturesBlockVisible) { %>
                <tr>
                	<td background="<%= EmailImageUrl %>images/bg_images_04_2.png" bgcolor="#004ea6" width="648" height="420" valign="top">
                      <!--[if gte mso 9]>
                      <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:420px;">
                        <v:fill type="tile" src="<%= EmailImageUrl %>images/bg_images_04_2.png" color="#004ea6" />
                        <v:textbox inset="0,0,0,0">
                      <![endif]-->
                        <% if (IsMlsPriceBlockVisible) { %>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <table cellpadding="0" cellspacing="0" border="0" width="478" style="color:#ffffff">
                                        <tr>
                                            <% if (Flyer.MlsNumber.HasText()) { %>
                                            <td width="50%" align="center" valign="bottom" style="padding:20px 0;"><img src="<%= EmailImageUrl %>images/icons_1.png" style="vertical-align:text-bottom" />MLS: <strong><%= Flyer.MlsNumber %></strong></td>
                                            <% } %>
                                            <% if (Price.HasText()) { %>
                                            <td width="50%" align="center" valign="bottom" style=" padding:20px 0;"><img src="<%= EmailImageUrl %>images/icons_2.png"  style="vertical-align:text-bottom"  />Price: <strong><%= Price %></strong></td>
                                            <% } %>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                        <% } %>
                        <% if (IsPropertyFeaturesBlockVisible) { %>
                        <p style="font-size:36px; line-height:26px; letter-spacing:0.1px; text-align:center; font-weight:600; width:auto; color:#ffffff;">Property Features</p>
                            <table cellpadding="0" cellspacing="0" width="100%"  style="font-size:14px; line-height:16px; color:#ffffff;">
                            	<uc:PropertyFeatures ID="propertyFeatures" runat="server" />
                            </table>
                        <% } %>
                      <!--[if gte mso 9]>
                        </v:textbox>
                      </v:rect>
                      <![endif]-->
                    </td>
                </tr>
                <% } %>
                <% if (IsFourthPhotoBlockVisible || IsFifthPhotoBlockVisible) { %>
                <tr>
                	<td>
                     <table cellpadding="0" cellspacing="0" width="100%" style="margin-top:-33px;">
                        	<tr>
                                <% if (IsFourthPhotoBlockVisible) { %>
                                <td align="right" <%= ShouldColspan(2, 2) ? "colspan='2'" : null %>><img src="<%= GetSecondaryPhotoUrl(2, 2) %>" style="display:inline-block;" /></td>
                                <% } %>
                                <% if (IsFifthPhotoBlockVisible) { %>
                                <td align="left" <%= ShouldColspan(3, 2) ? "colspan='2'" : null %>><img src="<%= GetSecondaryPhotoUrl(3, 2) %>" style="display:inline-block;" /></td>
                                <% } %>
                            </tr>
                        </table>
                    </td>
                </tr>
                <% } %>
                <% if (IsOpenHousesBlockVisible) { %>
                <tr>
                     <td>
								<table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center" style="font-size:36px; line-height:26px; letter-spacing:0.1px; font-weight:600; color:#000000; padding:63px 0 0; ">
                                    Open Houses
                                </td>
                             </tr>
                             <tr>
                                <td align="center" style="padding:22px 8% 0;">
									<%= Flyer.OpenHouses %>
                                </td>
                           </tr> 
                      	</table>
                    </td>
                </tr> 
                <% } %>
                <% if (IsDescriptionBlockVisible) { %>
                <tr>
                    <td style="height:194px; padding:59px 8% 0; text-align:center; width:100%; float:left; font-size:15px; line-height:26px; letter-spacing:0.6px;" align="center">
                    	<table cellpadding="0" cellspacing="0" width="84%">
                        <tr>
                        <td style="font-size:36px; line-height:26px; letter-spacing:0.1px; text-align:center; font-weight:600;">Property Description</td>
                        </tr>
                        <tr>
                        <td style="width:84%; padding:32px 0;"><%= Description %></td>
                        </tr>
                        </table>
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
                    <td background="<%= EmailImageUrl %>images/bf.png" bgcolor="#09509e" width="648" height="208" valign="top">
                          <!--[if gte mso 9]>
                          <v:rect xmlns:v="urn:schemas-microsoft-com:vml" fill="true" stroke="false" style="width:648px;height:208px;">
                            <v:fill type="tile" src="<%= EmailImageUrl %>images/bf.png" color="#09509e" />
                            <v:textbox inset="0,0,0,0">
                          <![endif]-->
                    	<table cellpadding="0" cellspacing="0" width="100%" style="font-size:14px; line-height:22px">
                        	<tr>
                            	<td style="padding:47px 0 41px 24px; color:#ffffff;" width="26%" align="right"><img src="<%= AvatarUrl %>"/></td>
                                <td style="padding:47px 0 41px 24px; color:#ffffff;" width="40%" >
                                	<strong style="font-size:20px; line-height:22px; font-weight:600;"><%= Flyer.Name %></strong><br>
                                    <%= BrokerageName %><br>
                                    Phone: <%= Phone %><br>
                                    <% if (MobilePhone.HasText()) { %>
                                    Mobile: <%= MobilePhone %><br>
                                    <% } %>
                                    <a href="mailto:<%= Flyer.Email %>" style="color:#09509e;"><%= Flyer.Email %></a>
                                </td>
                                <td  style="padding:47px 0 41px 24px; color:#ffffff;" width="34%" ><img src="<%= CompanyLogoUrl %>" /></td>
                            </tr>
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