using FlyerMe.BLL.CreateFlyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls
{
    public abstract class FlyerWizardPageBase : PageBase
    {
        protected abstract String PageName { get; }

        protected abstract FlyerTypes FlyerType { get; }

        protected abstract ProfileCommon ProfileCommon { get; }

        protected abstract Literal LtlScripts { get; }

        protected abstract Dictionary<String, String> StepTransitions { get; }

        protected void Page_Load(Object sender, EventArgs e)
        {
            Request.RedirectToHttpIfRequired(Response);

            var shouldRedirect = (Request.UrlReferrer == null || (Request.UrlReferrer.ToString().IndexOf("createflyer.aspx", StringComparison.OrdinalIgnoreCase) >= 0)) && String.IsNullOrEmpty(Request["step"]);

            if (shouldRedirect)
            {
                var url = "~/createflyer/" + PageName + "?step=" + StepTransitions.ElementAt(0).Key;

                WizardFlyer.DeleteFlyer();
                Response.Redirect(url, true);
            }

            InitFields();
            SetWizardStep();
            SetScripts();
            SetSessionData();
            SetControls();
        }

        protected void InitFields()
        {
            basePageUrl = String.Format(basePageUrl, PageName);
        }

        #region private

        private CreateFlyerWizardControlBase currentWizardStep;
        private CreateFlyerWizardControlBase renderWizardStep;
        private String currentWizardStepName;
        private String nextWizardStepName;
        private const String baseControlUrl = "~/controls/createflyer/wizardsteps/";
        private String basePageUrl = "~/createflyer/{0}?step=";

        private void SetWizardStep()
        {
            currentWizardStepName = GetCurrentWizardStepName();
            nextWizardStepName = StepTransitions[currentWizardStepName];

            if (StepTransitions.ContainsKey(currentWizardStepName))
            {
                currentWizardStep = GetWizardStepControl(currentWizardStepName, nextWizardStepName);
                renderWizardStep = GetWizardStepToRender();
            }

            var contentPlaceHolder = Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;

            contentPlaceHolder.Controls.Add(renderWizardStep);
            
            var hfStep = new HtmlInputHidden
                                {
                                    ID = "Step",
                                    Value = renderWizardStep.Step
                                };

            contentPlaceHolder.Controls.Add(hfStep);
        }

        private void SetScripts()
        {
            var stepName = renderWizardStep.ID;
            var scripts = new List<String>();

            if (String.Compare(stepName, "flyertitle", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/flyertitle.js");
            }
            else if (String.Compare(stepName, "contactdetails", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/contactdetails.js");
            }
            else if (String.Compare(stepName, "location", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/location.js");
            }
            else if (String.Compare(stepName, "description", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/description.js");
            }
            else if (String.Compare(stepName, "amenities", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/amenities.js");
            }
            else if (String.Compare(stepName, "photo", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/photo.js");
            }
            else if (String.Compare(stepName, "price", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/price.js");
            }
            else if (String.Compare(stepName, "pricerange", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/pricerange.js");
            }
            else if (String.Compare(stepName, "chooseflyer", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/chooseflyer.js");
            }
            else if (String.Compare(stepName, "uploadflyer", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/uploadflyer.js");
            }
            else if (String.Compare(stepName, "selectmarketarea", true) == 0)
            {
                scripts.Add("~/scripts/createflyer/wizardsteps/selectmarketarea.js");
            }

            if (scripts.Count > 0)
            {
                foreach (var s in scripts)
                {
                    LtlScripts.Text += String.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", ResolveUrl(s));
                }
            }
        }

        private void SetSessionData()
        {
            WizardFlyer flyer = null;
            var shouldGetFlyerFromOrder = !String.IsNullOrEmpty(Request["orderid"]);

            if (shouldGetFlyerFromOrder)
            {
                var order = Helper.GetOrder(Request, Response, User.Identity.Name);

                if (String.Compare(order.status, Order.flyerstatus.Incomplete.ToString(), true) != 0)
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, String.Format("Edit operation is available only for incomplete flyers. This flyer (ID = {0}) status is {1}.", order.order_id.ToString(), order.status.ToUpper()));
                }

                flyer = WizardFlyer.FromOrder(order);

                if (order.LastPageNo > 0 && order.LastPageNo < StepTransitions.Count)
                {
                    Response.Redirect(String.Format("{0}createflyer/{1}?step={2}", clsUtility.GetRootHost, PageName, StepTransitions.ElementAt(order.LastPageNo).Key), true);
                }
            }
            else
            {
                flyer = WizardFlyer.GetFlyer();

                if (flyer == null)
                {
                    flyer = WizardFlyer.SetFlyer(FlyerType);
                }

                CreateOrderIfNeeded(flyer);
            }

            currentWizardStep.SetSessionData(Request.IsGet());

            if (renderWizardStep != currentWizardStep)
            {
                renderWizardStep.SetSessionData(true);
            }
        }

        private CreateFlyerWizardControlBase GetWizardStepControl(String controlName, String nextControlName)
        {
            var result = LoadControl(baseControlUrl + controlName + ".ascx") as CreateFlyerWizardControlBase;

            result.ID = controlName;
            result.FormAction = ResolveClientUrl(basePageUrl + nextControlName);
            result.ParentPageName = PageName;
            result.Step = controlName;

            return result;
        }

        private CreateFlyerWizardControlBase GetWizardStepToRender()
        {
            CreateFlyerWizardControlBase result = renderWizardStep;

            if (result == null)
            {
                if (Request.IsGet())
                {
                    result = currentWizardStep;
                }
                else if (StepTransitions.ContainsKey(nextWizardStepName))
                {
                    result = GetWizardStepControl(nextWizardStepName, StepTransitions[nextWizardStepName]);
                }
            }

            return result;
        }

        private String GetCurrentWizardStepName()
        {
            var result = Request["step"]; 
            
            if (Request.IsGet())
            {
                result = result ?? StepTransitions.Keys.First();
            }
            else
            {
                foreach(var st in StepTransitions)
                {
                    if (String.Compare(st.Value, result, true) == 0)
                    {
                        result = st.Key;
                        break;
                    }
                }
            }

            return result;
        }

        private void CreateOrderIfNeeded(WizardFlyer flyer)
        {
            var shouldCreateOrder = Request.IsPost() && (!flyer.OrderId.HasValue);

            if (shouldCreateOrder)
            {
                flyer.ToOrder();
            }
        }

        private void SetControls()
        {
            var cphAfterFooter = Master.FindControl("cphAfterFooter") as ContentPlaceHolder;
            var control = LoadControl("~/controls/masterpageaccount/popup.ascx");

            cphAfterFooter.Controls.Add(control);
        }

        #endregion
    }
}