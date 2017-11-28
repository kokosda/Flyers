using System;

namespace FlyerMe.Controls.MasterPageAccount
{
    public partial class Popup : System.Web.UI.UserControl
    {
        protected string RootURL;
        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
        }
    }
}
