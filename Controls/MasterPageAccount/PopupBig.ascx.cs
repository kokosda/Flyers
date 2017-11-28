using System;

namespace FlyerMe.Controls.MasterPageAccount
{
    public partial class Popup : System.Web.UI.UserControl
    {
        public Boolean Big { get; set; }

        protected string RootURL;
        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
        }
    }
}
