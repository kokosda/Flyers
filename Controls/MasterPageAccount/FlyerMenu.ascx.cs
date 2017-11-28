using System;
using System.Web.UI;

namespace FlyerMe.Controls.MasterPageAccount
{
    public partial class FlyerMenu : UserControl
    {
        protected string RootURL;

        public String FlyerTypeDisplayString { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;

            ltlFlyerTypeFriendlyName.Text = !String.IsNullOrEmpty(FlyerTypeDisplayString) ? FlyerTypeDisplayString : "Unknown";
        }
    }
}
