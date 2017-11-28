using FlyerMe.BLL.CreateFlyer;
using System;
using System.Text;
using System.Web.Script.Serialization;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class Checkout : CreateFlyerWizardControlBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            if (Request.IsPost())
            {
                var result = true;

                if (Flyer.FlyerType == FlyerTypes.Seller || Flyer.FlyerType == FlyerTypes.Buyer)
                {
                    result = Flyer.FlyerTitleStepCompleted && Flyer.ContactDetailsStepCompleted;
                }

                SetResult(result, "Required steps were not completed. Please go back and proceed.");
            }
        }

        [Serializable]
        public class CheckoutResult
        {
            public Boolean Result { get; set; }

            public String Message { get; set; }
        }

        #region private

        private void SetResult(Boolean result, String message)
        {
            var isBackgroundCheck = Request.ParseCheckboxValue("backgroundcheck") ?? false;

            if (isBackgroundCheck)
            {
                var checkoutResult = new CheckoutResult 
                                         { 
                                             Result = result, 
                                             Message = message 
                                         };

                Helper.RespondWithJsonObject(checkoutResult, Response);
            }
            else
            {
                if (result)
                {
                    var order = Flyer.ToOrder();

                    order.Save();
                    WizardFlyer.DeleteFlyer();
                    Response.SuppressContent = true;
                    Response.Redirect("~/cart.aspx?orderid=" + order.order_id.ToString());
                }
                else
                {
                    throw new Exception(message);
                }
            }
        }

        #endregion
    }
}
