using FlyerMe.Controls;
using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class HtmlSubmitInstructions : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Post My Flyer on Criagslist | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
        }
    }
}
