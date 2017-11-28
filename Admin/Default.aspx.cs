using FlyerMe.Admin.Models;
using System;

namespace FlyerMe.Admin
{
    public partial class Default : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            if (AdminUserModel.GetAdminUserModelFromSession() == null)
            {
                Response.Redirect("~/admin/login.aspx", true);
            }
        }
    }
}
