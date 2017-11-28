using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls.MasterPage
{
    public partial class LogIn : System.Web.UI.UserControl
    {
        protected string RootURL;
        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
        }
    }
}
