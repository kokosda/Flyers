using FlyerMe.Admin.Models;
using System;
using System.Web.UI;

namespace FlyerMe.Admin.Controls.MasterPagePopup
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

        protected void Page_Load(Object sender, EventArgs e)
        {
        }
    }
}
