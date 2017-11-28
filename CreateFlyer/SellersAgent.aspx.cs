using FlyerMe.BLL.CreateFlyer;
using FlyerMe.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.CreateFlyer
{
    public partial class SellersAgent : FlyerWizardPageBase
    {
        static SellersAgent()
        {
            stepTransitions = new Dictionary<String, String>
                                    {
                                        { "flyertitle", "contactdetails" },
                                        { "contactdetails", "propertytype" },
                                        { "propertytype", "location" },
                                        { "location", "description" },
                                        { "description", "amenities" },
                                        { "amenities", "photo" },
                                        { "photo", "price" },
                                        { "price", "chooseflyer" },
                                        { "chooseflyer", "selectmarketarea" },
                                        { "selectmarketarea", "checkout" },
                                        { "checkout", String.Empty }
                                    };
        }

        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/createflyer/sellersagent";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Seller's Agent Flyer | Create Flyer | {0}", clsUtility.SiteBrandName);
            }
        }

        protected override String PageName
        {
            get 
            { 
                return "sellersagent.aspx"; 
            }
        }

        protected override FlyerTypes FlyerType
        {
            get 
            {
                return FlyerTypes.Seller;
            }
        }

        protected override ProfileCommon ProfileCommon
        {
            get 
            {
                return Profile;
            }
        }

        protected override Literal LtlScripts
        {
            get 
            {
                return ltlScripts;
            }
        }

        protected override Dictionary<String, String> StepTransitions
        {
            get 
            {
                return stepTransitions;
            }
        }

        #region private

        private static readonly Dictionary<String, String> stepTransitions;

        #endregion
    }
}
