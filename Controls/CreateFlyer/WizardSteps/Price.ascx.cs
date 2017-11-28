using System;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class Price : CreateFlyerWizardControlBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            InitFields(Profile);
            SetInputs();
            form.Action = FormAction;
        }

        public override void SetSessionData(Boolean isInitial)
        {
            InitFields(Profile);

            if (!isInitial)
            {
                Flyer.PriceRent = Request["pricerent"];

                Decimal price;

                if (!String.IsNullOrEmpty(Request["price"]) && Decimal.TryParse(Request["price"], out price))
                {
                    Flyer.Price = price;
                }

                Flyer.PerArea = Request["perarea"];

                if (String.Compare(Flyer.PriceRent, "rent", true) == 0)
                {
                    Flyer.RentPeriod = Request["rentperiod"];
                }
                else
                {
                    Flyer.RentPeriod = null;
                }

                Flyer.PriceStepCompleted = true;
            }
        }

        protected Boolean IsPriceRentChecked(String name)
        {
            var result = false;

            if (!String.IsNullOrEmpty(name))
            {
                result = String.Compare(Flyer.PriceRent, name, true) == 0;
            }

            if (!result)
            {
                result = String.Compare("price", name, true) == 0;
            }

            return result;
        }

        #region private

        private void SetInputs()
        {
            inputPrice.Value = Flyer.Price.HasValue ? Flyer.Price.ToString() : null;
            SetDdlPerAreSelectedIndex();
            SetDdlRentPeriodSelectedIndex();
            ddlPerArea.Attributes.Add("data-clientname", "PerArea");
            ddlRentPeriod.Attributes.Add("data-clientname", "RentPeriod");

            if (!IsPriceRentChecked("rent"))
            {
                ddlRentPeriod.Attributes.Add("style", "display:none;");
            }
        }

        private void SetDdlPerAreSelectedIndex()
        {
            if (!String.IsNullOrEmpty(Flyer.PerArea))
            {
                var item = ddlPerArea.Items.FindByValue(Flyer.PerArea);

                if (item != null)
                {
                    ddlPerArea.SelectedIndex = ddlPerArea.Items.IndexOf(item);
                }
            }
        }

        private void SetDdlRentPeriodSelectedIndex()
        {
            if (!String.IsNullOrEmpty(Flyer.RentPeriod))
            {
                var item = ddlRentPeriod.Items.FindByValue(Flyer.RentPeriod);

                if (item != null)
                {
                    ddlRentPeriod.SelectedIndex = ddlRentPeriod.Items.IndexOf(item);
                }
            }
        }

        #endregion
    }
}
