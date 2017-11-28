using FlyerMe.Controls;
using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class Sitemap : PageBase
    {
        protected String RootUrl { get; set; }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Sitemap | Real Estate Email Flyers | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs args)
        {
            RootUrl = clsUtility.GetRootHost;
        }
    }
}