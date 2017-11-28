using FlyerMe.Controls;
using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class WhatsNew : PageBase
    {
        protected String RootUrl { get; set; }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("What's New | Stay up to Date with Real Estate Marketing News | {0}", clsUtility.ProjectName)
                                 .SetKeywords("{0}, real estate marketing, real estate email flyers pricing, real estate email  marketing, real estate flyers, real estate email flyers, flyer real estate, real estate advertising, realtor marketing", clsUtility.SiteBrandName.ToLower())
                                 .SetDescription("{0} offers real estate agents the latest marketing, social media tips, news, updates on the latest {0} features and the easiest way to design and send real estate email flyers.", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs args)
        {
            RootUrl = clsUtility.GetRootHost;
        }
    }
}