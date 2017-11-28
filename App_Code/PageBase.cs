using FlyerMe.Controls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public abstract class PageBase : Page
    {
        protected virtual String ScriptsBundleName
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return "~/bundles/scripts/defaultauthenticated";
                }

                return "~/bundles/scripts/default";
            }
        }

        protected String SiteRootUrl
        {
            get
            {
                return clsUtility.GetRootHost;
            }
        }

        protected virtual MetaObject MetaObject
        {
            get
            {
                return null;
            }
        }

        protected void Page_Init(Object sender, EventArgs args)
        {
            InsertScriptsBundle();
            SetMetaInformation();
        }

        #region private

        private void InsertScriptsBundle()
        {
            if (ScriptsBundleName.HasText() && Master != null)
            {
                var scriptsControl = LoadControl("~/controls/masterpage/scripts.ascx");

                scriptsControl.GetType().GetProperty("BundleName").SetValue(scriptsControl, ScriptsBundleName);
                (Master.FindControl("cphScriptsControl") as ContentPlaceHolder).Controls.Add(scriptsControl);
            }
        }

        private void SetMetaInformation()
        {
            var mo = MetaObject;

            if (mo != null)
            {
                var meta = GetMetaControl();

                if (meta != null)
                {
                    meta.Set(mo);
                }
            }
        }

        private MetaControl GetMetaControl()
        {
            var result = Master.FindControl("meta") as MetaControl;

            return result;
        }

        #endregion
    }
}