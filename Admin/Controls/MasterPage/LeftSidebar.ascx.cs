using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace FlyerMe.Admin.Controls.MasterPage
{
    public partial class LeftSidebar : UserControl
    {
        static LeftSidebar()
        {
            items = new Dictionary<String, Item>
                            {
                                { 
                                    "users", new Item("users", "Users", 
                                                        new Dictionary<String, Item>
                                                                {
                                                                    { "websiteusers", new Item("websiteusers", "Website users") },
                                                                    { "userreferrals", new Item("userreferrals", "User referrals") }
                                                                })
                                },
                                { "flyers", new Item("flyers", "Flyers") },
                                { "agents", new Item("agents", "Agents") },
                                { "discounts", new Item("discounts", "Discounts",
                                                        new Dictionary<String, Item>
                                                                {
                                                                    { "promo", new Item("promo", "Promo") },
                                                                    { "offer", new Item("offer", "Offer") },
                                                                    { "offerdetails", new Item("offerdetails", "Offer Details") }
                                                                }) },
                                { "others", new Item("others", "Others",
                                                        new Dictionary<String, Item>
                                                                {
                                                                    { "generatefeedsforsyndication", new Item("generatefeedsforsyndication", "Generate Feeds For Syndication") },
                                                                    { "generatesitemaps", new Item("generatesitemaps", "Generate Sitemaps") },
                                                                    { "customertestimonials", new Item("customertestimonials", "Customer Testimonials") },
                                                                    { "blockedemails", new Item("blockedemails", "Blocked Emails") },
                                                                    { "transferflyers", new Item("transferflyers", "Transfer Flyers") }
                                                                }) },
                                { "reports", new Item("reports", "Reports") },
                                { "alterusername", new Item("alterusername", "Alter Username") },
                            };

            reportsItems = new Dictionary<String, Item>
                                {
                                    { "emaildelivery", new Item("reports/emaildelivery", "Email Delivery",
                                                                new Dictionary<String, Item>
                                                                {
                                                                    { "emailwithsearch", new Item("emailwithsearch", "Email with Search") },
                                                                    { "detailemail", new Item("detailemail", "Detail Email") },
                                                                    { "applicationsstatus", new Item("applicationsstatus", "Applications Status") },
                                                                    { "listofflyers", new Item("listofflyers", "List of Flyers") },
                                                                    { "queuedflyers", new Item("queuedflyers", "Queued Flyers") }
                                                                })
                                    },
                                    { "senddelivery", new Item("reports/senddelivery", "Send Delivery") },
                                    { "archiveddelivery", new Item("reports/archiveddelivery", "Archived Delivery") },
                                    { "customersbyesp", new Item("reports/customersbyesp", "Customers by ESP") },
                                    { "unsubscribption", new Item("reports/unsubscribption", "Unsubscription") },
                                    { "marketarea", new Item("reports/marketarea", "Market Area") },
                                    { "customerreport", new Item("reports/customerreport", "Customer Report") },
                                    { "flyersemailcount", new Item("reports/flyersemailcount", "Flyers Email Count") },
                                    { "spamabuse", new Item("reports/spamabuse", "Spam Abuse") }
                                };
        }

        protected Dictionary<String, Item> Items
        {
            get
            {
                return selectedItems;
            }
        }

        protected Item ActiveItemLevel1 { get; set; }

        protected Item ActiveItemLevel2 { get; set; }

        protected void Page_Load(Object sender, EventArgs args)
        {
            SetSelectedItems();
            SetActiveItems();
            SetMainMenuElements();
        }

        public sealed class Item
        {
            public Item(String value, String text)
            {
                Value = value;
                Text = text;
            }

            public Item(String value, String text, Dictionary<String, Item> items) : this(value, text)
            {
                Items = items;
            }

            public String Value { get; private set; }

            public String Text { get; private set; }

            public Dictionary<String, Item> Items { get; private set; }
        }

        #region private

        private Dictionary<String, Item> selectedItems;

        private void SetActiveItems()
        {
            if (ShouldSetDefaultActiveItem())
            {
                ActiveItemLevel1 = Items.ElementAt(0).Value;

                if (ActiveItemLevel1.Items != null)
                {
                    ActiveItemLevel2 = ActiveItemLevel1.Items.First().Value;
                }
            }

            var segments = Request.Url.AbsolutePath.Replace(".aspx", String.Empty).Split('/');
            String segment;

            for (var i = 0; i < segments.Length; i++)
            {
                segment = segments[i].ToLower();

                if (Items.ContainsKey(segment))
                {
                    ActiveItemLevel1 = Items[segment];

                    if (i < segments.Length - 1 && ActiveItemLevel1.Items != null)
                    {
                        for (++i; i < segments.Length; i++)
                        {
                            segment = segments[i].ToLower();

                            if (ActiveItemLevel1.Items.ContainsKey(segment))
                            {
                                ActiveItemLevel2 = ActiveItemLevel1.Items[segment];
                                break;
                            }
                        }
                    }

                    break;
                }
            }
        }

        private Boolean ShouldSetDefaultActiveItem()
        {
            var header = Page.Master.FindControl("header") as Header;
            var result = !(header.IsActive || Request.Url.AbsolutePath.EndsWith("/default.aspx"));

            return result;
        }

        private void SetSelectedItems()
        {
            if (Request.Url.AbsolutePath.StartsWith(ResolveUrl("~/admin/reports/"), StringComparison.OrdinalIgnoreCase))
            {
                selectedItems = reportsItems;
            }
            else
            {
                selectedItems = items;
            }
        }

        private Boolean SetMainMenuElements()
        {
            var result = false;

            if (selectedItems == reportsItems)
            {
                spanMainMenu.Visible = true;
            }

            return result;  
        }

        private static readonly Dictionary<String, Item> items;

        private static readonly Dictionary<String, Item> reportsItems;

        #endregion
    }
}
