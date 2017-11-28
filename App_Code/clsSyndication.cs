using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;
using FlyerMe;
using System.IO;
using System.Globalization;
using PersonalWebsiteTableAdapters;
/// <summary>
/// Summary description for clsSyndication
/// </summary>
public class clsSyndication
{
	public clsSyndication()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void GenerateTruliaFeed(ProfileCommon Profile)
    {
        Xml Feed= new Xml();

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/TruliaFeed.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        CultureInfo ci = new CultureInfo("de-DE");
        xtwFeed.WriteStartElement("properties");

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{
            //properties Start
        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("property");


                //Location Start
                xtwFeed.WriteStartElement("location");
                xtwFeed.WriteElementString("unit-number", "");
                xtwFeed.WriteStartElement("street-address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city-name");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state-code");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zipcode");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("longitude", "");
                xtwFeed.WriteElementString("latitude", "");
                xtwFeed.WriteElementString("geocode-type", "");
                xtwFeed.WriteElementString("display-address", "yes");
                xtwFeed.WriteEndElement();
                //Location End

                //details Start

                xtwFeed.WriteStartElement("details");

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }

                xtwFeed.WriteElementString("price", Price);


                //For using old data of custom fields
                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("year-built", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                    xtwFeed.WriteElementString("num-bedrooms", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteElementString("num-full-bathrooms", OrderTable.Rows[i]["FullBaths"].ToString().Trim());
                    xtwFeed.WriteElementString("num-half-bathrooms", OrderTable.Rows[i]["HalfBaths"].ToString().Trim());

                    xtwFeed.WriteStartElement("lot-size");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["LotSize"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("square-feet");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);
                    xtwFeed.WriteElementString("year-built", dr["YearBuilt"].ToString());
                    xtwFeed.WriteElementString("num-bedrooms", dr["Bedrooms"].ToString());
                    xtwFeed.WriteElementString("num-full-bathrooms", dr["FullBaths"].ToString());
                    xtwFeed.WriteElementString("num-half-bathrooms", dr["HalfBaths"].ToString());

                    xtwFeed.WriteStartElement("lot-size");
                    xtwFeed.WriteCData(dr["LotSize"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("square-feet");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteStartElement("date-listed");
                xtwFeed.WriteCData(Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("yyyy-MM-dd"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("property-type", OrderTable.Rows[i]["PropertyType"].ToString().Trim());

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("mlsId");
                xtwFeed.WriteCData(OrderTable.Rows[i]["mls_number"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("provider-listingid", OrderTable.Rows[i]["order_id"].ToString());
                xtwFeed.WriteEndElement();
                //details End

                //landing-page Start
                xtwFeed.WriteStartElement("landing-page");

                xtwFeed.WriteStartElement("lp-url");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("virtual-tour-url");
                xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //landing-page End

                if (OrderTable.Rows[i]["virtualtour_link"].ToString().Contains("rent"))
                {
                    xtwFeed.WriteElementString("listing-type", "rental");
                    xtwFeed.WriteElementString("status", "for rent");
                }
                else
                {
                    xtwFeed.WriteElementString("listing-type", "");
                    xtwFeed.WriteElementString("status", "For Sale");
                }

                xtwFeed.WriteElementString("foreclosure-status", "");

                //site Start
                xtwFeed.WriteStartElement("site");
                xtwFeed.WriteElementString("site-url", clsUtility.SiteHttpWwwRootUrl);
                xtwFeed.WriteElementString("site-name", clsUtility.SiteBrandName);
                xtwFeed.WriteEndElement();
                //site End

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                //agent Start
                xtwFeed.WriteStartElement("agent");

                xtwFeed.WriteStartElement("agent-name");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-phone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-email");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-picture-url");
                xtwFeed.WriteCData(((profile.ImageURL.Length) > 0 ? clsUtility.GetRootHost 
                    + profile.ImageURL : clsUtility.GetRootHost + "images/noMyPhoto.gif"));
                xtwFeed.WriteEndElement();

                //images/noMyPhoto.gif
                xtwFeed.WriteEndElement();
                //agent End

                //broker Start
                xtwFeed.WriteStartElement("broker");

                xtwFeed.WriteStartElement("broker-name");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("broker-phone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("broker-email");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //broker End

                ////office Start
                //xtwFeed.WriteStartElement("office");
                //xtwFeed.WriteElementString("office-name", "");
                //xtwFeed.WriteElementString("office-phone", "");
                //xtwFeed.WriteElementString("office-email", "");
                //xtwFeed.WriteEndElement();
                ////office End

                ////franchise Start
                //xtwFeed.WriteStartElement("franchise");
                //xtwFeed.WriteElementString("franchise-name", "");
                //xtwFeed.WriteElementString("franchise-phone", "");
                //xtwFeed.WriteElementString("franchise-email", "");
                //xtwFeed.WriteEndElement();
                ////franchise End

                //pictures Start
                xtwFeed.WriteStartElement("pictures");

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() 
                        + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() 
                        + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo3"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() 
                        + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo4"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() 
                        + "/photo4/" + OrderTable.Rows[i]["photo4"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo5"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() 
                        + "/photo5/" + OrderTable.Rows[i]["photo5"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo6"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() 
                        + "/photo6/" + OrderTable.Rows[i]["photo6"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo7"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() 
                        + "/photo7/" + OrderTable.Rows[i]["photo7"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                xtwFeed.WriteEndElement();
                //pictures End

                ////open-home Start
                //xtwFeed.WriteStartElement("open-home");
                //xtwFeed.WriteElementString("period1-date", "");
                //xtwFeed.WriteElementString("period1-start-time", "");
                //xtwFeed.WriteElementString("period1-end-time", "");
                //xtwFeed.WriteElementString("period1-details", "");
                //xtwFeed.WriteEndElement();
                ////open-home End

                ////advertise-with-us Start
                //xtwFeed.WriteStartElement("advertise-with-us");
                //xtwFeed.WriteElementString("channel", "");
                //xtwFeed.WriteElementString("featured", "");
                //xtwFeed.WriteElementString("branded", "");
                //xtwFeed.WriteElementString("branded-logo-url", "");
                //xtwFeed.WriteEndElement();
                ////advertise-with-us End

                xtwFeed.WriteEndElement();
                //property End
            }
        }

        vw_PersonalWebsite_DisplayTableAdapter DAPersonalWebSite = new vw_PersonalWebsite_DisplayTableAdapter();
        PersonalWebsite.vw_PersonalWebsite_DisplayDataTable DtPersonalWebSite = new PersonalWebsite.vw_PersonalWebsite_DisplayDataTable();
        DAPersonalWebSite.FillPersonalWebsites(DtPersonalWebSite);

        //try
        //{
        //properties Start
        for (int i = 0; i <= DtPersonalWebSite.Rows.Count - 1; i++)
        {
            if (DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim() != ""
                && DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim().ToLower() != "free"
                && Convert.ToBoolean(DtPersonalWebSite.Rows[i]["IsActivated"].ToString()) != false)
            {
                //property Start
                xtwFeed.WriteStartElement("property");


                //Location Start
                xtwFeed.WriteStartElement("location");
                xtwFeed.WriteElementString("unit-number", "");
                xtwFeed.WriteStartElement("street-address");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city-name");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["City"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state-code");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["State"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zipcode");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["ZipCode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("longitude", "");
                xtwFeed.WriteElementString("latitude", "");
                xtwFeed.WriteElementString("geocode-type", "");
                xtwFeed.WriteElementString("display-address", "yes");
                xtwFeed.WriteEndElement();
                //Location End

                //details Start

                xtwFeed.WriteStartElement("details");

                xtwFeed.WriteElementString("price", DtPersonalWebSite.Rows[i]["AskingPrice"].ToString());

                xtwFeed.WriteElementString("year-built", DtPersonalWebSite.Rows[i]["YearBuilt"].ToString().Trim());
                xtwFeed.WriteElementString("num-bedrooms", DtPersonalWebSite.Rows[i]["Bedrooms"].ToString().Trim());
                xtwFeed.WriteElementString("num-full-bathrooms", DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim());
                xtwFeed.WriteElementString("num-half-bathrooms", DtPersonalWebSite.Rows[i]["BathroomPartial"].ToString().Trim());

                xtwFeed.WriteStartElement("lot-size");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["LotSize"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("square-feet");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["SquareFootage"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("date-listed");
                xtwFeed.WriteCData(Convert.ToDateTime(DtPersonalWebSite.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("property-type", DtPersonalWebSite.Rows[i]["PropertyType"].ToString().Trim());

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("mlsId");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["MlsNumber"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("provider-listingid", Convert.ToString(Convert.ToInt32(DtPersonalWebSite.Rows[i]["PersonalWebsiteID"].ToString()) + 100000));
                xtwFeed.WriteEndElement();
                //details End

                //landing-page Start
                xtwFeed.WriteStartElement("landing-page");

                xtwFeed.WriteStartElement("lp-url");
                if (DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString().Trim() != "")
                {
                    if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 2)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 3)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/Singlewebposts.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                }
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("virtual-tour-url");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["VirtualTour"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //landing-page End

                if (DtPersonalWebSite.Rows[i]["IsForSale"].ToString().ToLower() != "true")
                {
                    xtwFeed.WriteElementString("listing-type", "rental");
                    xtwFeed.WriteElementString("status", "for rent");
                }
                else
                {
                    xtwFeed.WriteElementString("listing-type", "");
                    xtwFeed.WriteElementString("status", "for sale");
                }
                xtwFeed.WriteElementString("foreclosure-status", "");

                //site Start
                xtwFeed.WriteStartElement("site");
                xtwFeed.WriteElementString("site-url", clsUtility.SiteHttpWwwRootUrl);
                xtwFeed.WriteElementString("site-name", clsUtility.SiteBrandName);
                xtwFeed.WriteEndElement();
                //site End

                ProfileCommon profile = Profile.GetProfile(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());

                //agent Start
                xtwFeed.WriteStartElement("agent");

                xtwFeed.WriteStartElement("agent-name");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-phone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-email");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-picture-url");
                xtwFeed.WriteCData(((profile.ImageURL.Length) > 0 ? clsUtility.GetRootHost + profile.ImageURL : clsUtility.GetRootHost + "images/noMyPhoto.gif"));
                xtwFeed.WriteEndElement();

                //images/noMyPhoto.gif
                xtwFeed.WriteEndElement();
                //agent End

                //broker Start
                xtwFeed.WriteStartElement("broker");

                xtwFeed.WriteStartElement("broker-name");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("broker-phone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("broker-email");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //broker End

                ////office Start
                //xtwFeed.WriteStartElement("office");
                //xtwFeed.WriteElementString("office-name", "");
                //xtwFeed.WriteElementString("office-phone", "");
                //xtwFeed.WriteElementString("office-email", "");
                //xtwFeed.WriteEndElement();
                ////office End

                ////franchise Start
                //xtwFeed.WriteStartElement("franchise");
                //xtwFeed.WriteElementString("franchise-name", "");
                //xtwFeed.WriteElementString("franchise-phone", "");
                //xtwFeed.WriteElementString("franchise-email", "");
                //xtwFeed.WriteEndElement();
                ////franchise End

                //pictures Start
                xtwFeed.WriteStartElement("pictures");

                fly_SlideShowImagesTableAdapter DA = new fly_SlideShowImagesTableAdapter();
                PersonalWebsite.fly_SlideShowImagesDataTable DtSlideShowImages = new PersonalWebsite.fly_SlideShowImagesDataTable();
                DA.FillSlideShowDisplayBySlideShowId(DtSlideShowImages, Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_SlideShow"].ToString()));

                string[] Folder = DtPersonalWebSite.Rows[i]["EmailAddress"].ToString().Split('@');

                for (int j = 0; j < DtSlideShowImages.Rows.Count; j++)
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/SlideShow/" + Folder[0] + "/" + DtSlideShowImages.Rows[j]["SlideShowImageName"].ToString());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteEndElement();
                //pictures End

                ////open-home Start
                //xtwFeed.WriteStartElement("open-home");
                //xtwFeed.WriteElementString("period1-date", "");
                //xtwFeed.WriteElementString("period1-start-time", "");
                //xtwFeed.WriteElementString("period1-end-time", "");
                //xtwFeed.WriteElementString("period1-details", "");
                //xtwFeed.WriteEndElement();
                ////open-home End

                ////advertise-with-us Start
                //xtwFeed.WriteStartElement("advertise-with-us");
                //xtwFeed.WriteElementString("channel", "");
                //xtwFeed.WriteElementString("featured", "");
                //xtwFeed.WriteElementString("branded", "");
                //xtwFeed.WriteElementString("branded-logo-url", "");
                //xtwFeed.WriteEndElement();
                ////advertise-with-us End

                xtwFeed.WriteEndElement();
                //property End
            }
        }
        
        xtwFeed.WriteEndElement();
            //properties End
        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}
    }


    public void GenerateZillowFeed(ProfileCommon Profile)
    {
        Xml Feed = new Xml();

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/ZillowFeed.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        CultureInfo ci = new CultureInfo("de-DE");
        xtwFeed.WriteStartElement("Listings");

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();
        //try
        //{
        //properties Start
        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("Listing");


                //Location Start
                xtwFeed.WriteStartElement("Location");
                xtwFeed.WriteStartElement("StreetAddress");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("UnitNumber", "");

                xtwFeed.WriteStartElement("City");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("State");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("Zip");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("DisplayAddress", "yes");
                xtwFeed.WriteEndElement();
                //Location End

                //ListingDetails End
                xtwFeed.WriteStartElement("ListingDetails");

                xtwFeed.WriteElementString("Status", "Active");

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }

                xtwFeed.WriteElementString("Price", Price);

                xtwFeed.WriteStartElement("ListingUrl");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("MlsId");
                xtwFeed.WriteCData(OrderTable.Rows[i]["mls_number"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("DateListed");
                xtwFeed.WriteCData(Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("R", ci));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("VirtualTourUrl");
                xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //ListingDetails End

                //BasicDetails Start
                xtwFeed.WriteStartElement("BasicDetails");

                xtwFeed.WriteElementString("PropertyType", GetPropertyTypeForZillow(OrderTable.Rows[i]["PropertyType"].ToString().Trim()));

                xtwFeed.WriteStartElement("Title");
                xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("Description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                //For using old data of custom fields
                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("Bedrooms", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    if (OrderTable.Rows[i]["FullBaths"].ToString().Trim() != "" && OrderTable.Rows[i]["HalfBaths"].ToString().Trim() != "")
                    {
                        if (IsInteger(OrderTable.Rows[i]["FullBaths"].ToString().Trim()) && IsInteger(OrderTable.Rows[i]["HalfBaths"].ToString().Trim()))
                        {
                            if (Convert.ToInt32(OrderTable.Rows[i]["FullBaths"].ToString().Trim()) > 0)
                            {
                                xtwFeed.WriteElementString("Bathrooms", Convert.ToString(Convert.ToInt32(OrderTable.Rows[i]["FullBaths"].ToString().Trim()) + (Convert.ToInt32(OrderTable.Rows[i]["HalfBaths"].ToString().Trim()) / Convert.ToInt32(OrderTable.Rows[i]["FullBaths"].ToString().Trim()))));
                            }
                        }
                    }
                    else
                    {
                        xtwFeed.WriteElementString("Bathrooms", OrderTable.Rows[i]["FullBaths"].ToString().Trim());
                    }
                    xtwFeed.WriteElementString("FullBathrooms", OrderTable.Rows[i]["FullBaths"].ToString().Trim());
                    xtwFeed.WriteElementString("HalfBathrooms", OrderTable.Rows[i]["HalfBaths"].ToString().Trim());

                    xtwFeed.WriteStartElement("LivingArea");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("LotSize");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["LotSize"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("YearBuilt", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteElementString("Bedrooms", dr["Bedrooms"].ToString());

                    if (dr["HalfBaths"].ToString()!= "" && dr["HalfBaths"].ToString() != "")
                    {
                        if (IsInteger(dr["FullBaths"].ToString()) && IsInteger(dr["HalfBaths"].ToString()))
                        {
                            if (Convert.ToInt32(dr["FullBaths"].ToString()) > 0)
                            {
                                xtwFeed.WriteElementString("Bathrooms", Convert.ToString(Convert.ToInt32(dr["FullBaths"].ToString()) + (Convert.ToInt32(dr["HalfBaths"].ToString()) / Convert.ToInt32(dr["FullBaths"].ToString()))));
                            }
                        }
                    }
                    else
                    {
                        xtwFeed.WriteElementString("Bathrooms", dr["FullBaths"].ToString());
                    }
                    
                    xtwFeed.WriteElementString("FullBathrooms", dr["FullBaths"].ToString());
                    xtwFeed.WriteElementString("HalfBathrooms", dr["HalfBaths"].ToString());

                    xtwFeed.WriteStartElement("LivingArea");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("LotSize");
                    xtwFeed.WriteCData(dr["LotSize"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("YearBuilt", dr["YearBuilt"].ToString());
                }

                xtwFeed.WriteEndElement();
                //BasicDetails End

                //Pictures Start
                xtwFeed.WriteStartElement("Pictures");

                //Picture Start
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Picture");
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                    xtwFeed.WriteEndElement();
                }
                //Picture End

                //Picture Start
                if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Picture");
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                    xtwFeed.WriteEndElement();
                }
                //Picture End

                //Picture Start
                if (OrderTable.Rows[i]["photo3"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Picture");
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                    xtwFeed.WriteEndElement();
                }
                //Picture End

                //Picture Start
                if (OrderTable.Rows[i]["photo4"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Picture");
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo4/" + OrderTable.Rows[i]["photo4"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                    xtwFeed.WriteEndElement();
                }
                //Picture End

                //Picture Start
                if (OrderTable.Rows[i]["photo5"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Picture");
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo5/" + OrderTable.Rows[i]["photo5"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                    xtwFeed.WriteEndElement();
                }
                //Picture End

                //Picture Start
                if (OrderTable.Rows[i]["photo6"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Picture");
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo6/" + OrderTable.Rows[i]["photo6"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                    xtwFeed.WriteEndElement();
                }
                //Picture End

                //Picture Start
                if (OrderTable.Rows[i]["photo7"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Picture");
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo7/" + OrderTable.Rows[i]["photo7"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                    xtwFeed.WriteEndElement();
                }
                //Picture End

                xtwFeed.WriteEndElement();
                //Pictures End

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                //Agent Start
                xtwFeed.WriteStartElement("Agent");

                xtwFeed.WriteStartElement("FirstName");
                xtwFeed.WriteCData(profile.FirstName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("LastName");
                xtwFeed.WriteCData(profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("EmailAddress");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("PictureUrl");
                xtwFeed.WriteCData(((profile.ImageURL.Length) > 0 ? clsUtility.GetRootHost + profile.ImageURL : clsUtility.GetRootHost + "images/noMyPhoto.gif"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("OfficeLineNumber");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("MobilePhoneLineNumber");
                xtwFeed.WriteCData(profile.Contact.PhoneCell);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("FaxLineNumber");
                xtwFeed.WriteCData(profile.Contact.Fax);
                xtwFeed.WriteEndElement();
                xtwFeed.WriteEndElement();
                //agent End

                //Office Start
                xtwFeed.WriteStartElement("Office");

                xtwFeed.WriteStartElement("BrokerageName");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("StreetAddress");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageAddress1);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("UnitNumber", "");

                xtwFeed.WriteStartElement("City");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageCity);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("State");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageState);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("Zip");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageZipcode);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //Office End

                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        vw_PersonalWebsite_DisplayTableAdapter DAPersonalWebSite = new vw_PersonalWebsite_DisplayTableAdapter();
        PersonalWebsite.vw_PersonalWebsite_DisplayDataTable DtPersonalWebSite = new PersonalWebsite.vw_PersonalWebsite_DisplayDataTable();
        DAPersonalWebSite.FillPersonalWebsites(DtPersonalWebSite);
        //try
        //{
        //properties Start
        for (int i = 0; i <= DtPersonalWebSite.Rows.Count - 1; i++)
        {
            if (DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim() != ""
                && DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim().ToLower() != "free"
                && Convert.ToBoolean(DtPersonalWebSite.Rows[i]["IsActivated"].ToString()) != false)
            {
                //property Start
                xtwFeed.WriteStartElement("Listing");

                //Location Start
                xtwFeed.WriteStartElement("Location");
                xtwFeed.WriteStartElement("StreetAddress");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("UnitNumber", "");

                xtwFeed.WriteStartElement("City");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["City"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("State");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["State"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("Zip");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("DisplayAddress", "yes");

                xtwFeed.WriteEndElement();
                //Location End

                //ListingDetails End
                xtwFeed.WriteStartElement("ListingDetails");
                if (DtPersonalWebSite.Rows[i]["IsForSale"].ToString().ToLower() != "true")
                {
                    xtwFeed.WriteElementString("Status", "For Rent");
                }
                else
                {
                    xtwFeed.WriteElementString("Status", "Active");
                }
                xtwFeed.WriteElementString("Price", DtPersonalWebSite.Rows[i]["AskingPrice"].ToString());

                xtwFeed.WriteStartElement("ListingUrl");
                if (DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString().Trim() != "")
                {
                    if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 2)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 3)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/Singlewebposts.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                }
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("MlsId");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["MlsNumber"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("DateListed");
                xtwFeed.WriteCData(Convert.ToDateTime(DtPersonalWebSite.Rows[i]["CreateDate"].ToString()).ToString("R", ci));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("VirtualTourUrl");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["VirtualTour"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //ListingDetails End

                //BasicDetails Start
                xtwFeed.WriteStartElement("BasicDetails");

                xtwFeed.WriteElementString("PropertyType", GetPropertyTypeForZillow(DtPersonalWebSite.Rows[i]["PropertyType"].ToString().Trim()));

                xtwFeed.WriteStartElement("Title");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim() + ", " +
                    DtPersonalWebSite.Rows[i]["City"].ToString().Trim() + ", " +
                    DtPersonalWebSite.Rows[i]["State"].ToString().Trim() + " " + DtPersonalWebSite.Rows[i]["ZipCode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("Description");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("Bedrooms", DtPersonalWebSite.Rows[i]["Bedrooms"].ToString().Trim());
                if (DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim() != "0"
                    && DtPersonalWebSite.Rows[i]["BathroomPartial"].ToString().Trim() != "0")
                {
                    if (IsInteger(DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim()) && IsInteger(DtPersonalWebSite.Rows[i]["BathroomPartial"].ToString().Trim()))
                    {
                        if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim()) > 0)
                        {
                            xtwFeed.WriteElementString("Bathrooms", Convert.ToString(Convert.ToInt32(DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim()) +
                                (Convert.ToInt32(DtPersonalWebSite.Rows[i]["BathroomPartial"].ToString().Trim()) / Convert.ToInt32(DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim()))));
                        }
                    }
                }
                else
                {
                    xtwFeed.WriteElementString("Bathrooms", DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim());
                }
                xtwFeed.WriteElementString("FullBathrooms", DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim());
                xtwFeed.WriteElementString("HalfBathrooms", DtPersonalWebSite.Rows[i]["BathroomPartial"].ToString().Trim());

                xtwFeed.WriteStartElement("LivingArea");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["SquareFootage"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("LotSize");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["LotSize"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("YearBuilt", DtPersonalWebSite.Rows[i]["YearBuilt"].ToString().Trim());

                xtwFeed.WriteEndElement();
                //BasicDetails End

                //Pictures Start
                xtwFeed.WriteStartElement("Pictures");

                //Picture Start
                fly_SlideShowImagesTableAdapter DA = new fly_SlideShowImagesTableAdapter();
                PersonalWebsite.fly_SlideShowImagesDataTable DtSlideShowImages = new PersonalWebsite.fly_SlideShowImagesDataTable();
                DA.FillSlideShowDisplayBySlideShowId(DtSlideShowImages, Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_SlideShow"].ToString()));

                string[] Folder = DtPersonalWebSite.Rows[i]["EmailAddress"].ToString().Split('@');

                for (int j = 0; j < DtSlideShowImages.Rows.Count; j++)
                {
                    xtwFeed.WriteStartElement("Picture");
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/SlideShow/" + Folder[0] + "/" + DtSlideShowImages.Rows[j]["SlideShowImageName"].ToString());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteEndElement();
                //Pictures End

                ProfileCommon profile = Profile.GetProfile(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());

                //Agent Start
                xtwFeed.WriteStartElement("Agent");

                xtwFeed.WriteStartElement("FirstName");
                xtwFeed.WriteCData(profile.FirstName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("LastName");
                xtwFeed.WriteCData(profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("EmailAddress");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("PictureUrl");
                xtwFeed.WriteCData(((profile.ImageURL.Length) > 0 ? clsUtility.GetRootHost + profile.ImageURL : clsUtility.GetRootHost + "images/noMyPhoto.gif"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("OfficeLineNumber");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("MobilePhoneLineNumber");
                xtwFeed.WriteCData(profile.Contact.PhoneCell);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("FaxLineNumber");
                xtwFeed.WriteCData(profile.Contact.Fax);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //agent End

                //Office Start
                xtwFeed.WriteStartElement("Office");

                xtwFeed.WriteStartElement("BrokerageName");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("StreetAddress");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageAddress1);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("UnitNumber", "");

                xtwFeed.WriteStartElement("City");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageCity);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("State");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageState);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("Zip");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageZipcode);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //Office End

                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        xtwFeed.WriteEndElement();
        //Listings End
        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}
    }


    public void GeneratePropBotFeed(ProfileCommon Profile)
    {

        StringBuilder strxmlns = new StringBuilder();
        StreamWriter swXML = new StreamWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/PropBot.xml"));
        strxmlns.Append("<rss version='2.0' xmlns:opmxl='http://www.propbot.org'>\n");
        strxmlns.Append("<channel>\n");
        strxmlns.Append("<title>" + clsUtility.SiteTopLevelDomain + "</title>\n");
        strxmlns.Append("<link>" + clsUtility.SiteHttpWwwRootUrl + "</link>\n");
        strxmlns.Append("<description>Effective Real Estate Email Flyer Marketing</description>\n");
        strxmlns.Append("<image>\n");
        strxmlns.Append("<title>FlyerMe</title>\n");
        strxmlns.Append("<url>" + clsUtility.SiteHttpWwwRootUrl + "/images/homeHeader_01.jpg</url>\n");
        strxmlns.Append("<link>" + clsUtility.SiteHttpWwwRootUrl + "</link>\n");
        strxmlns.Append("</image>\n");

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{

        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                strxmlns.Append("<item>\n");

                strxmlns.Append("<title><![CDATA[" + OrderTable.Rows[i]["title"].ToString().Trim() + "]]></title>\n");
                strxmlns.Append("<link><![CDATA[" + clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "]]></link>\n");
                strxmlns.Append("<guid><![CDATA[" + clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "]]></guid>\n");
                strxmlns.Append("<description><![CDATA[" + OrderTable.Rows[i]["prop_desc"].ToString().Trim() + "]]></description>\n");
                CultureInfo ci = new CultureInfo("de-DE");
                strxmlns.Append("<pubDate>" + Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("R", ci) + "</pubDate>\n");
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    strxmlns.Append("<opmxl:image1><![CDATA[" + clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim() + "]]></opmxl:image1>\n");
                }
                else
                {
                    strxmlns.Append("<opmxl:image1><![CDATA[" + clsUtility.GetRootHost + "images/noMyFlyerIconPhoto.jpg]]></opmxl:image1>\n");
                }
                strxmlns.Append("<opmxl:category>" + GetPropertyCategoryForPropBot(OrderTable.Rows[i]["Category"].ToString(), OrderTable.Rows[i]["PropertyType"].ToString()) + "</opmxl:category>\n");
                strxmlns.Append("<opmxl:uniqueId>" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "</opmxl:uniqueId>\n");
                strxmlns.Append("<opmxl:propertytype>" + GetPropertyTypeForPropBot(OrderTable.Rows[i]["PropertyType"].ToString().Trim()) + "</opmxl:propertytype>\n");
                strxmlns.Append("<opmxl:status>Active</opmxl:status>\n");
                strxmlns.Append("<opmxl:address><![CDATA[" + OrderTable.Rows[i]["prop_address1"].ToString().Trim() + "]]></opmxl:address>\n");
                strxmlns.Append("<opmxl:locality><![CDATA[" + OrderTable.Rows[i]["prop_city"].ToString().Trim() + "]]></opmxl:locality>\n");
                strxmlns.Append("<opmxl:province><![CDATA[" + OrderTable.Rows[i]["prop_state"].ToString().Trim() + "]]></opmxl:province>\n");
                strxmlns.Append("<opmxl:postalcode><![CDATA[" + OrderTable.Rows[i]["prop_zipcode"].ToString().Trim() + "]]></opmxl:postalcode>\n");
                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }
                //if (IsInteger(Price))
                //{
                strxmlns.Append("<opmxl:price>" + Price.Replace(",", "").Replace("$", "") + "</opmxl:price>\n");
                //}
                //else
                //{
                  //  strxmlns.Append("<opmxl:price>0.00</opmxl:price>");
                //}
                strxmlns.Append("<opmxl:pricetype>USD</opmxl:pricetype>\n");

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    strxmlns.Append("<opmxl:lotsize><![CDATA[" + OrderTable.Rows[i]["LotSize"].ToString().Trim() + "]]></opmxl:lotsize>\n");
                    strxmlns.Append("<opmxl:lotsizetype>sqfeet</opmxl:lotsizetype>\n");
                    strxmlns.Append("<opmxl:interiorsize><![CDATA[" + OrderTable.Rows[i]["SqFoots"].ToString().Trim() + "]]></opmxl:interiorsize>\n");
                    strxmlns.Append("<opmxl:interiorsizetype>sqfeet</opmxl:interiorsizetype>\n");
                    strxmlns.Append("<opmxl:bedrooms><![CDATA[" + OrderTable.Rows[i]["Bedrooms"].ToString().Trim() + "]]></opmxl:bedrooms>\n");
                    strxmlns.Append("<opmxl:bathrooms><![CDATA[" + OrderTable.Rows[i]["FullBaths"].ToString().Trim() + "]]></opmxl:bathrooms>\n");
                    strxmlns.Append("<opmxl:latitude></opmxl:latitude>\n");
                    strxmlns.Append("<opmxl:longitude></opmxl:longitude>\n");
                    strxmlns.Append("<opmxl:yearbuilt><![CDATA[" + OrderTable.Rows[i]["YearBuilt"].ToString().Trim() + "]]></opmxl:yearbuilt>\n");
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    strxmlns.Append("<opmxl:lotsize><![CDATA[" + dr["LotSize"].ToString() + "]]></opmxl:lotsize>\n");
                    strxmlns.Append("<opmxl:lotsizetype>sqfeet</opmxl:lotsizetype>\n");
                    strxmlns.Append("<opmxl:interiorsize><![CDATA[" + dr["SqFoots"].ToString() + "]]></opmxl:interiorsize>\n");
                    strxmlns.Append("<opmxl:interiorsizetype>sqfeet</opmxl:interiorsizetype>\n");
                    strxmlns.Append("<opmxl:bedrooms><![CDATA[" + dr["Bedrooms"].ToString() + "]]></opmxl:bedrooms>\n");
                    strxmlns.Append("<opmxl:bathrooms><![CDATA[" + dr["FullBaths"].ToString() + "]]></opmxl:bathrooms>\n");
                    strxmlns.Append("<opmxl:latitude></opmxl:latitude>\n");
                    strxmlns.Append("<opmxl:longitude></opmxl:longitude>\n");
                    strxmlns.Append("<opmxl:yearbuilt><![CDATA[" + dr["YearBuilt"].ToString() + "]]></opmxl:yearbuilt>\n");
                }

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                strxmlns.Append("<opmxl:contactname><![CDATA[" + profile.FirstName + " " + profile.LastName + "]]></opmxl:contactname>\n");
                strxmlns.Append("<opmxl:contactphone><![CDATA[" + profile.Contact.PhoneBusiness + "]]></opmxl:contactphone>\n");
                strxmlns.Append("<opmxl:contactemail><![CDATA[" + OrderTable.Rows[i]["customer_id"].ToString().Trim() + "]]></opmxl:contactemail>\n");
                strxmlns.Append("<opmxl:contactdomain><![CDATA[http://www." + OrderTable.Rows[i]["customer_id"].ToString().Substring(OrderTable.Rows[i]["customer_id"].ToString().IndexOf('@') + 1, (OrderTable.Rows[i]["customer_id"].ToString().Length - OrderTable.Rows[i]["customer_id"].ToString().IndexOf('@') - 1)) + "]]></opmxl:contactdomain>\n");
                strxmlns.Append("</item>\n");
            }
        }

        vw_PersonalWebsite_DisplayTableAdapter DAPersonalWebSite = new vw_PersonalWebsite_DisplayTableAdapter();
        PersonalWebsite.vw_PersonalWebsite_DisplayDataTable DtPersonalWebSite = new PersonalWebsite.vw_PersonalWebsite_DisplayDataTable();
        DAPersonalWebSite.FillPersonalWebsites(DtPersonalWebSite);

        //try
        //{

        for (int i = 0; i <= DtPersonalWebSite.Rows.Count - 1; i++)
        {
            if (DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim() != ""
                && DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim().ToLower() != "free"
                && Convert.ToBoolean(DtPersonalWebSite.Rows[i]["IsActivated"].ToString()) != false)
            {
                strxmlns.Append("<item>\n");

                if (DtPersonalWebSite.Rows[i]["Subtitle"].ToString().Trim().Length > 50)
                {
                    strxmlns.Append("<title><![CDATA[" + DtPersonalWebSite.Rows[i]["Subtitle"].ToString().Trim().Substring(0, 25) 
                        + ".......]]></title>\n");
                }
                else
                {
                    strxmlns.Append("<title><![CDATA[" + DtPersonalWebSite.Rows[i]["Subtitle"].ToString().Trim() + "]]></title>\n");
                }

                if (DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString().Trim() != "")
                {
                    if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 2)
                    {
                        strxmlns.Append("<link><![CDATA[http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim() + "]]></link>\n");
                    }
                    else if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 3)
                    {
                        strxmlns.Append("<link><![CDATA[http://webposts." + clsUtility.SiteTopLevelDomain + "/Singlewebposts.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim() + "]]></link>\n");
                    }
                    else
                    {
                        strxmlns.Append("<link><![CDATA[http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim() + "]]></link>\n");
                    }
                }

                strxmlns.Append("<guid><![CDATA[http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim() + "]]></guid>\n");
                strxmlns.Append("<description><![CDATA[" + DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim() + "]]></description>\n");
                CultureInfo ci = new CultureInfo("de-DE");
                strxmlns.Append("<pubDate>" + Convert.ToDateTime(DtPersonalWebSite.Rows[i]["CreateDate"].ToString()).ToString("R", ci) + "</pubDate>\n");

                string[] Folder = DtPersonalWebSite.Rows[i]["EmailAddress"].ToString().Split('@');

                if (DtPersonalWebSite.Rows[i]["WebSiteImage"].ToString().Trim() != "")
                {
                    strxmlns.Append("<opmxl:image1><![CDATA[http://webposts." + clsUtility.SiteTopLevelDomain + "/SlideShow/" + Folder[0] + "/" + "Thumbs100_" + DtPersonalWebSite.Rows[i]["WebSiteImage"].ToString() + "]]></opmxl:image1>\n");
                }
                else
                {
                    strxmlns.Append("<opmxl:image1><![CDATA[http://webposts." + clsUtility.SiteTopLevelDomain + "/images/NoimageHome.jpg]]></opmxl:image1>\n");
                }

                strxmlns.Append("<opmxl:category>" + GetPropertyCategoryForPropBot(DtPersonalWebSite.Rows[i]["Category"].ToString(), DtPersonalWebSite.Rows[i]["PropertyType"].ToString()) + "</opmxl:category>\n");
                strxmlns.Append("<opmxl:uniqueId>" + (Convert.ToInt32(DtPersonalWebSite.Rows[i]["PersonalWebsiteID"].ToString()) + 100000) + "</opmxl:uniqueId>\n");
                strxmlns.Append("<opmxl:propertytype>" + GetPropertyTypeForPropBot(DtPersonalWebSite.Rows[i]["PropertyType"].ToString().Trim()) + "</opmxl:propertytype>\n");
                strxmlns.Append("<opmxl:status>Active</opmxl:status>\n");
                strxmlns.Append("<opmxl:address><![CDATA[" + DtPersonalWebSite.Rows[i]["Street"].ToString().Trim() + "]]></opmxl:address>\n");
                strxmlns.Append("<opmxl:locality><![CDATA[" + DtPersonalWebSite.Rows[i]["City"].ToString().Trim() + "]]></opmxl:locality>\n");
                strxmlns.Append("<opmxl:province><![CDATA[" + DtPersonalWebSite.Rows[i]["State"].ToString().Trim() + "]]></opmxl:province>\n");
                strxmlns.Append("<opmxl:postalcode><![CDATA[" + DtPersonalWebSite.Rows[i]["ZipCode"].ToString().Trim() + "]]></opmxl:postalcode>\n");
                //if (IsInteger(Price))
                //{
                strxmlns.Append("<opmxl:price>" + DtPersonalWebSite.Rows[i]["AskingPrice"].ToString() + "</opmxl:price>\n");
                //}
                //else
                //{
                //  strxmlns.Append("<opmxl:price>0.00</opmxl:price>");
                //}
                strxmlns.Append("<opmxl:pricetype>USD</opmxl:pricetype>\n");

                if (DtPersonalWebSite.Rows[i]["IsForSale"].ToString().ToLower() != "true")
                {
                    strxmlns.Append("<opmxl:renttype>$/Month</opmxl:renttype>\n");
                }

                strxmlns.Append("<opmxl:lotsize><![CDATA[" + DtPersonalWebSite.Rows[i]["LotSize"].ToString().Trim() + "]]></opmxl:lotsize>\n");
                strxmlns.Append("<opmxl:lotsizetype>sqfeet</opmxl:lotsizetype>\n");
                strxmlns.Append("<opmxl:interiorsize><![CDATA[" + DtPersonalWebSite.Rows[i]["SquareFootage"].ToString().Trim() + "]]></opmxl:interiorsize>\n");
                strxmlns.Append("<opmxl:interiorsizetype>sqfeet</opmxl:interiorsizetype>\n");
                strxmlns.Append("<opmxl:bedrooms><![CDATA[" + DtPersonalWebSite.Rows[i]["Bedrooms"].ToString().Trim() + "]]></opmxl:bedrooms>\n");
                strxmlns.Append("<opmxl:bathrooms><![CDATA[" + DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim() + "]]></opmxl:bathrooms>\n");
                strxmlns.Append("<opmxl:latitude></opmxl:latitude>\n");
                strxmlns.Append("<opmxl:longitude></opmxl:longitude>\n");
                strxmlns.Append("<opmxl:yearbuilt><![CDATA[" + DtPersonalWebSite.Rows[i]["YearBuilt"].ToString().Trim() + "]]></opmxl:yearbuilt>\n");

                ProfileCommon profile = Profile.GetProfile(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());
                strxmlns.Append("<opmxl:contactname><![CDATA[" + profile.FirstName + " " + profile.LastName + "]]></opmxl:contactname>\n");
                strxmlns.Append("<opmxl:contactphone><![CDATA[" + profile.Contact.PhoneBusiness + "]]></opmxl:contactphone>\n");
                strxmlns.Append("<opmxl:contactemail><![CDATA[" + DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim() + "]]></opmxl:contactemail>\n");
                strxmlns.Append("<opmxl:contactdomain><![CDATA[http://www." + DtPersonalWebSite.Rows[i]["UserID"].ToString().Substring(DtPersonalWebSite.Rows[i]["UserID"].ToString().IndexOf('@') + 1, (DtPersonalWebSite.Rows[i]["UserID"].ToString().Length - DtPersonalWebSite.Rows[i]["UserID"].ToString().IndexOf('@') - 1)) + "]]></opmxl:contactdomain>\n");
                strxmlns.Append("</item>\n");
            }
        }

        strxmlns.Append("</channel>\n");
        strxmlns.Append("</rss>");

        swXML.Write(strxmlns);
        swXML.Close();
    }


    public void GenerateVastFeed(ProfileCommon Profile)
    {
        /*
        <?xml version="1.0" encoding="utf-8"?>
        <!-- Example of xml feed format to send real estate listings to Vast.com. -->
        <listings>
            <listing>
                <record_id>12345678</record_id>
                <title>1234 Main Street, San Francisco, CA</title>
                <url>http://www.yoursite.com/realestate/12345678.html</url>
                <category>real estate</category>
                <subcategory>single family home</subcategory>
                <image>http://www.yoursite.com/realestate/12345678.jpg</image>
                <address>1234 Main Street</address>
                <city>San Francisco</city>
                <state>CA</state>
                <zip>94105</zip>
                <country>United States</country>
                <bedrooms>4</bedrooms>
                <bathrooms>2</bathrooms>
                <square_footage>1600</square_footage>
                <stories>1</stories>
                <lot_size></lot_size>
                <parking_spots>1</parking_spots>
                <year_built>1999</year_built>
                <currency>USD</currency>
                <price>150,000</price>
                <amenities>dishwasher, fenced yard, air conditioning, garage, garden</amenities>
                <description>Only 12 min to downtown. This house has been totally updated in 2007.</description>
                <listing_time>2007-09-16-12:00:00</listing_time>
                <expire_time>2007-10-16-12:00:00</expire_time>
            </listing>
        </listings> */

        //Xml Feed = new Xml();

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/VastFeed.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        //try
        //{
        //properties Start
        xtwFeed.WriteStartElement("listings");
        CultureInfo ci = new CultureInfo("de-DE");

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("listing");

                //Location Start

                xtwFeed.WriteElementString("record_id", OrderTable.Rows[i]["order_id"].ToString());

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("url");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("category", "real estate");

                xtwFeed.WriteStartElement("subcategory");
                xtwFeed.WriteCData(OrderTable.Rows[i]["PropertyType"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("image");
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                }
                else if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                }
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zip");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("country", "United States");

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("bedrooms", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteElementString("bathrooms", OrderTable.Rows[i]["FullBaths"].ToString().Trim());

                    xtwFeed.WriteStartElement("square_footage");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("stories");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["Floors"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("lot_size");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["LotSize"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("parking_spots");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["Parking"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("year_built", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());

                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteElementString("bedrooms", dr["Bedrooms"].ToString());
                    xtwFeed.WriteElementString("bathrooms", dr["FullBaths"].ToString());

                    xtwFeed.WriteStartElement("square_footage");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("stories", "");

                    xtwFeed.WriteStartElement("lot_size");
                    xtwFeed.WriteCData(dr["LotSize"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("parking_spots", "");

                    xtwFeed.WriteElementString("year_built", dr["YearBuilt"].ToString());
                }


                xtwFeed.WriteElementString("currency", "USD");

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }

                xtwFeed.WriteElementString("price", Price);

                string[] strPropertyFeatures = OrderTable.Rows[i]["PropertyFeatures"].ToString().Split(':');
                string[] strPropertyFeaturesValues = OrderTable.Rows[i]["PropertyFeaturesValues"].ToString().Split(':');

                string strAmenities = "";

                if (strPropertyFeatures.Length >= 30)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        if (Convert.ToBoolean(strPropertyFeaturesValues[j].ToString()) == true)
                        {
                            if (strAmenities.Trim() != "")
                            {
                                strAmenities += ", ";
                            }
                            strAmenities += strPropertyFeatures[j].ToString();
                        }
                    }
                }

                xtwFeed.WriteStartElement("amenities");
                xtwFeed.WriteCData(strAmenities);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listing_time");
                xtwFeed.WriteCData(Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("R", ci));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("expire_time", "");

                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        vw_PersonalWebsite_DisplayTableAdapter DAPersonalWebSite = new vw_PersonalWebsite_DisplayTableAdapter();
        PersonalWebsite.vw_PersonalWebsite_DisplayDataTable DtPersonalWebSite = new PersonalWebsite.vw_PersonalWebsite_DisplayDataTable();
        DAPersonalWebSite.FillPersonalWebsites(DtPersonalWebSite);

        for (int i = 0; i <= DtPersonalWebSite.Rows.Count - 1; i++)
        {
            if (DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim() != ""
                && DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim().ToLower() != "free"
                && Convert.ToBoolean(DtPersonalWebSite.Rows[i]["IsActivated"].ToString()) != false)
            {
                //property Start
                xtwFeed.WriteStartElement("listing");

                //Location Start

                xtwFeed.WriteElementString("record_id", Convert.ToString((Convert.ToInt32(DtPersonalWebSite.Rows[i]["PersonalWebsiteID"].ToString()) + 100000)));

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["City"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["State"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("url");

                if (DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString().Trim() != "")
                {
                    if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 2)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 3)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/Singlewebposts.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                }

                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("category", "real estate");

                xtwFeed.WriteStartElement("subcategory");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["PropertyType"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("image");
                string[] Folder = DtPersonalWebSite.Rows[i]["EmailAddress"].ToString().Split('@');
                if (DtPersonalWebSite.Rows[i]["WebSiteImage"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/SlideShow/" + Folder[0] + "/" + "Thumbs100_" + DtPersonalWebSite.Rows[i]["WebSiteImage"].ToString());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                }
                else
                {
                    xtwFeed.WriteStartElement("PictureUrl");
                    xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/images/NoimageHome.jpg");
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Caption", "");
                }
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["City"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["State"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zip");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["ZipCode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("country", "United States");

                xtwFeed.WriteElementString("bedrooms", DtPersonalWebSite.Rows[i]["Bedrooms"].ToString().Trim());
                xtwFeed.WriteElementString("bathrooms", DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim());

                xtwFeed.WriteStartElement("square_footage");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["SquareFootage"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("stories");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Floors"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("lot_size");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["LotSize"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("parking_spots");
                xtwFeed.WriteCData("1");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("year_built", DtPersonalWebSite.Rows[i]["YearBuilt"].ToString().Trim());

                xtwFeed.WriteElementString("currency", "USD");

                xtwFeed.WriteElementString("price", DtPersonalWebSite.Rows[i]["AskingPrice"].ToString());

                fly_PersonalWebSiteFeaturesTableAdapter DAPersonalWebSiteFeature = new fly_PersonalWebSiteFeaturesTableAdapter();
                PersonalWebsite.fly_PersonalWebSiteFeaturesDataTable DtPersonalWebSiteFeature = new PersonalWebsite.fly_PersonalWebSiteFeaturesDataTable();
                DAPersonalWebSiteFeature.FillPersonalWebSiteFeaturesByPersonalWebSiteIDFeaturesID(DtPersonalWebSiteFeature, 
                    Convert.ToInt64(DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString()), 1);

                string strAmenities = "";

                for (int j = 0; j < DtPersonalWebSiteFeature.Rows.Count; j++)
                {
                    if (strAmenities.Trim() != "")
                    {
                        strAmenities += ", ";
                    }
                    strAmenities += DtPersonalWebSiteFeature.Rows[j]["FeaturesOptions"].ToString().Trim();
                }

                xtwFeed.WriteStartElement("amenities");
                xtwFeed.WriteCData(strAmenities);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listing_time");
                xtwFeed.WriteCData(Convert.ToDateTime(DtPersonalWebSite.Rows[i]["CreateDate"].ToString()).ToString("R", ci));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("expire_time", "");

                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        xtwFeed.WriteEndElement();
        //Listings End
        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}
    }


    public void GenerateoodleFeed(ProfileCommon Profile)
    {
        /*
        <listings>
         <listing>
          <agent>Jamie Davis</agent> 
          <amenities>cable, fireplace, internet, parking, refrigerator, washer dryer</amenities> 
          <balconies>3</balconies> 
          <bathrooms>2</bathrooms> 
          <bedrooms>4</bedrooms> 
          <broker>Jason Tevas</broker> 
          <condition>existing</condition> 
          <create_time>2004-07-16T14:00:00-07:00</create_time> 
          <currency>USD</currency> 
          <description>Fairly new home, set on a large yard. Good schools, friendly neighborhood. Large tree in the back yard.</description> 
          <event_date>2004-09-16</event_date> 
          <expire_time>2004-09-16T14:00:00-07:00</expire_time> 
          <facing>north</facing> 
          <fee>yes</fee> 
          <furnished>Partly Furnished</furnished> 
          <image_url>http://www.example.com/listing/image1.jpg|http://www.example.com/listing/image2.jpg</image_url> 
          <ip_address>142.72.8.120</ip_address> 
          <lot_size>4 acres</lot_size> 
          <lot_size_units>acres</lot_size_units> 
          <mls_id>161713</mls_id> 
          <price>398750</price> 
          <registration>yes</registration> 
          <seller_email>joe@smith.com</seller_email> 
          <seller_name>Joe Smith</seller_name> 
          <seller_type>broker</seller_type> 
          <seller_url>http://smith-classifieds.com/</seller_url> 
          <square_feet>2400</square_feet> 
          <vastu_compliant>yes</vastu_compliant> 
          <year>2001</year> 
          </listing>
          </listings>
         */

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/oodleFeed.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        CultureInfo ci = new CultureInfo("de-DE");
        xtwFeed.WriteStartElement("listings");

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{
        //properties Start
        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("listing");

                //Location Start

                xtwFeed.WriteStartElement("category");
                xtwFeed.WriteCData(OrderTable.Rows[i]["PropertyType"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("id", OrderTable.Rows[i]["order_id"].ToString());

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("url");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("country", "US");

                xtwFeed.WriteElementString("neighborhood", "");

                xtwFeed.WriteStartElement("state");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zip_code");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();


                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteStartElement("Agent");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                string[] strPropertyFeatures = OrderTable.Rows[i]["PropertyFeatures"].ToString().Split(':');
                string[] strPropertyFeaturesValues = OrderTable.Rows[i]["PropertyFeaturesValues"].ToString().Split(':');

                string strAmenities = "";

                if (strPropertyFeatures.Length >= 30)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        if (Convert.ToBoolean(strPropertyFeaturesValues[j].ToString()) == true)
                        {
                            if (strAmenities.Trim() != "")
                            {
                                strAmenities += ", ";
                            }
                            strAmenities += strPropertyFeatures[j].ToString();
                        }
                    }
                }

                xtwFeed.WriteStartElement("amenities");
                xtwFeed.WriteCData(strAmenities);
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteElementString("balconies", "");

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("bathrooms", OrderTable.Rows[i]["FullBaths"].ToString().Trim());
                    xtwFeed.WriteElementString("bedrooms", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteElementString("bedrooms", dr["Bedrooms"].ToString());
                    xtwFeed.WriteElementString("bathrooms", dr["FullBaths"].ToString());
                }

                xtwFeed.WriteStartElement("broker");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("condition", "existing");


                xtwFeed.WriteStartElement("create_time");
                xtwFeed.WriteCData(Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("yyyy-MM-dd"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("currency", "USD");

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("event_date");
                xtwFeed.WriteCData(Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("yyyy-MM-dd"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("expire_time", "");

                //xtwFeed.WriteElementString("facing", "");
                xtwFeed.WriteElementString("fee", "no");
                //xtwFeed.WriteElementString("furnished", "");

                //xtwFeed.WriteElementString("id", OrderTable.Rows[i]["order_id"].ToString());

                xtwFeed.WriteStartElement("image_url");
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                }
                else
                {
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                }
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("ip_address", "");

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("lot_size");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["LotSize"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("lot_size_units", "acres");
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteStartElement("lot_size");
                    xtwFeed.WriteCData(dr["LotSize"].ToString());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("lot_size_units", "acres");
                }

                xtwFeed.WriteStartElement("mls_id");
                xtwFeed.WriteCData(OrderTable.Rows[i]["mls_number"].ToString().Trim());
                xtwFeed.WriteEndElement();

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }

                xtwFeed.WriteElementString("price", Price);

                xtwFeed.WriteElementString("registration", "no");

                xtwFeed.WriteStartElement("seller_email");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("seller_name");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("seller_type", "broker");
                xtwFeed.WriteElementString("seller_url", clsUtility.SiteHttpWwwRootUrl);

                // vastu_compliant is for only India
                //xtwFeed.WriteElementString("vastu_compliant", "yes");

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("square_feet");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);
                    xtwFeed.WriteStartElement("square_feet");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("year");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);
                    xtwFeed.WriteStartElement("year");
                    xtwFeed.WriteCData(dr["YearBuilt"].ToString());
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        vw_PersonalWebsite_DisplayTableAdapter DAPersonalWebSite = new vw_PersonalWebsite_DisplayTableAdapter();
        PersonalWebsite.vw_PersonalWebsite_DisplayDataTable DtPersonalWebSite = new PersonalWebsite.vw_PersonalWebsite_DisplayDataTable();
        DAPersonalWebSite.FillPersonalWebsites(DtPersonalWebSite);

        //try
        //{
        //properties Start
        for (int i = 0; i <= DtPersonalWebSite.Rows.Count - 1; i++)
        {
            if (DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim() != ""
                && DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim().ToLower() != "free"
                && Convert.ToBoolean(DtPersonalWebSite.Rows[i]["IsActivated"].ToString()) != false)
            {
                //property Start
                xtwFeed.WriteStartElement("listing");

                //Location Start

                xtwFeed.WriteStartElement("category");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["PropertyType"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("id", Convert.ToString(Convert.ToInt32(DtPersonalWebSite.Rows[i]["PersonalWebsiteID"].ToString()) + 100000));

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim() + ", " +
                    DtPersonalWebSite.Rows[i]["City"].ToString().Trim() + ", " +
                    DtPersonalWebSite.Rows[i]["State"].ToString().Trim() + " " + 
                    DtPersonalWebSite.Rows[i]["ZipCode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("url");
                if (DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString().Trim() != "")
                {
                    if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 2)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 3)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/Singlewebposts.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                }
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["City"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("country", "US");

                xtwFeed.WriteElementString("neighborhood", "");

                xtwFeed.WriteStartElement("state");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["State"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zip_code");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["ZipCode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                ProfileCommon profile = Profile.GetProfile(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());

                xtwFeed.WriteStartElement("Agent");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                fly_PersonalWebSiteFeaturesTableAdapter DAPersonalWebSiteFeature = new fly_PersonalWebSiteFeaturesTableAdapter();
                PersonalWebsite.fly_PersonalWebSiteFeaturesDataTable DtPersonalWebSiteFeature = new PersonalWebsite.fly_PersonalWebSiteFeaturesDataTable();
                DAPersonalWebSiteFeature.FillPersonalWebSiteFeaturesByPersonalWebSiteIDFeaturesID(DtPersonalWebSiteFeature,
                    Convert.ToInt64(DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString()), 1);

                string strAmenities = "";

                for (int j = 0; j < DtPersonalWebSiteFeature.Rows.Count; j++)
                {
                    if (strAmenities.Trim() != "")
                    {
                        strAmenities += ", ";
                    }
                    strAmenities += DtPersonalWebSiteFeature.Rows[j]["FeaturesOptions"].ToString().Trim();
                }

                xtwFeed.WriteStartElement("amenities");
                xtwFeed.WriteCData(strAmenities);
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteElementString("balconies", "");

                xtwFeed.WriteElementString("bathrooms", DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim());
                xtwFeed.WriteElementString("bedrooms", DtPersonalWebSite.Rows[i]["Bedrooms"].ToString().Trim());

                xtwFeed.WriteStartElement("broker");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("condition", "existing");


                xtwFeed.WriteStartElement("create_time");
                xtwFeed.WriteCData(Convert.ToDateTime(DtPersonalWebSite.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("currency", "USD");

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("event_date");
                xtwFeed.WriteCData(Convert.ToDateTime(DtPersonalWebSite.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("expire_time", "");

                //xtwFeed.WriteElementString("facing", "");
                xtwFeed.WriteElementString("fee", "no");
                //xtwFeed.WriteElementString("furnished", "");

                //xtwFeed.WriteElementString("id", OrderTable.Rows[i]["order_id"].ToString());

                xtwFeed.WriteStartElement("image_url");

                if (DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString().Trim() != "")
                {
                    if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 2)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 3)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/Singlewebposts.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                }

                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("ip_address", "");

                xtwFeed.WriteStartElement("lot_size");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["LotSize"].ToString().Trim());
                xtwFeed.WriteEndElement();
                xtwFeed.WriteElementString("lot_size_units", "acres");

                xtwFeed.WriteStartElement("mls_id");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["MlsNumber"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("price", DtPersonalWebSite.Rows[i]["AskingPrice"].ToString());

                xtwFeed.WriteElementString("registration", "no");

                xtwFeed.WriteStartElement("seller_email");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["UserID"].ToString());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("seller_name");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("seller_type", "broker");
                xtwFeed.WriteElementString("seller_url", clsUtility.SiteHttpWwwRootUrl);

                // vastu_compliant is for only India
                //xtwFeed.WriteElementString("vastu_compliant", "yes");

                xtwFeed.WriteStartElement("square_feet");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["SquareFootage"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("year");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["YearBuilt"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        xtwFeed.WriteEndElement();
        //Listings End
        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}
    }


    public void GenerateRealeStateAdvisorFeed(ProfileCommon Profile)
    {
        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/RealeStateAdvisor.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{
        //properties Start
        xtwFeed.WriteStartElement("properties");
        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("property");

                //Location Start
                xtwFeed.WriteStartElement("location");

                xtwFeed.WriteElementString("longitude", "");
                xtwFeed.WriteElementString("latitude", "");
                xtwFeed.WriteElementString("street-number", "");

                xtwFeed.WriteStartElement("street-name");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city-name");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("county", "");

                xtwFeed.WriteStartElement("state-code");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zipcode");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //Location End

                //Details Start
                xtwFeed.WriteStartElement("details");

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }

                xtwFeed.WriteElementString("price", Price);

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("year-built");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    
                    xtwFeed.WriteStartElement("num-bedrooms");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("num-bathrooms");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["FullBaths"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteStartElement("year-built");
                    xtwFeed.WriteCData(dr["YearBuilt"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("num-bedrooms");
                    xtwFeed.WriteCData(dr["Bedrooms"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("num-bathrooms");
                    xtwFeed.WriteCData(dr["FullBaths"].ToString());
                    xtwFeed.WriteEndElement();
                }


                xtwFeed.WriteElementString("apartment-number", "");

                xtwFeed.WriteStartElement("date-listed");
                xtwFeed.WriteCData(OrderTable.Rows[i]["created_on"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("date-sold", "");

                xtwFeed.WriteElementString("listingID", OrderTable.Rows[i]["order_id"].ToString());

                xtwFeed.WriteEndElement();
                //Details End

                //Agent Start
                xtwFeed.WriteStartElement("agent");

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteStartElement("agent-name");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("broker-name");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("phone-number");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("email");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //Agent End

                //Status Start
                xtwFeed.WriteStartElement("status");
                xtwFeed.WriteElementString("for-sale", "1");
                xtwFeed.WriteElementString("sold", "0");
                xtwFeed.WriteElementString("pending", "0");
                xtwFeed.WriteEndElement();
                //Status End

                //property-type Start
                xtwFeed.WriteStartElement("property-type");
                xtwFeed.WriteElementString("apartment-condo-townhouse", GetPropertyTypeForRealeStateAdvisor(1, OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteElementString("mobile-manufactured", GetPropertyTypeForRealeStateAdvisor(2, OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteElementString("farm-ranch", GetPropertyTypeForRealeStateAdvisor(3, OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteElementString("multi-family", GetPropertyTypeForRealeStateAdvisor(4, OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteElementString("lot-land", GetPropertyTypeForRealeStateAdvisor(5, OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteElementString("single-family-home", GetPropertyTypeForRealeStateAdvisor(6, OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteEndElement();
                //Status End

                //pictures Start
                xtwFeed.WriteStartElement("pictures");
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                xtwFeed.WriteEndElement();
                //pictures End

                //landing-page Start
                xtwFeed.WriteStartElement("landing-page");
                xtwFeed.WriteStartElement("url");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();
                xtwFeed.WriteStartElement("has-virtual-tour");
                xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                xtwFeed.WriteEndElement();
                xtwFeed.WriteEndElement();
                //pictures End
                
                xtwFeed.WriteEndElement();
                //property End
            }
        }
        xtwFeed.WriteEndElement();
        //property End
        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}
    }


    public void GeneratePropSmartFeed(ProfileCommon Profile)
    {
        /*
        <?xml version="1.0"?>
        <rss version="2.0" xmlns:ps="http://www.propsmart.com/datafeed">
        <channel>
        <title>XYZ Realty RSS 2.0 Feed</title>

        <link>http://www.xyzrealty.com/rss2feed/propsmart.php</link>
        <description>A RSS 2.0 data feed used by Propsmart to add properties.</description>
          <item>
            <title>Beautiful 2 story victorian house</title>
            <description>Nestled on a beautifully landscaped...</description>

            <ps:address>123 main street</ps:address>
            <ps:city>Kansas City</ps:city>
            <ps:state>MO</ps:state>
            <ps:price>105000</ps:price>

            <ps:bedroom>4</ps:bedroom>
            <ps:bathroom>2.5</ps:bathroom>
            <ps:image1>http://www.xyzrealty.com/image/123-main-1.jpg</ps:image1>
            <ps:image2>http://www.xyzrealty.com/image/123-main-2.jpg</ps:image2>
            <ps:image3>http://www.xyzrealty.com/image/123-main-3.jpg</ps:image3>
          </item>
        </channel>
        </rss>         
         */

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/PropSmart.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{
        //rss Start
        xtwFeed.WriteStartElement("rss");
        xtwFeed.WriteAttributeString("version", "2.0");
        xtwFeed.WriteAttributeString("xmlns:ps", "http://www.propsmart.com/datafeed");
        //channel Start
        xtwFeed.WriteStartElement("channel");
        xtwFeed.WriteElementString("title", clsUtility.ProjectName);
        xtwFeed.WriteElementString("link", clsUtility.SiteHttpWwwRootUrl + "/XMLFeeds/PropSmart.xml");
        xtwFeed.WriteElementString("description", "Efactive Real Estate Email Flyer Marketing");

        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //item Start
                xtwFeed.WriteStartElement("item");

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("ps:address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("ps:city");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("ps:state");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("ps:postalcode");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }
                xtwFeed.WriteElementString("ps:price", Price);

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("ps:bedroom", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteElementString("ps:bathroom", OrderTable.Rows[i]["FullBaths"].ToString().Trim());

                    string[] SqFeet = OrderTable.Rows[i]["SqFoots"].ToString().Split(' ');

                    xtwFeed.WriteElementString("ps:squarefeet", SqFeet[0]);
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteElementString("ps:bedroom", dr["Bedrooms"].ToString());
                    xtwFeed.WriteElementString("ps:bathroom", dr["FullBaths"].ToString());

                    string[] SqFeet = dr["SqFoots"].ToString().Split(' ');

                    xtwFeed.WriteElementString("ps:squarefeet", SqFeet[0]);
                }

                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ps:image1");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ps:image2");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo3"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ps:image3");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo4"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ps:image4");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo4/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteElementString("ps:contactemail", OrderTable.Rows[i]["customer_id"].ToString());

                xtwFeed.WriteEndElement();
                //item End
            }
        }
        xtwFeed.WriteEndElement();
        //channel End

        xtwFeed.WriteEndElement();
        //rss End

        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}
    }


    public void GenerateHomeScapeFeed(ProfileCommon Profile)
    {
        /*
            <LISTING>
            <IDNUMBER>60898</IDNUMBER>
            <CITY>Avondale</CITY>
            <STATE>CA</STATE>
            <LISTPRICE>329900</LISTPRICE>
            <OFFICECODE>1513</OFFICECODE>
            <OFFICEVOICENUM>215.996.1390</OFFICEVOICENUM>
            <AGENTCODE>1513</AGENTCODE>
            <AGENTNAME>Adam McCambridge</AGENTNAME>
            <PROPTYPE_NAME_EXACT>LAND</PROPTYPE_NAME_EXACT>
            <AGENTVOICENUM>215.996.1390</AGENTVOICENUM>
            <AGENTEMAIL>perryapg@gmail.com</AGENTEMAIL>
            <OFFICEURL>http://www.real-estate-agent-web-site.com</OFFICEURL>
            <AGENTURL>http://www.real-estate-agent-web-site.com</AGENTURL>
            <COUNTY>US</COUNTY>
            <ADDRESS>Avondale Blvd. & Durango</ADDRESS>
            <ZIPCODE>85323</ZIPCODE>
            <BEDROOM_QTY>5</BEDROOM_QTY>
            <TOTALBATH_QTY>4</TOTALBATH_QTY>
                <IMAGE>
            http://www.agentpanelgold2.com/accounts/webagentsolutions/photos/60898/320960_mt.jpg
            </IMAGE>
            <PROP_SQFT>2646</PROP_SQFT>
                <VIRTUALTOURURL>
            http://www.real-estate-agent-web-site.com/custompages_propdetail/60898.htm
            </VIRTUALTOURURL>
            </LISTING>         
         */
        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/Homescape.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{
        //LISTINGS Start
        xtwFeed.WriteStartElement("LISTINGS");

        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //item Start
                xtwFeed.WriteStartElement("LISTING");

                xtwFeed.WriteStartElement("IDNUMBER");
                xtwFeed.WriteCData(OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }
                xtwFeed.WriteElementString("LISTPRICE", Price);

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteElementString("OFFICECODE", OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteElementString("OFFICENAME", OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteStartElement("OFFICEVOICENUM");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("OFFICEVOICENUMEXT");
                xtwFeed.WriteCData("1");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("AGENTCODE", OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteStartElement("AGENTNAME");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("BROKERZIP");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("PROPTYPE_NAME_EXACT");
                xtwFeed.WriteCData(GetPropertyTypeForHomeSpace(OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("AGENTVOICENUM");
                xtwFeed.WriteCData(profile.Contact.PhoneCell);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("AGENTEMAIL");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("OFFICEURL");
                xtwFeed.WriteCData(clsUtility.SiteHttpWwwRootUrl);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("AGENTURL");
                xtwFeed.WriteCData(clsUtility.SiteHttpWwwRootUrl);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("ADDRESS");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("CITY");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("STATE");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("ZIPCODE");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("BEDROOM_QTY", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteElementString("TOTALBATH_QTY", OrderTable.Rows[i]["FullBaths"].ToString().Trim());

                    xtwFeed.WriteStartElement("IMAGE");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    string[] SqFeet = OrderTable.Rows[i]["SqFoots"].ToString().Split(' ');
                    xtwFeed.WriteElementString("PROP_SQFT", SqFeet[0]);
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteElementString("BEDROOM_QTY", dr["Bedrooms"].ToString());
                    xtwFeed.WriteElementString("TOTALBATH_QTY", dr["FullBaths"].ToString());

                    xtwFeed.WriteStartElement("IMAGE");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    string[] SqFeet = dr["SqFoots"].ToString().Split(' ');
                    xtwFeed.WriteElementString("PROP_SQFT", SqFeet[0]);
                }

                xtwFeed.WriteStartElement("VIRTUALTOURURL");
                xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //LISTINGS End
            }
        }
        xtwFeed.WriteEndElement();
        //LISTINGS End

        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}

    }


    public void GenerateBackPageFeed(ProfileCommon Profile)
    {
        /*
         * 
			<rss version="2.0" xmlns:content="http://blogs.law.harvard.edu/tech/rss" xmlns:backpage="http://www.backpage.com/rss/2.0">
			<channel>  
				<title>webagentsolutions.com</title>
				<webMaster>support@webagentsolutions.com</webMaster>
				<link>http://www.webagentsolutions.com</link>
				<description>CRM Technology for Successful Agents and Brokers</description>
				<language>en-us</language>
				<lastBuildDate></lastBuildDate>
				<copyright>Copyright 2007, WebAgentSolutions</copyright>
				<ttl>120</ttl>
				<backpage:import user="myname@mydomain.com" site="http://newyork.backpage.com" />
                <property>
                    <landing-page>
                        <lp-url><![CDATA['.$strProLink.']]></lp-url>
                        <virtual-tour-url><![CDATA['.$strProLink.']]></virtual-tour-url>
                    </landing-page>
                    <listing-type>foreclosure</listing-type>
                    <status>'.$strProListType.'</status>
                    <foreclosure-status>Notice of Default</foreclosure-status>
                    <site>
                        <site-url><![CDATA['.$strSiteURL.']]></site-url>
                        <site-name><![CDATA['.$strSiteName.']]></site-name>
                    </site>
                    <agent>
                        <agent-name><![CDATA['.$strBrokerName.']]></agent-name>
                        <agent-phone><![CDATA['.$strBrokerPhoneNo.']]></agent-phone>
                        <agent-email><![CDATA['.$strBrokerEmail.']]></agent-email>
                        <agent-picture-url></agent-picture-url>
                    </agent>
                    <broker>
                        <broker-name><![CDATA['.$strBrokerName.']]></broker-name>
                        <broker-phone><![CDATA['.$strBrokerPhoneNo.']]></broker-phone>
                        <broker-email><![CDATA['.$strBrokerEmail.']]></broker-email>
                    </broker>
                    <pictures>
                        <picture>
                            <picture-url><![CDATA['.$strProImagePath.']]></picture-url>
                        </picture>
                    </pictures>
                </property>
         */
        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("BackPage.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{
        //rss Start
        xtwFeed.WriteStartElement("rss");
        xtwFeed.WriteAttributeString("version", "2.0");
        xtwFeed.WriteAttributeString("xmlns:content", "http://blogs.law.harvard.edu/tech/rss");
        xtwFeed.WriteAttributeString("xmlns:backpage", "http://www.backpage.com/rss/2.0");
        xtwFeed.WriteElementString("webMaster", "");
        xtwFeed.WriteElementString("link", clsUtility.SiteHttpWwwRootUrl);
        xtwFeed.WriteElementString("description", "Efactive Real Estate Email Flyer Marketing");
        xtwFeed.WriteElementString("language", "en-us");
        xtwFeed.WriteElementString("lastBuildDate", "");
        xtwFeed.WriteElementString("copyright", "Copyrights © 2007 " + clsUtility.SiteBrandName + " All rights Reserved");
        xtwFeed.WriteElementString("ttl", "");
        xtwFeed.WriteStartElement("backpage:import");
        xtwFeed.WriteAttributeString("user", "myname@mydomain.com");
        xtwFeed.WriteAttributeString("site", "http://newyork.backpage.com");

        //properties Start
        xtwFeed.WriteStartElement("channel");
        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("property");

                /*
                 */

                //Location Start
                xtwFeed.WriteStartElement("location");
                xtwFeed.WriteElementString("unit-number", "");
                xtwFeed.WriteStartElement("street-address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city-name");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state-code");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zipcode");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("longitude", "");
                xtwFeed.WriteElementString("latitude", "");
                xtwFeed.WriteElementString("geocode-type", "");
                xtwFeed.WriteElementString("display-address", "yes");
                xtwFeed.WriteEndElement();
                //Location End

                //details Start

                xtwFeed.WriteStartElement("details");

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }

                xtwFeed.WriteElementString("price", Price);


                //For using old data of custom fields
                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("year-built", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                    xtwFeed.WriteElementString("num-bedrooms", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteElementString("num-full-bathrooms", OrderTable.Rows[i]["FullBaths"].ToString().Trim());
                    xtwFeed.WriteElementString("num-half-bathrooms", OrderTable.Rows[i]["HalfBaths"].ToString().Trim());

                    xtwFeed.WriteStartElement("lot-size");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["LotSize"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("square-feet");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);
                    xtwFeed.WriteElementString("year-built", dr["YearBuilt"].ToString());
                    xtwFeed.WriteElementString("num-bedrooms", dr["Bedrooms"].ToString());
                    xtwFeed.WriteElementString("num-full-bathrooms", dr["FullBaths"].ToString());
                    xtwFeed.WriteElementString("num-half-bathrooms", dr["HalfBaths"].ToString());

                    xtwFeed.WriteStartElement("lot-size");
                    xtwFeed.WriteCData(dr["LotSize"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("square-feet");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteElementString("date-listed", OrderTable.Rows[i]["created_on"].ToString().Trim());
                xtwFeed.WriteElementString("property-type", OrderTable.Rows[i]["PropertyType"].ToString().Trim());

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("mlsId");
                xtwFeed.WriteCData(OrderTable.Rows[i]["mls_number"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("provider-listingid", "");
                xtwFeed.WriteEndElement();
                //details End

                //landing-page Start
                xtwFeed.WriteStartElement("landing-page");

                xtwFeed.WriteStartElement("lp-url");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("virtual-tour-url");
                xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //landing-page End

                xtwFeed.WriteElementString("listing-type", "");
                xtwFeed.WriteElementString("status", "For Sale");
                xtwFeed.WriteElementString("foreclosure-status", "");

                //site Start
                xtwFeed.WriteStartElement("site");
                xtwFeed.WriteElementString("site-url", clsUtility.SiteHttpWwwRootUrl);
                xtwFeed.WriteElementString("site-name", clsUtility.ProjectName);
                xtwFeed.WriteEndElement();
                //site End

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());


                //agent Start
                xtwFeed.WriteStartElement("agent");

                xtwFeed.WriteStartElement("agent-name");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-phone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-email");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-picture-url");
                xtwFeed.WriteCData(((profile.ImageURL.Length) > 0 ? clsUtility.GetRootHost + profile.ImageURL : clsUtility.GetRootHost + "images/noMyPhoto.gif"));
                xtwFeed.WriteEndElement();

                //images/noMyPhoto.gif
                xtwFeed.WriteEndElement();
                //agent End

                //broker Start
                xtwFeed.WriteStartElement("broker");

                xtwFeed.WriteStartElement("broker-name");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("broker-phone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("broker-email");
                xtwFeed.WriteCData(profile.Contact.EmailSecondary);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //broker End

                ////office Start
                //xtwFeed.WriteStartElement("office");
                //xtwFeed.WriteElementString("office-name", "");
                //xtwFeed.WriteElementString("office-phone", "");
                //xtwFeed.WriteElementString("office-email", "");
                //xtwFeed.WriteEndElement();
                ////office End

                ////franchise Start
                //xtwFeed.WriteStartElement("franchise");
                //xtwFeed.WriteElementString("franchise-name", "");
                //xtwFeed.WriteElementString("franchise-phone", "");
                //xtwFeed.WriteElementString("franchise-email", "");
                //xtwFeed.WriteEndElement();
                ////franchise End

                //pictures Start
                xtwFeed.WriteStartElement("pictures");

                //pictures/picture Start
                xtwFeed.WriteStartElement("picture");
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                xtwFeed.WriteEndElement();
                //picture End

                //pictures/picture Start
                xtwFeed.WriteStartElement("picture");
                if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                xtwFeed.WriteEndElement();
                //picture End

                //pictures/picture Start
                xtwFeed.WriteStartElement("picture");
                if (OrderTable.Rows[i]["photo3"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                xtwFeed.WriteEndElement();
                //picture End

                //pictures/picture Start
                xtwFeed.WriteStartElement("picture");
                if (OrderTable.Rows[i]["photo4"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo4/" + OrderTable.Rows[i]["photo4"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                xtwFeed.WriteEndElement();
                //picture End

                //pictures/picture Start
                xtwFeed.WriteStartElement("picture");
                if (OrderTable.Rows[i]["photo5"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo5/" + OrderTable.Rows[i]["photo5"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                xtwFeed.WriteEndElement();
                //picture End

                //pictures/picture Start
                xtwFeed.WriteStartElement("picture");
                if (OrderTable.Rows[i]["photo6"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo6/" + OrderTable.Rows[i]["photo6"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                xtwFeed.WriteEndElement();
                //picture End

                //pictures/picture Start
                xtwFeed.WriteStartElement("picture");
                if (OrderTable.Rows[i]["photo7"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo7/" + OrderTable.Rows[i]["photo7"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                xtwFeed.WriteEndElement();
                //picture End

                xtwFeed.WriteEndElement();
                //pictures End

                ////open-home Start
                //xtwFeed.WriteStartElement("open-home");
                //xtwFeed.WriteElementString("period1-date", "");
                //xtwFeed.WriteElementString("period1-start-time", "");
                //xtwFeed.WriteElementString("period1-end-time", "");
                //xtwFeed.WriteElementString("period1-details", "");
                //xtwFeed.WriteEndElement();
                ////open-home End

                ////advertise-with-us Start
                //xtwFeed.WriteStartElement("advertise-with-us");
                //xtwFeed.WriteElementString("channel", "");
                //xtwFeed.WriteElementString("featured", "");
                //xtwFeed.WriteElementString("branded", "");
                //xtwFeed.WriteElementString("branded-logo-url", "");
                //xtwFeed.WriteEndElement();
                ////advertise-with-us End

                xtwFeed.WriteEndElement();
                //property End
            }
        }
        xtwFeed.WriteEndElement();
        //chennal End

        xtwFeed.WriteEndElement();
        //rss End

        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}

    }


    public void GenerateCLRSearchFeed(ProfileCommon Profile)
    {
        /*
         * 
			<clrsearch xmlns="http://www.clrsearch.com/schemas/propertyFeed">
			<feed_created><![CDATA['.$strCurrentDate.']]></feed_created>
			<property_list>
                        <property id="'.$arrFetchProperty["propertyid"].'">
                            <listed_date><![CDATA['.$arrFetchProperty["listingdate"].']]></listed_date>
                            <status><![CDATA['.$strProListType.']]></status>
                            <type><![CDATA['.$strPropertyType.']]></type>
                            <beds>'.$intBedrooms.'</beds>
                            <baths>'.$intBathRoom.'</baths>
                            <sqft>'.$strSqFeet.'</sqft>
                            <garage></garage>
                            <price>'.$arrFetchProperty["listprice"].'</price>
                            <address><![CDATA['.$arrFetchProperty["streetaddress"].']]></address>
                            <city><![CDATA['.@$arrFetchProperty["city"].']]></city>
                            <state_abbr>'.$strStateCode.'</state_abbr>
                            <zipcode>'.$strZipCode.'</zipcode>
                            <pool></pool>
                            <acreage></acreage>
                            <description><![CDATA['.$strDescription.']]></description>
                            <mlsid>'.$arrFetchProperty["mlsnumber"].'</mlsid>
                            <yearbuilt>'.$strYearBuilt.'</yearbuilt>
                            <listingphone><![CDATA['.$strBrokerPhoneNo.']]></listingphone>
                            <listingagent><![CDATA['.$strBrokerName.']]></listingagent>
                            <listingbroker><![CDATA['.$strBrokerName.']]></listingbroker>
                            <brokerlogo></brokerlogo>
                            <listingurl><![CDATA['.$strProLink.']]></listingurl>
                            <virtual_tour_url></virtual_tour_url>
                            <image_list>
                                <image primary="true">
                                    <title><![CDATA['.$strProTitle.']]></title>
                                    <thumbnail><![CDATA['.$strProImagePath.']]></thumbnail>
                                    <fullsize><![CDATA['.$strProImagePath.']]></fullsize>
                                </image>
                            </image_list>
                        </property>
         */


        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/CLRSearch.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        xtwFeed.WriteStartElement("clrsearch");
        xtwFeed.WriteAttributeString("xmlns", "http://www.clrsearch.com/schemas/propertyFeed");
        xtwFeed.WriteStartElement("feed_created");
        xtwFeed.WriteCData(DateTime.Now.ToString("o"));
        xtwFeed.WriteEndElement();

        //properties Start
        xtwFeed.WriteStartElement("property_list");

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{
        //rss Start
        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("property");
                xtwFeed.WriteAttributeString("id", OrderTable.Rows[i]["order_id"].ToString().Trim());

                //Location Start
                xtwFeed.WriteStartElement("listed_date");
                xtwFeed.WriteCData(DateTime.Parse(OrderTable.Rows[i]["created_on"].ToString()).ToLocalTime().ToString("o"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("status");
                xtwFeed.WriteCData("For Sale");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("type");
                xtwFeed.WriteCData(GetPropertyTypeForCLRSearch(OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                //For using old data of custom fields
                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("beds", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteElementString("baths", OrderTable.Rows[i]["FullBaths"].ToString().Trim());

                    xtwFeed.WriteStartElement("sqft");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);
                    xtwFeed.WriteElementString("beds", dr["Bedrooms"].ToString());
                    xtwFeed.WriteElementString("baths", dr["FullBaths"].ToString());

                    xtwFeed.WriteStartElement("sqft");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteElementString("garage", "");

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }
                xtwFeed.WriteElementString("price", Price.Replace(",",""));

                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state_abbr");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zipcode");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("pool", "false");
                xtwFeed.WriteElementString("acreage", OrderTable.Rows[i]["LotSize"].ToString().Trim());

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("mlsid");
                xtwFeed.WriteCData(OrderTable.Rows[i]["mls_number"].ToString().Trim());
                xtwFeed.WriteEndElement();

                //For using old data of custom fields
                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("yearbuilt", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);
                    xtwFeed.WriteElementString("yearbuilt", dr["YearBuilt"].ToString());
                }

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                //start amenity_list
                xtwFeed.WriteStartElement("amenity_list");

                string[] strPropertyFeatures = OrderTable.Rows[i]["PropertyFeatures"].ToString().Split(':');
                string[] strPropertyFeaturesValues = OrderTable.Rows[i]["PropertyFeaturesValues"].ToString().Split(':');



                if (strPropertyFeatures.Length >= 30)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        if (Convert.ToBoolean(strPropertyFeaturesValues[j].ToString()) == true)
                        {
                            xtwFeed.WriteStartElement("amenity");
                            xtwFeed.WriteCData(strPropertyFeatures[j].ToString());
                            xtwFeed.WriteEndElement();
                        }
                    }
                }

                xtwFeed.WriteEndElement();
                //end amenity_list

                xtwFeed.WriteStartElement("listingagent");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listingbroker");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("brokerlogo", "");

                xtwFeed.WriteStartElement("agent-email");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listingurl");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listingphone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteStartElement("virtual_tour_url");
                //xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                //xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("image_list");

                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("image");
                    xtwFeed.WriteAttributeString("primary", "true");

                    xtwFeed.WriteStartElement("title");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("thumbnail");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("fullsize");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("image");

                    if (OrderTable.Rows[i]["photo1"].ToString().Trim() == "")
                    {
                        xtwFeed.WriteAttributeString("primary", "true");
                    }
                    else
                    {
                        xtwFeed.WriteAttributeString("primary", "false");
                    }

                    xtwFeed.WriteStartElement("title");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("thumbnail");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("fullsize");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo3"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("image");

                    xtwFeed.WriteAttributeString("primary", "false");

                    xtwFeed.WriteStartElement("title");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("thumbnail");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("fullsize");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo4"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("image");

                    xtwFeed.WriteAttributeString("primary", "false");

                    xtwFeed.WriteStartElement("title");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("thumbnail");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo4/" + OrderTable.Rows[i]["photo4"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("fullsize");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo4/" + OrderTable.Rows[i]["photo4"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo5"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("image");

                    xtwFeed.WriteAttributeString("primary", "false");

                    xtwFeed.WriteStartElement("title");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("thumbnail");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo5/" + OrderTable.Rows[i]["photo5"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("fullsize");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo5/" + OrderTable.Rows[i]["photo5"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo6"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("image");

                    xtwFeed.WriteAttributeString("primary", "false");

                    xtwFeed.WriteStartElement("title");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("thumbnail");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo6/" + OrderTable.Rows[i]["photo6"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("fullsize");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo6/" + OrderTable.Rows[i]["photo6"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo7"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("image");

                    xtwFeed.WriteAttributeString("primary", "false");

                    xtwFeed.WriteStartElement("title");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("thumbnail");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo7/" + OrderTable.Rows[i]["photo7"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("fullsize");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo7/" + OrderTable.Rows[i]["photo7"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteEndElement();


                xtwFeed.WriteEndElement();
                //property End
            }
        }

        vw_PersonalWebsite_DisplayTableAdapter DAPersonalWebSite = new vw_PersonalWebsite_DisplayTableAdapter();
        PersonalWebsite.vw_PersonalWebsite_DisplayDataTable DtPersonalWebSite = new PersonalWebsite.vw_PersonalWebsite_DisplayDataTable();
        DAPersonalWebSite.FillPersonalWebsites(DtPersonalWebSite);

        //try
        //{
        //rss Start
        for (int i = 0; i <= DtPersonalWebSite.Rows.Count - 1; i++)
        {
            if (DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim() != ""
                && DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim().ToLower() != "free"
                && Convert.ToBoolean(DtPersonalWebSite.Rows[i]["IsActivated"].ToString()) != false)
            {
                //property Start
                xtwFeed.WriteStartElement("property");
                xtwFeed.WriteAttributeString("id", Convert.ToString(Convert.ToInt32(DtPersonalWebSite.Rows[i]["PersonalWebsiteID"].ToString()) + 100000));

                //Location Start
                xtwFeed.WriteStartElement("listed_date");
                xtwFeed.WriteCData(DateTime.Parse(DtPersonalWebSite.Rows[i]["CreateDate"].ToString()).ToLocalTime().ToString("o"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("status");
                xtwFeed.WriteCData("For Sale");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("type");
                xtwFeed.WriteCData(GetPropertyTypeForCLRSearch(DtPersonalWebSite.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("beds", DtPersonalWebSite.Rows[i]["Bedrooms"].ToString().Trim());
                xtwFeed.WriteElementString("baths", DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim());

                xtwFeed.WriteStartElement("sqft");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["SquareFootage"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("garage", "");

                xtwFeed.WriteElementString("price", DtPersonalWebSite.Rows[i]["AskingPrice"].ToString());

                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["City"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state_abbr");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["State"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zipcode");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("pool", "false");
                xtwFeed.WriteElementString("acreage", DtPersonalWebSite.Rows[i]["LotSize"].ToString().Trim());

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("mlsid");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["MlsNumber"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("yearbuilt", DtPersonalWebSite.Rows[i]["YearBuilt"].ToString().Trim());

                ProfileCommon profile = Profile.GetProfile(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());

                //start amenity_list
                xtwFeed.WriteStartElement("amenity_list");

                fly_PersonalWebSiteFeaturesTableAdapter DAPersonalWebSiteFeature = new fly_PersonalWebSiteFeaturesTableAdapter();
                PersonalWebsite.fly_PersonalWebSiteFeaturesDataTable DtPersonalWebSiteFeature = new PersonalWebsite.fly_PersonalWebSiteFeaturesDataTable();
                DAPersonalWebSiteFeature.FillPersonalWebSiteFeaturesByPersonalWebSiteIDFeaturesID(DtPersonalWebSiteFeature,
                    Convert.ToInt64(DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString()), 1);



                for (int j = 0; j < DtPersonalWebSiteFeature.Rows.Count; j++)
                {
                    xtwFeed.WriteStartElement("amenity");
                    xtwFeed.WriteCData(DtPersonalWebSiteFeature.Rows[j]["FeaturesOptions"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteEndElement();
                //end amenity_list

                xtwFeed.WriteStartElement("listingagent");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listingbroker");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("brokerlogo", "");

                xtwFeed.WriteStartElement("agent-email");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listingurl");

                if (DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString().Trim() != "")
                {
                    if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 2)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 3)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/Singlewebposts.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                }

                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listingphone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteStartElement("virtual_tour_url");
                //xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                //xtwFeed.WriteEndElement();

                fly_SlideShowImagesTableAdapter DA = new fly_SlideShowImagesTableAdapter();
                PersonalWebsite.fly_SlideShowImagesDataTable DtSlideShowImages = new PersonalWebsite.fly_SlideShowImagesDataTable();
                DA.FillSlideShowDisplayBySlideShowId(DtSlideShowImages, Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_SlideShow"].ToString()));

                string[] Folder = DtPersonalWebSite.Rows[i]["EmailAddress"].ToString().Split('@');

                xtwFeed.WriteStartElement("image_list");

                for (int j = 0; j < DtSlideShowImages.Rows.Count; j++)
                {
                    xtwFeed.WriteStartElement("image");

                    if (j == 0)
                    {
                        xtwFeed.WriteAttributeString("primary", "true");
                    }
                    else
                    {
                        xtwFeed.WriteAttributeString("primary", "false");
                    }

                    xtwFeed.WriteStartElement("title");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("thumbnail");
                    xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/SlideShow/" + Folder[0] + "/" + DtSlideShowImages.Rows[j]["SlideShowImageName"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("fullsize");
                    xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/SlideShow/" + Folder[0] + "/" + DtSlideShowImages.Rows[j]["SlideShowImageName"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteEndElement();


                xtwFeed.WriteEndElement();
                //property End
            }
        }

        xtwFeed.WriteEndElement();
        //chennal End

        xtwFeed.WriteEndElement();
        //rss End

        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}


    }

    public void GenerateClickAbleDirectoryFeed(ProfileCommon Profile)
    {
        /*
            <ccd_feed>
                <properties>
                    <property>
					<name><![CDATA['.$strProTitle.']]></name>
					<address><![CDATA['.$arrFetchProperty["streetaddress"].']]></address>
					<city>'.$strLocality.'</city>
					<state><![CDATA['.$strStateCode.']]></state>
					<postcode>'.$strZipCode.'</postcode>
					<country>USA</country>
					<latitude></latitude>
					<longitude></longitude>
					<price>'.$arrFetchProperty["listprice"].'</price>
					<lot_size></lot_size>
					<sq_feet>'.$strSqFeet.'</sq_feet>
					<garage_size></garage_size>
					<no_beds>'.$intBedrooms.'</no_beds>
					<no_baths>'.$intBathRoom.'</no_baths>
					<no_floors></no_floors>
					<year_built>'.$strYearBuilt.'</year_built>
					<amenities></amenities>
					<school_district></school_district>
					<description><![CDATA['.$strDescription.']]></description>
					<propertytype><![CDATA['.$strListingType.']]></propertytype>
					<seller>
						<sellername><![CDATA['.$strBrokerName.']]></sellername>
						<sellerphone><![CDATA['.$strBrokerPhoneNo.']]></sellerphone>
						<sellerfax></sellerfax>
						<selleremail><![CDATA['.$strBrokerEmail.']]></selleremail>
					</seller>
					<addlurl><![CDATA['.$strProLink.']]></addlurl>
					<listingid>'.$arrFetchProperty["propertyid"].'</listingid>
					<photos>
						<photo><![CDATA['.$strProImagePath.']]></photo>
					</photos>
				</property>
         */

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/ClickAbleCityDirecotry.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{
        //ccd_feed Start
        xtwFeed.WriteStartElement("ccd_feed");

        //properties Start
        xtwFeed.WriteStartElement("properties");
        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("property");

                xtwFeed.WriteStartElement("name");
                xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("postcode");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("country", "USA");

                xtwFeed.WriteElementString("latitude", "");

                xtwFeed.WriteElementString("longitude", "");

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }
                xtwFeed.WriteElementString("price", Price);

                //For using old data of custom fields
                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("lot_size");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["LotSize"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("sq_feet");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("garage_size", "");

                    xtwFeed.WriteElementString("no_beds", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteElementString("no_baths", OrderTable.Rows[i]["FullBaths"].ToString().Trim());
                    xtwFeed.WriteElementString("no_floors", OrderTable.Rows[i]["Floors"].ToString().Trim());

                    xtwFeed.WriteElementString("year_built", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                    //xtwFeed.WriteStartElement("year_built");
                    //xtwFeed.WriteCData(OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                    //xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteStartElement("lot_size");
                    xtwFeed.WriteCData(dr["LotSize"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("sq_feet");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("garage_size", "");

                    xtwFeed.WriteElementString("no_beds", dr["Bedrooms"].ToString());
                    xtwFeed.WriteElementString("no_baths", dr["FullBaths"].ToString());
                    xtwFeed.WriteElementString("no_floors", OrderTable.Rows[i]["Floors"].ToString().Trim());

                    xtwFeed.WriteElementString("year_built", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                    //xtwFeed.WriteStartElement("year_built");
                    //xtwFeed.WriteCData(dr["YearBuilt"].ToString());
                    //xtwFeed.WriteEndElement();
                }


                string[] strPropertyFeatures = OrderTable.Rows[i]["PropertyFeatures"].ToString().Split(':');
                string[] strPropertyFeaturesValues = OrderTable.Rows[i]["PropertyFeaturesValues"].ToString().Split(':');

                string strAmenities = "";

                if (strPropertyFeatures.Length >= 30)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        if (Convert.ToBoolean(strPropertyFeaturesValues[j].ToString()) == true)
                        {
                            if (strAmenities.Trim() != "")
                            {
                                strAmenities += ", ";
                            }
                            strAmenities += strPropertyFeatures[j].ToString();
                        }
                    }
                }

                xtwFeed.WriteStartElement("amenities");
                xtwFeed.WriteCData(strAmenities);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("school_district", "");

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("propertytype");
                xtwFeed.WriteCData(GetPropertyTypeForClickAbleDirectory(OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                //agent Start

                xtwFeed.WriteStartElement("seller");

                xtwFeed.WriteStartElement("sellername");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("sellerphone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("sellerfax");
                xtwFeed.WriteCData(profile.Contact.Fax);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("selleremail");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("addlurl");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listingid");
                xtwFeed.WriteCData(OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("photos");
                for (int j = 1; j <= 7; j++)
                {
                    if (OrderTable.Rows[i]["photo" + j].ToString().Trim() != "")
                    {
                        xtwFeed.WriteStartElement("photo");
                        xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo" + j + "/" + OrderTable.Rows[i]["photo" + j].ToString().Trim());
                        xtwFeed.WriteEndElement();
                    }
                }
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //property End
            }
        }
        xtwFeed.WriteEndElement();
        //properties End

        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}

    }

    public void GenerateCityCribsFeed(ProfileCommon Profile)
    {
        /*
            <properties>
              <property>
                <location>
                  <street-address>East 72nd Street</street-address>
                  <x-street-address>W 4th Street</x-street-address>
                  <area-name>Upper East Side</area-name>
                  <city-name>New York</city-name>
                  <county></county>
                  <state-code>NY</state-code>
                  <zipcode>10021</zipcode>
                  <full-address>520 East 72nd Street, New York, NY 10021</full-address>
                  <lat>-73.95350</lat>
                  <long>40.76690</long>
                </location>

                <details>
                  <price>2999.00</price>
                  <year-built>2001</year-built>
                  <num-bedrooms>2</num-bedrooms>
                  <num-bathrooms>1.5</num-bathrooms>
                  <apartment-number></apartment-number>
                  <lot-size></lot-size>
                  <square-feet>3453</square-feet>
                  <date-listed>2005-08-25</date-listed>
                  <date-sold></date-sold>
                  <description>This BEAUTIFUL  APT WITH FULL BATH AND GREAT HARDWOOD FLOORS is located 
                   on a quiet cul-de-sac of East 72nd street.  Brand New Kitchen with white tile and all new
                   appliances and cabinets.  Tons of closet space and high ceilings!!! Call Paul to schedule 
                   at 917 555 5555 </description>
                  <title>East 72nd Street 2 Bedroom DELIGHT</title>
                  <refid>207417</redid>
                </details>

                <status>For Rent</status>

                <property-type>Apartments</property-type>

                <site>
                 <site-url>www.allstarbroker.com</site-url>
                 <site-name>allstarbroker</site-name>
                </site>

                <pictures>
                  <picture>
                    <picture-url>http://www.allstarbroker.com/Pictures/Interior_Photo/207417_int_photo1.jpg</picture-url>
                  </picture>
                  <picture>
                    <picture-url>http://www.allstarbroker.com/Pictures/Interior_Photo/207417_int_photo2.jpg</picture-url>
                  </picture>
                </pictures>

                <landing-page>
                 <lp-url>http://www.allstarbroker.com/listing_details.asp?listingid=207417</lp-url>
                 <has-virtual-tour>false</has-virtual-tour>
                </landing-page>

                <agent>
                  <agent-email>john@smith.com</agent-email>
                  <agent-phone>555-783</agent-phone>
                  <first-name>John</first-name>
                  <last-name>Smith</last-name>
                </agent>

                <features>
                  <feature>no-fee</feature>
                  <feature>elevator</feature>
                  <feature>doorman</feature>
                  <feature>private outdoor</feature>
                  <feature>pets</feature>
                  <feature>sundeck</feature>
                </features>
              </property>
            </properties>
         */

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/CityCribs.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        //try
        //{

        //properties Start
        xtwFeed.WriteStartElement("properties");
        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("property");

                //Location Start
                xtwFeed.WriteStartElement("location");

                xtwFeed.WriteStartElement("street-address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("x-street-address", "");

                xtwFeed.WriteStartElement("area-name");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city-name");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("county", "");

                xtwFeed.WriteStartElement("state-code");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zipcode");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("full-address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim() + " " + OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("lat", "");

                xtwFeed.WriteElementString("long", "");

                xtwFeed.WriteEndElement();
                //Location End

                //Details Start
                xtwFeed.WriteStartElement("details");

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }
                xtwFeed.WriteElementString("price", Price);

                //For using old data of custom fields
                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("year-built", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());

                    xtwFeed.WriteElementString("num-bedrooms", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());

                    xtwFeed.WriteElementString("num-bathrooms", OrderTable.Rows[i]["FullBaths"].ToString().Trim());

                    xtwFeed.WriteElementString("apartment-number", "");

                    xtwFeed.WriteStartElement("lot-size");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["LotSize"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("square-feet");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    //xtwFeed.WriteStartElement("year_built");
                    //xtwFeed.WriteCData(OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                    //xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteElementString("year-built", dr["YearBuilt"].ToString().Trim());

                    xtwFeed.WriteElementString("num-bedrooms", dr["Bedrooms"].ToString());

                    xtwFeed.WriteElementString("num-bathrooms", dr["FullBaths"].ToString());

                    xtwFeed.WriteElementString("apartment-number", "");

                    xtwFeed.WriteStartElement("lot-size");
                    xtwFeed.WriteCData(dr["LotSize"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("square-feet");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();

                    //xtwFeed.WriteStartElement("year_built");
                    //xtwFeed.WriteCData(dr["YearBuilt"].ToString());
                    //xtwFeed.WriteEndElement();
                }


                xtwFeed.WriteElementString("date-listed", DateTime.Parse(OrderTable.Rows[i]["created_on"].ToString().Trim()).ToString("yyyy-MM-dd"));

                xtwFeed.WriteElementString("date-sold", "");

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("refid", OrderTable.Rows[i]["order_id"].ToString());

                xtwFeed.WriteEndElement();
                //Details End

                xtwFeed.WriteElementString("status", "For Sale");

                xtwFeed.WriteStartElement("property-type");
                xtwFeed.WriteCData(GetPropertyTypeForZillow(OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                //Site Start
                xtwFeed.WriteStartElement("site");
                xtwFeed.WriteElementString("site-url", clsUtility.SiteHttpWwwRootUrl);
                xtwFeed.WriteElementString("site-name", clsUtility.ProjectName);
                xtwFeed.WriteEndElement();
                //Site End


                //pictures Start
                xtwFeed.WriteStartElement("pictures");
                for (int j = 1; j <= 7; j++)
                {
                    if (OrderTable.Rows[i]["photo" + j].ToString().Trim() != "")
                    {
                        xtwFeed.WriteStartElement("picture");
                        xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo" + j + "/" + OrderTable.Rows[i]["photo" + j].ToString().Trim());
                        xtwFeed.WriteEndElement();
                    }
                }
                xtwFeed.WriteEndElement();
                //pictures End

                //landing-page Start
                xtwFeed.WriteStartElement("landing-page");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                if (OrderTable.Rows[i]["order_id"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("has-virtual-tour");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["order_id"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    xtwFeed.WriteElementString("has-virtual-tour", "false");
                }
                xtwFeed.WriteEndElement();
                //landing-page End

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                //agent Start
                xtwFeed.WriteStartElement("agent");

                xtwFeed.WriteStartElement("agent-email");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("agent-phone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("first-name");
                xtwFeed.WriteCData(profile.FirstName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("last-name");
                xtwFeed.WriteCData(profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //agent End

                xtwFeed.WriteEndElement();
                //property End
            }
        }
        xtwFeed.WriteEndElement();
        //properties End

        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}

    }

    public void GenerateGoogleBase(ProfileCommon Profile)
    {
        /*
            <?xml version="1.0" encoding="UTF-8" ?>
            <rdf:RDF
            xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#"
            xmlns="http://purl.org/rss/1.0/"
            xmlns:g="http://base.google.com/ns/1.0">

            <channel rdf:about="http://www.example.com/RSS1.xml">
	            <title>The name of your data feed.</title>
  	            <description>A description of your content.</description>
	            <link>http://www.example.com</link>

            <items>
	            <rdf:Seq>

		            <rdf:li resource="http://www.example.com/item1-info-page.html"/>
		            <rdf:li resource="http://www.example.com/item2-info-page.html"/>
		            <rdf:li resource="http://www.example.com/item3-info-page.html"/>
	            </rdf:Seq>
            </items>
            </channel>

            <item rdf:about="http://www.example.com/item1-info-page.html">
            <title>Room for rent in Mountain View, CA</title>
            <g:agent>Sue Smith</g:agent>
            <g:area>1000 square ft.</g:area>

            <g:bathrooms>2.5</g:bathrooms>
            <g:bedrooms>4</g:bedrooms>
            <g:broker>Acme Real Estate</g:broker>
            <description>Sunlit room in a ranch style, single-story house.</description>
            <g:expiration_date>2006-12-20</g:expiration_date>
            <g:feature>Pet friendly</g:feature>
            <g:id>1</g:id>
            <g:image_link>http://www.example.com/image1.jpg</g:image_link>
            <link>http://www.example.com/item1-info-page.html</link>

            <g:listing_status>active</g:listing_status>
            <g:listing_type>for sale</g:listing_type>
            <g:location>1600 Amphitheatre Pkwy, Mountain View, CA, 94043</g:location>
            <g:mls_listing_id>246812</g:mls_listing_id>
            <g:mls_name>Acme MLS</g:mls_name>
            <g:open_house_date_range>
	            <g:start>2006-12-10T00:00:01</g:start>
	            <g:end>2006-12-16T23:59:59</g:end>

            </g:open_house_date_range>
            <g:price>225000</g:price>
            <g:price_type>negotiable</g:price_type>
            <g:property_type>apartment</g:property_type>
            <g:provider_class>broker</g:provider_class>
            <g:publish_date>2005-12-20</g:publish_date>
            <g:school>Union High School</g:school>
            <g:school_district>Union School District</g:school_district>
            <g:year>2005</g:year>

            <g:zoning>residential</g:zoning>
            </item>
            </rdf:RDF>
         */

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/GoogleBase.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();


        //try
        //{

        //properties Start
        xtwFeed.WriteStartElement("rdf:RDF");
        xtwFeed.WriteAttributeString("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
        xtwFeed.WriteAttributeString("xmlns", "http://purl.org/rss/1.0/");
        xtwFeed.WriteAttributeString("xmlns:g", "http://base.google.com/ns/1.0");

        xtwFeed.WriteStartElement("channel");
        xtwFeed.WriteAttributeString("rdf:about", clsUtility.SiteHttpWwwRootUrl + "/XMLFeeds/GoogleBase.xml");

        xtwFeed.WriteStartElement("title");
        xtwFeed.WriteCData(clsUtility.SiteBrandName + " - Realestate eFlyer Marketing");
        xtwFeed.WriteEndElement();

        xtwFeed.WriteStartElement("description");
        xtwFeed.WriteCData(clsUtility.SiteBrandName + " - One stop solution for real estate marketing. Includes real estate email flyers, property ads, free virtual tours and you can create personal property websites and personal websites. Also we syndicate your listings on top sites including Google Base, Craigslist, Trulia, Zillow and many more.");
        xtwFeed.WriteEndElement();

        xtwFeed.WriteStartElement("link");
        xtwFeed.WriteCData(clsUtility.SiteHttpWwwRootUrl);
        xtwFeed.WriteEndElement();

        xtwFeed.WriteStartElement("items");
            xtwFeed.WriteStartElement("rdf:Seq");
                xtwFeed.WriteElementString("link", clsUtility.SiteHttpWwwRootUrl);
                xtwFeed.WriteElementString("link", clsUtility.SiteHttpWwwRootUrl + "/Pricing/Pricing2.aspx");
                xtwFeed.WriteElementString("link", clsUtility.SiteHttpWwwRootUrl + "/SamplesSeller.aspx");
                xtwFeed.WriteElementString("link", clsUtility.SiteHttpWwwRootUrl + "/Sitemap.aspx");
                xtwFeed.WriteElementString("link", clsUtility.SiteHttpWwwRootUrl + "/ContactUs.aspx");
            xtwFeed.WriteEndElement();
        xtwFeed.WriteEndElement();

        xtwFeed.WriteEndElement();


        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {

                xtwFeed.WriteStartElement("item");
                xtwFeed.WriteAttributeString("rdf:about", clsUtility.SiteHttpWwwRootUrl + "/showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(OrderTable.Rows[i]["title"].ToString().Trim());
                xtwFeed.WriteEndElement();

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteStartElement("g:agent");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("g:area");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("g:bathrooms", OrderTable.Rows[i]["FullBaths"].ToString().Trim());
                    xtwFeed.WriteElementString("g:bedrooms", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());

                    xtwFeed.WriteElementString("year-built", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteStartElement("g:area");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("g:bathrooms", dr["FullBaths"].ToString());
                    xtwFeed.WriteElementString("g:bedrooms", dr["Bedrooms"].ToString());

                    xtwFeed.WriteElementString("year-built", dr["YearBuilt"].ToString().Trim());
                }

                xtwFeed.WriteStartElement("g:broker");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("g:id", OrderTable.Rows[i]["order_id"].ToString().Trim());

                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("g:image_link");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    xtwFeed.WriteStartElement("g:image_link");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }
                xtwFeed.WriteElementString("link", clsUtility.SiteHttpWwwRootUrl + "/showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteElementString("g:listing_status", "active");
                xtwFeed.WriteElementString("g:listing_type", "for sale");
                xtwFeed.WriteElementString("g:location", OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteElementString("g:mls_listing_id", OrderTable.Rows[i]["mls_number"].ToString().Trim());


                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }
                xtwFeed.WriteElementString("g:price", Price);
                xtwFeed.WriteElementString("g:price_type", "negotiable");
                xtwFeed.WriteElementString("g:property_type", GetPropertyTypeForGoogleBase(OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteElementString("g:publish_date", DateTime.Parse(OrderTable.Rows[i]["created_on"].ToString().Trim()).ToString("yyyy-MM-dd"));
                xtwFeed.WriteEndElement();
            }
        }
        xtwFeed.WriteEndElement();
        //properties End
        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}

    }

    public void GenerateOLXFeed(ProfileCommon Profile)
    {
        /*
        <?xml version="1.0" encoding="utf-8"?>
        <!-- Example of xml feed format to send real estate listings to Vast.com. -->
        <listings>
            <listing>
                <record_id>12345678</record_id>
                <title>1234 Main Street, San Francisco, CA</title>
                <url>http://www.yoursite.com/realestate/12345678.html</url>
                <category>real estate</category>
                <subcategory>single family home</subcategory>
                <images>http://www.yoursite.com/realestate/12345678.jpg</images>
                <address>1234 Main Street</address>
                <city>San Francisco</city>
                <state>CA</state>
                <zip>94105</zip>
                <country>United States</country>
                <bedrooms>4</bedrooms>
                <bathrooms>2</bathrooms>
                <square_footage>1600</square_footage>
                <stories>1</stories>
                <lot_size></lot_size>
                <parking_spots>1</parking_spots>
                <year_built>1999</year_built>
                <currency>USD</currency>
                <price>150,000</price>
                <amenities>dishwasher, fenced yard, air conditioning, garage, garden</amenities>
                <description>Only 12 min to downtown. This house has been totally updated in 2007.</description>
                <listing_time>2007-09-16-12:00:00</listing_time>
                <expire_time>2007-10-16-12:00:00</expire_time>
            </listing>
        </listings> */

        //Xml Feed = new Xml();

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/OLXFeed.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        //try
        //{
        //properties Start
        xtwFeed.WriteStartElement("listings");
        CultureInfo ci = new CultureInfo("de-DE");

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("listing");

                //Location Start

                xtwFeed.WriteElementString("record_id", OrderTable.Rows[i]["order_id"].ToString());

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("url");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("category", "real estate");

                xtwFeed.WriteStartElement("subcategory");
                xtwFeed.WriteCData(OrderTable.Rows[i]["PropertyType"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("OLX_Category");
                xtwFeed.WriteCData(GetPropertyTypeCodeForOLX(OrderTable.Rows[i]["PropertyType"].ToString().Trim(), OrderTable.Rows[i]["prop_price"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                //pictures Start
                xtwFeed.WriteStartElement("images");

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo3"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo4"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo4/" + OrderTable.Rows[i]["photo4"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo5"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo5/" + OrderTable.Rows[i]["photo5"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo6"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo6/" + OrderTable.Rows[i]["photo6"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo7"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo7/" + OrderTable.Rows[i]["photo7"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                xtwFeed.WriteEndElement();
                //pictures End


                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zip");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("country", "United States");

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteElementString("bedrooms", OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteElementString("bathrooms", OrderTable.Rows[i]["FullBaths"].ToString().Trim());

                    xtwFeed.WriteStartElement("square_footage");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("stories");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["Floors"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("lot_size");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["LotSize"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("parking_spots");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["Parking"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("year_built", OrderTable.Rows[i]["YearBuilt"].ToString().Trim());

                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteElementString("bedrooms", dr["Bedrooms"].ToString());
                    xtwFeed.WriteElementString("bathrooms", dr["FullBaths"].ToString());

                    xtwFeed.WriteStartElement("square_footage");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("stories", "");

                    xtwFeed.WriteStartElement("lot_size");
                    xtwFeed.WriteCData(dr["LotSize"].ToString());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("parking_spots", "");

                    xtwFeed.WriteElementString("year_built", dr["YearBuilt"].ToString());
                }


                xtwFeed.WriteElementString("currency", "USD");

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString();
                    }
                }

                xtwFeed.WriteElementString("price", Price);

                string[] strPropertyFeatures = OrderTable.Rows[i]["PropertyFeatures"].ToString().Split(':');
                string[] strPropertyFeaturesValues = OrderTable.Rows[i]["PropertyFeaturesValues"].ToString().Split(':');

                string strAmenities = "";

                if (strPropertyFeatures.Length >= 30)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        if (Convert.ToBoolean(strPropertyFeaturesValues[j].ToString()) == true)
                        {
                            if (strAmenities.Trim() != "")
                            {
                                strAmenities += ", ";
                            }
                            strAmenities += strPropertyFeatures[j].ToString();
                        }
                    }
                }

                xtwFeed.WriteStartElement("amenities");
                xtwFeed.WriteCData(strAmenities);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listing_time");
                xtwFeed.WriteCData(Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("R", ci));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("contact_email");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();


                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        vw_PersonalWebsite_DisplayTableAdapter DAPersonalWebSite = new vw_PersonalWebsite_DisplayTableAdapter();
        PersonalWebsite.vw_PersonalWebsite_DisplayDataTable DtPersonalWebSite = new PersonalWebsite.vw_PersonalWebsite_DisplayDataTable();
        DAPersonalWebSite.FillPersonalWebsites(DtPersonalWebSite);

        for (int i = 0; i <= DtPersonalWebSite.Rows.Count - 1; i++)
        {
            if (DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim() != ""
                && DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim().ToLower() != "free"
                && Convert.ToBoolean(DtPersonalWebSite.Rows[i]["IsActivated"].ToString()) != false)
            {
                //property Start
                xtwFeed.WriteStartElement("listing");

                //Location Start

                xtwFeed.WriteElementString("record_id", Convert.ToString((Convert.ToInt32(DtPersonalWebSite.Rows[i]["PersonalWebsiteID"].ToString()) + 100000)));

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["City"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["State"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("url");

                if (DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString().Trim() != "")
                {
                    if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 2)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 3)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/Singlewebposts.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                }

                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("category", "real estate");

                xtwFeed.WriteStartElement("subcategory");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["PropertyType"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("OLX_Category");
                xtwFeed.WriteCData(GetPropertyTypeCodeForOLX(DtPersonalWebSite.Rows[i]["PropertyType"].ToString().Trim(), DtPersonalWebSite.Rows[i]["AskingPrice"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                //pictures Start
                xtwFeed.WriteStartElement("images");

                fly_SlideShowImagesTableAdapter DA = new fly_SlideShowImagesTableAdapter();
                PersonalWebsite.fly_SlideShowImagesDataTable DtSlideShowImages = new PersonalWebsite.fly_SlideShowImagesDataTable();
                DA.FillSlideShowDisplayBySlideShowId(DtSlideShowImages, Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_SlideShow"].ToString()));

                string[] Folder = DtPersonalWebSite.Rows[i]["EmailAddress"].ToString().Split('@');

                for (int j = 0; j < DtSlideShowImages.Rows.Count; j++)
                {
                    if (j >= 20)
                        break;

                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/SlideShow/" + Folder[0] + "/" + DtSlideShowImages.Rows[j]["SlideShowImageName"].ToString());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteEndElement();
                //pictures End


                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["City"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["State"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zip");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["ZipCode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("country", "United States");

                xtwFeed.WriteElementString("bedrooms", DtPersonalWebSite.Rows[i]["Bedrooms"].ToString().Trim());
                xtwFeed.WriteElementString("bathrooms", DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim());

                xtwFeed.WriteStartElement("square_footage");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["SquareFootage"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("stories");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Floors"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("lot_size");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["LotSize"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("parking_spots");
                xtwFeed.WriteCData("1");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("year_built", DtPersonalWebSite.Rows[i]["YearBuilt"].ToString().Trim());

                xtwFeed.WriteElementString("currency", "USD");

                xtwFeed.WriteElementString("price", DtPersonalWebSite.Rows[i]["AskingPrice"].ToString());

                fly_PersonalWebSiteFeaturesTableAdapter DAPersonalWebSiteFeature = new fly_PersonalWebSiteFeaturesTableAdapter();
                PersonalWebsite.fly_PersonalWebSiteFeaturesDataTable DtPersonalWebSiteFeature = new PersonalWebsite.fly_PersonalWebSiteFeaturesDataTable();
                DAPersonalWebSiteFeature.FillPersonalWebSiteFeaturesByPersonalWebSiteIDFeaturesID(DtPersonalWebSiteFeature,
                    Convert.ToInt64(DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString()), 1);

                string strAmenities = "";

                for (int j = 0; j < DtPersonalWebSiteFeature.Rows.Count; j++)
                {
                    if (strAmenities.Trim() != "")
                    {
                        strAmenities += ", ";
                    }
                    strAmenities += DtPersonalWebSiteFeature.Rows[j]["FeaturesOptions"].ToString().Trim();
                }

                xtwFeed.WriteStartElement("amenities");
                xtwFeed.WriteCData(strAmenities);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("listing_time");
                xtwFeed.WriteCData(Convert.ToDateTime(DtPersonalWebSite.Rows[i]["CreateDate"].ToString()).ToString("R", ci));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        xtwFeed.WriteEndElement();
        //Listings End
        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}
    }

    public void GenerateTrovitFeed(ProfileCommon Profile)
    {
        /*
        <?xml version="1.0" encoding="utf-8"?>
        <trovit>
        <ad>
	        <id><![CDATA[7004578]]></id>
	        <url><![CDATA[http://www.yourdomain.com/ad/7004578]]></url>
	        <title><![CDATA[4 br, 2 bath House - 816 Brookwood]]></title>
	        <type><![CDATA[For rent]]></type>
	        <content><![CDATA[Large 2 bedroom with large eat-in kitchen area, and 2nd entrance with deck.]]></content>

                <price><![CDATA[1195]]></price>
	        <property_type><![CDATA[House]]></property_type>

	        <address><![CDATA[332 E. Jefferson]]]></address>
	        <floor_number><![CDATA[2]]></floor_number>
	        <city_area><![CDATA[Valley Park]]></city_area>
	        <city><![CDATA[Ann Arbor]]></city>
	        <postcode><![CDATA[48104]]></postcode>
	        <region><![CDATA[MI]]></region>

	        <latitude><![CDATA[42.276230]]></latitude>
	        <longitude><![CDATA[-83.7455700]]></longitude>
	        <orientation><![CDATA[]]></orientation>
	        <agency><![CDATA[Stanward Properties]]></agency>
                <mls_database><![CDATA[S86229U33B]]></mls_database>

	        <floor_area><![CDATA[2000]]></floor_area>
	        <plot_area unit="acres"><![CDATA[1]]></plot_area>

	        <rooms><![CDATA[4]]></rooms>
	        <bathrooms><![CDATA[2]]></bathrooms>
	        <condition><![CDATA[]]></condition>
	        <year><![CDATA[2006]]></year>
	        <virtual_tour><![CDATA[]]></virtual_tour>

                <pictures>
		        <picture>
			        <picture_url><![CDATA[http://www.yourdomain.com/image.jpg]]></picture_url>
			        <picture_title><![CDATA[living room]]></picture_title>
		        </picture>
	        </pictures>

	        <date><![CDATA[31/2/2011 17:30]]></date>
                <expiration_date><![CDATA[1/6/2012]]></expiration_date>
                <by_owner><![CDATA[1]]></by_owner>
	        <parking><![CDATA[1]]></parking>
	        <foreclosure><![CDATA[]]></foreclosure>
	        <is_furnished><![CDATA[0]]></is_furnished>
	        <is_new><![CDATA[0]]></is_new>
        </ad>
        </trovit>
         */

        //Xml Feed = new Xml();

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/TrovitFeed.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        //try
        //{
        //properties Start
        xtwFeed.WriteStartElement("trovit");
        CultureInfo ci = new CultureInfo("de-DE");

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("ad");

                //Location Start

                xtwFeed.WriteStartElement("id");
                xtwFeed.WriteCData(OrderTable.Rows[i]["order_id"].ToString());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("url");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString().Replace(",", "").Replace(".00", "");
                        int price1 = 0;
                        if (IsInteger(Price))
                        {
                            price1 = Convert.ToInt32(Price);
                            if (price1 < 100 || price1 > 100000000)
                            {
                                Price = "";
                            }
                        }
                        else
                        {
                            Price = "";
                        }
                    }
                }

                xtwFeed.WriteStartElement("type");
                try
                {
                    if (OrderTable.Rows[i]["prop_price"].ToString().ToLower().Contains("rent"))
                    {
                        xtwFeed.WriteCData("For rent");
                    }
                    else
                    {
                        int intPrice=0;
                        if(IsInteger(Price))
                        {
                            intPrice = Convert.ToInt32(Price);
                            if(intPrice<10000)
                            {
                                xtwFeed.WriteCData("For rent");
                            }
                            else
                            {
                                xtwFeed.WriteCData("For sale");
                            }
                        }
                        else
                        {
                            xtwFeed.WriteCData("For sale");
                        }
                    }
                }
                catch
                {
                    xtwFeed.WriteCData("For sale");
                }
                xtwFeed.WriteEndElement();

                string content;
                if (OrderTable.Rows[i]["prop_desc"].ToString().Trim() == "")
                {
                    content = "Property in " + OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim();
                }
                else if (OrderTable.Rows[i]["prop_desc"].ToString().Trim().Length < 30)
                {
                    content = OrderTable.Rows[i]["prop_desc"].ToString().Trim() + " - " + "Property in " + OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim();
                }
                else
                {
                    content = OrderTable.Rows[i]["prop_desc"].ToString().Trim().Replace("warehouses", "ware-houses");
                }

                xtwFeed.WriteStartElement("content");
                xtwFeed.WriteCData(content);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("price");
                xtwFeed.WriteCData(Price);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("property_type");
                xtwFeed.WriteCData(GetPropertyTypeForTrovit(OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("floor_number");
                xtwFeed.WriteCData(OrderTable.Rows[i]["Floors"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city_area");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("postcode");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("region");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteElementString("country", "United States");

                xtwFeed.WriteStartElement("latitude");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("longitude");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("orientation");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteStartElement("agency");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("mls_database");
                xtwFeed.WriteCData(OrderTable.Rows[i]["mls_number"].ToString().Trim());
                xtwFeed.WriteEndElement();

                string plotarea = "";
                string year = "";
                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    //xtwFeed.WriteStartElement("floor_area");
                    //xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim().Replace(",",""));
                    //xtwFeed.WriteEndElement();
                    plotarea = OrderTable.Rows[i]["SqFoots"].ToString().Trim() == "0" ? "" : OrderTable.Rows[i]["SqFoots"].ToString().Replace(",", "").Trim();
                    if (plotarea.Length > 0)
                    {
                        if (!IsInteger(plotarea))
                        {
                            plotarea = "";
                        }
                    }
                    xtwFeed.WriteStartElement("plot_area");
                    xtwFeed.WriteAttributeString("unit", "feet");
                    xtwFeed.WriteCData(plotarea);
                    xtwFeed.WriteEndElement();

                    string rooms = OrderTable.Rows[i]["Bedrooms"].ToString().Trim()=="0"? "": 
                        OrderTable.Rows[i]["Bedrooms"].ToString().Trim();
                    xtwFeed.WriteStartElement("rooms");
                    xtwFeed.WriteCData(rooms);
                    xtwFeed.WriteEndElement();

                    string bathrooms = OrderTable.Rows[i]["FullBaths"].ToString().Trim()=="0"? "": 
                        OrderTable.Rows[i]["FullBaths"].ToString().Trim();
                    string halfbath = OrderTable.Rows[i]["HalfBaths"].ToString().Trim();
                    if (bathrooms !="" && halfbath.Length > 0 && halfbath.Trim() != "0")
                    {
                        bathrooms = bathrooms + "." + halfbath;
                    }

                    xtwFeed.WriteStartElement("bathrooms");
                    xtwFeed.WriteCData(bathrooms);
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("condition");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();

                    year = OrderTable.Rows[i]["YearBuilt"].ToString().Trim() == "0" ? "" : OrderTable.Rows[i]["YearBuilt"].ToString().Trim();
                    if (year.Length > 0)
                    {
                        if (IsInteger(year))
                        {
                            if (Convert.ToInt32(year) < 1700)
                            {
                                year = "";
                            }
                        }
                        else
                        {
                            year = "";
                        }
                    }

                    xtwFeed.WriteStartElement("year");
                    xtwFeed.WriteCData(year);
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("virtual_tour");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                    xtwFeed.WriteEndElement();


                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    //xtwFeed.WriteStartElement("floor_area");
                    //xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    //xtwFeed.WriteEndElement();

                    plotarea = dr["SqFoots"].ToString().Trim() == "0" ? "" : dr["SqFoots"].ToString().Trim().Replace(",", "");
                    if (plotarea.Length > 0)
                    {
                        if (!IsInteger(plotarea))
                        {
                            plotarea = "";
                        }
                    }

                    xtwFeed.WriteStartElement("plot_area");
                    xtwFeed.WriteAttributeString("unit", "feet");
                    xtwFeed.WriteCData(plotarea);
                    xtwFeed.WriteEndElement();

                    string strRooms = dr["Bedrooms"].ToString().Trim() == "0" ? "" : dr["Bedrooms"].ToString().Trim();
                    xtwFeed.WriteStartElement("rooms");
                    xtwFeed.WriteCData(strRooms);
                    xtwFeed.WriteEndElement();

                    string strBathrooms = dr["FullBaths"].ToString().Trim() == "0" ? "" : dr["FullBaths"].ToString().Trim();
                    string strHalfbath = dr["HalfBaths"].ToString().Trim();
                    if (strHalfbath.Length > 0 && strHalfbath.Trim() != "")
                    {
                        strBathrooms = strBathrooms + "." + strHalfbath;
                    }
                    xtwFeed.WriteStartElement("bathrooms");
                    xtwFeed.WriteCData(strBathrooms);
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("condition");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();

                    year = dr["YearBuilt"].ToString().Trim() == "0" ? "" : dr["YearBuilt"].ToString().Trim();
                    if (year.Length > 0)
                    {
                        if (IsInteger(year))
                        {
                            if (Convert.ToInt32(year) < 1700)
                            {
                                year = "";
                            }
                        }
                        else
                        {
                            year = "";
                        }
                    }

                    xtwFeed.WriteStartElement("year");
                    xtwFeed.WriteCData(year);
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("virtual_tour");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }



                //pictures Start
                xtwFeed.WriteStartElement("pictures");

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture_url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture_url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo3"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture_url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo4"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture_url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo4/" + OrderTable.Rows[i]["photo4"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo5"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture_url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo5/" + OrderTable.Rows[i]["photo5"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo6"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture_url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo6/" + OrderTable.Rows[i]["photo6"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo7"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture_url");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo7/" + OrderTable.Rows[i]["photo7"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }
                //picture End

                xtwFeed.WriteEndElement();
                //pictures End

                xtwFeed.WriteStartElement("date");
                xtwFeed.WriteCData(Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("dd/MM/yyyy"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("expiration_date");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("by_owner");
                xtwFeed.WriteCData("0");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("parking");
                xtwFeed.WriteCData("1");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("foreclosure");
                xtwFeed.WriteCData("0");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("is_furnished");
                xtwFeed.WriteCData("1");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("is_new");
                xtwFeed.WriteCData("1");
                xtwFeed.WriteEndElement();


                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        vw_PersonalWebsite_DisplayTableAdapter DAPersonalWebSite = new vw_PersonalWebsite_DisplayTableAdapter();
        PersonalWebsite.vw_PersonalWebsite_DisplayDataTable DtPersonalWebSite = new PersonalWebsite.vw_PersonalWebsite_DisplayDataTable();
        DAPersonalWebSite.FillPersonalWebsites(DtPersonalWebSite);

        for (int i = 0; i <= DtPersonalWebSite.Rows.Count - 1; i++)
        {
            if (DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim() != ""
                && DtPersonalWebSite.Rows[i]["TransactionID"].ToString().Trim().ToLower() != "free"
                && Convert.ToBoolean(DtPersonalWebSite.Rows[i]["IsActivated"].ToString()) != false)
            {
                //property Start
                xtwFeed.WriteStartElement("ad");

                //Location Start

                xtwFeed.WriteStartElement("id");
                xtwFeed.WriteCData(Convert.ToString((Convert.ToInt32(DtPersonalWebSite.Rows[i]["PersonalWebsiteID"].ToString()) + 100000)));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("url");

                if (DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString().Trim() != "")
                {
                    if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 2)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else if (Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_PlanID"].ToString()) == 3)
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/Singlewebposts.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                    else
                    {
                        xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/OnlineFreeWebPost.aspx?PWID=" + DtPersonalWebSite.Rows[i]["PersonalWebSiteID"].ToString().Trim());
                    }
                }

                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData((DtPersonalWebSite.Rows[i]["Street"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["City"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["State"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                string Price = DtPersonalWebSite.Rows[i]["AskingPrice"].ToString();

                xtwFeed.WriteStartElement("type");
                try
                {
                    if (DtPersonalWebSite.Rows[i]["IsForSale"].ToString().ToLower()!="true")
                    {
                        xtwFeed.WriteCData("For rent");
                    }
                    else
                    {
                        int intPrice = 0;
                        if (IsInteger(Price))
                        {
                            intPrice = Convert.ToInt32(Price);
                            if (intPrice < 10000)
                            {
                                xtwFeed.WriteCData("For rent");
                            }
                            else
                            {
                                xtwFeed.WriteCData("For sale");
                            }
                        }
                        else
                        {
                            xtwFeed.WriteCData("For sale");
                        }
                    }
                }
                catch
                {
                    xtwFeed.WriteCData("For sale");
                }
                xtwFeed.WriteEndElement();

                string content;
                if (DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim() == "")
                {
                    content = "Property in " + DtPersonalWebSite.Rows[i]["Street"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["City"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["State"].ToString().Trim();
                }
                else if (DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim().Length < 30)
                {
                    content = DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim() + " - " + "Property in " + DtPersonalWebSite.Rows[i]["Street"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["City"].ToString().Trim() + ", " + DtPersonalWebSite.Rows[i]["State"].ToString().Trim();
                }
                else
                {
                    content = DtPersonalWebSite.Rows[i]["PropertyDescription"].ToString().Trim().Replace("warehouses", "ware-houses");
                }

                xtwFeed.WriteStartElement("content");
                xtwFeed.WriteCData(content);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("price");
                xtwFeed.WriteCData(Price);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("property_type");
                xtwFeed.WriteCData(GetPropertyTypeForTrovit(DtPersonalWebSite.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("address");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Street"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("floor_number");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["Floors"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city_area");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["City"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("postcode");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["ZipCode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("region");
                xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["State"].ToString().Trim());
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteElementString("country", "United States");

                xtwFeed.WriteStartElement("latitude");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("longitude");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("orientation");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                ProfileCommon profile = Profile.GetProfile(DtPersonalWebSite.Rows[i]["UserID"].ToString().Trim());

                xtwFeed.WriteStartElement("agency");
                xtwFeed.WriteCData(profile.Brokerage.BrokerageName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("mls_database");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                string plotarea = "";
                string year = "";
              
                    //xtwFeed.WriteStartElement("floor_area");
                    //xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString().Trim().Replace(",",""));
                    //xtwFeed.WriteEndElement();
                    plotarea = DtPersonalWebSite.Rows[i]["SquareFootage"].ToString().Trim() == "0" ? "" 
                        : DtPersonalWebSite.Rows[i]["SquareFootage"].ToString().Trim().Replace(",", "").Trim();
                    if (plotarea.Length > 0)
                    {
                        if (!IsInteger(plotarea))
                        {
                            plotarea = "";
                        }
                    }
                    xtwFeed.WriteStartElement("plot_area");
                    xtwFeed.WriteAttributeString("unit", "feet");
                    xtwFeed.WriteCData(plotarea);
                    xtwFeed.WriteEndElement();

                    string rooms = DtPersonalWebSite.Rows[i]["Bedrooms"].ToString().Trim() == "0" ? "" :
                        DtPersonalWebSite.Rows[i]["Bedrooms"].ToString().Trim();
                    xtwFeed.WriteStartElement("rooms");
                    xtwFeed.WriteCData(rooms);
                    xtwFeed.WriteEndElement();

                    string bathrooms = DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim() == "0" ? "" :
                        DtPersonalWebSite.Rows[i]["BathroomFull"].ToString().Trim();
                    string halfbath = DtPersonalWebSite.Rows[i]["BathroomPartial"].ToString().Trim();
                    if (bathrooms != "" && halfbath.Length > 0 && halfbath.Trim() != "0")
                    {
                        bathrooms = bathrooms + "." + halfbath;
                    }

                    xtwFeed.WriteStartElement("bathrooms");
                    xtwFeed.WriteCData(bathrooms);
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("condition");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();

                    year = DtPersonalWebSite.Rows[i]["YearBuilt"].ToString().Trim() == "0" ? ""
                        : DtPersonalWebSite.Rows[i]["YearBuilt"].ToString().Trim();
                    if (year.Length > 0)
                    {
                        if (IsInteger(year))
                        {
                            if (Convert.ToInt32(year) < 1700)
                            {
                                year = "";
                            }
                        }
                        else
                        {
                            year = "";
                        }
                    }

                    xtwFeed.WriteStartElement("year");
                    xtwFeed.WriteCData(year);
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("virtual_tour");
                    xtwFeed.WriteCData(DtPersonalWebSite.Rows[i]["VirtualTour"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                   
                //pictures Start
                xtwFeed.WriteStartElement("pictures");

                fly_SlideShowImagesTableAdapter DA = new fly_SlideShowImagesTableAdapter();
                PersonalWebsite.fly_SlideShowImagesDataTable DtSlideShowImages = new PersonalWebsite.fly_SlideShowImagesDataTable();
                DA.FillSlideShowDisplayBySlideShowId(DtSlideShowImages, Convert.ToInt32(DtPersonalWebSite.Rows[i]["fk_SlideShow"].ToString()));
                string[] Folder = DtPersonalWebSite.Rows[i]["EmailAddress"].ToString().Split('@');
                for (int j = 0; j < DtSlideShowImages.Rows.Count; j++)
                {
                    if (j >= 20)
                        break;

                    xtwFeed.WriteStartElement("picture");
                    xtwFeed.WriteStartElement("picture-url");
                    xtwFeed.WriteCData("http://webposts." + clsUtility.SiteTopLevelDomain + "/SlideShow/" + Folder[0] + "/" + DtSlideShowImages.Rows[j]["SlideShowImageName"].ToString());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndElement();
                }

                //picture End

                xtwFeed.WriteEndElement();
                //pictures End

                xtwFeed.WriteStartElement("date");
                xtwFeed.WriteCData(Convert.ToDateTime(DtPersonalWebSite.Rows[i]["CreateDate"].ToString()).ToString("dd/MM/yyyy"));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("expiration_date");
                xtwFeed.WriteCData("");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("by_owner");
                xtwFeed.WriteCData("0");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("parking");
                xtwFeed.WriteCData("1");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("foreclosure");
                xtwFeed.WriteCData("0");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("is_furnished");
                xtwFeed.WriteCData("1");
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("is_new");
                xtwFeed.WriteCData("1");
                xtwFeed.WriteEndElement();


                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        xtwFeed.WriteEndElement();
        //Listings End
        xtwFeed.Close();
        //}
        //catch (Exception error)
        //{
        //}
        //finally
        //{
        //}
    }

    public void  GenerateFrontDoorsFeed(ProfileCommon Profile)
    {
        /*
        <data>
	        <listing>
		        <IdWeb/>
		        <IdMLS/>
		        <IdAccount/>
		        <IdCommunity/>
		        <Address/>
		        <AddrDisplay/>
		        <AddrCrossStreet/>
		        <AddrNeighborhood/>
		        <AddrCity/>
		        <Subdivision/>
		        <AddrCounty/>
		        <AddrState/>
		        <AddrZip/>
		        <AddrCountry/>
		        <Latitude/>
		        <Longitude/>
		        <SchoolDistrictId/>
		        <SchoolElementary/>
		        <SchoolMiddle/>
		        <SchoolHigh/>
		        <CodeBasement/>
		        <CodeConstructionType/>
		        <CodeCoolingType/>
		        <CodeFeatured/>
		        <CodeListingType/>
		        <CodeMarketStatus/>
		        <CodeParking/>
		        <CodePopularFeature/>
		        <CodePropertyType/>
		        <CodeStyle/>
		        <NewConstruction/>
		        <CommentsShort/>
		        <CommentsLong/>
		        <DateNewSource/>
		        <BuiltYear/>
		        <MonthlyCCMaintainence/>
		        <PriceMin/>
		        <Price/>
		        <PriceDisplay/>
		        <RealEstateTaxAnnual/>
		        <RealEstateTaxYear/>
		        <BathsFull/>
		        <BathsPartial/>
		        <Bedroom/>
		        <SqFootAveragePrice/>
		        <LotAcreage/>
		        <LotSizeDisplay/>
		        <RoomsTotal/>
		        <SqFeetInterior/>
		        <SqFeetInteriorDisplay/>
		        <Stories/>
		        <BuildingName/>
		        <BuildingFloorsTotal/>
		        <BuildingUnitsTotal/>
		        <UnitAptId/>
		        <Photo1/>
		        <Photo1Caption/>
		        <Photo2/>
		        <Photo2Caption/>
		        <Photo3/>
		        <Photo3Caption/>
		        <Photo4/>
		        <Photo4Caption/>
		        <Photo5/>
		        <Photo5Caption/>
		        <Floorplan1/>
		        <Floorplan1Caption/>
		        <Floorplan2/>
		        <Floorplan2Caption/>
		        <Floorplan3/>
		        <Floorplan3Caption/>
		        <Floorplan4/>
		        <Floorplan4Caption/>
		        <Floorplan5/>
		        <Floorplan5Caption/>
		        <VirtualTour/>
		        <VideoTour/>
		        <AdvertiserHomePageURL/>
		        <AdvertiserListingURL/>
		        <AdvertiserLogo/>
		        <AdvertiserName/>
		        <Agent1Id/>
		        <Agent1Name/>
		        <Agent1Email/>
		        <Agent1PhonePrimary/>
		        <Agent1Photo/>
		        <Agent1Logo/>
		        <Agent2Id/>
		        <Agent2Name/>
		        <Agent2Email/>
		        <Agent2PhonePrimary/>
		        <Agent2Photo/>
		        <Agent2Logo/>
		        <OpenHouse1Date/>
		        <OpenHouse1Time/>
		        <OpenHouse2Date/>
		        <OpenHouse2Time/>
	        </listing>
        </data>
         */

        //Xml Feed = new Xml();

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/FrontDoorsFeed.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        //try
        //{
        //properties Start
        xtwFeed.WriteStartElement("data");

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //property Start
                xtwFeed.WriteStartElement("listing");

                //Location Start

                xtwFeed.WriteStartElement("IdWeb");
                xtwFeed.WriteCData(OrderTable.Rows[i]["order_id"].ToString());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("IdMLS","");
                xtwFeed.WriteElementString("IdAccount","");
                xtwFeed.WriteElementString("IdCommunity","");

                xtwFeed.WriteStartElement("Address");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("AddrDisplay");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("AddrCrossStreet","");
                xtwFeed.WriteElementString("AddrNeighborhood","");

                xtwFeed.WriteStartElement("AddrCity");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("Subdivision","");
                xtwFeed.WriteElementString("AddrCounty","");

                xtwFeed.WriteStartElement("AddrState");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("AddrZip");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("AddrCountry", "USA");

                xtwFeed.WriteElementString("Latitude", "");
                xtwFeed.WriteElementString("Longitude", "");
                xtwFeed.WriteElementString("SchoolDistrictId", "");
                xtwFeed.WriteElementString("SchoolElementary", "");
                xtwFeed.WriteElementString("SchoolMiddle", "");
                xtwFeed.WriteElementString("SchoolHigh", "");
                xtwFeed.WriteElementString("CodeBasement", "");
                xtwFeed.WriteElementString("CodeConstructionType", "");
                xtwFeed.WriteElementString("CodeCoolingType", "");
                xtwFeed.WriteElementString("CodeLotFeature", "");
                xtwFeed.WriteElementString("CodeMarketStatus", "");
                xtwFeed.WriteElementString("CodeParking", "");
                xtwFeed.WriteElementString("CodePets", "");
                xtwFeed.WriteElementString("CodePopularFeature", "");

                xtwFeed.WriteStartElement("CodePropertyType");
                xtwFeed.WriteCData(GetPropertyTypeForFrontDoor(OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("CodeStyle", "");
                xtwFeed.WriteElementString("NewConstruction", "");
                xtwFeed.WriteElementString("CommentsShort", "");

                xtwFeed.WriteStartElement("CommentsLong");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("DateNewSource", "");
                xtwFeed.WriteElementString("BuiltYear", "");
                xtwFeed.WriteElementString("MonthlyCCMaintainence", "");
                xtwFeed.WriteElementString("PriceMin", "");

                string Price = "0";

                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString().Replace(",", "").Replace(".00", "");
                    }
                }
                xtwFeed.WriteStartElement("Price");
                xtwFeed.WriteCData(Price);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("PriceDisplay", "");
                xtwFeed.WriteElementString("RealEstateTaxAnnual", "");
                xtwFeed.WriteElementString("RealEstateTaxYear", "");




                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("BathsFull");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["FullBaths"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("BathsPartial");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["HalfBaths"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("Bedroom");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("SqFootAveragePrice", "");
                    xtwFeed.WriteElementString("LotAcreage", "");
                    xtwFeed.WriteElementString("LotSizeDisplay", "");

                    xtwFeed.WriteStartElement("RoomsTotal");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("SqFeetInterior");
                    xtwFeed.WriteCData(OrderTable.Rows[i]["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    xtwFeed.WriteStartElement("BathsFull");
                    xtwFeed.WriteCData(dr["FullBaths"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("BathsPartial");
                    xtwFeed.WriteCData(dr["HalfBaths"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("Bedroom");
                    xtwFeed.WriteCData(dr["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteElementString("SqFootAveragePrice", "");
                    xtwFeed.WriteElementString("LotAcreage", "");
                    xtwFeed.WriteElementString("LotSizeDisplay", "");

                    xtwFeed.WriteStartElement("RoomsTotal");
                    xtwFeed.WriteCData(dr["Bedrooms"].ToString().Trim());
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("SqFeetInterior");
                    xtwFeed.WriteCData(dr["SqFoots"].ToString());
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteElementString("SqFeetInteriorDisplay", "");
                xtwFeed.WriteElementString("Stories", "");
                xtwFeed.WriteElementString("BuildingName", "");
                xtwFeed.WriteElementString("BuildingFloorsTotal", "");
                xtwFeed.WriteElementString("BuildingUnitsTotal", "");
                xtwFeed.WriteElementString("UnitAptId", "");


                //pictures/picture Start
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Photo1");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo1Caption", "");
                }
                else
                {
                    xtwFeed.WriteStartElement("Photo1");
                    xtwFeed.WriteCData(" ");
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo1Caption", "");
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Photo2");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo2Caption", "");
                }
                else
                {
                    xtwFeed.WriteStartElement("Photo2");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo2Caption", "");
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo3"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Photo3");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo3Caption", "");
                }
                else
                {
                    xtwFeed.WriteStartElement("Photo3");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo3Caption", "");
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo4"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Photo4");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo4/" + OrderTable.Rows[i]["photo4"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo4Caption", "");
                }
                else
                {
                    xtwFeed.WriteStartElement("Photo4");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo4Caption", "");
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo5"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Photo5");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo5/" + OrderTable.Rows[i]["photo5"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo5Caption", "");
                }
                else
                {
                    xtwFeed.WriteStartElement("Photo5");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo5Caption", "");
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo6"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Photo6");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo6/" + OrderTable.Rows[i]["photo6"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo6Caption", "");
                }
                else
                {
                    xtwFeed.WriteStartElement("Photo6");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo6Caption", "");
                }
                //picture End

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo7"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("Photo7");
                    xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo7/" + OrderTable.Rows[i]["photo7"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo7Caption", "");
                }
                else
                {
                    xtwFeed.WriteStartElement("Photo7");
                    xtwFeed.WriteCData("");
                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteElementString("Photo7Caption", "");
                }
                //picture End

                xtwFeed.WriteElementString("Photo8", "");
                xtwFeed.WriteElementString("Photo8Caption", "");
                xtwFeed.WriteElementString("Photo9", "");
                xtwFeed.WriteElementString("Photo9Caption", "");
                xtwFeed.WriteElementString("Photo10", "");
                xtwFeed.WriteElementString("Photo10Caption", "");
                xtwFeed.WriteElementString("Photo11", "");
                xtwFeed.WriteElementString("Photo11Caption", "");
                xtwFeed.WriteElementString("Photo12", "");
                xtwFeed.WriteElementString("Photo12Caption", "");
                xtwFeed.WriteElementString("Photo13", "");
                xtwFeed.WriteElementString("Photo13Caption", "");
                xtwFeed.WriteElementString("Photo14", "");
                xtwFeed.WriteElementString("Photo14Caption", "");
                xtwFeed.WriteElementString("Photo15", "");
                xtwFeed.WriteElementString("Photo15Caption", "");
                xtwFeed.WriteElementString("Photo16", "");
                xtwFeed.WriteElementString("Photo16Caption", "");
                xtwFeed.WriteElementString("Photo17", "");
                xtwFeed.WriteElementString("Photo17Caption", "");
                xtwFeed.WriteElementString("Photo18", "");
                xtwFeed.WriteElementString("Photo18Caption", "");
                xtwFeed.WriteElementString("Photo19", "");
                xtwFeed.WriteElementString("Photo19Caption", "");
                xtwFeed.WriteElementString("Photo20", "");
                xtwFeed.WriteElementString("Photo20Caption", "");

                xtwFeed.WriteElementString("Floorplan1", "");
                xtwFeed.WriteElementString("Floorplan1Caption", "");
                xtwFeed.WriteElementString("Floorplan2", "");
                xtwFeed.WriteElementString("Floorplan2Caption", "");
                xtwFeed.WriteElementString("Floorplan3", "");
                xtwFeed.WriteElementString("Floorplan3Caption", "");
                xtwFeed.WriteElementString("Floorplan4", "");
                xtwFeed.WriteElementString("Floorplan4Caption", "");
                xtwFeed.WriteElementString("Floorplan5", "");
                xtwFeed.WriteElementString("Floorplan5Caption", "");

                xtwFeed.WriteStartElement("VirtualTour");
                xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("VideoTour", "");

                xtwFeed.WriteStartElement("AdvertiserHomePageURL");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("AdvertiserListingURL");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("AdvertiserLogo", "");
                
                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteStartElement("AdvertiserName");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("Agent1Id", "");

                xtwFeed.WriteStartElement("Agent1Name");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("Agent1Email");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteElementString("Agent1PhonePrimary", "");
                xtwFeed.WriteElementString("Agent1Photo", "");
                xtwFeed.WriteElementString("Agent1Logo", "");
                xtwFeed.WriteElementString("Agent2Id", "");
                xtwFeed.WriteElementString("Agent2Name", "");
                xtwFeed.WriteElementString("Agent2Email", "");
                xtwFeed.WriteElementString("Agent2PhonePrimary", "");
                xtwFeed.WriteElementString("Agent2Photo", "");
                xtwFeed.WriteElementString("Agent2Logo", "");
                xtwFeed.WriteElementString("OpenHouse1Date", "");
                xtwFeed.WriteElementString("OpenHouse1Time", "");
                xtwFeed.WriteElementString("OpenHouse2Date", "");
                xtwFeed.WriteElementString("OpenHouse2Time", "");
                xtwFeed.WriteElementString("OpenHouse3Date", "");
                xtwFeed.WriteElementString("OpenHouse3Time", "");
                xtwFeed.WriteElementString("OpenHouse4Date", "");
                xtwFeed.WriteElementString("OpenHouse4Time", "");
                xtwFeed.WriteEndElement();
            }
        }
        xtwFeed.WriteEndElement();
        xtwFeed.Close();
    }

    public void GenerateHotPadsFeed(ProfileCommon Profile)
    {
        /*
        <?xml version="1.0" encoding="UTF-8"?>
            <hotPadsItems version="2.1"> 
            <!-- companies are optional -->
             
            <!-- include multiple -->
             
            <!-- id (required) is the company ID in your system -->
             -<Company id="company2"> <name>Revlon Realty</name> 
            <!-- limit of 255 characters for URL -->
             <website>http://somesite.com</website> 
            <!-- limit of 255 characters for URL -->
             <CompanyLogo source="http://yoursite.com/path/to/logo.gif"/> </Company> 
            <!-- id (required) is the unique identifier in your system -->
             
            <!-- type (required) must be one of the following: RENTAL, SALE, CORPORATE, SUBLET, ROOM, FORECLOSURE, AUCTION, NEW_HOME, VACATION -->
             
            <!-- companyId (optional) from company section above -->
             
            <!-- propertyType (required) must be one of the following: CONDO = single unit in a community or building, HOUSE = single family house, TOWNHOUSE = townhouse or rowhouse, LAND = acreage or empty lot -->
             -<Listing id="abc123" propertyType="CONDO" companyId="company2" type="RENTAL"> 
            <!-- name &amp; location -->
             <name>The Willows at Yorkshire</name> 
            <!-- apartment unit # if needed -->
             <unit>55C</unit> 
            <!-- if hide is true, the street address will be hidden from users -->
             <street hide="false">124 Main St.</street> <city>El Paso</city> <state>CT</state> <zip>00040</zip> <country>US</country> 
            <!-- ISO Country Codes only. see http://www.iso.org/iso/iso3166_en_code_lists.txt -->
             
            <!-- lat/long are optional but guarantee correct positioning -->
             
            <!-- if included, MUST contain more than 2 digits of precision past the decimal point -->
             <latitude>34.123456</latitude> <longitude>-77.654321</longitude> <contactName>Rosemarie</contactName> 
            <!-- contactEmail is where email leads are sent -->
             
            <!-- contactEmail and/or contactPhone required -->
             <contactEmail>rosemarie@somesite.com</contactEmail> <contactPhone>123-456-7890</contactPhone> <contactFax>123-456-7899</contactFax> <contactTimes>Sunday: 9:00 am to 5:00 pm Monday: 9:00 am to 5:00 pm Tuesday: 9:00 am to 5:00 pm Wednesday: 9:00 am to 5:00 pm Thursday: 9:00 am to 5:00 pm Friday: 9:00 am to 5:00 pm Saturday: 9:00 am to 5:00 pm </contactTimes> 
            <!-- limit of 255 characters for Preview Message -->
             <previewMessage>Spacious Everything!</previewMessage> <description>This apartment comes with 3 parking spaces. Check out the kitchen photos</description> <terms>One year lease, then month to month. Deposit equals first month's rent</terms> 
            <!-- limit of 255 characters for URL -->
             <website>http://somesite.com/listings.asp?id=299</website> 
            <!-- limit of 255 characters for URL -->
             <virtualTourUrl>http://somesite.com</virtualTourUrl> 
            <!-- ListingTag elements are all optional. If you decide to include them, type is required and must be on of the following: PROPERTY_AMENITY = outdoor amenity. if building/community, then shared by residents MODEL_AMENITY = inside amenity RENT_INCLUDES = item included in rent MLS_NAME = Name of MLS this property is listed in. Do not include unless known. MLS_LISTING_ID = ID assigned to this property by an MLS. Do not include unless known. DOGS_ALLOWED = 'true' if dogs allowed. CATS_ALLOWED = 'true' if cats allowed. YEAR_BUILT = Year built, if known LOT_SIZE = Lot size, if known STORIES = Number of stories in this unit. SCHOOL_DISTRICT HIGH_SCHOOL MIDDLE_SCHOOL ELEMENTARY_SCHOOL PARKING_SPACES PARKING_SPACES_COVERED -->
             -<ListingTag type="PROPERTY_AMENITY"><tag>Pool</tag></ListingTag> -<ListingTag type="PROPERTY_AMENITY"><tag>Covered Parking</tag></ListingTag> -<ListingTag type="PROPERTY_AMENITY"><tag>Free Hot Dogs on Saturdays</tag></ListingTag> -<ListingTag type="MODEL_AMENITY"><tag>Washer/Dryer</tag></ListingTag> -<ListingTag type="MODEL_AMENITY_SELECT"><tag>Hardwood Floors</tag></ListingTag> -<ListingTag type="RENT_INCLUDES"><tag>Gas</tag></ListingTag> -<ListingTag type="RENT_INCLUDES"><tag>Water</tag></ListingTag> -<ListingTag type="DOGS_ALLOWED"><tag>false</tag></ListingTag> 
            <!-- ListingPermission elements are optional. Each ListingPermission element you choose to include should contain one email address of a HotPads user account that you wish to have access to the listing. -->
             <ListingPermission>yourEmailAddress@example.com</ListingPermission> 
            <!-- include multiple, first photo is put on main information screen. -->
             
            <!-- source is required, label and caption are optional. -->
             
            <!-- limit of 255 characters for URL -->
             -<ListingPhoto source="http://yoursite.com/path/to/photo1.jpg"> <label>Garage</label> 
            <!-- 30 character limit, no markup -->
             <caption>Park your car here.</caption> 
            <!-- 60 character limit, no markup -->
             </ListingPhoto> -<ListingPhoto source="http://yoursite.com/path/to/photo2.jpg"> <label>Seesaw</label> 
            <!-- 30 character limit, no markup -->
             <caption>A rare find!</caption> 
            <!-- 60 character limit, no markup -->
             </ListingPhoto> <price>320000</price> <pricingFrequency>ONCE</pricingFrequency> 
            <!-- acceptable values: ONCE, MONTH, WEEK, DAY -->
             <HOA-Fee>295</HOA-Fee> <numBedrooms>2</numBedrooms> <numFullBaths>2</numFullBaths> <numHalfBaths>1</numHalfBaths> <squareFeet>1500</squareFeet> </Listing> </hotPadsItems>
         */

        //Xml Feed = new Xml();

        XmlTextWriter xtwFeed = new XmlTextWriter(HttpContext.Current.Server.MapPath("~/XMLFeeds/HotPadsFeed.xml"), Encoding.UTF8);
        xtwFeed.WriteStartDocument();

        // The mandatory rss tag

        //try
        //{
        //properties Start
        xtwFeed.WriteStartElement("hotPadsItems");
        xtwFeed.WriteAttributeString("version", "2.1");
        CultureInfo ci = new CultureInfo("de-DE");

        xtwFeed.WriteStartElement("Company");
        xtwFeed.WriteAttributeString("id", clsUtility.ProjectName.ToLower());
        xtwFeed.WriteElementString("name", clsUtility.ProjectName);
        xtwFeed.WriteElementString("website", clsUtility.SiteHttpWwwRootUrl);
        xtwFeed.WriteStartElement("CompanyLogo");
        xtwFeed.WriteAttributeString("source", clsUtility.SiteHttpWwwRootUrl + "/images/logo.jpg");
        xtwFeed.WriteEndElement();  //Company Logo End
        xtwFeed.WriteEndElement();  //Company End

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        for (int i = 0; i <= OrderTable.Rows.Count - 1; i++)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                string type = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().ToLower().Contains("rent"))
                {
                    type= "RENTAL";
                }
                else
                {
                    type = "SALE";
                }

                //property Start
                xtwFeed.WriteStartElement("Listing");
                xtwFeed.WriteAttributeString("id", OrderTable.Rows[i]["order_id"].ToString());
                xtwFeed.WriteAttributeString("propertyType", GetPropertyTypeForHotPads(OrderTable.Rows[i]["PropertyType"].ToString().Trim()));
                xtwFeed.WriteAttributeString("companyId", clsUtility.ProjectName.ToLower());
                xtwFeed.WriteAttributeString("type", type);

                xtwFeed.WriteStartElement("name");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("street");
                xtwFeed.WriteAttributeString("hide", "false");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("city");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_city"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("state");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("zip");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_zipcode"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("country");
                xtwFeed.WriteCData("US");
                xtwFeed.WriteEndElement();

                ProfileCommon profile = Profile.GetProfile(OrderTable.Rows[i]["customer_id"].ToString().Trim());

                xtwFeed.WriteStartElement("contactName");
                xtwFeed.WriteCData(profile.FirstName + " " + profile.LastName);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("contactEmail");
                xtwFeed.WriteCData(OrderTable.Rows[i]["customer_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("contactPhone");
                xtwFeed.WriteCData(profile.Contact.PhoneBusiness);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("website");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("virtualTourUrl");
                xtwFeed.WriteCData(OrderTable.Rows[i]["virtualtour_link"].ToString().Trim());
                xtwFeed.WriteEndElement();

                //pictures Start

                //pictures/picture Start
                if (OrderTable.Rows[i]["photo1"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ListingPhoto");
                    xtwFeed.WriteAttributeString("source", clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo2"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ListingPhoto");
                    xtwFeed.WriteAttributeString("source", clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo2/" + OrderTable.Rows[i]["photo2"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo3"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ListingPhoto");
                    xtwFeed.WriteAttributeString("source", clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo3/" + OrderTable.Rows[i]["photo3"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo4"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ListingPhoto");
                    xtwFeed.WriteAttributeString("source", clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo4/" + OrderTable.Rows[i]["photo4"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo5"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ListingPhoto");
                    xtwFeed.WriteAttributeString("source", clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo5/" + OrderTable.Rows[i]["photo5"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo6"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ListingPhoto");
                    xtwFeed.WriteAttributeString("source", clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo6/" + OrderTable.Rows[i]["photo6"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                if (OrderTable.Rows[i]["photo7"].ToString().Trim() != "")
                {
                    xtwFeed.WriteStartElement("ListingPhoto");
                    xtwFeed.WriteAttributeString("source", clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo7/" + OrderTable.Rows[i]["photo7"].ToString().Trim());
                    xtwFeed.WriteEndElement();
                }

                //picture End

                string Price = "";
                if (OrderTable.Rows[i]["prop_price"].ToString().Trim() != "")
                {
                    string[] ArrPrice = OrderTable.Rows[i]["prop_price"].ToString().Split('|');
                    if (ArrPrice.Length > 1)
                    {
                        Price = ArrPrice[1].ToString().Replace(",", "").Replace(".00", "");
                    }
                }

                xtwFeed.WriteStartElement("price");
                xtwFeed.WriteCData(Price);
                xtwFeed.WriteEndElement();

                xtwFeed.WriteStartElement("pricingFrequency");
                xtwFeed.WriteCData("ONCE");
                xtwFeed.WriteEndElement();


                string plotarea = "";

                if (OrderTable.Rows[i]["Bedrooms"].ToString().Trim() != "")
                {
                    string rooms = OrderTable.Rows[i]["Bedrooms"].ToString().Trim() == "0" ? "" :
                        OrderTable.Rows[i]["Bedrooms"].ToString().Trim();
                    xtwFeed.WriteStartElement("numBedrooms");
                    xtwFeed.WriteCData(rooms);
                    xtwFeed.WriteEndElement();

                    string bathrooms = OrderTable.Rows[i]["FullBaths"].ToString().Trim() == "0" ? "" :
                        OrderTable.Rows[i]["FullBaths"].ToString().Trim();

                    xtwFeed.WriteStartElement("numFullBaths");
                    xtwFeed.WriteCData(bathrooms);
                    xtwFeed.WriteEndElement();

                    string halfBathRoom = OrderTable.Rows[i]["HalfBaths"].ToString().Trim() == "0" ? "" :
                        OrderTable.Rows[i]["HalfBaths"].ToString().Trim();

                    xtwFeed.WriteStartElement("numHalfBaths");
                    xtwFeed.WriteCData(halfBathRoom);
                    xtwFeed.WriteEndElement();

                    plotarea = OrderTable.Rows[i]["SqFoots"].ToString().Trim() == "0" ? "" : OrderTable.Rows[i]["SqFoots"].ToString().Replace(",", "").Trim();
                    if (plotarea.Length > 0)
                    {
                        if (!IsInteger(plotarea))
                        {
                            plotarea = "";
                        }
                    }
                    xtwFeed.WriteStartElement("squareFeet");
                    xtwFeed.WriteCData(plotarea);
                    xtwFeed.WriteEndElement();
                }
                else
                {
                    DataRow dr = ReturnCustomFieldValue(OrderTable.Rows[i]);

                    string rooms = dr["Bedrooms"].ToString().Trim() == "0" ? "" :
                        dr["Bedrooms"].ToString().Trim();
                    xtwFeed.WriteStartElement("numBedrooms");
                    xtwFeed.WriteCData(rooms);
                    xtwFeed.WriteEndElement();

                    string bathrooms = dr["FullBaths"].ToString().Trim() == "0" ? "" :
                        dr["FullBaths"].ToString().Trim();

                    xtwFeed.WriteStartElement("numFullBaths");
                    xtwFeed.WriteCData(bathrooms);
                    xtwFeed.WriteEndElement();

                    string halfBathRoom = dr["HalfBaths"].ToString().Trim() == "0" ? "" :
                        dr["HalfBaths"].ToString().Trim();

                    xtwFeed.WriteStartElement("numHalfBaths");
                    xtwFeed.WriteCData(halfBathRoom);
                    xtwFeed.WriteEndElement();

                    plotarea = dr["SqFoots"].ToString().Trim() == "0" ? "" : 
                        dr["SqFoots"].ToString().Replace(",", "").Trim();
                    if (plotarea.Length > 0)
                    {
                        if (!IsInteger(plotarea))
                        {
                            plotarea = "";
                        }
                    }
                    xtwFeed.WriteStartElement("squareFeet");
                    xtwFeed.WriteCData(plotarea);
                    xtwFeed.WriteEndElement();
                }

                xtwFeed.WriteEndElement();
                //Listing End
            }
        }

        xtwFeed.WriteEndElement();
        xtwFeed.Close();
    }

    public StringBuilder GenerateRSS()
    { 
    //<?xml version="1.0" encoding="utf-8"?>
    //<rss version="2.0">
    //    <channel>
    //        <title>RSS feed Example</title>
    //        <description>This is an example of a generated RSS feed.</description>
    //        <link>http://rss-tutorial.com/rss-website-feed-manual.htm</link>
    //            <item>
    //                <title>This way is effective</title>
    //                <description>This is an effective way to create and maintain an RSS feed.</description>
    //                <link>http://rss-tutorial.com/rss-website-feed-manual.htm</link>
    //            </item>
    //            <item>
    //                <title>This way is efficient</title>
    //                <description>This is an efficient way to create and maintain an RSS feed.</description>
    //                <link>http://rss-tutorial.com/rss-website-feed-manual.htm</link>
    //            </item>
    //    </channel>
    //</rss>    

        StringBuilder sb=new StringBuilder();
        XmlTextWriter xtwFeed = new XmlTextWriter(new StringWriterWithEncoding(sb, Encoding.UTF8));
        xtwFeed.WriteStartDocument();
        
        // The mandatory rss tag

        //try
        //{
        //properties Start
        xtwFeed.WriteStartElement("rss");
        xtwFeed.WriteAttributeString("version", "2.0");
        xtwFeed.WriteAttributeString(" xmlns:dc", "http://purl.org/dc/elements/1.1/");

        xtwFeed.WriteStartElement("channel");

        xtwFeed.WriteStartElement("title");
        xtwFeed.WriteCData(clsUtility.ProjectName + " Real Estate Email Marketing");
        xtwFeed.WriteEndElement();

        //xtwFeed.WriteElementString("link", clsUtility.SiteHttpWwwRootUrl);
        xtwFeed.WriteStartElement("link");
        xtwFeed.WriteCData(clsUtility.SiteHttpWwwRootUrl);
        xtwFeed.WriteEndElement();

        xtwFeed.WriteStartElement("description");
        xtwFeed.WriteCData(clsUtility.ProjectName + " is dedicated to meet the marketing needs of today’s real estate professionals with affordable and effective real estate marketing solutions providing innovative services focusing on creating and sending professional email flyers to other agents and real estate professionals in your area.");
        xtwFeed.WriteEndElement();

        FlyerMeDSTableAdapters.fly_orderTableAdapter OrderAdapter = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
        FlyerMeDS.fly_orderDataTable OrderTable = OrderAdapter.GetDataForPaidFlyer();

        for (int i = OrderTable.Rows.Count-1; i >= OrderTable.Rows.Count - 20; i--)
        {
            if (OrderTable.Rows[i]["invoice_transaction_id"].ToString().Trim() != "")
            {
                //item Start
                xtwFeed.WriteStartElement("item");

                //Location Start

                //xtwFeed.WriteElementString("title", OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim());

                xtwFeed.WriteStartElement("title");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_address1"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_city"].ToString().Trim() + ", " + OrderTable.Rows[i]["prop_state"].ToString().Trim());
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteElementString("link", clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteStartElement("link");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "showflyer.aspx?oid=" + OrderTable.Rows[i]["order_id"].ToString().Trim());
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteElementString("images", clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                xtwFeed.WriteStartElement("images");
                xtwFeed.WriteCData(clsUtility.GetRootHost + "order/" + OrderTable.Rows[i]["order_id"].ToString().Trim() + "/photo1/" + OrderTable.Rows[i]["photo1"].ToString().Trim());
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteElementString("description", OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteStartElement("description");
                xtwFeed.WriteCData(OrderTable.Rows[i]["prop_desc"].ToString().Trim());
                xtwFeed.WriteEndElement();

                //xtwFeed.WriteElementString("pubDate", Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("MM/dd/yyyy"));
                xtwFeed.WriteStartElement("pubDate");
                xtwFeed.WriteCData(Convert.ToDateTime(OrderTable.Rows[i]["created_on"].ToString()).ToString("MM/dd/yyyy"));
                xtwFeed.WriteEndElement();
                //end item
                xtwFeed.WriteEndElement();

            }
        }
        xtwFeed.WriteEndElement();
        xtwFeed.WriteEndElement();
        xtwFeed.WriteEndDocument();
        xtwFeed.Close();
        return sb;
    }

    private DataRow ReturnCustomFieldValue(DataRow drData)
    {
        string[] ArrAttributes ={ "Bedrooms", "Full Baths", "Half Baths", "Sq. Footage", "Year Built", "Lot Size" };
        string[] ArrAttributesFields ={ "Bedrooms", "FullBaths", "HalfBaths", "SqFoots", "YearBuilt", "LotSize" };

        /*
            Bedrooms
            Full Baths
            Half Baths
            Sq. Footage
            Year Built
            Lot Size            
         */
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("Bedrooms", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("FullBaths", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("HalfBaths", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("SqFoots", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("YearBuilt", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("LotSize", typeof(string));
        dt.Columns.Add(dc);
        DataRow dr;

        /*
         * custom_field1
         * custom_field2
         * custom_field3
         * custom_field4
         * custom_field5
         * custom_field6
         * custom_field7
         * custom_field8
         * custom_field9
         * custom_field10
         * custom_field_value1
         * custom_field_value2
         * custom_field_value3
         * custom_field_value4
         * custom_field_value5
         * custom_field_value6
         * custom_field_value7
         * custom_field_value8
         * custom_field_value9
         * custom_field_value10
         */

        dr = dt.NewRow();

        for (int i = 0; i <= ArrAttributes.Length - 1; i++)
        {
            if (drData["custom_field1"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value1"];
            }

            if (drData["custom_field2"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value2"];
            }

            if (drData["custom_field3"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value3"];
            }

            if (drData["custom_field4"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value4"];
            }

            if (drData["custom_field5"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value5"];
            }

            if (drData["custom_field6"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value6"];
            }

            if (drData["custom_field7"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value7"];
            }

            if (drData["custom_field8"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value8"];
            }

            if (drData["custom_field9"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value9"];
            }

            if (drData["custom_field10"].ToString() == ArrAttributes[i])
            {
                dr[ArrAttributesFields[i]] = drData["custom_field_value10"];
            }
        }

        dt.Rows.Add(dr);

        return dt.Rows[0];

    }

    private string GetPropertyTypeForZillow(string strProperty)
    {
        string strChangedToProperty = "Other";
        clsData objData = new clsData();
        DataTable dt = objData.GetData("select * from fly_PropertyType");
        if (strProperty == "Apartment/Condo/Townhouse" || strProperty == "Apartment" || strProperty == "Condo")
        {
            strChangedToProperty = "Condo";
        }
        else if (strProperty == "Townhouse")
        {
            strChangedToProperty = "Townhouse";
        }
        else if (strProperty == "Coop")
        {
            strChangedToProperty = "Coop";
        }
        else if (strProperty == "Mobile/Manufactured")
        {
            strChangedToProperty = "Manufactured";
        }
        else if (strProperty == "Multi-Family")
        {
            strChangedToProperty = "MultiFamily";
        }
        else if (strProperty == "Lot/Land")
        {
            strChangedToProperty = "VacantLand";
        }
        else if (strProperty == "Single-Family Home")
        {
            strChangedToProperty = "SingleFamily";
        }
        else
        {
            strChangedToProperty = "Other";
        }
        return strChangedToProperty;
    }

    private string GetPropertyTypeForHomeSpace(string strProperty)
    {
        string strChangedToProperty = "SFH";
        clsData objData = new clsData();
        DataTable dt = objData.GetData("select * from fly_PropertyType");
        if (strProperty == "Apartment/Condo/Townhouse" || strProperty == "Apartment" || strProperty == "Condo")
        {
            strChangedToProperty = "CONDO";
        }
        else if (strProperty == "Townhouse")
        {
            strChangedToProperty = "TOWNHOUSE";
        }
        else if (strProperty == "Coop")
        {
            strChangedToProperty = "COOP";
        }
        else if (strProperty == "Mobile/Manufactured")
        {
            strChangedToProperty = "MOBILE";
        }
        else if (strProperty == "Multi-Family")
        {
            strChangedToProperty = "MULTIFAM";
        }
        else if (strProperty == "Lot/Land")
        {
            strChangedToProperty = "LAND";
        }
        else if (strProperty == "Single-Family Home")
        {
            strChangedToProperty = "SFH";
        }
        else
        {
            strChangedToProperty = "SFH";
        }
        return strChangedToProperty;
    }

    private string GetPropertyCategoryForPropBot(string strPropertyCategory, string strPropertyType)
    {
        string strChangedCategory = "rfs";
        if (strPropertyType == "Apartment/Condo/Townhouse"
            || strPropertyType == "Apartment" 
            || strPropertyType == "Condo"
            || strPropertyType == "Townhouse"
            || strPropertyType == "Single-Family Home"
            || strPropertyType == "Multi-Family"
            || strPropertyType == "Loft"
            || strPropertyType == "Coop"
            || strPropertyType == "TIC"
            || strPropertyType == "Houseboat"
            || strPropertyType == "Income/Investment"
            || strPropertyType == "Mobile/Manufactured")
        {
            if (strPropertyCategory == "Rentals - Residential")
            {
                strChangedCategory = "rfr";
            }
            else
            {
                strChangedCategory = "rfs";
            }
        }
        else if (strPropertyType == "Farm/Ranch" || strPropertyType == "Lot/Land")
        {
            strChangedCategory = "lfs";
        }
        else
        {
            strChangedCategory = "rfs";
        }

        return strChangedCategory;
    }

    private string GetPropertyTypeForPropBot(string strPropertyType)
    {
        string strChangedType = "11";
        if (strPropertyType == "Apartment/Condo/Townhouse"
            || strPropertyType == "Single-Family Home" 
            || strPropertyType == "TIC" 
            || strPropertyType == "Houseboat" 
            || strPropertyType == "Income/Investment" 
            || strPropertyType == "Loft")
        {
            strChangedType="11";
        }
        else if (strPropertyType == "Apartment" || strPropertyType == "Multi-Family")
        {
            strChangedType="9";
        }
        else if(strPropertyType == "Condo")
        {
            strChangedType="63";
        }
        else if(strPropertyType == "Townhouse")
        {
            strChangedType="61";
        }
        else if(strPropertyType == "Coop")
        {
            strChangedType="88";
        }
        else if (strPropertyType == "Mobile/Manufactured")
        {
            strChangedType="76";
        }
        else if (strPropertyType == "Farm/Ranch")
        {
            strChangedType="99";
        }
        else if (strPropertyType == "Lot/Land")
        {
            strChangedType="99";
        }
        else
        {
            strChangedType="8";
        }

        return strChangedType;
    }

    private string GetPropertyTypeForClickAbleDirectory(string strPropertyType)
    {
        string strChangedType = "Single Family";
        if (strPropertyType == "Apartment/Condo/Townhouse"
            || strPropertyType == "Single-Family Home"
            || strPropertyType == "TIC"
            || strPropertyType == "Houseboat"
            || strPropertyType == "Income/Investment"
            || strPropertyType == "Loft")
        {
            strChangedType = "Single Family";
        }
        else if (strPropertyType == "Apartment" || strPropertyType == "Multi-Family")
        {
            strChangedType = "Multi Family";
        }
        else if (strPropertyType == "Condo")
        {
            strChangedType = "Condo";
        }
        else if (strPropertyType == "Townhouse")
        {
            strChangedType = "Townhome";
        }
        else if (strPropertyType == "Coop")
        {
            strChangedType = "Coop";
        }
        else if (strPropertyType == "Mobile/Manufactured")
        {
            strChangedType = "Mobile Home";
        }
        else if (strPropertyType == "Farm/Ranch")
        {
            strChangedType = "Farm";
        }
        else if (strPropertyType == "Lot/Land")
        {
            strChangedType = "Land";
        }
        else
        {
            strChangedType = "8";
        }

        return strChangedType;
    }

    private string GetPropertyTypeForRealeStateAdvisor(int PropertyID, string strProperty)
    {
        string strChangedToProperty = "0";
        if (PropertyID == 1)
        {
            if (strProperty == "Apartment/Condo/Townhouse" || strProperty == "Apartment" || strProperty == "Condo" || strProperty == "Townhouse")
            {
                strChangedToProperty = "1";
            }
            else
            {
                strChangedToProperty = "0";
            }
        }
        else if (PropertyID == 2)
        {
            if (strProperty == "Mobile/Manufactured")
            {
                strChangedToProperty = "1";
            }
            else
            {
                strChangedToProperty = "0";
            }
        }
        else if (PropertyID == 3)
        {
            if (strProperty == "Farm/Ranch")
            {
                strChangedToProperty = "1";
            }
            else
            {
                strChangedToProperty = "0";
            }
        }
        else if (PropertyID == 4)
        {
            if (strProperty == "Multi-Family")
            {
                strChangedToProperty = "1";
            }
            else
            {
                strChangedToProperty = "0";
            }
        }
        else if (PropertyID == 5)
        {
            if (strProperty == "Lot/Land")
            {
                strChangedToProperty = "1";
            }
            else
            {
                strChangedToProperty = "0";
            }
        }
        else if (PropertyID == 6)
        {
            if (strProperty == "Single-Family Home" || strProperty == "Coop")
            {
                strChangedToProperty = "1";
            }
            else
            {
                strChangedToProperty = "0";
            }
        }
        return strChangedToProperty;
    }

    private string GetPropertyTypeForCLRSearch(string strProperty)
    {
        string strChangedToProperty = "Commercial";
        clsData objData = new clsData();
        DataTable dt = objData.GetData("select * from fly_PropertyType");
        if (strProperty == "Apartment/Condo/Townhouse" || strProperty == "Apartment" || strProperty == "Coop")
        {
            strChangedToProperty = "Apartment/Co-op";
        }
        else if (strProperty == "Townhouse" || strProperty=="Condo")
        {
            strChangedToProperty = "Townhouse/Condo";
        }
        else if (strProperty == "Mobile/Manufactured")
        {
            strChangedToProperty = "Mobile/Manufactured Home";
        }
        else if (strProperty == "Multi-Family")
        {
            strChangedToProperty = "Multi-Family Home";
        }
        else if (strProperty == "Lot/Land")
        {
            strChangedToProperty = "Lot/Land";
        }
        else if (strProperty == "Single-Family Home")
        {
            strChangedToProperty = "Single-Family Home";
        }
        else
        {
            strChangedToProperty = "Commercial";
        }
        return strChangedToProperty;
    }

    private string GetPropertyTypeForGoogleBase(string strProperty)
    {
        string strChangedToProperty = "other";
        clsData objData = new clsData();
        DataTable dt = objData.GetData("select * from fly_PropertyType");
        if (strProperty == "Apartment/Condo/Townhouse" || strProperty == "Apartment" || strProperty == "Condo")
        {
            strChangedToProperty = "apartment";
        }
        else if (strProperty == "Townhouse")
        {
            strChangedToProperty = "townhouse";
        }
        else if (strProperty == "Condo")
        {
            strChangedToProperty = "condo";
        }
        else if (strProperty == "Coop")
        {
            strChangedToProperty = "coop";
        }
        else if (strProperty == "Mobile/Manufactured")
        {
            strChangedToProperty = "manufactured";
        }
        else if (strProperty == "Multi-Family")
        {
            strChangedToProperty = "multifamily";
        }
        else if (strProperty == "Lot/Land")
        {
            strChangedToProperty = "land";
        }
        else if (strProperty == "Single-Family Home")
        {
            strChangedToProperty = "singlefamily";
        }
        else
        {
            strChangedToProperty = "other";
        }
        return strChangedToProperty;
    }

    private string GetPropertyTypeCodeForOLX(string strProperty, string Price)
    {
      /*Houses - Apartments for Sale	    367
        Houses - Apartments for Rent	    363
        Rooms for Rent - Shared		        301
        Housing Swap			            567
        Vacation Rentals		            388
        Parking Spots			            302
        Land				                410
        Office - Commercial Space	        368
        Shops for Rent - Sale		        415*/

        string strChangedToProperty = "Other";
        clsData objData = new clsData();
        DataTable dt = objData.GetData("select * from fly_PropertyType");
        if (strProperty == "Apartment/Condo/Townhouse" || strProperty == "Apartment" || strProperty == "Condo")
        {
            if (Price.ToLower().Contains("rent"))
                strChangedToProperty = "363";
            else
                strChangedToProperty = "367";
        }
        else if (strProperty == "Townhouse")
        {
            if (Price.ToLower().Contains("rent"))
                strChangedToProperty = "363";
            else
                strChangedToProperty = "367";
        }
        else if (strProperty == "Coop")
        {
            if (Price.ToLower().Contains("rent"))
                strChangedToProperty = "363";
            else
                strChangedToProperty = "367";
        }
        else if (strProperty == "Mobile/Manufactured")
        {
            if (Price.ToLower().Contains("rent"))
                strChangedToProperty = "363";
            else
                strChangedToProperty = "367";
        }
        else if (strProperty == "Multi-Family")
        {
            if (Price.ToLower().Contains("rent"))
                strChangedToProperty = "363";
            else
                strChangedToProperty = "367";
        }
        else if (strProperty == "Lot/Land")
        {
            strChangedToProperty = "410";
        }
        else if (strProperty == "Single-Family Home")
        {
            if (Price.ToLower().Contains("rent"))
                strChangedToProperty = "363";
            else
                strChangedToProperty = "367";
        }
        else
        {
            if (Price.ToLower().Contains("rent"))
                strChangedToProperty = "363";
            else
                strChangedToProperty = "367";
        }
        return strChangedToProperty;
    }

    private string GetPropertyTypeForTrovit(string strProperty)
    {
        string strChangedToProperty = "Other";
        clsData objData = new clsData();
        DataTable dt = objData.GetData("select * from fly_PropertyType");
        if (strProperty == "Apartment/Condo/Townhouse" || strProperty == "Apartment" || strProperty == "Condo")
        {
            strChangedToProperty = "apartment";
        }
        else if (strProperty == "Townhouse")
        {
            strChangedToProperty = "home";
        }
        else if (strProperty == "Coop")
        {
            strChangedToProperty = "condo";
        }
        else if (strProperty == "Mobile/Manufactured")
        {
            strChangedToProperty = "home";
        }
        else if (strProperty == "Multi-Family")
        {
            strChangedToProperty = "home";
        }
        else if (strProperty == "Lot/Land")
        {
            strChangedToProperty = "land";
        }
        else if (strProperty == "Single-Family Home")
        {
            strChangedToProperty = "home";
        }
        else
        {
            strChangedToProperty = "home";
        }
        return strChangedToProperty;
    }

    private string GetPropertyTypeForHotPads(string strProperty)
    {
        string strChangedToProperty = "Other";
        clsData objData = new clsData();
        DataTable dt = objData.GetData("select * from fly_PropertyType");
        if (strProperty == "Apartment/Condo/Townhouse" || strProperty == "Apartment" || strProperty == "Condo")
        {
            strChangedToProperty = "CONDO";
        }
        else if (strProperty == "Townhouse")
        {
            strChangedToProperty = "TOWNHOUSE";
        }
        else if (strProperty == "Coop")
        {
            strChangedToProperty = "CONDO";
        }
        else if (strProperty == "Mobile/Manufactured")
        {
            strChangedToProperty = "HOUSE";
        }
        else if (strProperty == "Multi-Family")
        {
            strChangedToProperty = "HOUSE";
        }
        else if (strProperty == "Lot/Land")
        {
            strChangedToProperty = "LAND";
        }
        else if (strProperty == "Single-Family Home")
        {
            strChangedToProperty = "HOUSE";
        }
        else
        {
            strChangedToProperty = "HOUSE";
        }
        return strChangedToProperty;
    }

    private string GetPropertyTypeForFrontDoor(string strProperty)
    {
        /*
            1 	Single Family
            2 	Multi Family
            4 	Condo
            8 	Land
            16 	Townhouse
            32 	Other
         * */

        string strChangedToProperty = "Other";
        clsData objData = new clsData();
        DataTable dt = objData.GetData("select * from fly_PropertyType");
        if (strProperty == "Apartment/Condo/Townhouse" || strProperty == "Apartment" || strProperty == "Condo")
        {
            strChangedToProperty = "4";
        }
        else if (strProperty == "Townhouse")
        {
            strChangedToProperty = "16";
        }
        else if (strProperty == "Coop")
        {
            strChangedToProperty = "4";
        }
        else if (strProperty == "Mobile/Manufactured")
        {
            strChangedToProperty = "16";
        }
        else if (strProperty == "Multi-Family")
        {
            strChangedToProperty = "2";
        }
        else if (strProperty == "Lot/Land")
        {
            strChangedToProperty = "8";
        }
        else if (strProperty == "Single-Family Home")
        {
            strChangedToProperty = "1";
        }
        else
        {
            strChangedToProperty = "32";
        }
        return strChangedToProperty;
    }

    private bool IsInteger(string theValue)
    {
        try
        {
            Convert.ToInt32(theValue);
            return true;
        }
        catch
        {
            return false;
        }
    }

}

public class StringWriterWithEncoding : StringWriter
{
    private Encoding m_encoding;

    public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
        : base(sb)
    {
        m_encoding = encoding;
    }

    public override Encoding Encoding
    {
        get
        {
            return m_encoding;
        }
    }


}
