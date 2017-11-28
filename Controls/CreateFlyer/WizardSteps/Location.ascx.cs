using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class Location : CreateFlyerWizardControlBase
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
                Flyer.StreetAddress = Request["streetaddress"];
                Flyer.AptSuiteBldg = Request["aptsuitebldg"];
                Flyer.City = Request["city"];
                Flyer.State = Request["state"];
                Flyer.ZipCode = Request["zipcode"];
                Flyer.AddMap = Request.ParseCheckboxValue("addmap");
                SetMapLink();
                Flyer.LocationStepCompleted = true;
            }
        }

        #region private

        private void SetInputs()
        {
            inputStreetAddress.Value = Flyer.StreetAddress;
            inputAptSuiteBldg.Value = Flyer.AptSuiteBldg;
            inputCity.Value = Flyer.City;
            inputZipCode.Value = Flyer.ZipCode;

            if (ddlState.DataSource == null)
            {
                SetDdlStateDataSource();
            }

            SetDdlStateSelectedIndex();
            ddlState.Attributes.Add("data-clientname", "State");
            inputMapLink.Value = Flyer.MapLink;
            inputAddMap.Checked = !Flyer.AddMap.HasValue || Flyer.AddMap.Value;
        }

        private void SetDdlStateDataSource()
        {
            var ods = new ObjectDataSource("FlyerMe.StatesBLL", "GetStates")
                            {
                                OldValuesParameterFormatString = "original_{0}"
                            };

            ddlState.DataSource = ods.Select();
            ddlState.DataBind();
        }

        private void SetDdlStateSelectedIndex()
        {
            if (!String.IsNullOrEmpty(Flyer.State))
            {
                var item = ddlState.Items.FindByValue(Flyer.State);

                if (item != null)
                {
                    ddlState.SelectedIndex = ddlState.Items.IndexOf(item);
                }
            }
        }

        private void SetMapLink()
        {
            var addMap = Request.ParseCheckboxValue("addmap");

            if (addMap.HasValue && addMap.Value)
            {
                Flyer.MapLink = Request["maplink"];
            }
            else
            {
                Flyer.MapLink = null;
            }
        }

        #endregion
    }
}
