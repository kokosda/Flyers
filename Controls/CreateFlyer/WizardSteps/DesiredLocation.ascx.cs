using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class DesiredLocation : CreateFlyerWizardControlBase
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
                Flyer.Location = Request["desiredlocation"];
                Flyer.DesiredLocationStepCompleted = true;
            }
        }

        #region private

        private void SetInputs()
        {
            textareaDesiredLocation.Value = Flyer.Location;
        }

        #endregion
    }
}
