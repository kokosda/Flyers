using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class PropertyType : CreateFlyerWizardControlBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            SetInputs();
            form.Action = FormAction;
        }

        public override void SetSessionData(Boolean isInitial)
        {
            if (!isInitial)
            {
                Flyer.OfferType = Request["offer"];
                Flyer.PropertyType = Request["property"];
                Flyer.PropertyCategoryId = GetPropertyCategoryId();
                Flyer.ResidentialType = Request["residentialtype"];
                Flyer.PropertyTypeStepCompleted = true;
            }
        }

        protected Boolean IsOfferChecked(String offer)
        {
            var result = false;

            if (String.IsNullOrEmpty(Flyer.OfferType))
            {
                result = String.Compare(offer, "real estate", true) == 0;
            }
            else
            {
                result = String.Compare(Flyer.OfferType, offer, true) == 0;
            }

            return result;
        }

        protected Boolean IsPropertyTypeChecked(String propertyType)
        {
            var result = false;

            if (String.IsNullOrEmpty(Flyer.PropertyType))
            {
                result = String.Compare(propertyType, "residential", true) == 0;
            }
            else
            {
                result = String.Compare(Flyer.PropertyType, propertyType, true) == 0;
            }

            return result;
        }

        #region private

        private void SetInputs()
        {
            if (ddlResidentialType.DataSource == null)
            {
                SetDdlResidentialTypeDataSource();
            }

            SetDdlResidentialTypeSelectedIndex();
            ddlResidentialType.Attributes.Add("data-clientname", "residentialtype");
        }

        private void SetDdlResidentialTypeDataSource()
        {
            var sdsResidentialType = new SqlDataSource(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString, "SELECT [PropertyTypeID], [PropertyType] FROM [fly_PropertyType] where IsActive=1");
            var data = sdsResidentialType.Select(new DataSourceSelectArguments());

            ddlResidentialType.DataSource = data;
            ddlResidentialType.DataBind();
        }

        private void SetDdlResidentialTypeSelectedIndex()
        {
            if (!String.IsNullOrEmpty(Flyer.ResidentialType))
            {
                var item = ddlResidentialType.Items.FindByValue(Flyer.ResidentialType);

                if (item != null)
                {
                    ddlResidentialType.SelectedIndex = ddlResidentialType.Items.IndexOf(item);
                }
            }
        }

        private String GetPropertyCategoryId()
        {
            String result = null;
            var sdsPropertyCategory = new SqlDataSource(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString, "SELECT * FROM [fly_PropertyCategory]");
            var data = sdsPropertyCategory.Select(new DataSourceSelectArguments());
            String category;

            foreach (DataRowView drv in data)
            {
                category = drv["Category"].ToString();

                if (category.IndexOf(Flyer.OfferType, StringComparison.OrdinalIgnoreCase) >= 0 && 
                    category.IndexOf(Flyer.PropertyType, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result = drv["CategoryID"].ToString();
                    break;
                }
            }

            if (result == null)
            {
                throw new Exception("Cannot infer property category id.");
            }

            return result;
        }

        #endregion
    }
}
