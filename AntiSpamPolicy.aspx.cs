using FlyerMe.Controls;
using System.Web.UI;

namespace FlyerMe
{
    public partial class AntiSpamPolicy : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Anti-Spam Policy | {0}", clsUtility.ProjectName);
            }
        }
    }
}