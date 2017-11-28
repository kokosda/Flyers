using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class SelectMarketArea : CreateFlyerWizardControlBase, IPricesView
    {
        public String RootUrl { get; set; }

        protected void Page_Load(Object sender, EventArgs args)
        {
            presenter = new PricesPresenter(this);
            presenter.OnPageLoad();
            InitFields(Profile);
            SetInputs();
            form.Action = FormAction;
            pager.PageName = "createflyer/" + ParentPageName + "?step=selectmarketarea";
        }

        public override void SetSessionData(Boolean isInitial)
        {
            InitFields(Profile);

            if (!isInitial)
            {
                Flyer.MarketState = State;
                SetDeliveryDate();
                Flyer.MarketType = Market;
                Flyer.SelectMarketAreaStepCompleted = true;
            }
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
                var result = Request["state"];

                if (String.IsNullOrEmpty(result))
                {
                    result = Flyer.MarketState;

                    if (String.IsNullOrEmpty(result))
                    {
                        result = ddlState.SelectedValue;
                    }
                }

                return result;
            }
        }

        public String Market
        {
            get
            {
                var result = Request["market"];

                if (String.IsNullOrEmpty(result))
                {
                    result = Flyer.MarketType;
                }

                return result;
            }
        }

        public String PricesJoin
        {
            get
            {
                var result = Request["prices"];

                if (String.IsNullOrEmpty(result) && Flyer.SelectedPricesList != null)
                {
                    result = Flyer.SelectedPricesList.Prices;
                }

                return result;
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

        public Boolean ShouldSaveSelectedPricesIntoFlyer
        {
            get
            {
                return true;
            }
        }

        public void SetFilter(IFilter filter)
        {
            pager.Filter = filter;
        }

        String IPricesView.Flyer
        {
            get
            {
                return Flyer.FlyerType.ToString().ToLower();
            }
        }

        public Boolean IsCheckoutDisabled()
        {
            return GetSelectedPricesList().Count == 0 || (!Flyer.DeliveryDate.HasValue);
        }

        #region private

        private PricesPresenter presenter;

        private void SetInputs()
        {
            inputDeliveryDate.Value = Flyer.DeliveryDate.HasValue ? Flyer.DeliveryDate.Value.FormatDate() : DateTime.Now.Date.FormatDate();
        }

        private void SetDeliveryDate()
        {
            DateTime deliveryDate;

            if (DateTime.TryParse(Request["deliverydate"], out deliveryDate))
            {
                Flyer.DeliveryDate = deliveryDate;
            }
            else
            {
                throw new Exception("Date format is incorrect.");
            }
        }

        #endregion
    }
}
