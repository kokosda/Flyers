using System;

namespace FlyerMe.Admin
{
    public partial class Reports : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            Response.Redirect("~/admin/reports/emaildelivery/emailwithsearch.aspx", true);
        }
    }
}
