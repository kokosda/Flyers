using System;

namespace FlyerMe.Controls.MasterPage
{
    public partial class RecoverPassword : System.Web.UI.UserControl
    {
        protected string RootURL;
        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
        }
    }
}
