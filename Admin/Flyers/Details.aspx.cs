using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Flyers
{
    public partial class Details : AdminPageBase
    {
        protected override string ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/admin/flyers/details";
            }
        }

        public override SqlDataSource SqlDataSource
        {
            get
            {
                return sds;
            }
        }

        protected void Page_Load(Object sender, EventArgs args)
        {
        }
        
        protected void rpt_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            if (Request["edit"].HasText())
            {
                e.Item.FindControl("edit").Visible = true;
            }
            else
            {
                e.Item.FindControl("details").Visible = true;
            }
        }
    }
}
