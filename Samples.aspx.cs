using FlyerMe.Controls;
using System;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class Samples : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/samples";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                if (IsSellers)
                {
                    return MetaObject.Create()
                                     .SetPageTitle("Seller Flyer Samples | Beautiful Real Estate Email Flyers | {0}", clsUtility.ProjectName)
                                     .SetTitle("Sample Gallery of Effective Email Marketing Flyers | {0}", clsUtility.ProjectName)
                                     .SetKeywords("Real estate marketing design, real estate sample flyers, real estate email flyer samples, real estate email marketing gallery, real estate flyer samples, flyer real estate, real estate advertising, realtor marketing, email templates")
                                     .SetDescription("Samples of beautiful, professionally designed real estate email flyers. Create and send your own flyer in seconds with {0}.", clsUtility.ProjectName);
                }
                else
                {
                    return MetaObject.Create()
                                     .SetPageTitle("Buyer Flyer Samples | Beautiful Real Estate Email Flyers | {0}", clsUtility.ProjectName)
                                     .SetTitle("Sample Gallery of Effective Email Marketing Flyers | {0}", clsUtility.ProjectName)
                                     .SetKeywords("Real estate marketing design, real estate sample flyers, real estate email flyer samples, real estate email marketing gallery, real estate flyer samples, flyer real estate, real estate advertising, realtor marketing, email templates")
                                     .SetDescription("Samples of beautiful, professionally designed real estate email flyers. Create and send your own flyer in seconds with {0}.", clsUtility.ProjectName);
                }
            }
        }

        protected String RootURL;

        protected Boolean IsSellers
        {
            get
            {
                return Request.QueryString["buyers"].HasNoText();
            }
        }
        
        protected void Page_Load(Object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;

            if (IsSellers)
            {
                hlSellers.CssClass += " active";
                SetSellersFlyers();
            }
            else
            {
                hlBuyers.CssClass += " active";
                SetBuyersFlyers();
                divFlyerDiz.Attributes["class"] += " center";
            }

            hlSellers.NavigateUrl = "~/samples.aspx";
            hlBuyers.NavigateUrl = "~/samples.aspx?buyers=true";
        }

        private void SetSellersFlyers()
        {
            var samples = new String[6];

            for(var i = 0; i < samples.Length; i++)
            {
                samples[i] = String.Format("~/preview.aspx?l=ac{0}_sampleseller", i.ToString());
            }

            rptSamples.DataSource = samples;
            rptSamples.DataBind();
        }

        private void SetBuyersFlyers()
        {
            var samples = new String[2];

            for (var i = 0; i < samples.Length; i++)
            {
                samples[i] = String.Format("~/preview.aspx?l=ad{0}_samplebuyer", i.ToString());
            }

            rptSamples.DataSource = samples;
            rptSamples.DataBind();
        }

        protected void rptSamples_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            var hyperLink = e.Item.FindControl("hlPreview") as HyperLink;

            hyperLink.NavigateUrl = e.Item.DataItem as String;
            hyperLink.Attributes.Add("data-clientname", "Preview");
            hyperLink = e.Item.FindControl("hlPreview2") as HyperLink;
            hyperLink.NavigateUrl = e.Item.DataItem as String;
            hyperLink.Attributes.Add("data-clientname", "Preview");

            var image = e.Item.FindControl("imageTemplate") as Image;

            if (IsSellers)
            {
                image.ImageUrl = String.Format("~/images/smple/template_{0}.png", (e.Item.ItemIndex + 1).ToString());
            }
            else
            {
                image.ImageUrl = String.Format("~/images/smple/template_buyer_{0}.png", (e.Item.ItemIndex + 1).ToString());
            }
        }
    }
}