using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Reflection;

namespace FlyerMe
{
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// Layouts class - Handle all business operations pertaining to flyer laytouts
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [Serializable]
    public class Layouts
    {
        //string siteRoot = ConfigurationManager.AppSettings["SiteRoot"];
        string siteRoot = clsUtility.GetRootHost;
        string priceCaption = string.Empty;
        string priceAmount = string.Empty;

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Format property pricing based of sell or lease mode
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        private void SetPriceRentValues(string priceDelimitedSting)
        {
            priceCaption = string.Empty;
            priceAmount = string.Empty;

            if (priceDelimitedSting.Trim().Length > 0)
            {
                string[] priceBits = new string[4];
                priceBits = priceDelimitedSting.Split('|');
                priceAmount = String.Format("{0:c}", Convert.ToDouble(priceBits[1])) + priceBits[2] + priceBits[3];
                priceCaption = priceBits[0];
            }
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for layout link header
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string GetLinkHeader(string OrderID)
        {
            //--------- Prepare LINKS HEADER
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/LinksHeader.txt"), FileMode.Open));
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();
            sb.Replace("~@FLYER-ONLINE-LINK@~", clsUtility.SiteHttpWwwRootUrl + "/ShowFlyer.aspx?oid=" + OrderID);
            return sb.ToString();
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for layout link footer
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string GetLinkFooter(string subscriber_id, string subscriber_last_name, bool ForClient)
        {
            StreamReader sr = null;
            //----------- Prepare LINKS FOOTER
            if (ForClient)
            {
                sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/LinksFooter-Client.txt"), FileMode.Open));
            }
            else
            {
                sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/LinksFooter.txt"), FileMode.Open));
            }

            //populate stringbuilder with content from layout file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();
            string subsID = Convert.ToString(Convert.ToInt32(subscriber_id) + 55555) + "-" + subscriber_last_name; //id = subscriber_id + 55555 + " -" + lastname
            sb.Replace("~@UNSUBSCRIBE-LINK@~", clsUtility.SiteHttpWwwRootUrl + "/Unsubscribe.aspx?id=" + subsID);
            return sb.ToString();
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for layout forwarding message
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string GetForwadingMessage(string CustomerName, string CustomerMessage)
        {
            //read template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/ForwardingMessage.txt"), FileMode.Open));
            //populate stringbuilder with content from  file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            sb.Replace("~@CUSTOMER-NAME@~", CustomerName);
            if (CustomerMessage.Trim().Length > 0)
            {
                sb.Replace("~@CUSTOMER-MESSAGE@~", "<br/><strong>" + CustomerName + "'s Message:</strong>" + CustomerMessage);
            }
            else
            { sb.Replace("~@CUSTOMER-MESSAGE@~", string.Empty); }

            return sb.ToString();
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for sent message
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string GetSentMessage(string CustomerName, string CustomerMessage)
        {
            //read template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/SentMessage.txt"), FileMode.Open));
            //populate stringbuilder with content from  file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            sb.Replace("~@CUSTOMER-NAME@~", CustomerName);
            if (CustomerMessage.Trim().Length > 0)
            {
                sb.Replace("~@CUSTOMER-MESSAGE@~", "<br/><strong>" + CustomerName + "'s Message:</strong> " + CustomerMessage);
            }
            else
            { sb.Replace("~@CUSTOMER-MESSAGE@~", string.Empty); }

            return sb.ToString();
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup listing agent contact info
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string GetListingAgentInfo(ProfileCommon laProfile)
        {
            //read template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/ListingAgent.txt"), FileMode.Open));
            //populate stringbuilder with content from  file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //~@LISTING-AGENT@~
            if (laProfile.MiddleInitial.Trim().Length > 0)
            {
                sb.Replace("~@LISTING-AGENT@~", laProfile.FirstName + " " + laProfile.MiddleInitial + " " + laProfile.LastName);
            }
            else
            {
                sb.Replace("~@LISTING-AGENT@~", laProfile.FirstName + " " + laProfile.LastName);
            }
            //~@LISTING-OFFICE@~
            sb.Replace("~@LISTING-OFFICE@~", laProfile.Brokerage.BrokerageName);

            return sb.ToString();
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup buyer flyer footer
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string BuyerFooter(Order order)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<!-- Theme footer - Begin --><TABLE WIDTH='670' align='center' BORDER='0' CELLPADDING='0' CELLSPACING='0'><TR><TD>");
            switch (order.theme)
            {
                case "T01":
                    sb.Append("<img src='" + siteRoot + "Flyer/Layouts/buyer-footer-1.jpg' alt=''/>");
                    break;
                case "T02":
                    sb.Append("<img src='" + siteRoot + "Flyer/Layouts/buyer-footer-2.jpg' alt=''/>");
                    break;
                case "T03":
                    sb.Append("<img src='" + siteRoot + "Flyer/Layouts/buyer-footer-3.jpg' alt=''/>");
                    break;
                case "T04":
                    sb.Append("<img src='" + siteRoot + "Flyer/Layouts/buyer-footer-4.jpg' alt=''/>");
                    break;
                case "T05":
                    sb.Append("<img src='" + siteRoot + "Flyer/Layouts/buyer-footer-5.jpg' alt=''/>");
                    break;
                case "T06":
                    sb.Append("<img src='" + siteRoot + "Flyer/Layouts/buyer-footer-6.jpg' alt=''/>");
                    break;
            }
            sb.Append("</TD></TR></TABLE><!-- Theme footer - Eng -->");

            return sb.ToString();
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for buyer flyer B51 type
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string B51(Order order)
        {
            string headline = order.headline;
            string theme = order.theme;
            string layout = order.layout;

            string content = string.Empty;
            StringBuilder sbContent = new StringBuilder();

            if (layout == null || layout.Trim().Length < 1) { return string.Empty; } //exit if layout is empty

            //read layout template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/" + layout + ".txt"), FileMode.Open));
            //populate stringbuilder with content from layout file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (!(sb.Length > 0)) { return string.Empty; }


            //THEME HEADER / THEME FOOTER
            switch (order.theme)
            {
                case "T01":
                    sb.Replace("~@THEME-HEADER@~", "<img src='" + siteRoot + "Flyer/Layouts/buyer-header-1.jpg' alt=''/>");
                    break;
                case "T02":
                    sb.Replace("~@THEME-HEADER@~", "<img src='" + siteRoot + "Flyer/Layouts/buyer-header-2.jpg' alt=''/>");
                    break;
                case "T03":
                    sb.Replace("~@THEME-HEADER@~", "<img src='" + siteRoot + "Flyer/Layouts/buyer-header-3.jpg' alt=''/>");
                    break;
                case "T04":
                    sb.Replace("~@THEME-HEADER@~", "<img src='" + siteRoot + "Flyer/Layouts/buyer-header-4.jpg' alt=''/>");
                    break;
                case "T05":
                    sb.Replace("~@THEME-HEADER@~", "<img src='" + siteRoot + "Flyer/Layouts/buyer-header-5.jpg' alt=''/>");
                    break;
                case "T06":
                    sb.Replace("~@THEME-HEADER@~", "<img src='" + siteRoot + "Flyer/Layouts/buyer-header-6.jpg' alt=''/>");
                    break;
            }

            //~@FLYER-TITLE@~
            sb.Replace("~@FLYER-TITLE@~", order.title);

            //~@FLYER-SUB-TITLE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.sub_title.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td align='center' style='color: #CC9900; font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold; font-size: 12px;'>");
                sbContent.Append(order.sub_title + "</td>");
                sbContent.Append("</tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='10' alt=''></td></tr></table>");
            }
            sb.Replace("~@FLYER-SUB-TITLE@~", sbContent.ToString());



            //~@AGENT-MESSAGE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.buyer_message.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td align='center' style='padding-right: 5px;font-size: 12px;font-weight: bold;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                sbContent.Append(order.buyer_message + "</td>");
                sbContent.Append("</tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='10' alt=''></td></tr></table>");
            }
            sb.Replace("~@AGENT-MESSAGE@~", sbContent.ToString());

            #region >>> Custom Fields.....

            sbContent.Remove(0, sbContent.Length);
            //Check if there is any custom field value to show the custom fields section
            if (order.custom_field_value1.Trim().Length > 0 || order.custom_field_value2.Trim().Length > 0 || order.custom_field_value3.Trim().Length > 0 || order.custom_field_value4.Trim().Length > 0 || order.custom_field_value5.Trim().Length > 0 || order.custom_field_value6.Trim().Length > 0 || order.custom_field_value7.Trim().Length > 0 || order.custom_field_value8.Trim().Length > 0)
            {
                sbContent.Append("<!-- Flyer Custom Fields --><table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0>");
                sbContent.Append("<tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>DESIRED PROPERTY FEATURES:</td>");
                sbContent.Append("</tr><tr><td align='left' valign='top'><!-- Custom Rows 1 3 5 7 --><table BORDER=0 CELLPADDING=0 CELLSPACING=0>");
                sbContent.Append("<tr><td><img src='" + siteRoot + "images/space.gif' width='130' height='4' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='197' height='4' alt=''></td></tr>");
                if (order.custom_field_value1.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw3.gif' alt=''>" + order.custom_field1 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value1 + "</td></tr>"); }
                if (order.custom_field_value3.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw3.gif' alt=''>" + order.custom_field3 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value3 + "</td></tr>"); }
                if (order.custom_field_value5.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw3.gif' alt=''>" + order.custom_field5 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value5 + "</td></tr>"); }
                if (order.custom_field_value7.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw3.gif' alt=''>" + order.custom_field7 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value7 + "</td></tr>"); }
                sbContent.Append("</table></td><td><!-- Custom Rows 2 4 6 8 -->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='130' height='4' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='197' height='4' alt=''></td></tr>");
                if (order.custom_field_value2.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw3.gif' alt=''>" + order.custom_field2 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value2 + "</td></tr>"); }
                if (order.custom_field_value4.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw3.gif' alt=''>" + order.custom_field4 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value4 + "</td></tr>"); }
                if (order.custom_field_value6.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw3.gif' alt=''>" + order.custom_field6 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value6 + "</td></tr>"); }
                if (order.custom_field_value8.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw3.gif' alt=''>" + order.custom_field8 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value8 + "</td></tr>"); }
                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            //sb.Replace("~@DESIRED-FEATURES@~", sbContent.ToString());

            #endregion

            #region >>> Property Features(Custom Fields.....)

            //sbContent.Remove(0, sbContent.Length);

            //Check if there is any custom field value to show the custom fields section
            if ((order.Bedrooms.Trim().Length > 0 && order.Bedrooms.Trim() != "0") || (order.Subdivision.Trim().Length > 0 && order.Subdivision.Trim() != "0") || (order.FullBaths.Trim().Length > 0 && order.FullBaths.Trim() != "0") || (order.YearBuilt.Trim().Length > 0 && order.YearBuilt.Trim() != "0") || (order.HalfBaths.Trim().Length > 0 && order.HalfBaths.Trim() != "0") || (order.HOA.Trim().Length > 0 && order.HOA.Trim() != "0") || (order.LotSize.Trim().Length > 0 && order.LotSize.Trim() != "0") || (order.Parking.Trim().Length > 0 && order.Parking.Trim() != "0") || (order.SqFoots.Trim().Length > 0 && order.SqFoots.Trim() != "0") || (order.Floors.Trim().Length > 0 && order.Floors.Trim() != "0"))
            {
                sbContent.Append("<!-- Flyer Custom Fields-->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table><table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
                sbContent.Append("DESIRED PROPERTY FEATURES:</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></td></tr>");

                string[] PropFeatures ={ "Bedrooms", "Subdivision", "Full Baths", "Year Built", "Half Baths", "HOA", "Lot Size", "Parking", "Sq. Foots", "Floors" };
                string[] PropFeaturesValues ={ order.Bedrooms, order.Subdivision, order.FullBaths, order.YearBuilt, order.HalfBaths, order.HOA, order.LotSize, order.Parking, order.SqFoots, order.Floors };

                int tdCount = 0;

                for (int i = 0; i <= PropFeaturesValues.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                    }

                    if (PropFeaturesValues[i].Trim() != "" && PropFeaturesValues[i].Trim() != "0")
                    {
                        sbContent.Append("<td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + PropFeatures[i] + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + PropFeaturesValues[i] + "</td>");
                        tdCount = tdCount + 1;
                    }

                    if (tdCount == 2)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }
                }

                if (tdCount == 0)
                {
                    sbContent.Append("<tr>");
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@DESIRED-FEATURES@~", sbContent.ToString());

            #endregion


            //~@MORE-INFO@~
            sbContent.Remove(0, sbContent.Length);
            if (order.more_info.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 bgcolor='' CELLPADDING=0 CELLSPACING=0>");
                sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>MORE PROPERTY INFO / AMENITIES:</td>");
                sbContent.Append("</tr><tr><td style='padding-right: 5px;text-align:justify;font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                sbContent.Append(order.more_info);
                sbContent.Append("</td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@MORE-INFO@~", sbContent.ToString());

            //~@LOCATION@~
            sbContent.Remove(0, sbContent.Length);
            if (order.location.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 bgcolor='' CELLPADDING=0 CELLSPACING=0>");
                sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>DESIRED PROPERTY LOCATION:</td>");
                sbContent.Append("</tr><tr><td style='padding-right: 5px;text-align:justify;font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                sbContent.Append(order.location);
                sbContent.Append("</td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@LOCATION@~", sbContent.ToString());

            #region >>> Bottom Links....

            sbContent.Remove(0, sbContent.Length);

            if (order.order_id != 0)
            {
                sb.Replace("~@PDF-LINK@~", siteRoot + "ShowPDF.aspx?oid=" + order.order_id);
            }
            else
            {
                sb.Replace("~@PDF-LINK@~", "#");
            }

            #endregion

            //return B51 content
            return sb.ToString();

        } //END: B51

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for seller flyer type L01
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string L01(Order order, bool RenderBottomLinks)
        {
            string headline = order.headline;
            string theme = order.theme;
            string layout = order.layout;

            string content = string.Empty;
            StringBuilder sbContent = new StringBuilder();

            Helper helper = new Helper();

            if (layout == null || layout.Trim().Length < 1) { return string.Empty; } //exit if layout is empty

            //read layout template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/" + layout + ".txt"), FileMode.Open));
            //populate stringbuilder with content from layout file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (!(sb.Length > 0)) { return string.Empty; }

            //get property images 
            sb.Replace("~@PROPERTY-PHOTO-2@~", helper.GetPropImageNamePath(order.order_id.ToString(), 2));
            sb.Replace("~@PROPERTY-PHOTO-3@~", helper.GetPropImageNamePath(order.order_id.ToString(), 3));
            sb.Replace("~@PROPERTY-PHOTO-4@~", helper.GetPropImageNamePath(order.order_id.ToString(), 4));
            sb.Replace("~@PROPERTY-PHOTO-5@~", helper.GetPropImageNamePath(order.order_id.ToString(), 5));
            sb.Replace("~@PROPERTY-PHOTO-6@~", helper.GetPropImageNamePath(order.order_id.ToString(), 6));
            sb.Replace("~@PROPERTY-PHOTO-7@~", helper.GetPropImageNamePath(order.order_id.ToString(), 7));


            //~@FLYER-TITLE@~
            sb.Replace("~@FLYER-TITLE@~", order.title);

            //~@FLYER-SUB-TITLE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.sub_title.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td align='center' style='color: #CC9900; font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold; font-size: 12px;'>");
                sbContent.Append(order.sub_title + "</td>");
                sbContent.Append("</tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='10' alt=''></td></tr></table>");
            }
            sb.Replace("~@FLYER-SUB-TITLE@~", sbContent.ToString());



            //~@PROPERTY-ADDRESS@~
            sbContent.Remove(0, sbContent.Length);
            if (order.prop_address1.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td align='center' style='font-size: 12px;font-weight: bold;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                if (order.prop_address2.Trim().Length > 0)
                {
                    sbContent.Append(order.prop_address1 + ", " + order.prop_address2 + ", " + order.prop_city + " " + order.prop_state + " " + order.prop_zipcode + "</td>");
                }
                else
                {
                    sbContent.Append(order.prop_address1 + ", " + order.prop_city + " " + order.prop_state + " " + order.prop_zipcode + "</td>");
                }
                sbContent.Append("</tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='10' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROPERTY-ADDRESS@~", sbContent.ToString());

            //~@MLS-PRICE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.mls_number.Trim().Length > 0 || order.prop_price.ToString().Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td align='center'>");
                if (order.mls_number.Trim().Length > 0)
                {
                    sbContent.Append("<span style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>MLS:</span>");
                    sbContent.Append("<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.mls_number + "</span>");
                }
                if (order.mls_number.Trim().Length > 0 && order.prop_price.ToString().Trim().Length > 0)
                {
                    sbContent.Append("<img src='" + siteRoot + "images/space.gif' width='20' height='10' alt=''>");
                }
                if (order.prop_price.ToString().Trim().Length > 0)
                {
                    SetPriceRentValues(order.prop_price.ToString());
                    sbContent.Append("<span style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>" + priceCaption + ":</span>");
                    sbContent.Append("<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + priceAmount + "</span>");
                }
                sbContent.Append("</td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@MLS-PRICE@~", sbContent.ToString());


            #region >>> Bullets.....

            sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the bullets section
            //if (order.bullet1.Trim().Length > 0 || order.bullet2.Trim().Length > 0 || order.bullet3.Trim().Length > 0 || order.bullet4.Trim().Length > 0 || order.bullet5.Trim().Length > 0 || order.bullet6.Trim().Length > 0 || order.bullet7.Trim().Length > 0 || order.bullet8.Trim().Length > 0)
            //{
            //    sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
            //    sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
            //    sbContent.Append("PROPERTY HIGHLIGHTS:</td></tr><tr><td align='left' valign='top'><!-- Highlight 1 3 5 7 9 --><table border='0' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='225' height='1' alt=''></td></tr>");
            //    if (order.bullet1.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet1 + "</td></tr>"); }
            //    if (order.bullet3.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet3 + "</td></tr>"); }
            //    if (order.bullet5.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet5 + "</td></tr>"); }
            //    if (order.bullet7.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet7 + "</td></tr>"); }
            //    //if (order.bullet9.Trim().Length > 0){sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet9 + "</td></tr>");}
            //    sbContent.Append("</table></td><td align='left' valign='top'><!-- Highlight 2 4 6 8 10 --><table border='0' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='225' height='1' alt=''></td></tr>");
            //    if (order.bullet2.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet2 + "</td></tr>"); }
            //    if (order.bullet4.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet4 + "</td></tr>"); }
            //    if (order.bullet6.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet6 + "</td></tr>"); }
            //    if (order.bullet8.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet8 + "</td></tr>"); }
            //    //if (order.bullet10.Trim().Length > 0){sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet10 + "</td></tr>");}
            //    sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            //}
            //sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion



            #region >>> CheckBox Features.....

           // sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the property features section
            if (order.PropertyFeaturesValues.Contains("True"))
            {
                sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
                sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
                sbContent.Append("PROPERTY FEATURES:</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] strPropertyFeatures = order.PropertyFeatures.ToString().Split(':');
                string[] strPropertyFeaturesValues = order.PropertyFeaturesValues.ToString().Split(':');

                int tdCount=0;
                for (int i = 0; i <= strPropertyFeaturesValues.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }
                    if (strPropertyFeaturesValues[i] == "True")
                    {
                        sbContent.Append("<td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + strPropertyFeatures[i] + "</td>");
                        tdCount = tdCount + 1;
                    }
                    if (tdCount == 4)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }

                }
                if (tdCount > 0)
                {
                    sbContent.Append("</tr>");
                    tdCount = 0;
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            //sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion



            #region >>> Other Features.....

            // sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the property features section
            if (order.OtherPropertyFeatures.Length > 1)
            {
                sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
                sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;padding-bottom:2px;'>");
                sbContent.Append("OTHER FEATURES:</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] strPropertyOtherFeatures = order.OtherPropertyFeatures.ToString().Split('^');
                string strPropertyFeaturesValues = strPropertyOtherFeatures[0];

                strPropertyOtherFeatures = strPropertyFeaturesValues.Split('|');


                int tdCount = 0;
                for (int i = 0; i <= strPropertyOtherFeatures.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }

                    sbContent.Append("<td valign='top' style='padding-top:4px;'><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;vertical-align:top;'>" + strPropertyOtherFeatures[i] + "</td>");
                    tdCount = tdCount + 1;

                    if (tdCount == 4)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }

                }
                if (tdCount > 0)
                {
                    sbContent.Append("</tr>");
                    tdCount = 0;
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion
            
            
            
            
            //~@PROP-DESC@~
            sbContent.Remove(0, sbContent.Length);
            if (order.prop_desc.Trim().Length > 0)
            {
                sbContent.Append("<!-- Flyer Description --><table width='100%' BORDER=0 bgcolor='' CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>PROPERTY DESCRIPTION:</td>");
                sbContent.Append("</tr><tr><td style='padding-right: 5px;text-align:justify;font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                sbContent.Append(order.prop_desc);
                sbContent.Append("</td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROP-DESC@~", sbContent.ToString());

            #region >>> Custom Fields.....

            sbContent.Remove(0, sbContent.Length);

            //Check if there is any custom field value to show the custom fields section
            if ((order.custom_field_value1.Trim().Length > 0 || order.custom_field_value2.Trim().Length > 0 || order.custom_field_value3.Trim().Length > 0 || order.custom_field_value4.Trim().Length > 0 || order.custom_field_value5.Trim().Length > 0 || order.custom_field_value6.Trim().Length > 0 || order.custom_field_value7.Trim().Length > 0 || order.custom_field_value8.Trim().Length > 0) && order.Bedrooms.Trim()=="")
            {
                sbContent.Append("<!-- Flyer Custom Fields --><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0>");
                sbContent.Append("<tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'></td></tr><tr><td align='left' valign='top'>");
                sbContent.Append("<!-- Custom Rows 1 3 5 7 -->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='105' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='144' height='1' alt=''></td></tr>");
                if (order.custom_field_value1.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field1) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value1 + "</td></tr>"); }
                if (order.custom_field_value3.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field3) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value3 + "</td></tr>"); }
                if (order.custom_field_value5.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field5) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value5 + "</td></tr>"); }
                if (order.custom_field_value7.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field7) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value7 + "</td></tr>"); }
                //if (order.custom_field_value9.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + order.custom_field9 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value9 + "</td></tr>"); }
                sbContent.Append("</table></td><td><!-- Custom Rows 2 4 6 8 -->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='105' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='142' height='1' alt=''></td></tr>");
                if (order.custom_field_value2.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field2) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value2 + "</td></tr>"); }
                if (order.custom_field_value4.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field4) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value4 + "</td></tr>"); }
                if (order.custom_field_value6.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field6) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value6 + "</td></tr>"); }
                if (order.custom_field_value8.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field8) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value8 + "</td></tr>"); }
                //if (order.custom_field_value10.Trim().Length > 0) {sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + order.custom_field10 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value10 + "</td></tr>"); }
                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            //sb.Replace("~@CUST-FIELDS@~", sbContent.ToString());

            #endregion

            #region >>> Property Features(Custom Fields.....)

            //sbContent.Remove(0, sbContent.Length);

            //Check if there is any custom field value to show the custom fields section
            if ((order.Bedrooms.Trim().Length > 0 && order.Bedrooms.Trim() != "0") || (order.Subdivision.Trim().Length > 0 && order.Subdivision.Trim() != "0") || (order.FullBaths.Trim().Length > 0 && order.FullBaths.Trim() != "0") || (order.YearBuilt.Trim().Length > 0 && order.YearBuilt.Trim() != "0") || (order.HalfBaths.Trim().Length > 0 && order.HalfBaths.Trim() != "0") || (order.HOA.Trim().Length > 0 && order.HOA.Trim() != "0") || (order.LotSize.Trim().Length > 0 && order.LotSize.Trim() !="0") || (order.Parking.Trim().Length > 0 && order.Parking.Trim() !="0") || (order.SqFoots.Trim().Length > 0 && order.SqFoots.Trim() !="0") || (order.Floors.Trim().Length > 0 && order.Floors.Trim()!="0"))
            {
                sbContent.Append("<!-- Flyer Custom Fields-->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table><table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'></td></tr>");
                sbContent.Append("<tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] PropFeatures ={ "Bedrooms", "Subdivision", "Full Baths", "Year Built", "Half Baths", "HOA", "Lot Size", "Parking", "Sqft", "Floors" };
                string[] PropFeaturesValues ={ order.Bedrooms, order.Subdivision, order.FullBaths, order.YearBuilt, order.HalfBaths, order.HOA, order.LotSize, order.Parking, order.SqFoots, order.Floors };

                int tdCount = 0;

                for (int i = 0; i <= PropFeaturesValues.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }

                    if (PropFeaturesValues[i].Trim() != "" && PropFeaturesValues[i].Trim() !="0")
                    {
                        sbContent.Append("<td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + PropFeatures[i] +":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + PropFeaturesValues[i] + "</td>");
                        tdCount = tdCount + 1;
                    }

                    if (tdCount == 3)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }
                }

                if (tdCount == 1)
                {
                    sbContent.Append("</tr>");
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@CUST-FIELDS@~", sbContent.ToString());

            #endregion


            #region >>> Bottom Links....

            sbContent.Remove(0, sbContent.Length);
            if (RenderBottomLinks)
            {
                sbContent.Append("<!-- Links -->");
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                //MLS Link
                if (order.mls_link.Trim().Length > 0) { sbContent.Append("<td align='center'><a href='" + order.mls_link + "' id='aMLSLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color:#ff6633;text-decoration: underline;'>FIND MORE</a></td>"); }

                //Map Link
                if (order.map_link.Trim().Length > 0)
                {
                    if (order.map_link.IndexOf("FlyerMap.aspx?") != -1)
                    {
                        sbContent.Append("<td align='center'><a href='" + siteRoot + order.map_link + "' id='aMapLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW MAP</a></td>");
                    }
                    else
                    {
                        sbContent.Append("<td align='center'><a href='" + order.map_link + "' id='aMapLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW MAP</a></td>");
                    }
                }

                // PDF link
                if (order.order_id != 0)
                {
                    sbContent.Append("<td align='center'><a id='aPDFLink' href='" + siteRoot + "ShowPDF.aspx?oid=" + order.order_id + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW PDF</a></td>");
                }
                else
                {
                    sbContent.Append("<td align='center'><a id='aPDFLink' href='#' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW PDF</a></td>");
                }


                // Foward to clients link
                if (order.order_id != 0)
                {
                    sbContent.Append("<td align='center'><a id='aFTCLink' href='" + siteRoot + "ForwardToClients.aspx?oid=" + order.order_id + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>SEND TO CLIENTS</a></td>");
                }
                else
                {
                    sbContent.Append("<td align='center'><a id='aFTCLink' href='#' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>SEND TO CLIENTS</a></td>");
                }

                //Virtual Tour Link
                if (order.virtualtour_link.Trim().Length > 0) { sbContent.Append("<td align='center'><a href='" + order.virtualtour_link + "' id='aVTLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIRTUAL TOUR</a></td>"); }
                sbContent.Append("</tr></table>");
            }
            sb.Replace("~@LINKS@~", sbContent.ToString());
            #endregion

            //return L01 content
            return sb.ToString();

        } //END: L001

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for seller flyer type L02
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string L02(Order order, bool RenderBottomLinks)
        {
            string headline = order.headline;
            string theme = order.theme;
            string layout = order.layout;

            string content = string.Empty;
            StringBuilder sbContent = new StringBuilder();

            Helper helper = new Helper();

            if (layout == null || layout.Trim().Length < 1) { return string.Empty; } //exit if layout is empty

            //read layout template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/" + layout + ".txt"), FileMode.Open));
            //populate stringbuilder with content from layout file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (!(sb.Length > 0)) { return string.Empty; }

            //get property images 
            sb.Replace("~@PROPERTY-PHOTO-2@~", helper.GetPropImageNamePath(order.order_id.ToString(), 2));
            sb.Replace("~@PROPERTY-PHOTO-3@~", helper.GetPropImageNamePath(order.order_id.ToString(), 3));
            sb.Replace("~@PROPERTY-PHOTO-4@~", helper.GetPropImageNamePath(order.order_id.ToString(), 4));
            sb.Replace("~@PROPERTY-PHOTO-5@~", helper.GetPropImageNamePath(order.order_id.ToString(), 5));
            sb.Replace("~@PROPERTY-PHOTO-6@~", helper.GetPropImageNamePath(order.order_id.ToString(), 6));
            sb.Replace("~@PROPERTY-PHOTO-7@~", helper.GetPropImageNamePath(order.order_id.ToString(), 7));

            //~@FLYER-TITLE@~
            sb.Replace("~@FLYER-TITLE@~", order.title);

            //~@FLYER-SUB-TITLE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.sub_title.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td align='center' style='color: #CC9900; font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold; font-size: 12px;'>");
                sbContent.Append(order.sub_title + "</td>");
                sbContent.Append("</tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='10' alt=''></td></tr></table>");
            }
            sb.Replace("~@FLYER-SUB-TITLE@~", sbContent.ToString());

            //~@PROPERTY-ADDRESS@~
            sbContent.Remove(0, sbContent.Length);
            if (order.prop_address1.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td align='center' style='font-size: 12px;font-weight: bold;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                if (order.prop_address2.Trim().Length > 0)
                {
                    sbContent.Append(order.prop_address1 + ", " + order.prop_address2 + ", " + order.prop_city + " " + order.prop_state + " " + order.prop_zipcode + "</td>");
                }
                else
                {
                    sbContent.Append(order.prop_address1 + ", " + order.prop_city + " " + order.prop_state + " " + order.prop_zipcode + "</td>");
                }
                sbContent.Append("</tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='10' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROPERTY-ADDRESS@~", sbContent.ToString());

            //~@MLS-PRICE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.mls_number.Trim().Length > 0 || order.prop_price.ToString().Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td align='center'>");
                if (order.mls_number.Trim().Length > 0)
                {
                    sbContent.Append("<span style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>MLS:</span>");
                    sbContent.Append("<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.mls_number + "</span>");
                }
                if (order.mls_number.Trim().Length > 0 && order.prop_price.ToString().Trim().Length > 0)
                {
                    sbContent.Append("<img src='" + siteRoot + "images/space.gif' width='20' height='10' alt=''>");
                }
                if (order.prop_price.ToString().Trim().Length > 0)
                {
                    SetPriceRentValues(order.prop_price.ToString());
                    sbContent.Append("<span style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>" + priceCaption + ":</span>");
                    sbContent.Append("<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + priceAmount + "</span>");
                }
                sbContent.Append("</td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@MLS-PRICE@~", sbContent.ToString());


            #region >>> Bullets.....

            sbContent.Remove(0, sbContent.Length);

            ////Check if there is any bullet available to show the bullets section
            //if (order.bullet1.Trim().Length > 0 || order.bullet2.Trim().Length > 0 || order.bullet3.Trim().Length > 0 || order.bullet4.Trim().Length > 0 || order.bullet5.Trim().Length > 0 || order.bullet6.Trim().Length > 0 || order.bullet7.Trim().Length > 0 || order.bullet8.Trim().Length > 0)
            //{
            //    sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
            //    sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
            //    sbContent.Append("PROPERTY HIGHLIGHTS:</td></tr><tr><td align='left' valign='top'><!-- Highlight 1 3 5 7 9 --><table border='0' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='225' height='1' alt=''></td></tr>");
            //    if (order.bullet1.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet1 + "</td></tr>"); }
            //    if (order.bullet3.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet3 + "</td></tr>"); }
            //    if (order.bullet5.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet5 + "</td></tr>"); }
            //    if (order.bullet7.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet7 + "</td></tr>"); }
            //    //if (order.bullet9.Trim().Length > 0){sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet9 + "</td></tr>");}
            //    sbContent.Append("</table></td><td align='left' valign='top'><!-- Highlight 2 4 6 8 10 --><table border='0' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='225' height='1' alt=''></td></tr>");
            //    if (order.bullet2.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet2 + "</td></tr>"); }
            //    if (order.bullet4.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet4 + "</td></tr>"); }
            //    if (order.bullet6.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet6 + "</td></tr>"); }
            //    if (order.bullet8.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet8 + "</td></tr>"); }
            //    //if (order.bullet10.Trim().Length > 0){sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet10 + "</td></tr>");}
            //    sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            //}
            ////sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion


            #region >>> CheckBox Features.....

            // sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the property features section
            if (order.PropertyFeaturesValues.Contains("True"))
            {
                sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
                sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
                sbContent.Append("PROPERTY FEATURES:</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] strPropertyFeatures = order.PropertyFeatures.ToString().Split(':');
                string[] strPropertyFeaturesValues = order.PropertyFeaturesValues.ToString().Split(':');

                int tdCount = 0;
                for (int i = 0; i <= strPropertyFeaturesValues.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }
                    if (strPropertyFeaturesValues[i] == "True")
                    {
                        sbContent.Append("<td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + strPropertyFeatures[i] + "</td>");
                        tdCount = tdCount + 1;
                    }
                    if (tdCount == 4)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }

                }
                if (tdCount > 0)
                {
                    sbContent.Append("</tr>");
                    tdCount = 0;
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            //sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion


            #region >>> Other Features.....

            // sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the property features section
            if (order.OtherPropertyFeatures.Length > 1)
            {
                sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
                sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;padding-bottom:2px;'>");
                sbContent.Append("OTHER FEATURES:</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] strPropertyOtherFeatures = order.OtherPropertyFeatures.ToString().Split('^');
                string strPropertyFeaturesValues = strPropertyOtherFeatures[0];

                strPropertyOtherFeatures = strPropertyFeaturesValues.Split('|');


                int tdCount = 0;
                for (int i = 0; i <= strPropertyOtherFeatures.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }

                    sbContent.Append("<td valign='top' style='padding-top:4px;'><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;vertical-align:top;'>" + strPropertyOtherFeatures[i] + "</td>");
                    tdCount = tdCount + 1;

                    if (tdCount == 4)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }

                }
                if (tdCount > 0)
                {
                    sbContent.Append("</tr>");
                    tdCount = 0;
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion
            

            //~@PROP-DESC@~
            sbContent.Remove(0, sbContent.Length);
            if (order.prop_desc.Trim().Length > 0)
            {
                sbContent.Append("<!-- Flyer Description --><table width='100%' BORDER=0 bgcolor='' CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>PROPERTY DESCRIPTION:</td>");
                sbContent.Append("</tr><tr><td style='padding-right: 5px;text-align:justify;font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                sbContent.Append(order.prop_desc);
                sbContent.Append("</td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROP-DESC@~", sbContent.ToString());

            #region >>> Custom Fields.....

            sbContent.Remove(0, sbContent.Length);

            //Check if there is any custom field value to show the custom fields section
            if ((order.custom_field_value1.Trim().Length > 0 || order.custom_field_value2.Trim().Length > 0 || order.custom_field_value3.Trim().Length > 0 || order.custom_field_value4.Trim().Length > 0 || order.custom_field_value5.Trim().Length > 0 || order.custom_field_value6.Trim().Length > 0 || order.custom_field_value7.Trim().Length > 0 || order.custom_field_value8.Trim().Length > 0) && order.Bedrooms.Trim() == "")
            {
                sbContent.Append("<!-- Flyer Custom Fields --><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0>");
                sbContent.Append("<tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'></td></tr><tr><td align='left' valign='top'>");
                sbContent.Append("<!-- Custom Rows 1 3 5 7 -->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='105' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='144' height='1' alt=''></td></tr>");
                if (order.custom_field_value1.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field1) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value1 + "</td></tr>"); }
                if (order.custom_field_value3.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field3) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value3 + "</td></tr>"); }
                if (order.custom_field_value5.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field5) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value5 + "</td></tr>"); }
                if (order.custom_field_value7.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field7) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value7 + "</td></tr>"); }
                //if (order.custom_field_value9.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + order.custom_field9 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value9 + "</td></tr>"); }
                sbContent.Append("</table></td><td><!-- Custom Rows 2 4 6 8 -->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='105' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='142' height='1' alt=''></td></tr>");
                if (order.custom_field_value2.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field2) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value2 + "</td></tr>"); }
                if (order.custom_field_value4.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field4) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value4 + "</td></tr>"); }
                if (order.custom_field_value6.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field6) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value6 + "</td></tr>"); }
                if (order.custom_field_value8.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field8) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value8 + "</td></tr>"); }
                //if (order.custom_field_value10.Trim().Length > 0) {sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + order.custom_field10 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value10 + "</td></tr>"); }
                sbContent.Append("</table></td></tr></table>");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='15' alt=''></td></tr></table>");
            }
            //sb.Replace("~@CUST-FIELDS@~", sbContent.ToString());

            #endregion

            #region >>> Property Features(Custom Fields.....)

            //sbContent.Remove(0, sbContent.Length);

            //Check if there is any custom field value to show the custom fields section
            if ((order.Bedrooms.Trim().Length > 0 && order.Bedrooms.Trim() != "0") || (order.Subdivision.Trim().Length > 0 && order.Subdivision.Trim() != "0") || (order.FullBaths.Trim().Length > 0 && order.FullBaths.Trim() != "0") || (order.YearBuilt.Trim().Length > 0 && order.YearBuilt.Trim() != "0") || (order.HalfBaths.Trim().Length > 0 && order.HalfBaths.Trim() != "0") || (order.HOA.Trim().Length > 0 && order.HOA.Trim() != "0") || (order.LotSize.Trim().Length > 0 && order.LotSize.Trim() != "0") || (order.Parking.Trim().Length > 0 && order.Parking.Trim() != "0") || (order.SqFoots.Trim().Length > 0 && order.SqFoots.Trim() != "0") || (order.Floors.Trim().Length > 0 && order.Floors.Trim() != "0"))
            {
                sbContent.Append("<!-- Flyer Custom Fields-->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table><table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
                sbContent.Append("</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] PropFeatures ={ "Bedrooms", "Subdivision", "Full Baths", "Year Built", "Half Baths", "HOA", "Lot Size", "Parking", "Sqft", "Floors" };
                string[] PropFeaturesValues ={ order.Bedrooms, order.Subdivision, order.FullBaths, order.YearBuilt, order.HalfBaths, order.HOA, order.LotSize, order.Parking, order.SqFoots, order.Floors };

                int tdCount = 0;

                for (int i = 0; i <= PropFeaturesValues.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }

                    if (PropFeaturesValues[i].Trim() != "" && PropFeaturesValues[i].Trim() !="0")
                    {
                        sbContent.Append("<td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + PropFeatures[i] + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + PropFeaturesValues[i] + "</td>");
                        tdCount = tdCount + 1;
                    }

                    if (tdCount == 3)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }
                }

                if (tdCount > 0)
                {
                    sbContent.Append("<tr>");
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@CUST-FIELDS@~", sbContent.ToString());

            #endregion


            #region >>> Bottom Links....

            sbContent.Remove(0, sbContent.Length);
            if (RenderBottomLinks)
            {
                sbContent.Append("<!-- Links -->");
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                //MLS Link
                if (order.mls_link.Trim().Length > 0) { sbContent.Append("<td align='center'><a href='" + order.mls_link + "' id='aMLSLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color:#ff6633;text-decoration: underline;'>FIND MORE</a></td>"); }

                //Map Link
                if (order.map_link.Trim().Length > 0)
                {
                    if (order.map_link.IndexOf("FlyerMap.aspx?") != -1)
                    {
                        sbContent.Append("<td align='center'><a href='" + siteRoot + order.map_link + "' id='aMapLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW MAP</a></td>");
                    }
                    else
                    {
                        sbContent.Append("<td align='center'><a href='" + order.map_link + "' id='aMapLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW MAP</a></td>");
                    }
                }

                // PDF link
                if (order.order_id != 0)
                {
                    sbContent.Append("<td align='center'><a id='aPDFLink' href='" + siteRoot + "ShowPDF.aspx?oid=" + order.order_id + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW PDF</a></td>");
                }
                else
                {
                    sbContent.Append("<td align='center'><a id='aPDFLink' href='#' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW PDF</a></td>");
                }


                // Foward to clients link
                if (order.order_id != 0)
                {
                    sbContent.Append("<td align='center'><a id='aFTCLink' href='" + siteRoot + "ForwardToClients.aspx?oid=" + order.order_id + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>SEND TO CLIENTS</a></td>");
                }
                else
                {
                    sbContent.Append("<td align='center'><a id='aFTCLink' href='#' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>SEND TO CLIENTS</a></td>");
                }
                //Virtual Tour Link
                if (order.virtualtour_link.Trim().Length > 0) { sbContent.Append("<td align='center'><a href='" + order.virtualtour_link + "' id='aVTLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIRTUAL TOUR</a></td>"); }
                sbContent.Append("</tr></table>");

            }
            sb.Replace("~@LINKS@~", sbContent.ToString());
            #endregion

            //return L01 content
            return sb.ToString();

        } //END: L001

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for seller flyer type L03
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string L03(Order order, bool RenderBottomLinks)
        {
            return L01(order, RenderBottomLinks);
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for seller flyer type L04
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string L04(Order order, bool RenderBottomLinks)
        {
            return L02(order, RenderBottomLinks);
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for seller flyer type L05
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string L05(Order order, bool RenderBottomLinks)
        {
            return L01(order, RenderBottomLinks);
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for seller flyer type L06
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string L06(Order order, bool RenderBottomLinks)
        {
            return L01(order, RenderBottomLinks);
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for seller flyer type L07
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string L07(Order order, bool RenderBottomLinks)
        {
            string headline = order.headline;
            string theme = order.theme;
            string layout = order.layout;

            string content = string.Empty;
            StringBuilder sbContent = new StringBuilder();

            Helper helper = new Helper();

            if (layout == null || layout.Trim().Length < 1) { return string.Empty; } //exit if layout is empty

            //read layout template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/" + layout + ".txt"), FileMode.Open));
            //populate stringbuilder with content from layout file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (!(sb.Length > 0)) { return string.Empty; }

            //get property images 
            sb.Replace("~@PROPERTY-PHOTO-2@~", helper.GetPropImageNamePath(order.order_id.ToString(), 2));
            sb.Replace("~@PROPERTY-PHOTO-3@~", helper.GetPropImageNamePath(order.order_id.ToString(), 3));
            sb.Replace("~@PROPERTY-PHOTO-4@~", helper.GetPropImageNamePath(order.order_id.ToString(), 4));
            sb.Replace("~@PROPERTY-PHOTO-5@~", helper.GetPropImageNamePath(order.order_id.ToString(), 5));
            sb.Replace("~@PROPERTY-PHOTO-6@~", helper.GetPropImageNamePath(order.order_id.ToString(), 6));
            sb.Replace("~@PROPERTY-PHOTO-7@~", helper.GetPropImageNamePath(order.order_id.ToString(), 7));

            //~@FLYER-TITLE@~
            sb.Replace("~@FLYER-TITLE@~", order.title);

            //~@FLYER-SUB-TITLE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.sub_title.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td align='center' style='color: #CC9900; font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold; font-size: 12px;'>");
                sbContent.Append(order.sub_title + "</td>");
                sbContent.Append("</tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='10' alt=''></td></tr></table>");
            }
            sb.Replace("~@FLYER-SUB-TITLE@~", sbContent.ToString());

            //~@PROPERTY-ADDRESS@~
            sbContent.Remove(0, sbContent.Length);
            if (order.prop_address1.Trim().Length > 0)
            {
                if (order.prop_address2.Trim().Length > 0)
                {
                    sbContent.Append("<span style='font-size: 12px;font-weight: bold;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'><strong>" + order.prop_address1 + "<br />" + order.prop_address2 + "<br />" + order.prop_city + " " + order.prop_state + " " + order.prop_zipcode + "</strong></span><br /><br />");
                }
                else
                {
                    sbContent.Append("<span style='font-size: 12px;font-weight: bold;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'><strong>" + order.prop_address1 + "<br />" + order.prop_city + " " + order.prop_state + " " + order.prop_zipcode + "</strong></span><br /><br />");
                }
            }
            sb.Replace("~@PROPERTY-ADDRESS@~", sbContent.ToString());

            //~@MLS-PRICE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.mls_number.Trim().Length > 0 || order.prop_price.ToString().Trim().Length > 0)
            {
                if (order.mls_number.Trim().Length > 0)
                {
                    sbContent.Append("<span style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>MLS:</span>");
                    sbContent.Append("<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.mls_number + "</span>");
                }
                if (order.mls_number.Trim().Length > 0 && order.prop_price.ToString().Trim().Length > 0)
                {
                    sbContent.Append("<br /><br />");
                }
                if (order.prop_price.ToString().Trim().Length > 0)
                {
                    SetPriceRentValues(order.prop_price.ToString());
                    sbContent.Append("<span style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>" + priceCaption + ":</span>");
                    sbContent.Append("<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + priceAmount + "</span>");
                }
            }
            sb.Replace("~@MLS-PRICE@~", sbContent.ToString());


            #region >>> Bullets.....

            sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the bullets section
            //if (order.bullet1.Trim().Length > 0 || order.bullet2.Trim().Length > 0 || order.bullet3.Trim().Length > 0 || order.bullet4.Trim().Length > 0 || order.bullet5.Trim().Length > 0 || order.bullet6.Trim().Length > 0 || order.bullet7.Trim().Length > 0 || order.bullet8.Trim().Length > 0)
            //{
            //    sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
            //    sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
            //    sbContent.Append("PROPERTY HIGHLIGHTS:</td></tr><tr><td align='left' valign='top'><!-- Highlight 1 3 5 7 9 --><table border='0' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='225' height='1' alt=''></td></tr>");
            //    if (order.bullet1.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet1 + "</td></tr>"); }
            //    if (order.bullet3.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet3 + "</td></tr>"); }
            //    if (order.bullet5.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet5 + "</td></tr>"); }
            //    if (order.bullet7.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet7 + "</td></tr>"); }
            //    //if (order.bullet9.Trim().Length > 0){sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet9 + "</td></tr>");}
            //    sbContent.Append("</table></td><td align='left' valign='top'><!-- Highlight 2 4 6 8 10 --><table border='0' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='225' height='1' alt=''></td></tr>");
            //    if (order.bullet2.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet2 + "</td></tr>"); }
            //    if (order.bullet4.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet4 + "</td></tr>"); }
            //    if (order.bullet6.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet6 + "</td></tr>"); }
            //    if (order.bullet8.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet8 + "</td></tr>"); }
            //    //if (order.bullet10.Trim().Length > 0){sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet10 + "</td></tr>");}
            //    sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            //}
            ////sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion


            #region >>> CheckBox Features.....

            // sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the property features section
            if (order.PropertyFeaturesValues.Contains("True"))
            {
                sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
                sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
                sbContent.Append("PROPERTY FEATURES:</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] strPropertyFeatures = order.PropertyFeatures.ToString().Split(':');
                string[] strPropertyFeaturesValues = order.PropertyFeaturesValues.ToString().Split(':');

                int tdCount = 0;
                for (int i = 0; i <= strPropertyFeaturesValues.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }
                    if (strPropertyFeaturesValues[i] == "True")
                    {
                        sbContent.Append("<td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + strPropertyFeatures[i] + "</td>");
                        tdCount = tdCount + 1;
                    }
                    if (tdCount == 4)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }

                }
                if (tdCount > 0)
                {
                    sbContent.Append("</tr>");
                    tdCount = 0;
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            //sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion

            #region >>> Other Features.....

            // sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the property features section
            if (order.OtherPropertyFeatures.Length > 1)
            {
                sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
                sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;padding-bottom:2px;'>");
                sbContent.Append("OTHER FEATURES:</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] strPropertyOtherFeatures = order.OtherPropertyFeatures.ToString().Split('^');
                string strPropertyFeaturesValues = strPropertyOtherFeatures[0];

                strPropertyOtherFeatures = strPropertyFeaturesValues.Split('|');


                int tdCount = 0;
                for (int i = 0; i <= strPropertyOtherFeatures.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }

                    sbContent.Append("<td  valign='top' style='padding-top:4px;'><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;vertical-align:top;'>" + strPropertyOtherFeatures[i] + "</td>");
                    tdCount = tdCount + 1;

                    if (tdCount == 4)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }

                }
                if (tdCount > 0)
                {
                    sbContent.Append("</tr>");
                    tdCount = 0;
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion
            
            //~@PROP-DESC@~
            sbContent.Remove(0, sbContent.Length);
            if (order.prop_desc.Trim().Length > 0)
            {
                sbContent.Append("<!-- Flyer Description --><table width='100%' BORDER=0 bgcolor='' CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>PROPERTY DESCRIPTION:</td>");
                sbContent.Append("</tr><tr><td style='padding-right: 5px;text-align:justify;font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                sbContent.Append(order.prop_desc);
                sbContent.Append("</td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROP-DESC@~", sbContent.ToString());

            #region >>> Custom Fields.....

            sbContent.Remove(0, sbContent.Length);

            //Check if there is any custom field value to show the custom fields section
            if ((order.custom_field_value1.Trim().Length > 0 || order.custom_field_value2.Trim().Length > 0 || order.custom_field_value3.Trim().Length > 0 || order.custom_field_value4.Trim().Length > 0 || order.custom_field_value5.Trim().Length > 0 || order.custom_field_value6.Trim().Length > 0 || order.custom_field_value7.Trim().Length > 0 || order.custom_field_value8.Trim().Length > 0) && order.Bedrooms.Trim() == "")
            {
                sbContent.Append("<!-- Flyer Custom Fields --><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0>");
                sbContent.Append("<tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'></td></tr><tr><td align='left' valign='top'>");
                sbContent.Append("<!-- Custom Rows 1 3 5 7 -->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='105' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='144' height='1' alt=''></td></tr>");
                if (order.custom_field_value1.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field1) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value1 + "</td></tr>"); }
                if (order.custom_field_value3.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field3) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value3 + "</td></tr>"); }
                if (order.custom_field_value5.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field5) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value5 + "</td></tr>"); }
                if (order.custom_field_value7.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field7) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value7 + "</td></tr>"); }
                //if (order.custom_field_value9.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + order.custom_field9 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value9 + "</td></tr>"); }
                sbContent.Append("</table></td><td><!-- Custom Rows 2 4 6 8 -->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='105' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='142' height='1' alt=''></td></tr>");
                if (order.custom_field_value2.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field2) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value2 + "</td></tr>"); }
                if (order.custom_field_value4.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field4) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value4 + "</td></tr>"); }
                if (order.custom_field_value6.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field6) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value6 + "</td></tr>"); }
                if (order.custom_field_value8.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field8) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value8 + "</td></tr>"); }
                //if (order.custom_field_value10.Trim().Length > 0) {sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + order.custom_field10 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value10 + "</td></tr>"); }
                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            //sb.Replace("~@CUST-FIELDS@~", sbContent.ToString());

            #endregion

            #region >>> Property Features(Custom Fields.....)

            //sbContent.Remove(0, sbContent.Length);

            //Check if there is any custom field value to show the custom fields section
            if ((order.Bedrooms.Trim().Length > 0 && order.Bedrooms.Trim() != "0") || (order.Subdivision.Trim().Length > 0 && order.Subdivision.Trim() != "0") || (order.FullBaths.Trim().Length > 0 && order.FullBaths.Trim() != "0") || (order.YearBuilt.Trim().Length > 0 && order.YearBuilt.Trim() != "0") || (order.HalfBaths.Trim().Length > 0 && order.HalfBaths.Trim() != "0") || (order.HOA.Trim().Length > 0 && order.HOA.Trim() != "0") || (order.LotSize.Trim().Length > 0 && order.LotSize.Trim() != "0") || (order.Parking.Trim().Length > 0 && order.Parking.Trim() != "0") || (order.SqFoots.Trim().Length > 0 && order.SqFoots.Trim() != "0") || (order.Floors.Trim().Length > 0 && order.Floors.Trim() != "0"))
            {
                sbContent.Append("<!-- Flyer Custom Fields-->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table><table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
                sbContent.Append("</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] PropFeatures ={ "Bedrooms", "Subdivision", "Full Baths", "Year Built", "Half Baths", "HOA", "Lot Size", "Parking", "Sqft", "Floors" };
                string[] PropFeaturesValues ={ order.Bedrooms, order.Subdivision, order.FullBaths, order.YearBuilt, order.HalfBaths, order.HOA, order.LotSize, order.Parking, order.SqFoots, order.Floors };

                int tdCount = 0;

                for (int i = 0; i <= PropFeaturesValues.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }

                    if (PropFeaturesValues[i].Trim() != "" && PropFeaturesValues[i].Trim()!="0")
                    {
                        sbContent.Append("<td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + PropFeatures[i] + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + PropFeaturesValues[i] + "</td>");
                        tdCount = tdCount + 1;
                    }

                    if (tdCount == 3)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }
                }

                if (tdCount > 0)
                {
                    sbContent.Append("<tr>");
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@CUST-FIELDS@~", sbContent.ToString());

            #endregion

            #region >>> Bottom Links....

            sbContent.Remove(0, sbContent.Length);
            if (RenderBottomLinks)
            {
                sbContent.Append("<!-- Links -->");
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                //MLS Link
                if (order.mls_link.Trim().Length > 0) { sbContent.Append("<td align='center'><a href='" + order.mls_link + "' id='aMLSLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color:#ff6633;text-decoration: underline;'>FIND MORE</a></td>"); }

                //Map Link
                if (order.map_link.Trim().Length > 0)
                {
                    if (order.map_link.IndexOf("FlyerMap.aspx?") != -1)
                    {
                        sbContent.Append("<td align='center'><a href='" + siteRoot + order.map_link + "' id='aMapLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW MAP</a></td>");
                    }
                    else
                    {
                        sbContent.Append("<td align='center'><a href='" + order.map_link + "' id='aMapLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW MAP</a></td>");
                    }
                }

                // PDF link
                if (order.order_id != 0)
                {
                    sbContent.Append("<td align='center'><a id='aPDFLink' href='" + siteRoot + "ShowPDF.aspx?oid=" + order.order_id + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW PDF</a></td>");
                }
                else
                {
                    sbContent.Append("<td align='center'><a id='aPDFLink' href='#' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW PDF</a></td>");
                }


                // Foward to clients link
                if (order.order_id != 0)
                {
                    sbContent.Append("<td align='center'><a id='aFTCLink' href='" + siteRoot + "ForwardToClients.aspx?oid=" + order.order_id + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>SEND TO CLIENTS</a></td>");
                }
                else
                {
                    sbContent.Append("<td align='center'><a id='aFTCLink' href='#' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>SEND TO CLIENTS</a></td>");
                }

                //Virtual Tour Link
                if (order.virtualtour_link.Trim().Length > 0) { sbContent.Append("<td align='center'><a href='" + order.virtualtour_link + "' id='aVTLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIRTUAL TOUR</a></td>"); }
                sbContent.Append("</tr></table>");

            }
            sb.Replace("~@LINKS@~", sbContent.ToString());
            #endregion

            //return L01 content
            return sb.ToString();

        } //END: L001

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for seller flyer type L08
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string L08(Order order, bool RenderBottomLinks)
        {
            string headline = order.headline;
            string theme = order.theme;
            string layout = order.layout;

            string content = string.Empty;
            StringBuilder sbContent = new StringBuilder();

            Helper helper = new Helper();

            if (layout == null || layout.Trim().Length < 1) { return string.Empty; } //exit if layout is empty

            //read layout template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/" + layout + ".txt"), FileMode.Open));
            //populate stringbuilder with content from layout file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (!(sb.Length > 0)) { return string.Empty; }

            //get property images 
            sb.Replace("~@PROPERTY-PHOTO-2@~", helper.GetPropImageNamePath(order.order_id.ToString(), 2));
            sb.Replace("~@PROPERTY-PHOTO-3@~", helper.GetPropImageNamePath(order.order_id.ToString(), 3));
            sb.Replace("~@PROPERTY-PHOTO-4@~", helper.GetPropImageNamePath(order.order_id.ToString(), 4));
            sb.Replace("~@PROPERTY-PHOTO-5@~", helper.GetPropImageNamePath(order.order_id.ToString(), 5));
            sb.Replace("~@PROPERTY-PHOTO-6@~", helper.GetPropImageNamePath(order.order_id.ToString(), 6));
            sb.Replace("~@PROPERTY-PHOTO-7@~", helper.GetPropImageNamePath(order.order_id.ToString(), 7));

            //~@FLYER-TITLE@~
            sb.Replace("~@FLYER-TITLE@~", order.title);

            //~@FLYER-SUB-TITLE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.sub_title.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td align='center' style='color: #CC9900; font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold; font-size: 12px;'>");
                sbContent.Append(order.sub_title + "</td>");
                sbContent.Append("</tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='10' alt=''></td></tr></table>");
            }
            sb.Replace("~@FLYER-SUB-TITLE@~", sbContent.ToString());



            //~@PROPERTY-ADDRESS@~
            sbContent.Remove(0, sbContent.Length);
            if (order.prop_address1.Trim().Length > 0)
            {
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td align='center' style='font-size: 12px;font-weight: bold;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                if (order.prop_address2.Trim().Length > 0)
                {
                    sbContent.Append(order.prop_address1 + ", " + order.prop_address2 + ", " + order.prop_city + " " + order.prop_state + " " + order.prop_zipcode + "</td>");
                }
                else
                {
                    sbContent.Append(order.prop_address1 + ", " + order.prop_city + " " + order.prop_state + " " + order.prop_zipcode + "</td>");
                }
                sbContent.Append("</tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='10' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROPERTY-ADDRESS@~", sbContent.ToString());

            //~@MLS-PRICE@~
            sbContent.Remove(0, sbContent.Length);
            if (order.mls_number.Trim().Length > 0 || order.prop_price.ToString().Trim().Length > 0)
            {
                if (order.mls_number.Trim().Length > 0)
                {
                    sbContent.Append("<span style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>MLS:</span>");
                    sbContent.Append("<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.mls_number + "</span>");
                }
                if (order.mls_number.Trim().Length > 0 && order.prop_price.ToString().Trim().Length > 0)
                {
                    sbContent.Append("<br /><br />");
                }
                if (order.prop_price.ToString().Trim().Length > 0)
                {
                    SetPriceRentValues(order.prop_price.ToString());
                    sbContent.Append("<span style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>" + priceCaption + ":</span>");
                    sbContent.Append("<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + priceAmount + "</span>");
                }
            }
            sb.Replace("~@MLS-PRICE@~", sbContent.ToString());


            #region >>> Bullets.....

            sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the bullets section
            //if (order.bullet1.Trim().Length > 0 || order.bullet2.Trim().Length > 0 || order.bullet3.Trim().Length > 0 || order.bullet4.Trim().Length > 0 || order.bullet5.Trim().Length > 0 || order.bullet6.Trim().Length > 0 || order.bullet7.Trim().Length > 0 || order.bullet8.Trim().Length > 0)
            //{
            //    sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
            //    sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
            //    sbContent.Append("PROPERTY HIGHLIGHTS:</td></tr><tr><td align='left' valign='top'><!-- Highlight 1 3 5 7 9 --><table border='0' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='225' height='1' alt=''></td></tr>");
            //    if (order.bullet1.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet1 + "</td></tr>"); }
            //    if (order.bullet3.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet3 + "</td></tr>"); }
            //    if (order.bullet5.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet5 + "</td></tr>"); }
            //    if (order.bullet7.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet7 + "</td></tr>"); }
            //    //if (order.bullet9.Trim().Length > 0){sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet9 + "</td></tr>");}
            //    sbContent.Append("</table></td><td align='left' valign='top'><!-- Highlight 2 4 6 8 10 --><table border='0' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='225' height='1' alt=''></td></tr>");
            //    if (order.bullet2.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet2 + "</td></tr>"); }
            //    if (order.bullet4.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet4 + "</td></tr>"); }
            //    if (order.bullet6.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet6 + "</td></tr>"); }
            //    if (order.bullet8.Trim().Length > 0) { sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet8 + "</td></tr>"); }
            //    //if (order.bullet10.Trim().Length > 0){sbContent.Append("<tr><td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.bullet10 + "</td></tr>");}
            //    sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            //}
            ////sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion

            #region >>> CheckBox Features.....

            // sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the property features section
            if (order.PropertyFeaturesValues.Contains("True"))
            {
                sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
                sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
                sbContent.Append("PROPERTY FEATURES:</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] strPropertyFeatures = order.PropertyFeatures.ToString().Split(':');
                string[] strPropertyFeaturesValues = order.PropertyFeaturesValues.ToString().Split(':');

                int tdCount = 0;
                for (int i = 0; i <= strPropertyFeaturesValues.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }
                    if (strPropertyFeaturesValues[i] == "True")
                    {
                        sbContent.Append("<td><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + strPropertyFeatures[i] + "</td>");
                        tdCount = tdCount + 1;
                    }
                    if (tdCount == 4)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }

                }
                if (tdCount > 0)
                {
                    sbContent.Append("</tr>");
                    tdCount = 0;
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            //sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion

            #region >>> Other Features.....

            // sbContent.Remove(0, sbContent.Length);

            //Check if there is any bullet available to show the property features section
            if (order.OtherPropertyFeatures.Length > 1)
            {
                sbContent.Append("<!-- Flyer HIGHLIGHTS -->");
                sbContent.Append("<table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;padding-bottom:2px;'>");
                sbContent.Append("OTHER FEATURES:</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='10' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] strPropertyOtherFeatures = order.OtherPropertyFeatures.ToString().Split('^');
                string strPropertyFeaturesValues = strPropertyOtherFeatures[0];

                strPropertyOtherFeatures = strPropertyFeaturesValues.Split('|');


                int tdCount = 0;
                for (int i = 0; i <= strPropertyOtherFeatures.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }

                    sbContent.Append("<td valign='top' style='padding-top:4px;'><img src='" + siteRoot + "images/arw.gif' alt=''></td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;vertical-align:top;'>" + strPropertyOtherFeatures[i] + "</td>");
                    tdCount = tdCount + 1;

                    if (tdCount == 4)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }

                }
                if (tdCount > 0)
                {
                    sbContent.Append("</tr>");
                    tdCount = 0;
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROPERTY-BULLETS@~", sbContent.ToString());

            #endregion


            //~@PROP-DESC@~
            sbContent.Remove(0, sbContent.Length);
            if (order.prop_desc.Trim().Length > 0)
            {
                sbContent.Append("<!-- Flyer Description --><table width='100%' BORDER=0 bgcolor='' CELLPADDING=0 CELLSPACING=0><tr>");
                sbContent.Append("<td style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>PROPERTY DESCRIPTION:</td>");
                sbContent.Append("</tr><tr><td style='padding-right: 5px;text-align:justify;font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>");
                sbContent.Append(order.prop_desc);
                sbContent.Append("</td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@PROP-DESC@~", sbContent.ToString());

            #region >>> Custom Fields.....

            sbContent.Remove(0, sbContent.Length);

            //Check if there is any custom field value to show the custom fields section
            if ((order.custom_field_value1.Trim().Length > 0 || order.custom_field_value2.Trim().Length > 0 || order.custom_field_value3.Trim().Length > 0 || order.custom_field_value4.Trim().Length > 0 || order.custom_field_value5.Trim().Length > 0 || order.custom_field_value6.Trim().Length > 0 || order.custom_field_value7.Trim().Length > 0 || order.custom_field_value8.Trim().Length > 0) && order.Bedrooms.Trim() == "")
            {
                sbContent.Append("<!-- Flyer Custom Fields --><table BORDER=0 CELLPADDING=0 CELLSPACING=0>");
                sbContent.Append("<tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'></td></tr><tr><td align='left' valign='top'>");
                sbContent.Append("<!-- Custom Rows 1 3 5 7 -->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='105' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='144' height='1' alt=''></td></tr>");
                if (order.custom_field_value1.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field1) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value1 + "</td></tr>"); }
                if (order.custom_field_value3.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field3) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value3 + "</td></tr>"); }
                if (order.custom_field_value5.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field5) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value5 + "</td></tr>"); }
                if (order.custom_field_value7.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field7) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value7 + "</td></tr>"); }
                //if (order.custom_field_value9.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + order.custom_field9 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value9 + "</td></tr>"); }
                sbContent.Append("</table></td><td><!-- Custom Rows 2 4 6 8 -->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='105' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='142' height='1' alt=''></td></tr>");
                if (order.custom_field_value2.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field2) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value2 + "</td></tr>"); }
                if (order.custom_field_value4.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field4) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value4 + "</td></tr>"); }
                if (order.custom_field_value6.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field6) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value6 + "</td></tr>"); }
                if (order.custom_field_value8.Trim().Length > 0) { sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + Helper.ProperCase(order.custom_field8) + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value8 + "</td></tr>"); }
                //if (order.custom_field_value10.Trim().Length > 0) {sbContent.Append("<tr><td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + order.custom_field10 + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + order.custom_field_value10 + "</td></tr>"); }
                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            //sb.Replace("~@CUST-FIELDS@~", sbContent.ToString());

            #endregion

            #region >>> Property Features(Custom Fields.....)

            //sbContent.Remove(0, sbContent.Length);

            //Check if there is any custom field value to show the custom fields section
            if ((order.Bedrooms.Trim().Length > 0 && order.Bedrooms.Trim() != "0") || (order.Subdivision.Trim().Length > 0 && order.Subdivision.Trim() != "0") || (order.FullBaths.Trim().Length > 0 && order.FullBaths.Trim() != "0") || (order.YearBuilt.Trim().Length > 0 && order.YearBuilt.Trim() != "0") || (order.HalfBaths.Trim().Length > 0 && order.HalfBaths.Trim() != "0") || (order.HOA.Trim().Length > 0 && order.HOA.Trim() != "0") || (order.LotSize.Trim().Length > 0 && order.LotSize.Trim() != "0") || (order.Parking.Trim().Length > 0 && order.Parking.Trim() != "0") || (order.SqFoots.Trim().Length > 0 && order.SqFoots.Trim() != "0") || (order.Floors.Trim().Length > 0 && order.Floors.Trim() != "0"))
            {
                sbContent.Append("<!-- Flyer Custom Fields-->");
                sbContent.Append("<table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table><table width='100%' border='0' CELLPADDING=0 CELLSPACING=0><tr><td colspan='2' style='font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;color: #336699;font-weight: bold;'>");
                sbContent.Append("</td></tr><tr><td align='left' colspan='2' valign='top'><table border='0' width='100%' CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='60' height='1' alt=''></td><td><img src='" + siteRoot + "images/space.gif' width='150' height='1' alt=''></td></tr>");

                string[] PropFeatures ={ "Bedrooms", "Subdivision", "Full Baths", "Year Built", "Half Baths", "HOA", "Lot Size", "Parking", "Sqft", "Floors" };
                string[] PropFeaturesValues ={ order.Bedrooms, order.Subdivision, order.FullBaths, order.YearBuilt, order.HalfBaths, order.HOA, order.LotSize, order.Parking, order.SqFoots, order.Floors };

                int tdCount = 0;

                for (int i = 0; i <= PropFeaturesValues.Length - 1; i++)
                {
                    if (tdCount == 0)
                    {
                        sbContent.Append("<tr>");
                        tdCount = 1;
                    }

                    if (PropFeaturesValues[i].Trim() != "" && PropFeaturesValues[i].Trim() != "0")
                    {
                        sbContent.Append("<td style='font-family: Verdana, Arial, Helvetica, sans-serif; color: #CC3300; font-weight: bold; font-size: 12px;'><img src='" + siteRoot + "images/arw2.gif' alt=''>" + PropFeatures[i] + ":</td><td style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>" + PropFeaturesValues[i] + "</td>");
                        tdCount = tdCount + 1;
                    }

                    if (tdCount == 3)
                    {
                        sbContent.Append("</tr>");
                        tdCount = 0;
                    }
                }

                if (tdCount > 0)
                {
                    sbContent.Append("<tr>");
                }

                sbContent.Append("</table></td></tr></table><table BORDER=0 CELLPADDING=0 CELLSPACING=0><tr><td><img src='" + siteRoot + "images/space.gif' width='200' height='20' alt=''></td></tr></table>");
            }
            sb.Replace("~@CUST-FIELDS@~", sbContent.ToString());

            #endregion

            #region >>> Bottom Links....

            sbContent.Remove(0, sbContent.Length);
            if (RenderBottomLinks)
            {
                sbContent.Append("<!-- Links -->");
                sbContent.Append("<table width='100%' BORDER=0 CELLPADDING=0 CELLSPACING=0><tr>");
                //MLS Link
                if (order.mls_link.Trim().Length > 0) { sbContent.Append("<td align='center'><a href='" + order.mls_link + "' id='aMLSLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color:#ff6633;text-decoration: underline;'>FIND MORE</a></td>"); }

                //Map Link
                if (order.map_link.Trim().Length > 0)
                {
                    if (order.map_link.IndexOf("FlyerMap.aspx?") != -1)
                    {
                        sbContent.Append("<td align='center'><a href='" + siteRoot + order.map_link + "' id='aMapLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW MAP</a></td>");
                    }
                    else
                    {
                        sbContent.Append("<td align='center'><a href='" + order.map_link + "' id='aMapLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW MAP</a></td>");
                    }
                }

                // PDF link
                if (order.order_id != 0)
                {
                    sbContent.Append("<td align='center'><a id='aPDFLink' href='" + siteRoot + "ShowPDF.aspx?oid=" + order.order_id + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW PDF</a></td>");
                }
                else
                {
                    sbContent.Append("<td align='center'><a id='aPDFLink' href='#' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIEW PDF</a></td>");
                }


                // Foward to clients link
                if (order.order_id != 0)
                {
                    sbContent.Append("<td align='center'><a id='aFTCLink' href='" + siteRoot + "ForwardToClients.aspx?oid=" + order.order_id + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>SEND TO CLIENTS</a></td>");
                }
                else
                {
                    sbContent.Append("<td align='center'><a id='aFTCLink' href='#' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>SEND TO CLIENTS</a></td>");
                }

                //Virtual Tour Link
                if (order.virtualtour_link.Trim().Length > 0) { sbContent.Append("<td align='center'><a href='" + order.virtualtour_link + "' id='aVTLink' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif; font-weight: bold;color: #ff6633;text-decoration: underline;'>VIRTUAL TOUR</a></td>"); }
                sbContent.Append("</tr></table>");

            }
            sb.Replace("~@LINKS@~", sbContent.ToString());
            #endregion

            //return L01 content
            return sb.ToString();

        } //END: L001

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for seller flyer type L01
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string L01_OLD(Order order)
        {
            string headline = order.headline;
            string theme = order.theme;
            string layout = order.layout;

            //~@MLS@~ <b>MLS:</b>12545255
            //~@PRICE@~ <b>PRICE:</b> $450,000
            //~@BULLET-1@~ <img src="../images/aarow.gif"/><font face="Verdana" size="1">&nbsp;New Hardwood Flooring<br> 

            string content = string.Empty;

            Helper helper = new Helper();

            if (layout == null || layout.Trim().Length < 1) { return string.Empty; } //exit if layout is empty

            //read layout template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/" + layout + ".txt"), FileMode.Open));
            //populate stringbuilder with content from layout file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (!(sb.Length > 0)) { return string.Empty; }

            //get property images 
            sb.Replace("~@PROPERTY-PHOTO-2@~", helper.GetPropImageNamePath(order.order_id.ToString(), 2));
            sb.Replace("~@PROPERTY-PHOTO-3@~", helper.GetPropImageNamePath(order.order_id.ToString(), 3));
            sb.Replace("~@PROPERTY-PHOTO-4@~", helper.GetPropImageNamePath(order.order_id.ToString(), 4));
            sb.Replace("~@PROPERTY-PHOTO-5@~", helper.GetPropImageNamePath(order.order_id.ToString(), 5));

            //~@FLYER-TITLE@~
            sb.Replace("~@FLYER-TITLE@~", order.title);

            //~@FLYER-SUB-TITLE@~
            content = order.sub_title;
            sb.Replace("~@FLYER-SUB-TITLE@~", content);

            //~@PROPERTY-ADDRESS@~
            content = order.prop_address1 + ", " + order.prop_address2 + ", " + order.prop_city + " " + order.prop_state + " " + order.prop_zipcode;
            sb.Replace("~@PROPERTY-ADDRESS@~", content);

            //~@MLS@~  <b>MLS:</b>12545255
            content = string.Empty;
            if (order.mls_number.Trim().Length > 0) { content = "<b>MLS:</b>" + order.mls_number; }
            sb.Replace("~@MLS@~", content);

            //~@PRICE@~
            content = string.Empty;

            if (order.prop_price.ToString().Trim().Length > 0)
            {
                SetPriceRentValues(order.prop_price.ToString());
                content = "<b>" + priceCaption + ":</b>" + priceAmount;
            }
            sb.Replace("~@PRICE@~", content);

            #region >>> Bullets.....

            //~@BULLET-1@~ <img src="../images/aarow.gif"/><font face="Verdana" size="1">&nbsp;New Hardwood Flooring<br>
            content = string.Empty;
            if (order.bullet1.Trim().Length > 0)
            {
                content = "<img src='" + siteRoot + "images/aarow.gif'/><font face='Verdana' size='1'>&nbsp;" + order.bullet1 + "<br/>";
            }
            sb.Replace("~@BULLET-1@~", content);

            content = string.Empty;
            if (order.bullet2.Trim().Length > 0)
            {
                content = "<img src='" + siteRoot + "images/aarow.gif'/><font face='Verdana' size='1'>&nbsp;" + order.bullet2 + "<br/>";
            }
            sb.Replace("~@BULLET-2@~", content);

            content = string.Empty;
            if (order.bullet3.Trim().Length > 0)
            {
                content = "<img src='" + siteRoot + "images/aarow.gif'/><font face='Verdana' size='1'>&nbsp;" + order.bullet3 + "<br/>";
            }
            sb.Replace("~@BULLET-3@~", content);

            content = string.Empty;
            if (order.bullet4.Trim().Length > 0)
            {
                content = "<img src='" + siteRoot + "images/aarow.gif'/><font face='Verdana' size='1'>&nbsp;" + order.bullet4 + "<br/>";
            }
            sb.Replace("~@BULLET-4@~", content);

            content = string.Empty;
            if (order.bullet5.Trim().Length > 0)
            {
                content = "<img src='" + siteRoot + "images/aarow.gif'/><font face='Verdana' size='1'>&nbsp;" + order.bullet5 + "<br/>";
            }
            sb.Replace("~@BULLET-5@~", content);

            content = string.Empty;
            if (order.bullet6.Trim().Length > 0)
            {
                content = "<img src='" + siteRoot + "images/aarow.gif'/><font face='Verdana' size='1'>&nbsp;" + order.bullet6 + "<br/>";
            }
            sb.Replace("~@BULLET-6@~", content);

            content = string.Empty;
            if (order.bullet7.Trim().Length > 0)
            {
                content = "<img src='" + siteRoot + "images/aarow.gif'/><font face='Verdana' size='1'>&nbsp;" + order.bullet7;
            }
            sb.Replace("~@BULLET-7@~", content);

            content = string.Empty;
            if (order.bullet8.Trim().Length > 0)
            {
                content = "<img src='" + siteRoot + "images/aarow.gif'/><font face='Verdana' size='1'>&nbsp;" + order.bullet8;
            }
            sb.Replace("~@BULLET-8@~", content);

            #endregion

            //~@PROP-DESC@~
            content = order.prop_desc;
            sb.Replace("~@PROP-DESC@~", content);

            #region >>> Custom Fields.....

            //~@CUST-FIELD-1@~  ~@CUST-FIELD-VALUE-1@~
            content = order.custom_field1;
            sb.Replace("~@CUST-FIELD-1@~", content);
            content = order.custom_field_value1;
            sb.Replace("~@CUST-FIELD-VALUE-1@~", content);

            content = order.custom_field2;
            sb.Replace("~@CUST-FIELD-2@~", content);
            content = order.custom_field_value2;
            sb.Replace("~@CUST-FIELD-VALUE-2@~", content);

            content = order.custom_field3;
            sb.Replace("~@CUST-FIELD-3@~", content);
            content = order.custom_field_value3;
            sb.Replace("~@CUST-FIELD-VALUE-3@~", content);

            content = order.custom_field4;
            sb.Replace("~@CUST-FIELD-4@~", content);
            content = order.custom_field_value4;
            sb.Replace("~@CUST-FIELD-VALUE-4@~", content);

            content = order.custom_field5;
            sb.Replace("~@CUST-FIELD-5@~", content);
            content = order.custom_field_value5;
            sb.Replace("~@CUST-FIELD-VALUE-5@~", content);

            content = order.custom_field6;
            sb.Replace("~@CUST-FIELD-6@~", content);
            content = order.custom_field_value6;
            sb.Replace("~@CUST-FIELD-VALUE-6@~", content);

            content = order.custom_field7;
            sb.Replace("~@CUST-FIELD-7@~", content);
            content = order.custom_field_value7;
            sb.Replace("~@CUST-FIELD-VALUE-7@~", content);

            content = order.custom_field8;
            sb.Replace("~@CUST-FIELD-8@~", content);
            content = order.custom_field_value8;
            sb.Replace("~@CUST-FIELD-VALUE-8@~", content);

            content = order.custom_field9;
            sb.Replace("~@CUST-FIELD-9@~", content);
            content = order.custom_field_value9;
            sb.Replace("~@CUST-FIELD-VALUE-9@~", content);

            content = order.custom_field10;
            sb.Replace("~@CUST-FIELD-10@~", content);
            content = order.custom_field_value10;
            sb.Replace("~@CUST-FIELD-VALUE-10@~", content);

            #endregion

            #region >>> Bottom Links....

            //~@MLS-LINK@~ <font face="Verdana" size="1" color="#FF9933"><a title="" style="COLOR: #FF6600" href="#"><b>MLS Link</b></a></font>
            content = string.Empty;
            if (order.mls_link.Trim().Length > 0)
            {
                content = "<font face='Verdana' size='1' color='#FF9933'><a title='' style='COLOR: #FF6600' href='" + order.mls_link + "' id='aMLSLink'><b>MLS Link</b></a></font>";
            }
            sb.Replace("~@MLS-LINK@~", content);

            content = string.Empty;
            if (order.map_link.Trim().Length > 0)
            {
                content = "<font face='Verdana' size='1' color='#FF9933'><a title='' style='COLOR: #FF6600' href='" + siteRoot + order.map_link + "' id='aMapLink'><b>View Map</b></a></font>";
            }
            sb.Replace("~@VIEW-MAP@~", content);


            content = "<font face='Verdana' size='1' color='#FF9933'><a title='' style='COLOR: #FF6600' id='aPDFLink' href='" + siteRoot + "ShowPDF.aspx?oid=" + order.order_id + "'><b>Print PDF</b></a></font>"; //test pdf link
            sb.Replace("~@VIEW-PDF@~", content);

            content = string.Empty;
            if (order.virtualtour_link.Trim().Length > 0)
            {
                content = "<font face='Verdana' size='1' color='#FF9933'><a title='' style='COLOR: #FF6600' href='" + order.virtualtour_link + "' id='aVTLink'><b>Virtual Tour</b></a></font>";
            }
            sb.Replace("~@VIRTUAL-TOUR@~", content);

            #endregion

            //return L01 content
            return sb.ToString();

        } //END: L01

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct agent info markup for forwarded flyers
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string ForwardingAgentInfo(Order order, ProfileCommon profile)
        {
            //ProfileCommon profile = HttpContext.Current.Profile.GetProfile(HttpContext.Current.User.Identity.Name);

            string content = string.Empty;
            string buffer = string.Empty;

            //read agent info template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/ForwardingAgentInfo.txt"), FileMode.Open));
            //populate stringbuilder with content from agent info file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (sb.Length > 0)
            {

                //~@MY-PHOTO@~ <img border="1" src="../images/agent1.jpg">
                content = "";
                content = "<img src='" + siteRoot + profile.ImageURL + "' style='border:1px solid #333333;' alt=''>";
                sb.Replace("~@MY-PHOTO@~", content);

                //~@MY-NAME@~
                content = "";
                content = profile.FirstName + " " + profile.LastName;
                sb.Replace("~@MY-NAME@~", content);

                //~@MY-BROKER@~
                content = "";
                content = profile.Brokerage.BrokerageName;
                sb.Replace("~@MY-BROKER@~", content);

                //~@MY-BUSINESS-PHONE@~ (214) 125-2589 Ext. 1254<BR/>
                //~@MY-BUSINESS-PHONE-EXT@~ 
                if (profile.Contact.PhoneBusinessExt.Trim().Length > 0)
                {
                    sb.Replace("~@MY-BUSINESS-PHONE@~", "Phone:" + profile.Contact.PhoneBusiness.Replace("-", ".") + " Ext." + profile.Contact.PhoneBusinessExt);
                }
                else
                {
                    sb.Replace("~@MY-BUSINESS-PHONE@~", "Phone:" + profile.Contact.PhoneBusiness.Replace("-", "."));
                }

                //~@MY-CELL-PHONE@~ (214) 125-2589<br/>
                if (profile.Contact.PhoneCell.Trim().Length > 0)
                {
                    sb.Replace("~@MY-CELL-PHONE@~", "<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>Mobile: " + profile.Contact.PhoneCell.Replace("-", ".") + "</span><br/>");
                }
                else
                {
                    sb.Replace("~@MY-CELL-PHONE@~", string.Empty);
                }

                //~@MY-FAX@~ 
                if (profile.Contact.Fax.Trim().Length > 0)
                {
                    sb.Replace("~@MY-FAX@~", "<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>Fax: " + profile.Contact.Fax.Replace("-", ".") + "</span><br/>");
                }
                else
                {
                    sb.Replace("~@MY-FAX@~", string.Empty);
                }

                //~@MY-EMAIL@~
                content = string.Empty;
                if (profile.UserName.Trim().Length <= 30)
                {
                    content = "<a href='mailto:" + profile.UserName + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #ff6633;text-decoration: underline;'>" + profile.UserName + "</a><br/>";
                }
                else
                {
                    content = "<a href='mailto:" + profile.UserName + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #ff6633;text-decoration: underline;'>Email Me</a><br/>";
                }
                sb.Replace("~@MY-EMAIL@~", content);



                //~@MY-WEBSITE@~
                content = string.Empty;
                buffer = profile.Website.Replace("https://", "");
                buffer = buffer.Replace("http://", "");
                if (profile.Website.Trim().Length < 26)
                {
                    content = "<a href='" + profile.Website + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #ff6633;text-decoration: underline;'>" + buffer + "</a>";
                }
                else
                {
                    content = "<a href='" + profile.Website + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #ff6633;text-decoration: underline;'>My Website</a>";
                }
                sb.Replace("~@MY-WEBSITE@~", content);


                //~@MY-LOGO@~ <img border="0" src="../images/brokerlogo.gif">
                content = "";
                if (profile.LogoURL.Trim().Length > 0)
                {
                    content = "<img border='0' src='" + siteRoot + profile.LogoURL + "' alt=''>";
                }
                sb.Replace("~@MY-LOGO@~", content);

                //~@MLS-LOGO@~ <img border="0" src="../images/mlsLogo.gif" width="80" height="30"><br>
                if (order.use_mls_logo)
                {
                    sb.Replace("~@MLS-LOGO@~", "<img border='0' src='" + siteRoot + "images/mlsLogo.gif' width='80' height='30'><br/><br/>");
                }
                else
                {
                    sb.Replace("~@MLS-LOGO@~", string.Empty);
                }

                //~@EQUAL-HOUSING-LOGO@~ <br><img border="0" src="../images/equalHousing.gif" width="33" height="32">
                if (order.use_housing_logo)
                {
                    sb.Replace("~@EQUAL-HOUSING-LOGO@~", "<br><img border='0' src='" + siteRoot + "images/equalHousing.gif' width='33' height='32'>");
                }
                else
                {
                    sb.Replace("~@EQUAL-HOUSING-LOGO@~", string.Empty);
                }
            }

            //render agent info content to the browser
            return sb.ToString();
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct agent info mark up for seller flyers
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string AgentInfo1(Order order, ProfileCommon profile)
        {
            //ProfileCommon profile = HttpContext.Current.Profile.GetProfile(HttpContext.Current.User.Identity.Name);

            string content = string.Empty;
            string buffer = string.Empty;

            //read agent info template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/AgentInfo.txt"), FileMode.Open));
            //populate stringbuilder with content from agent info file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (sb.Length > 0)
            {

                //~@MY-PHOTO@~ <img border="1" src="../images/agent1.jpg">
                content = "";
                if (profile.ImageURL != null && profile.ImageURL.Trim().Length > 0)
                {
                    content = "<img src='" + siteRoot + profile.ImageURL + "' style='border:1px solid #333333;' alt=''>";
                }
                sb.Replace("~@MY-PHOTO@~", content);

                //~@MY-NAME@~
                content = "";
                content = profile.FirstName + " " + profile.LastName;
                sb.Replace("~@MY-NAME@~", content);

                //~@MY-BROKER@~
                content = "";
                content = profile.Brokerage.BrokerageName;
                sb.Replace("~@MY-BROKER@~", content);

                //~@MY-BUSINESS-PHONE@~ (214) 125-2589 Ext. 1254<BR/>
                //~@MY-BUSINESS-PHONE-EXT@~ 
                if (profile.Contact.PhoneBusinessExt.Trim().Length > 0)
                {
                    sb.Replace("~@MY-BUSINESS-PHONE@~", "Phone:" + profile.Contact.PhoneBusiness.Replace("-", ".") + " Ext." + profile.Contact.PhoneBusinessExt);
                }
                else
                {
                    sb.Replace("~@MY-BUSINESS-PHONE@~", "Phone:" + profile.Contact.PhoneBusiness.Replace("-", "."));
                }

                //~@MY-CELL-PHONE@~ (214) 125-2589<br/>
                if (profile.Contact.PhoneCell.Trim().Length > 0)
                {
                    sb.Replace("~@MY-CELL-PHONE@~", "<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>Mobile: " + profile.Contact.PhoneCell.Replace("-", ".") + "</span><br/>");
                }
                else
                {
                    sb.Replace("~@MY-CELL-PHONE@~", string.Empty);
                }

                //~@MY-FAX@~ 
                if (profile.Contact.Fax.Trim().Length > 0)
                {
                    if (profile.DRE.Trim().Length > 0)
                    {
                        sb.Replace("~@MY-FAX@~", "<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>Fax: " + profile.Contact.Fax.Replace("-", ".") + "</span><br/>" +
                            "<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>BRE#:" + profile.DRE + "</span><br/>");
                    }
                    else
                    {
                        sb.Replace("~@MY-FAX@~", "<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>Fax: " + profile.Contact.Fax.Replace("-", ".") + "</span><br/>");
                    }
                }
                else
                {
                    if (profile.DRE.Trim().Length > 0)
                    {
                        sb.Replace("~@MY-FAX@~", "<span style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #666666;'>BRE#:" + profile.DRE + "</span><br/>");
                    }
                    else
                    {
                        sb.Replace("~@MY-FAX@~", string.Empty);
                    }
                }

                //~@MY-EMAIL@~
                content = string.Empty;
                if (profile.UserName.Trim().Length <= 30)
                {
                    content = "<a href='mailto:" + profile.UserName + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #ff6633;text-decoration: underline;'>" + profile.UserName + "</a><br/>";
                }
                else
                {
                    content = "<a href='mailto:" + profile.UserName + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #ff6633;text-decoration: underline;'>Email Me</a><br/>";
                }
                sb.Replace("~@MY-EMAIL@~", content);



                //~@MY-WEBSITE@~
                content = string.Empty;
                buffer = profile.Website.Replace("https://", "");
                buffer = buffer.Replace("http://", "");
                if (profile.Website.Trim().Length < 26)
                {
                    content = "<a href='" + profile.Website + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #ff6633;text-decoration: underline;'>" + buffer + "</a>";
                }
                else
                {
                    content = "<a href='" + profile.Website + "' style='font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;color: #ff6633;text-decoration: underline;'>My Website</a>";
                }
                sb.Replace("~@MY-WEBSITE@~", content);


                //~@MY-LOGO@~ <img border="0" src="../images/brokerlogo.gif">
                content = "";
                if (profile.LogoURL != null && profile.LogoURL.Trim().Length > 0)
                {
                    content = "<img border='0' src='" + siteRoot + profile.LogoURL + "' alt=''>";
                }
                sb.Replace("~@MY-LOGO@~", content);

                //~@MLS-LOGO@~ <img border="0" src="../images/mlsLogo.gif" width="80" height="30"><br>
                if (order.use_mls_logo)
                {
                    sb.Replace("~@MLS-LOGO@~", "<img border='0' src='" + siteRoot + "images/mlsLogo.gif' width='80' height='30'><br/><br/>");
                }
                else
                {
                    sb.Replace("~@MLS-LOGO@~", string.Empty);
                }

                //~@EQUAL-HOUSING-LOGO@~ <br><img border="0" src="../images/equalHousing.gif" width="33" height="32">
                if (order.use_housing_logo)
                {
                    sb.Replace("~@EQUAL-HOUSING-LOGO@~", "<br><img border='0' src='" + siteRoot + "images/equalHousing.gif' width='33' height='32'>");
                }
                else
                {
                    sb.Replace("~@EQUAL-HOUSING-LOGO@~", string.Empty);
                }
            }

            //render agent info content to the browser
            return sb.ToString();
        }


        public string AgentInfo1_OLD(Order order, ProfileCommon profile)
        {
            //ProfileCommon profile = HttpContext.Current.Profile.GetProfile(HttpContext.Current.User.Identity.Name);

            string content = string.Empty;
            string buffer = string.Empty;

            //read agent info template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/AgentInfo.txt"), FileMode.Open));
            //populate stringbuilder with content from agent info file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (sb.Length > 0)
            {

                //~@MY-PHOTO@~ <img border="1" src="../images/agent1.jpg">
                content = "";
                content = "<img border='1' src='" + siteRoot + profile.ImageURL + "'>";
                sb.Replace("~@MY-PHOTO@~", content);

                //~@MY-NAME@~
                content = "";
                content = profile.FirstName + " " + profile.LastName;
                sb.Replace("~@MY-NAME@~", content);

                //~@MY-BUSINESS-PHONE@~ (214) 125-2589 Ext. 1254<BR/>
                //~@MY-BUSINESS-PHONE-EXT@~ 
                if (profile.Contact.PhoneBusinessExt.Trim().Length > 0)
                {
                    sb.Replace("~@MY-BUSINESS-PHONE@~", profile.Contact.PhoneBusiness);
                    sb.Replace("~@MY-BUSINESS-PHONE-EXT@~", profile.Contact.PhoneBusinessExt + "<br/>");
                }
                else
                {
                    sb.Replace("~@MY-BUSINESS-PHONE@~", profile.Contact.PhoneBusiness + "<br/>");
                    sb.Replace("~@MY-BUSINESS-PHONE-EXT@~", string.Empty);
                }

                //~@MY-CELL-PHONE@~ (214) 125-2589<br/>
                if (profile.Contact.PhoneCell.Trim().Length > 0)
                {
                    sb.Replace("~@MY-CELL-PHONE@~", profile.Contact.PhoneCell);
                }
                else
                {
                    sb.Replace("~@MY-CELL-PHONE@~", string.Empty);
                }

                //~@MY-HOME-PHONE@~ (214) 125-2589<br/>
                if (profile.Contact.PhoneHome.Trim().Length > 0)
                {
                    sb.Replace("~@MY-HOME-PHONE@~", profile.Contact.PhoneHome);
                }
                else
                {
                    sb.Replace("~@MY-HOME-PHONE@~", string.Empty);
                }

                //~@MY-EMAIL@~
                //<a rel="nofollow" style="font:normal 10px Verdana, Arial, Helvetica, sans-serif;color:#FF6600;text-decoration:none;" 
                //target="_blank" href="mailto:xyz@asdfas.com">mariastalon@remax.com</a><BR/>
                content = string.Empty;
                if (profile.UserName.Trim().Length < 22)
                {
                    content = "<a rel='nofollow' style='font:normal 10px Verdana, Arial, Helvetica, sans-serif;color:#FF6600;text-decoration:none;' target='_blank' href='mailto:" + profile.UserName + "'>" + profile.UserName + "</a><BR/>";
                }
                else
                {
                    content = "<a rel='nofollow' style='font:normal 10px Verdana, Arial, Helvetica, sans-serif;color:#FF6600;text-decoration:none;' target='_blank' href='mailto:" + profile.UserName + "'>Email Me</a><BR/>";
                }
                sb.Replace("~@MY-EMAIL@~", content);



                //~@MY-WEBSITE@~
                //<a rel="nofollow" style="font:normal 10px Verdana, Arial, Helvetica, sans-serif;color:#FF6600;text-decoration:none;" 
                //target="_blank" href="#">www.mariastalon.com</a>	
                content = string.Empty;
                buffer = profile.Website.Replace("https://", "");
                buffer = buffer.Replace("http://", "");
                if (profile.Website.Trim().Length < 22)
                {

                    content = "<a rel='nofollow' style='font:normal 10px Verdana, Arial, Helvetica, sans-serif;color:#FF6600;text-decoration:none;' target='_blank' href='" + profile.Website + "'>" + buffer + "</a>";
                }
                else
                {
                    content = "<a rel='nofollow' style='font:normal 10px Verdana, Arial, Helvetica, sans-serif;color:#FF6600;text-decoration:none;' target='_blank' href='" + profile.Website + "'>My Website</a>";
                }
                sb.Replace("~@MY-WEBSITE@~", content);


                //~@MY-LOGO@~ <img border="0" src="../images/brokerlogo.gif">
                content = "";
                content = "<img border='0' src='" + siteRoot + profile.LogoURL + "'>";
                sb.Replace("~@MY-LOGO@~", content);

                //~@MY-BROKER@~
                //<b><font face="Verdana" size="1">ReMax Realtors Inc.</font></b><font face="Verdana" size="1">
                //<br>1340 Burburn Lake Dr.<br>Suite 145<br>Carrolton, TX 75025</font>
                if (profile.Brokerage.BrokerageAddress2.Trim().Length > 0)
                {
                    content = "<b><font face='Verdana' size='1'>" + profile.Brokerage.BrokerageName + "</font></b><font face='Verdana' size='1'><br>" + profile.Brokerage.BrokerageAddress1 + "<br>" + profile.Brokerage.BrokerageAddress2 + "<br>" + profile.Brokerage.BrokerageCity + ", " + profile.Brokerage.BrokerageState + " " + profile.Brokerage.BrokerageZipcode + "</font>";
                }
                else
                {
                    content = "<b><font face='Verdana' size='1'>" + profile.Brokerage.BrokerageName + "</font></b><font face='Verdana' size='1'><br>" + profile.Brokerage.BrokerageAddress1 + "<br>" + profile.Brokerage.BrokerageCity + ", " + profile.Brokerage.BrokerageState + " " + profile.Brokerage.BrokerageZipcode + "</font>";
                }
                sb.Replace("~@MY-BROKER@~", content);

                //~@MLS-LOGO@~ <img border="0" src="../images/mlsLogo.gif" width="80" height="30"><br>
                if (order.use_mls_logo)
                {
                    sb.Replace("~@MLS-LOGO@~", "<img border='0' src='" + siteRoot + "images/mlsLogo.gif' width='80' height='30'><br>");
                }
                else
                {
                    sb.Replace("~@MLS-LOGO@~", string.Empty);
                }

                //~@EQUAL-HOUSING-LOGO@~ <br><img border="0" src="../images/equalHousing.gif" width="33" height="32">
                if (order.use_housing_logo)
                {
                    sb.Replace("~@EQUAL-HOUSING-LOGO@~", "<br><img border='0' src='" + siteRoot + "images/equalHousing.gif' width='33' height='32'>");
                }
                else
                {
                    sb.Replace("~@EQUAL-HOUSING-LOGO@~", string.Empty);
                }
            }

            //render agent info content to the browser
            return sb.ToString();
        }
    }

}
