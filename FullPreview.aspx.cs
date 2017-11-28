using System;

namespace FlyerMe
{
    public partial class FullPreview : PageBase
    {
        protected string RootURL;
        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
        }
    }
}