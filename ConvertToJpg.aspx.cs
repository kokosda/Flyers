using FlyerMe.Controls;
using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class ConvertToJpg : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Convert to JPG | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["oid"] != null)
                {
                    imgOrder.ImageUrl = clsUtility.GetRootHost + "orderjpg/" + Request.QueryString["oid"].ToString() + ".jpg";
                }
            }
        }
    }
}
