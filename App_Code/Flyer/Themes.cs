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

namespace FlyerMe
{
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// Flyer Theme class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [Serializable]
    public class Themes
    {
        //string siteRoot = ConfigurationManager.AppSettings["SiteRoot"];
        string siteRoot = clsUtility.GetRootHost;

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for flyer theme header
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string GetThemeHeader(Order order)
        {
            string headline = order.headline;
            string theme = order.theme;
            //string siteRoot = ConfigurationManager.AppSettings["SiteRoot"];
            string siteRoot = clsUtility.GetRootHost;
            Helper helper = new Helper();

            //read theme header template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/ThemeHeader.txt"), FileMode.Open));
            //populate stringbuilder with content from theme header file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (sb.Length > 0)
            {
                //replace placeholders for theme images 
                for (int i = 1; i < 6; i++)
                {
                    sb.Replace("~@THEME-HEADER-BG-IMAGE" + i + "@~", siteRoot + "Flyer/Themes/" + headline + "/images/" + headline + theme + "_0" + i + ".jpg");
                }

                //render placeholder for main property photo
                sb.Replace("~@PROPERTY-PHOTO-1@~", helper.GetPropImageNamePath(order.order_id.ToString(), 1));

                //Render custom headline
                if (order.field1.Trim().Length > 0)
                {
                    sb.Replace("~@CUSTOMHEADLINE@~", order.field1.Trim());
                }
                else
                {
                    sb.Replace("~@CUSTOMHEADLINE@~", string.Empty);
                }
            }

            //render theme header content to the browser
            return sb.ToString();
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Construct markup for flyer theme footer
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string GetThemeFooter(Order order)
        {
            string headline = order.headline;
            string theme = order.theme;

            Helper helper = new Helper();

            //read theme footer template file
            StreamReader sr = new StreamReader(new FileStream(HttpContext.Current.Server.MapPath("Flyer/Markup/Themefooter.txt"), FileMode.Open));
            //populate stringbuilder with content from theme footer file
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();

            //check if there is any content to process
            if (sb.Length > 0)
            {
                //replace placeholders for theme images 
                sb.Replace("~@THEME-FOOTER-BG-IMAGE@~", siteRoot + "Flyer/Themes/" + headline + "/images/" + headline + theme + "_bottom.jpg");
            }

            //render theme footer content to the browser
            return sb.ToString();
        }


    } //END: class
}
