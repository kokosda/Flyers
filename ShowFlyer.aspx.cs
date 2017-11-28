using FlyerMe.BLL.CreateFlyer;
using FlyerMe.Controls;
using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class ShowFlyer : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                Order order = null;
                WizardFlyer flyer = null;
                String propertyType = null;
                String priceRent = "Sale/Rent";

                try
                {
                    order = Helper.GetOrder(Request, Response);
                    flyer = WizardFlyer.FromOrder(order);
                    propertyType = flyer.GetResidentialTypeName();
                    
                    if (flyer.OfferType.HasText())
                    {
                        if (flyer.OfferType.IndexOf("real estate", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            priceRent = "Sale";
                        }
                        else if (flyer.OfferType.IndexOf("rentals", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            priceRent = "Rent";
                        }
                    }
                }
                catch
                {
                }

                if (flyer == null)
                {
                    return MetaObject.Create()
                                     .SetPageTitle("Flyer | {0}", clsUtility.ProjectName)
                                     .SetKeywords("{0}, real estate marketing, real estate email flyers pricing, real estate email  marketing, real estate flyers, real estate email flyers, flyer real estate, real estate advertising, realtor marketing", clsUtility.SiteBrandName.ToLower())
                                     .SetDescription("View details and photos of flyer.");
                }
                else
                {
                    return MetaObject.Create()
                                     .SetPageTitle("{1} - {2} - Property for {3} | {0}", clsUtility.ProjectName, propertyType, flyer.GetFullAddress(), priceRent)
                                     .SetKeywords("{0}, real estate marketing, real estate email flyers pricing, real estate email  marketing, real estate flyers, real estate email flyers, flyer real estate, real estate advertising, realtor marketing", clsUtility.SiteBrandName.ToLower())
                                     .SetDescription("View details and photos of {0} property at {1}.", propertyType, flyer.GetFullAddress());
                }
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            var orderId = !String.IsNullOrEmpty(Request["orderid"]) ? Request["orderid"] : Request["oid"];

            if (!String.IsNullOrEmpty(orderId))
            {
                var order = Helper.GetOrder(orderId, Response);

                ltlFlyerMarkup.Text = order.markup;
            }
        }
    }
}