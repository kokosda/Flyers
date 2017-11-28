using FlyerMe.BLL.CreateFlyer;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace FlyerMe.Controls.CreateFlyer
{
    public partial class LeftMenu : UserControl
    {
        public LeftMenu()
        {
            Items = new List<Item>();
        }

        public FlyerTypes FlyerType
        {
            get
            {
                return flyerType.Value;
            }
        }

        protected List<Item> Items { get; set; }

        protected String RootURL { get; set; }

        protected String BaseAbsoluteUrl
        {
            get
            {
                return baseAbsoluteUrl;
            }
        }

        protected void Page_Load(Object sender, EventArgs args)
        {
            RootURL = clsUtility.GetRootHost;
            flyer = WizardFlyer.GetFlyer();

            SetFlyerType();
            SetItems();
        }

        public sealed class Item
        {
            public Item(String step, String text, Boolean isCompleted)
            {
                Step = step;
                Text = text;
                IsCompleted = isCompleted;
            }

            public Item(String step, String text, Boolean isCompleted, Boolean isRequired) 
                : this(step, text, isCompleted)
            {
                IsRequired = isRequired;
            }

            public String Step { get; private set; }

            public String Text { get; private set; }

            public Boolean IsActive { get; private set; }

            public Boolean IsCompleted { get; private set; }

            public Boolean IsRequired { get; private set; }

            public String CssClass
            {
                get
                {
                    String result = null;

                    if (IsActive)
                    {
                        result = "active";
                    }
                    else if (IsCompleted)
                    {
                        result = "completed";
                    }

                    return result;
                }
            }

            public void SetActive()
            {
                IsActive = true;
            }

            public void UnsetActive()
            {
                IsActive = false;
            }
        }

        #region private

        private FlyerTypes? flyerType;
        private String baseAbsoluteUrl;
        private WizardFlyer flyer;

        private void SetFlyerType()
        {
            if (!flyerType.HasValue)
            {
                var url = Request.Url.ToString();

                flyerType = FlyerTypes.Seller;
                baseAbsoluteUrl = "createflyer/sellersagent.aspx";

                if (url.IndexOf(FlyerTypes.Buyer.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    flyerType = FlyerTypes.Buyer;
                    baseAbsoluteUrl = "createflyer/buyersagent.aspx";
                }
                else if (url.IndexOf(FlyerTypes.Custom.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    flyerType = FlyerTypes.Custom;
                    baseAbsoluteUrl = "createflyer/custom.aspx/";
                }
            }
        }

        private void SetItems()
        {
            switch(FlyerType)
            {
                case FlyerTypes.Seller:
                        {
                            SetSellersAgentItems();
                        }
                        break;
                case FlyerTypes.Buyer:
                        {
                            SetBuyersAgentItems();
                        }
                        break;
            }
        }

        private void SetSellersAgentItems()
        {
            Items.Add(new Item("flyertitle", "Flyer title", flyer.FlyerTitleStepCompleted, true));
            Items.Add(new Item("contactdetails", "Contact details", flyer.ContactDetailsStepCompleted, true));
            Items.Add(new Item("propertytype", "Property type", flyer.PropertyTypeStepCompleted));
            Items.Add(new Item("location", "Location", flyer.LocationStepCompleted));
            Items.Add(new Item("description", "Description", flyer.DescriptionStepCompleted));
            Items.Add(new Item("amenities", "Amenities", flyer.AmenitiesStepCompleted));
            Items.Add(new Item("photo", "Photo", flyer.PhotoStepCompleted));
            Items.Add(new Item("price", "Price", flyer.PriceStepCompleted));
            Items.Add(new Item("chooseflyer", "Choose Flyer", flyer.ChooseFlyerStepCompleted));

            SetActiveItem();
        }

        private void SetBuyersAgentItems()
        {
            Items.Add(new Item("flyertitle", "Flyer title", flyer.FlyerTitleStepCompleted, true));
            Items.Add(new Item("contactdetails", "Contact details", flyer.ContactDetailsStepCompleted, true));
            Items.Add(new Item("desiredlocation", "Location", flyer.DesiredLocationStepCompleted));
            Items.Add(new Item("description", "Description", flyer.DescriptionStepCompleted));
            Items.Add(new Item("amenities", "Amenities", flyer.AmenitiesStepCompleted));
            Items.Add(new Item("pricerange", "Price", flyer.PriceRangeStepCompleted));
            Items.Add(new Item("chooseflyer", "Choose Flyer", flyer.ChooseFlyerStepCompleted));

            SetActiveItem();
        }

        private void SetActiveItem()
        {
            Items[0].SetActive();

            var url = Request.Url.ToString();

            foreach (var item in Items)
            {
                if (url.IndexOf(item.Step) >= 0)
                {
                    if (item == Items[0])
                    {
                        break;
                    }

                    item.SetActive();
                    Items[0].UnsetActive();

                    break;
                }
            }
        }

        #endregion
    }
}
