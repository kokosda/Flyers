<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Receipt.aspx.cs" Inherits="FlyerMe.Flyer.MarkUp.Receipt" MasterPageFile="" Theme="" %>
<%@ Import Namespace="FlyerMe" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
    	<td align="center">
			<link href='https://fonts.googleapis.com/css?family=Lato:400,700,900&subset=latin,latin-ext' rel='stylesheet' type='text/css' />
        	<table width="648" cellpadding="0" cellspacing="0" border="0" style="font-family: 'Lato', sans-serif;">
            	<tr>
                	<td>
                        <div style="padding:26px 48px 12px 49px; overflow:hidden; line-height:18px; font-size:14px; letter-spacing:0.1px;">
                            <a href="<%= RootUrl %>" style="float:left;"><img src="<%= EmailImageUrl %>images/<%= clsUtility.ProjectName %>_biglogo.png" /></a>
                            <div style="float:right; ">
                                <br />
						        <a href="mailto:<%= clsUtility.ContactUsEmail %>"><%= clsUtility.ContactUsEmail %></a>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                	<td style="background:#3c3d41; color:#ffffff; text-align:center; height:48px;">
                    	    <span style="padding:0 24px;"><strong style="font-weight:700">Order Date:</strong> <%= OrderDate %></span>
                            <span style="padding:0 24px;"><strong style="font-weight:700">Transaction ID:</strong> <%= TransactionId %></span>
                    </td>
                </tr>
                <tr>
                	<td style="background:#f5f5f5; text-align:center; line-height:24px; padding:52px 52px 32px">
                		<div style="font-size:24px; font-weight:700;">Dear <%= CustomerName %></div>
                        <p style="padding:10px 0; letter-spacing:0.1px;">Thank you for your order at <%= clsUtility.SiteBrandName %>. Your transaction has been successfully completed. Please retain this receipt for your records. After receiving this invoice please watch for a customer copy to be arriving in your inbox. If you do not receive one, please look in your spam filter to make sure it has not been filtered there. If it has, please mark our domain name as a safe sender so all future flyers arrive directly into your inbox.<br>
                            <strong style="font-weight:700">Please click Flyer ID to view your flyer.</strong></p>
                        <p style="color:#f37b22;letter-spacing:0.1px;">Please note: You might receive more than one copy but the agents in your selected market area will just receive one copy.</p>
                    </td>
                </tr>
                <tr>
                	<td style="background:#f5f5f5; line-height:24px; padding:0px 52px 32px;">
                    <div style="border:1px solid #dce0e0; background:#ffffff;">
                    	<div style="padding:16px 32px; background:#edefed; border-bottom:1px solid #dce0e0; font-size:20px; font-weight:700;">Purchase Summary</div>
                        <div style="padding:40px 30px; font-size:14px;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <thead>
                                    <tr align="left">
                                        <th style="border-bottom:2px solid #dce0e0; font-size:12px;">FLYER ID</th>
                                        <th style="border-bottom:2px solid #dce0e0; font-size:12px;">FLYER TITLE</th>
                                        <th style="border-bottom:2px solid #dce0e0; font-size:12px;" align="right">TOTAL</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%  
                                    var orders = Orders;

                                    if (orders != null) {                                                     
                                        foreach (var order in Orders) { %>
                                    <tr>
                                        <td style="padding:23px 0;" width="27%">
                                            <a href="<%= RootUrl %>showflyer.aspx?oid=<%= order.order_id %>" target="_blank"><%= order.order_id %></a>
                                        </td>
                                        <td style="padding:23px 0; text-transform:uppercase; font-weight:700"><%= order.title.HasText() ? order.title : order.email_subject %></td>
                                        <td style="padding:23px 0;" align="right" width="13%"><%= order.tota_price.FormatPrice() %></td>
                                    </tr>
                                        <% }
                                    } %>
                                    <tr>
                                    	<td colspan="3" >
                                    		<table width="100%" style="border-top:2px solid #dce0e0;" align="left">
                                            	<tr>
                                                    <% if (!String.IsNullOrEmpty(TaxCost)) { %>
                                                	<td style="padding-top:16px;">Tax(<%= TaxRate %>): <strong><%= TaxCost %></strong></td>
                                                    <% } %>
                                                    <% if (!String.IsNullOrEmpty(Discount)) { %>
                                                    <td style="padding-top:16px;">Discount: <strong><%= Discount %></strong></td>
                                                    <% } %>
                                                    <td style="padding-top:16px;">Sub-total: <strong><%= SubTotal %></strong></td>
                                                    <td style="padding-top:16px;" align="right">Total Price: <strong><%= TotalPrice %></strong></td>
                                                </tr>
                                            </table>
                                    	</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        </div>
                        <p style="text-align:center; font-size:14px; color:#9b9b9b; padding:29px 0; letter-spacing:0.1px;">We are growing like crazy and we have you to thank! We have recently linked our<br /> website to Facebook and Twitter. Click here <a href="https://www.facebook.com/flyermeHQ" target="_blank"><img src="<%= EmailImageUrl %>images/fb_like.png" style="vertical-align:middle" /></a> to Like Us on Facebook.
                        </p>
                        <p style="text-align:center; font-size:14px; color:#9b9b9b; padding:10px 0;">We value your business and look forward <br />
                            to providing you with exellent service.<br>
                            <span style="padding:7px 0 0 0; display:block">Sincerely, <strong style="color:#000000;">The <%= clsUtility.ProjectName %> Team</strong></span>
                        </p>
                        <p style="text-align:center; font-size:20px; color:#f37b22">Thank you for your business!</p>
                    </td>
                </tr>
                <tr>
                	<td style="padding:26px 48px 12px 49px; overflow:hidden; line-height:18px; font-size:14px; letter-spacing:0.1px;">
                        <a href="<%= RootUrl %>" style="display:block; float:left"><img src="<%= EmailImageUrl %>images/<%= clsUtility.ProjectName %>_biglogo.png" /></a>
                        <div style="float:right; ">
                            <br />
						    <a href="mailto:<%= clsUtility.ContactUsEmail %>"><%= clsUtility.ContactUsEmail %></a>
                        </div>                      
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>