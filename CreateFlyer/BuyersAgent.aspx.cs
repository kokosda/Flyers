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
    public partial class BuyersAgent : FlyerWizardPageBase
    {
        static BuyersAgent()
        {
            stepTransitions = new Dictionary<String, String>
                                    {
                                        { "flyertitle", "contactdetails" },
                                        { "contactdetails", "desiredlocation" },
                                        { "desiredlocation", "description" },
                                        { "description", "amenities" },
                                        { "amenities", "pricerange" },
                                        { "pricerange", "chooseflyer" },
                                        { "chooseflyer", "selectmarketarea" },
                                        { "selectmarketarea", "checkout" },
                                        { "checkout", String.Empty }
                                    };
        }

        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/createflyer/buyersagent";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Buyer's Agent Flyer | Create Flyer | {0}", clsUtility.SiteBrandName);
            }
        }

        protected override String PageName
        {
            get 
            {
                return "buyersagent.aspx"; 
            }
        }

        protected override FlyerTypes FlyerType
        {
            get 
            {
                return FlyerTypes.Buyer;
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
