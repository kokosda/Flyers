using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class Error403 : Page
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            Response.StatusCode = 403;
        }
    }
}