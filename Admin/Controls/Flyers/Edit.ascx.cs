using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Flyers
{
    public partial class Edit : UserControl
    {
        public void Page_Load(Object sender, EventArgs e)
        {
        }
        protected void save_Command(Object sender, CommandEventArgs e)
        {
            var sds = (Page as AdminPageBase).SqlDataSource;

            foreach (Parameter p in sds.UpdateParameters)
            {
                if (Request[p.Name].HasText())
                {
                    p.DefaultValue = Request[p.Name];
                }
                else if (p.Type == TypeCode.String)
                {
                    p.DefaultValue = String.Empty;
                    p.ConvertEmptyStringToNull = false;
                }
            }

            sds.UpdateParameters["order_id"].DefaultValue = e.CommandArgument as String;
            sds.UpdateParameters["type"].DefaultValue = ddlTypes.SelectedValue;
            sds.UpdateParameters["status"].DefaultValue = ddlStatus.SelectedValue;
            sds.UpdateParameters["fk_PropertyCategory"].DefaultValue = ddlPropertyCategory.SelectedValue;
            sds.UpdateParameters["fk_PropertyType"].DefaultValue = ddlPropertyType.SelectedValue;
            (Page as AdminPageBase).SqlDataSource.Update();
            Response.Redirect("~/admin/flyers/details.aspx?orderid=" + e.CommandArgument, true);
        }
    }
}