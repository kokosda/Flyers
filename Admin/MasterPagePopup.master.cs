using FlyerMe.Admin.Models;
using System;
using System.Web.UI;

namespace FlyerMe.Admin
{
    public partial class MasterPagePopup : System.Web.UI.MasterPage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            InitializeScripts();

            if (AdminUserModel.GetAdminUserModelFromSession() == null)
            {
                String returnUrl = null;

                if (Request.Url.AbsolutePath.IndexOf("/admin/login.aspx") < 0)
                {
                    returnUrl = Server.UrlEncode(Server.UrlEncode(Request.Url.ToString()));
                }

                var url = "~/admin/login.aspx";

                if (returnUrl.HasText())
                {
                    url += "?returnurl=" + returnUrl;
                }

                Response.Redirect(url, true);
            }
        }

        #region private

        private void InitializeScripts()
        {
            if (cphScriptsControl.Controls.Count > 0)
            {
                var scripts = cphScriptsControl.Controls[0];

                scripts.GetType().GetProperty("RenderAnalytics").SetValue(scripts, false);
                scripts.GetType().GetProperty("RenderChat").SetValue(scripts, false);
            }
        }

        #endregion
    }
}