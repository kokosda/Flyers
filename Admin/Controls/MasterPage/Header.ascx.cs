using FlyerMe.Admin.Models;
using System;
using System.Web.UI;

namespace FlyerMe.Admin.Controls.MasterPage
{
    public partial class Header : UserControl
    {
        protected String FirstName
        {
            get
            {
                return AdminUserModel.GetAdminUserModelFromSession().FirstName;
            }
        }

        public Boolean IsActive
        {
            get
            {
                return isActive;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            SetLinks();
        }

        private Boolean isActive;

        private void SetLinks()
        {
            var url = Request.Url.AbsolutePath;

            if (url.IndexOf("admin/profile.aspx", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlProfile.CssClass += " active";
                isActive = true;
            }
            else if (url.IndexOf("admin/reports/", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                hlReports.CssClass += " active";
                isActive = true;
            }
        }
    }
}
