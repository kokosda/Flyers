using FlyerMe.Admin.Models;
using System;

namespace FlyerMe.Admin
{
    public partial class LogOut : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            AdminUserModel.RemoveAdminUserModelFromSession();
            Response.Redirect("~/admin/default.aspx");
        }
    }
}