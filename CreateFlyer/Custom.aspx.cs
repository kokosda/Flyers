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
    public partial class Custom : FlyerWizardPageBase
    {
        static Custom()
        {
            stepTransitions = new Dictionary<String, String>
                                    {
                                        { "uploadflyer", "selectmarketarea" },
                                        { "selectmarketarea", "checkout" },
                                        { "checkout", String.Empty }
                                    };
        }

        protected override string ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/createflyer/custom";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Custom Flyer | Create Flyer | {0}", clsUtility.SiteBrandName);
            }
        }

        protected override String PageName
        {
            get 
            {
                return "custom.aspx"; 
            }
        }

        protected override FlyerTypes FlyerType
        {
            get 
            {
                return FlyerTypes.Custom;
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
