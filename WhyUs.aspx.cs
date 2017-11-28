using FlyerMe.Controls;
using System.Web.UI;

namespace FlyerMe
{
    public partial class WhyUs : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Why Us | {0}", clsUtility.ProjectName);
            }
        }
    }
}