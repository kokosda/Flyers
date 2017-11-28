using System;
using System.Web.UI;

namespace FlyerMe.Controls.MasterPage
{
    public partial class Social : UserControl
    {
        protected String RootUrl
        {
            get
            {
                return clsUtility.GetRootHost;
            }
        }

        protected String LinkedInAuthenticationState
        {
            get
            {
                return SocialHelper.GetLinkedInAuthenticationState();
            }
        }

        protected void Page_Load(Object sender, EventArgs args)
        {
            if (SocialHelper.GetLinkedInAuthenticationState().HasNoText())
            {
                SocialHelper.SetLinkedInAuthenticationState();
            }
        }
    }
}
