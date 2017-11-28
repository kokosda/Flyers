using FlyerMe.Admin.Controls;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;

namespace FlyerMe.Admin.Others
{
    public partial class GenerateSitemaps : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            if (Request.IsGet())
            {
                GenerateFeedByRequest();
            }
        }

        protected void btnGoogleSitemap_Command(Object sender, CommandEventArgs e)
        {
            try
            {
                var dsOrder = new DataSet();

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    var daOrder = new SqlDataAdapter("select order_id from fly_order where status <> 'Incomplete'", conn);

                    daOrder.Fill(dsOrder);
                }

                using (var xtwFeed = new XmlTextWriter(Server.MapPath("~/xmlfeeds/sitemap.xml"), Encoding.UTF8))
                {
                    xtwFeed.WriteStartDocument();
                    xtwFeed.WriteStartElement("urlset");
                    xtwFeed.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                    xtwFeed.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    xtwFeed.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

                    xtwFeed.WriteStartElement("url");
                    xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl);
                    xtwFeed.WriteElementString("priority", "1.0");
                    xtwFeed.WriteElementString("changefreq", "daily");
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("url");
                    xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl + "/prices.aspx");
                    xtwFeed.WriteElementString("priority", "0.8");
                    xtwFeed.WriteElementString("changefreq", "daily");
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("url");
                    xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl + "/samples.aspx");
                    xtwFeed.WriteElementString("priority", "0.8");
                    xtwFeed.WriteElementString("changefreq", "daily");
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("url");
                    xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl + "/faq.aspx");
                    xtwFeed.WriteElementString("priority", "0.8");
                    xtwFeed.WriteElementString("changefreq", "daily");
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("url");
                    xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl.ToUri().UrlToHttps().ToString() + "signup.aspx");
                    xtwFeed.WriteElementString("priority", "0.8");
                    xtwFeed.WriteElementString("changefreq", "daily");
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("url");
                    xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl + "/syndicationtool.aspx");
                    xtwFeed.WriteElementString("priority", "0.8");
                    xtwFeed.WriteElementString("changefreq", "daily");
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("url");
                    xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl + "/sitemap.aspx");
                    xtwFeed.WriteElementString("priority", "0.8");
                    xtwFeed.WriteElementString("changefreq", "daily");
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("url");
                    xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl + "/contacts.aspx");
                    xtwFeed.WriteElementString("priority", "0.8");
                    xtwFeed.WriteElementString("changefreq", "daily");
                    xtwFeed.WriteEndElement();

                    xtwFeed.WriteStartElement("url");
                    xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl + "/whatsnew.aspx");
                    xtwFeed.WriteElementString("priority", "0.8");
                    xtwFeed.WriteElementString("changefreq", "daily");
                    xtwFeed.WriteEndElement();

                    foreach (DataRow dtOrder in dsOrder.Tables[0].Rows)
                    {
                        xtwFeed.WriteStartElement("url");
                        xtwFeed.WriteElementString("loc", clsUtility.SiteHttpWwwRootUrl + "/showflyer.aspx?orderid=" + dtOrder["order_id"].ToString());
                        xtwFeed.WriteElementString("priority", "0.8");
                        xtwFeed.WriteElementString("changefreq", "daily");
                        xtwFeed.WriteEndElement();
                    }

                    xtwFeed.WriteEndElement();
                    xtwFeed.WriteEndDocument();
                }

                message.MessageText = "Google Sitemap generated successfully!";
                message.MessageClass = MessageClassesEnum.Ok;
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            if (message.MessageClass == MessageClassesEnum.Ok)
            {
                message.RedirectToShowMessage();
            }
            else
            {
                message.ShowMessage();
            }
        }

        protected void btnYahooSitemap_Command(Object sender, CommandEventArgs e)
        {
            try
            {
                var dsOrder = new DataSet();
                
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString))
                {
                    if (conn.State != ConnectionState.Open) 
                    { 
                        conn.Open(); 
                    }

                    var daOrder = new SqlDataAdapter("select order_id from fly_order where status <> 'Incomplete'", conn);

                    daOrder.Fill(dsOrder);
                    conn.Close();
                }

                using (var srSitemap = new StreamWriter(Server.MapPath("~/xmlfeeds/urllist.txt"), false, Encoding.UTF8))
                {
                    srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl);
                    srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl + "/prices.aspx");
                    srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl + "/samples.aspx");
                    srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl + "/faq.aspx");
                    srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl.ToUri().UrlToHttps().ToString() + "signup.aspx");
                    srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl + "/syndicationtool.aspx");
                    srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl + "/sitemap.aspx");
                    srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl + "/contacts.aspx");
                    srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl + "/whatsnew.aspx");

                    foreach (DataRow dtOrder in dsOrder.Tables[0].Rows)
                    {
                        srSitemap.WriteLine(clsUtility.SiteHttpWwwRootUrl + "/showflyer.aspx?orderid=" + dtOrder["order_id"].ToString());
                    }

                    message.MessageText = "Yahoo Sitemap generated successfully!";
                    message.MessageClass = MessageClassesEnum.Ok;
                }
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            if (message.MessageClass == MessageClassesEnum.Ok)
            {
                message.RedirectToShowMessage();
            }
            else
            {
                message.ShowMessage();
            }
        }

        #region private

        private void GenerateFeedByRequest()
        {
            var feed = Request.QueryString["feed"];

            if (feed.HasText())
            {
                var generated = false;

                if (String.Compare(feed, "google", true) == 0)
                {
                    btnGoogleSitemap_Command(null, null);
                    generated = true;
                }
                else if (String.Compare(feed, "yahoo", true) == 0)
                {
                    btnYahooSitemap_Command(null, null);
                    generated = true;
                }

                if (generated)
                {
                    Response.ClearContent();
                    Response.SuppressContent = true;
                    Response.End();
                }
            }
        }

        #endregion
    }
}
