using System;

namespace FlyerMe.Controls.MasterPage
{
    public partial class Footer : System.Web.UI.UserControl
    {
        protected string RootURL;
        protected void Page_Load(Object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
            hlPrivacyPolicy.NavigateUrl = String.Format("{0}{1}", RootURL, "privacypolicy.aspx");
            hlFaq.NavigateUrl = String.Format("{0}{1}", RootURL, "faq.aspx");
            hlContacts.NavigateUrl = String.Format("{0}{1}", RootURL, "contacts.aspx");
            hlTermsAndConditions.NavigateUrl = String.Format("{0}{1}", RootURL, "termsofuse.aspx");
            hlAntiSpamPolicy.NavigateUrl = String.Format("{0}{1}", RootURL, "antispampolicy.aspx");
            hlSitemap.NavigateUrl = String.Format("{0}{1}", RootURL, "sitemap.aspx");

            SetMenu2Active();
        }

        #region private

        private void SetMenu2Active()
        {
            var pageName = Page.ToString();

            if (pageName.IndexOf("privacypolicy", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlPrivacyPolicy.CssClass += " active";
            }
            else if (pageName.IndexOf("faq", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlFaq.CssClass += " active";
            }
            else if (pageName.IndexOf("contacts", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlContacts.CssClass += " active";
            }
            else if (pageName.IndexOf("termsofuse", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlTermsAndConditions.CssClass += " active";
            }
            else if (pageName.IndexOf("antispampolicy", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlAntiSpamPolicy.CssClass += " active";
            }
            else if (pageName.IndexOf("sitemap", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlSitemap.CssClass += " active";
            }
        }

        #endregion
    }
}
