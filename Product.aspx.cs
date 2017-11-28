using FlyerMe.Controls;
using System;

namespace FlyerMe
{
    public partial class Product : PageBase
    {
        protected String RootURL;

        protected void Page_Load(Object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Product | Real Estate Email Flyers | {0}", clsUtility.ProjectName)
                                 .SetKeywords("{0}, property listings, about real estate marketing, get started in real estate marketing, sign up for email flyers, real estate email  marketing provider, real estate flyer CRM, real estate email flyer support, real estate advertising, realtor marketing support, real estate questions", clsUtility.SiteBrandName.ToLower())
                                 .SetDescription("{0} helps motivated real estate agents like you reclaim your time and results by connecting you to your target audience quickly. Available to you 24/7 and 365 days of the year, you can design an effective, beautiful flyer in minutes!", clsUtility.ProjectName);
            }
        }
    }
}