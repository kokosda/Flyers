using System;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class ContactDetails : CreateFlyerWizardControlBase
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
                Flyer.Name = Request["name"];
                Flyer.Phone = Request["phone"];
                Flyer.Ext = Request["ext"];
                Flyer.Email = Request["email"];
                Flyer.ContactDetailsStepCompleted = true;
            }
        }

        #region private

        private void SetInputs()
        {
            inputName.Value = String.IsNullOrEmpty(Flyer.Name) ? Helper.GetCustomerNameByEmail(ProfileCommon.UserName) : Flyer.Name;
            inputPhone.Value = String.IsNullOrEmpty(Flyer.Phone) ? ProfileCommon.Contact.PhoneBusiness : Flyer.Phone;
            inputExt.Value = String.IsNullOrEmpty(Flyer.Ext) ? ProfileCommon.Contact.PhoneBusinessExt : Flyer.Ext;
            inputEmail.Value = String.IsNullOrEmpty(Flyer.Email) ? ProfileCommon.UserName : Flyer.Email;

            inputEmail.Attributes["type"] = "email";

            inputNext.Disabled = inputName.Value.Trim().Length == 0 || String.IsNullOrEmpty(inputPhone.Value) || String.IsNullOrEmpty(inputEmail.Value);
        }

        #endregion
    }
}
