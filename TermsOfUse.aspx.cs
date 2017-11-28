using FlyerMe.Controls;
using System.Web.UI;

namespace FlyerMe
{
    public partial class TermsOfUse : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Terms of Use | {0}", clsUtility.ProjectName);
            }
        }
    }
}