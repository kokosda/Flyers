using FlyerMe.Controls;
using System;

namespace FlyerMe
{
    public partial class Faq : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/faq";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("FAQ | Learn About {0} | Real Estate Marketing Made Simple | {0}", clsUtility.ProjectName)
                                 .SetTitle("Your Real Estate Marketing Experts | Learn more about {0}", clsUtility.ProjectName)
                                 .SetKeywords("{0}, property listings, about real estate marketing, get started in real estate marketing, sign up for email flyers, real estate email  marketing provider, real estate flyer CRM, real estate email flyer support, real estate advertising, realtor marketing support, real estate questions, real estate faq", clsUtility.SiteBrandName.ToLower())
                                 .SetDescription("Find all the answers to your real estate marketing and real estate email flyers needs at {0}. Create and send gorgeous real estate email flyers to thousands agents with qualified buyer in seconds.  ", clsUtility.ProjectName);
            }
        }
    }
}