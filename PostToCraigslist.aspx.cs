using FlyerMe.Controls;
using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class PostToCraigslist : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Post Flyer to Criagslist | Generate HTML | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var order = Helper.GetOrder(Request, Response, Page.User.Identity.Name);
                var orderId = order.order_id;

                hfOrderId.Value = orderId.ToString();
                aConvertToJpg.HRef = ResolveUrl("~/converttojpg.aspx?oid=" + orderId.ToString());

                textareaMarkup.Value = order.markup.Replace(clsUtility.SiteWwwDomain, "images.leadzetta.com")
                                                   .Replace(clsUtility.SiteTopLevelDomain, "images.leadzetta.com")
                                                   .Replace(clsUtility.ProjectNameInLowerCase, String.Empty);
                ltlMarkup.Text = order.markup;
                checkboxIncludeEmail.Checked = true;
            }
        }

        protected void btnRegenerateCode_Click(object sender, EventArgs e)
        {
            var order = Helper.GetOrder(Request, Response, Page.User.Identity.Name);

            if (checkboxIncludeEmail.Checked)
            {
                textareaMarkup.Value = order.markup.Replace(clsUtility.SiteWwwDomain, "images.leadzetta.com")
                                                   .Replace(clsUtility.SiteTopLevelDomain, "images.leadzetta.com")
                                                   .Replace(clsUtility.ProjectNameInLowerCase, String.Empty);
            }
            else
            {
                var emails = new String[3];

                emails[0] = order.field1;
                emails[1] = Page.User.Identity.Name;
                emails[2] = order.customer_id;
                textareaMarkup.Value = order.markup;

                foreach (var email in emails)
                {
                    if (!String.IsNullOrEmpty(email))
                    {
                        textareaMarkup.Value = textareaMarkup.Value.Replace(email, String.Empty);
                    }
                }

                textareaMarkup.Value = textareaMarkup.Value.Replace(clsUtility.SiteWwwDomain, "images.leadzetta.com")
                                                           .Replace(clsUtility.SiteTopLevelDomain, "images.leadzetta.com")
                                                           .Replace(clsUtility.ProjectNameInLowerCase, String.Empty);
            }

            ltlMarkup.Text = order.markup;
        }
    }
}
