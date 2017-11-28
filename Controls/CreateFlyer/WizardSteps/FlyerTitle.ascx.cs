using System;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class FlyerTitle : CreateFlyerWizardControlBase
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
                Flyer.FlyerTitle = Request["flyertitle"];
                Flyer.EmailSubject = Request["emailsubject"];
                Flyer.FlyerTitleStepCompleted = true;
            }
        }

        #region private

        private void SetInputs()
        {
            inputFlyerTitle.Value = Flyer.FlyerTitle;
            inputEmailSubject.Value = Flyer.EmailSubject;
            inputNext.Disabled = String.IsNullOrEmpty(inputFlyerTitle.Value) || String.IsNullOrEmpty(inputEmailSubject.Value);
        }

        #endregion
    }
}
