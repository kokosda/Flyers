using System;
using System.Web.UI;

namespace FlyerMe.Controls
{
    public abstract class EmailMarkupPageBase : Page
    {
        protected String RootUrl
        {
            get
            {
                if (rootUrl == null)
                {
                    rootUrl = clsUtility.GetRootHost;
                }

                return rootUrl;
            }
        }

        protected String EmailImageUrl
        {
            get
            {
                return RootUrl + "email_flyer_email/";
            }
        }

        protected void Page_Load(Object sender, EventArgs args)
        {
            rootUrl = clsUtility.GetRootHost;
        }

        #region private

        private String rootUrl;

        #endregion
    }
}