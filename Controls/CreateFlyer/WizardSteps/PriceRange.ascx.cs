using System;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class PriceRange : CreateFlyerWizardControlBase
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
                Decimal price;

                if (!String.IsNullOrEmpty(Request["pricemin"]) && Decimal.TryParse(Request["pricemin"], out price))
                {
                    Flyer.PriceMin = price;
                }

                if (!String.IsNullOrEmpty(Request["pricemax"]) && Decimal.TryParse(Request["pricemax"], out price))
                {
                    Flyer.PriceMax = price;
                }

                Flyer.PriceRangeStepCompleted = true;
            }
        }

        #region private

        private void SetInputs()
        {
            min.Value = Flyer.PriceMin.HasValue ? Flyer.PriceMin.Value.ToString() : "100000";
            max.Value = Flyer.PriceMax.HasValue ? Flyer.PriceMax.Value.ToString() : "350000";
        }

        #endregion
    }
}
