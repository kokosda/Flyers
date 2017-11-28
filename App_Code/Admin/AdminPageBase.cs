using System;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public abstract class AdminPageBase : PageBase
    {
        public String AdminSiteRootUrl
        {
            get
            {
                return SiteRootUrl + "admin/";
            }
        }

        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/admin/default";
            }
        }

        public virtual SqlDataSource SqlDataSource
        {
            get
            {
                return null;
            }
        }

        public virtual String GetJsWindowPopupOnClickAttribute(String url, String windowTitle)
        {
            var result = String.Format("javascript:window.open('{0}', '{1}', 'status = 1, height = 470, width = 600, resizable = 0'); return false;", url, windowTitle.HasText() ? windowTitle : "popupWindow");

            return result;
        }
    }
}