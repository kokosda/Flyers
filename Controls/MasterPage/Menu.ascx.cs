using System;

namespace FlyerMe.Controls.MasterPage
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected string RootURL;

        public Boolean CreateFlyerIsHidden { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;

            SetNavigateUrl();
            SetCssClass();

            if (CreateFlyerIsHidden)
            {
                hlCreateFlyer.Parent.Visible = false;
            }
        }

        private void SetNavigateUrl()
        {
            hlCreateFlyer.NavigateUrl = String.Format("{0}{1}", RootURL, "createflyer.aspx");
            hlProduct.NavigateUrl = String.Format("{0}{1}", RootURL, "product.aspx");
            hlSamples.NavigateUrl = String.Format("{0}{1}", RootURL, "samples.aspx");
            hlPrices.NavigateUrl = String.Format("{0}{1}", RootURL, "prices.aspx");
            hlSearch.NavigateUrl = String.Format("{0}{1}", RootURL, "search.aspx");

            SetNavigateUrlExtended();
        }

        private void SetNavigateUrlExtended()
        {
            var currentMarketState = Session["CurrentMarketState"] as String;

            if (!String.IsNullOrEmpty(currentMarketState))
            {
                hlPrices.NavigateUrl += "?state=" + currentMarketState;
            }
        }

        private void SetCssClass()
        {
            var pageName = Page.ToString();

            if (pageName.IndexOf("createflyer", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlCreateFlyer.CssClass += " active";
            }
            else if (pageName.IndexOf("product", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlProduct.CssClass += " active";
            }
            else if (pageName.IndexOf("samples", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlSamples.CssClass += " active";
            }
            else if (pageName.IndexOf("prices", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlPrices.CssClass += " active";
            }
            else if (pageName.IndexOf("search", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlSearch.CssClass += " active";
            }
        }
    }
}
