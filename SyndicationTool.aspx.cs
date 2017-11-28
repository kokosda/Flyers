using FlyerMe.Controls;
using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class SyndicationTool : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Marketing Test | Take a Free Real Estate Marketing | {0}", clsUtility.ProjectName)
                                 .SetDescription("Take the free {0} real estate property marketing test to see if buyers can find your current listings.", clsUtility.ProjectName);
            }
        }
    }
}