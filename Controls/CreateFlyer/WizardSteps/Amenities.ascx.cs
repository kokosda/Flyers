using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class Amenities : CreateFlyerWizardControlBase
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
                var propertiesDict = Flyer.GetType().GetProperties().Where(p => p.PropertyType == typeof(Boolean)).ToDictionary(p => p.Name, p => p);
                PropertyInfo pi;
                Boolean? @bool;

                foreach (String key in Request.Form)
                {
                    if (propertiesDict.ContainsKey(key))
                    {
                        pi = propertiesDict[key];
                        @bool = Request.ParseCheckboxValue(key);

                        if (@bool.HasValue)
                        {
                            pi.SetValue(Flyer, @bool.Value, null);
                        }
                    }
                }

                Flyer.AmenitiesStepCompleted = true;
            }
        }

        #region private

        private void SetInputs()
        {
            var propertiesDict = Flyer.GetType().GetProperties().Where(p => p.PropertyType == typeof(Boolean)).ToDictionary(p => p.Name, p => p);
            HtmlInputCheckBox checkbox;
            PropertyInfo pi;

            foreach(Control control in form.Controls)
            {
                checkbox = control as HtmlInputCheckBox;

                if (checkbox != null && propertiesDict.ContainsKey(checkbox.Attributes["data-clientname"]))
                {
                    pi = propertiesDict[checkbox.Attributes["data-clientname"]];
                    checkbox.Checked = (Boolean)pi.GetValue(Flyer, null);
                }
            }
        }

        #endregion
    }
}
