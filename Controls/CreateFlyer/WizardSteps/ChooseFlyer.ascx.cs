using System;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class ChooseFlyer : CreateFlyerWizardControlBase
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
                Flyer.Layout = Request["layout"];
                Flyer.Markup = Helper.GetFlyerMarkup(Flyer.Layout);
                Flyer.ChooseFlyerStepCompleted = true;
            }
        }

        protected void rptSamples_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            var imageTemplate = e.Item.FindControl("imageTemplate") as Image;

            if (Flyer.FlyerType == FlyerTypes.Seller)
            {
                imageTemplate.ImageUrl = String.Format("~/images/smple/template_{0}.png", (e.Item.ItemIndex + 1).ToString());
            }
            else if (Flyer.FlyerType == FlyerTypes.Buyer)
            {
                imageTemplate.ImageUrl = String.Format("~/images/smple/template_buyer_{0}.png", (e.Item.ItemIndex + 1).ToString());
            }

            var divBlock = e.Item.FindControl("divBlock") as HtmlGenericControl;
            var aSelectMe = divBlock.FindControl("aSelectMe") as HtmlAnchor;
            var layoutValue = Helper.GetFlyerLayout((Int32)e.Item.DataItem, Flyer.FlyerType);

            aSelectMe.Attributes.Add("data-value", layoutValue);
            aSelectMe.Attributes.Add("href", String.Empty);

            if (String.Compare(inputLayout.Value, layoutValue, false) == 0)
            {
                divBlock.Attributes["class"] += " selected";
                inputLayout.Value = layoutValue;
            }

            var aPreview = divBlock.FindControl("aPreview") as HtmlAnchor;

            aPreview.Attributes.Add("href", ResolveUrl("~/preview.aspx?l=" + layoutValue));
        }

        #region private

        private void SetInputs()
        {
            inputLayout.Value = !String.IsNullOrEmpty(Flyer.Layout) ? Flyer.Layout : Helper.GetFlyerLayout(null, Flyer.FlyerType);
            SetRptSamplesDatasource();
        }

        private void SetRptSamplesDatasource()
        {
            var arraySize = 6;
            var startNumber = 20;

            if (Flyer.FlyerType == FlyerTypes.Buyer)
            {
                arraySize = 2;
                startNumber = 60;
            }

            var a = new Int32[arraySize];
            
            for(var i = 0; i < a.Length; i++)
            {
                a[i] = i + startNumber + 1;
            }

            rptSamples.DataSource = a;
            rptSamples.DataBind();
        }

        #endregion
    }
}
