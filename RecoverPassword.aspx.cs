using FlyerMe.Controls;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Web.UI;

namespace FlyerMe
{
    public partial class RecoverPassword : PageBase
    {
        protected override string ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/recoverpassword";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Recover Password | {0}", clsUtility.ProjectName);
            }
        }

        protected string RootURL;

        protected String Email
        {
            get
            {
                return Request["email"];
            }
        }

        protected String Message { get; set; }

        protected Boolean SuccessfullySent { get; set; }

        protected Boolean HasError { get; set; }

        protected void Page_Load(Object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;

            if(Request.IsPost())
            {
                var user = !String.IsNullOrEmpty(Email) ? Membership.GetUser(Email) : null;

                if (user != null)
                {
                    SendEmail(user);
                    SuccessfullySent = true;
                }
                else
                {
                    Message = "We were unable to access your information. Please try again.";
                    HasError = true;
                }
            }
        }

        #region private

        protected void SendEmail(MembershipUser user)
        {
            var nv = new NameValueCollection();

            nv.Add("login", Helper.GetUrlEncodedString(user.Email));
            nv.Add("password", Helper.GetUrlEncodedString(user.GetPassword()));

            var url = "~/emailtemplates/recoverpasswordemail.aspx?" + nv.NameValueToQueryString();
            var markup = Helper.GetPageMarkup(url);

            Helper.SendEmail(user.Email, clsUtility.SiteBrandName + ": Your password", markup);
        }

        #endregion
    }
}
