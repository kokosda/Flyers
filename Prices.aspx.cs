using FlyerMe.Controls;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class Prices : PageBase, IPricesView
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/prices";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Most Competitive Prices in the Real Estate Industry | {0}", clsUtility.ProjectName)
                                 .SetTitle("Pricing - affordable custom marketing flyers to reach your market area | {0}", clsUtility.ProjectName)
                                 .SetKeywords("Affordable flyers, affordable real estate marketing, real estate email flyers pricing, real estate email marketing pricing, real estate flyer cost, real estate email flyer cost, flyer real estate pricing, real estate advertising, realtor marketing costs")
                                 .SetDescription("{0} offers the most competitive pricing in the industry. Find all the prices of real estate email flyers by selecting your marketing area.", clsUtility.ProjectName);
            }
        }

        public String RootUrl { get; set; }

        protected void Page_Load(Object sender, EventArgs e)
        {
            presenter = new PricesPresenter(this);

            presenter.OnPageLoad();
        }

        public Boolean IsMarketChecked(String value)
        {
            return presenter.IsMarketChecked(value);
        }

        public Boolean IsFlyerChecked(String value)
        {
            return presenter.IsFlyerChecked(value);
        }

        public String GetFilterPrices()
        {
            return presenter.GetFilterPrices();
        }

        public Boolean IsPriceChecked(Int32 index)
        {
            return presenter.IsPriceChecked(index);
        }

        public Dictionary<Int32, FlyerMeDS.fly_sp_GetListSizeRow> GetSelectedPricesList()
        {
            return presenter.GetSelectedPricesList();
        }

        public Int32 GetTotalListSize()
        {
            return presenter.GetTotalListSize();
        }

        public Decimal GetTotalAmount()
        {
            return presenter.GetTotalAmount();
        }

        public String State
        {
            get 
            {
                return !String.IsNullOrEmpty(Request["state"]) ? Request["state"] : ddlState.SelectedValue;
            }
        }

        public String Market
        {
            get 
            {
                return Request["market"];
            }
        }

        public String Flyer
        {
            get 
            {
                return Request["flyer"];
            }
        }

        public String PricesJoin
        {
            get
            {
                return Request["prices"];
            }
        }

        public Int32 PageSize
        {
            get
            {
                return pager.PageSize;
            }
            set
            {
                pager.PageSize = value;
            }
        }

        public DropDownList DdlState
        {
            get
            {
                return ddlState;
            }
        }

        public Boolean ShouldSaveSelectedPricesIntoFlyer
        {
            get
            {
                return false;
            }
        }

        public void AddControl(Control control)
        {
            Controls.Add(control);
        }

        public void SetItemsCount(Int32 value)
        {
            pager.ItemsCount = value;
        }

        public IList<FlyerMeDS.fly_sp_GetListSizeRow> PricesList { get; set; }

        public Int32 PageNumber
        {
            get 
            {
                return pager.PageNumber;
            }
        }

        public void SetFilter(IFilter filter)
        {
            pager.Filter = filter;
        }

        #region private

        private PricesPresenter presenter;

        #endregion
    }
}
