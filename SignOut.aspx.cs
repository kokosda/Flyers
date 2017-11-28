using System;
using System.Web.Security;
using System.Web.UI;

namespace FlyerMe
{
    public partial class SignOut : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            Response.Redirect("~/");
        }
    }
}
