using FlyerMe.BLL.CreateFlyer;
using FlyerMe.Controls;
using System;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;

namespace FlyerMe
{
    public partial class CreateFlyer : PageBase
    {
        protected string RootURL;

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Create Flyer | {0}", clsUtility.ProjectName)
                                 .SetDescription("Create your beautiful real estate flyers in seconds. Send your one of a kind flyer to your specified market area with one click. It's easy with {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            Request.RedirectToHttpIfRequired(Response);
            RootURL = clsUtility.GetRootHost;            
        }
                
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static Object GetPropertyData(String address, String city, String state, String zipCode)
        {
            Object result = null;

            var zillowApi = new clsZillowApi();
            DataTable dt;

            if (zillowApi.GetPropertyAttributes(Helper.GetZillowApiId(), address, city, state, zipCode))
            {
                dt = zillowApi.editedFacts;

                var row = dt.Rows[0];
                Decimal number;
                Int32 bedrooms = -1;
                Int32 bathrooms = -1;


                if (dt.Columns.Contains("bedrooms") && row["bedrooms"] != null)
                {
                    if (Decimal.TryParse(row["bedrooms"].ToString(), out number))
                    {
                        bedrooms = (Int32)number;
                    }
                }
                if (dt.Columns.Contains("bathrooms") && row["bathrooms"] != null)
                {
                    if (Decimal.TryParse(row["bathrooms"].ToString(), out number))
                    {
                        bathrooms = (Int32)number;
                    }
                }

                var objResult = new 
                                 { 
                                     Bedrooms = bedrooms > -1 ? bedrooms.ToString() : null,
                                     Bathrooms = bathrooms > -1 ? bathrooms.ToString() : null,
                                     Sqft = dt.Columns.Contains("finishedSqFt") && row["finishedSqFt"] != null ? row["finishedSqFt"].ToString() : null,
                                     LotSize = dt.Columns.Contains("lotSizeSqFt") && row["lotSizeSqFt"] != null ? row["lotSizeSqFt"].ToString() : null,
                                     YearBuilt = dt.Columns.Contains("yearBuilt") && row["yearBuilt"] != null ? row["yearBuilt"].ToString() : null,
                                     Description = zillowApi.homeDescription
                                 };

                result = objResult;
            }

            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Object SaveInDrafts()
        {
            var flyer = WizardFlyer.GetFlyer();
            var saved = false;
            String message = null;

            if (flyer != null && flyer.OrderId.HasValue)
            {
                flyer.UpdateMarkup();

                var order = flyer.ToOrder();

                if (String.Compare(order.status, Order.flyerstatus.Incomplete.ToString(), true) != 0)
                {
                    saved = false;
                    message = String.Format("Save operation is available only for incomplete flyers. This flyer (ID = {0}) status is {1}.", order.order_id.ToString(), order.status.ToUpper());
                }
                else
                {
                    order.Save();
                    saved = true;
                }
            }

            if (!saved)
            {
                message = "Nothing to save.";
            }

            var rootUrl = clsUtility.GetRootHost;
            var result = new { Result = saved, Message = message, RedirectToUrl = rootUrl + "myflyers.aspx" };

            return result;
        }
    }
}
