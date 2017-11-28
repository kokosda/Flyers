using System;

namespace FlyerMe.Controls.MasterPageAccount
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected string RootURL;

        protected String VisibleUsername
        {
            get
            {
                String result = null;

                if (Profile.FirstName != null)
                {
                    result = Profile.FirstName;

                    if (Profile.LastName != null)
                    {
                        result = String.Format("{0} {1}", result, Profile.LastName);
                    }
                }
                else
                {
                    result = Page.User.Identity.Name;
                }

                return result;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
        }
    }
}
