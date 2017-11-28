using System;

namespace FlyerMe.Controls.MasterPage
{
    public partial class UserMenu : System.Web.UI.UserControl
    {
        protected string RootURL;

        public String VisibleUsername
        {
            get
            {
                String result = null;

                if (Profile.FirstName != null)
                {
                    result = Profile.FirstName;

                    if (Profile.LastName != null)
                    {
                        result = String.Format("{0} {1}", result, Profile.LastName);
                    }
                }
                else
                {
                    result = Page.User.Identity.Name;
                }

                return result;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
            imgUserPicture.AlternateText = VisibleUsername;

            if (!String.IsNullOrEmpty(Profile.ImageURL))
            {
                imgUserPicture.ImageUrl = Profile.ImageURL;
            }
            else
            {
                imgUserPicture.ImageUrl = "~/images/user-pic-small.png";
            }

            var incompleteFlyers = (Int32?)(new OrdersBLL().Adapter.GetOrdersCountByStatus(Order.flyerstatus.Incomplete.ToString(), Page.User.Identity.Name));

            if (incompleteFlyers > 0)
            {
                spanIncompleteFlyers.InnerText = " " + incompleteFlyers.ToString();

                if (incompleteFlyers == 1)
                {
                    spanIncompleteFlyers.Attributes["title"] = "You have 1 incomplete flyer.";
                }
                else
                {
                    spanIncompleteFlyers.Attributes["title"] = String.Format("You have {0} incomplete flyers.", incompleteFlyers.ToString());
                }
            }
            else
            {
                spanIncompleteFlyers.Visible = false;
            }
        }
    }
}
