using FlyerMe.Controls;
using System.Web.UI;

namespace FlyerMe
{
    public partial class PrivacyPolicy : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Privacy Policy | Security to Protect Real Estate Agents | {0}", clsUtility.ProjectName)
                                 .SetDescription("Team {0} created this privacy policy in order to disclose our information gathering and dissemination practices. {0} privacy policy was also created to protect the best interest of real estate agents.", clsUtility.ProjectName);
            }
        }
    }
}