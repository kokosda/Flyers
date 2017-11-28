using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class MasterPageAccount : System.Web.UI.MasterPage
    {
        protected String RootURL;

        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
        }
    }
}