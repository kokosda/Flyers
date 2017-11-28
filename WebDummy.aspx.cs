using System;
using System.Collections;
using System.Configuration;

namespace FlyerMe
{
    public partial class WebDummy : PageBase
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.CacheControl = "no-cache";
            Response.ContentType = "image/png";
            Response.WriteFile(Server.MapPath("~/images/webbeacon.png"));
            Response.End();
        }
    }
}
