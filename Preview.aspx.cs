using FlyerMe.BLL.CreateFlyer;
using FlyerMe.Controls;
using System;
using System.IO;
using System.Web.UI;

namespace FlyerMe
{
    public partial class Preview : PageBase
    {
        protected Boolean MarkupOnly
        {
            get
            {
                var result = false;

                if (!String.IsNullOrEmpty(Request["markuponly"]))
                {
                    var temp = Request.ParseCheckboxValue("markuponly");

                    if (temp.HasValue)
                    {
                        result = temp.Value;
                    }
                }

                return result;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            var layout = Request["l"];

            if (String.IsNullOrEmpty(layout))
            {
                if (!String.IsNullOrEmpty(Request["orderid"]))
                {
                    var order = Helper.GetOrder(Request, Response);

                    Response.Write(order.markup);
                }
                else
                {
                    var flyer = WizardFlyer.GetFlyer();

                    if (flyer == null)
                    {
                        layout = Helper.GetFlyerLayout(null, FlyerTypes.Seller);
                    }
                    else
                    {
                        layout = flyer.Layout != null ? flyer.Layout : Helper.GetFlyerLayout(null, flyer.FlyerType);
                    }
                }
            }

            if (!String.IsNullOrEmpty(layout))
            {
                var innerLayout = layout;

                if (String.Compare(innerLayout, "sampleseller", true) == 0)
                {
                    innerLayout = "ac0_sampleseller";
                }
                else if (String.Compare(innerLayout, "samplebuyer", true) == 0)
                {
                    innerLayout = "ad0_samplebuyer";
                }

                var control = LoadControl(String.Format("~/flyer/markup/{0}.ascx", innerLayout));

                if (MarkupOnly)
                {
                    Controls.Add(control);
                }
                else
                {
                    var previewControl = LoadControl("~/controls/preview.ascx");

                    previewControl.FindControl("body").Controls.Add(control);
                    Controls.Add(previewControl);
                }
            }
        }
    }
}