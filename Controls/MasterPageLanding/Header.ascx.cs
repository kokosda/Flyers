using System;

namespace FlyerMe.Controls.MasterPageLanding
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected string RootURL;

        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
        }
    }
}
