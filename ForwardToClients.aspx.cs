using FlyerMe.Controls;
using System;
using System.Net;
using System.Text;
using System.Web.UI;

namespace FlyerMe
{
    public partial class ForwardToClients : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/sendtoclients";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Forward to Clients | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (Request.IsGet())
            {
                if (!String.IsNullOrEmpty(Request["successmessage"]))
                {
                    divAccount.Visible = false;
                    divSuccess.Visible = true;
                    ltlSuccessMessage.Text = Request["successmessage"];
                }
                else
                {
                    var order = Helper.GetOrder(Request, Response);

                    if (String.IsNullOrEmpty(order.markup))
                    {
                        Helper.SetErrorResponse(HttpStatusCode.BadRequest, String.Format("Flyer is empty. Cannot handle flyer without any content. Go to My Flyers and choose Complete flyer option."));
                    }

                    ltlMarkup.Text = order.markup;
                    btnForwardToClients.Attributes.Add("data-serverbutton", "true");
                }
            }

            ClientScript.GetPostBackEventReference(this, String.Empty);
        }

        protected void btnForwardToClients_Click(Object sender, EventArgs e)
        {
            String message = null;

            if (String.IsNullOrEmpty(inputEmailSubject.Value) || String.IsNullOrEmpty(inputEmailSubject.Value.Trim()))
            {
                message = "Email Subject is required.";
            }

            if (String.IsNullOrEmpty(textareaEmailAddresses.Value) || String.IsNullOrEmpty(textareaEmailAddresses.Value.Trim()))
            {
                message = "Email Addresses are required.";
            }

            if (String.IsNullOrEmpty(message))
            {
                String[] emails;
                String[] names;
                const Int32 emailMaxCount = 10;

                Helper.ParseContacts(textareaEmailAddresses.Value, emailMaxCount, out emails, out names, out message);

                if (String.IsNullOrEmpty(message))
                {
                    var profile = Profile.GetProfile(Page.User.Identity.Name);
                    var customerName = String.Empty;

                    if (profile.MiddleInitial.Length > 0)
                    {
                        customerName = profile.FirstName + " " + profile.MiddleInitial + " " + profile.LastName;
                    }
                    else
                    {
                        customerName = profile.FirstName + " " + profile.LastName;
                    }

                    var order = Helper.GetOrder(Request, Response);
                    var helper = new Helper();
                    var emailSentCounter = 0;
                    var emailBody = CombineEmailBody(order, customerName, textareaMessage.Value.Trim());
                    var senderName = customerName + "  (" + clsUtility.SiteBrandName + ")";

                    for (var i = 0; i < emails.Length; i++)
                    {
                        if (emailSentCounter >= emailMaxCount)
                        {
                            break;
                        }

                        if (!String.IsNullOrEmpty(emails[i]))
                        {
                            if (!Helper.IsEmailInSpamList(emails[i]))
                            {
                                try
                                {
                                    var eb = emailBody.Replace(GetUnsubscribePattern(), Helper.GetUrlEncodedString(emails[i]));

                                    Helper.SendEmail(senderName, emails[i], names[i], inputEmailSubject.Value.Trim(), eb);
                                }
                                catch (Exception ex)
                                {
                                    message = String.Format("Flyer delivery failed. Please try again later or contact us for assistance. Failed on email address {0}. Exception: {1}", emails[i], ex.Message);
                                    break;
                                }
                            }

                            emailSentCounter++;
                        }
                    }
                }
            }            

            if (String.IsNullOrEmpty(message))
            {
                message = Helper.GetEncodedUrlParameter("Flyer forwarded to clients successfully!");
                Response.Redirect("~/forwardtoclients.aspx?successmessage=" + message, true);
            }
            else
            {
                divSummaryError.Visible = true;
                ltlMessage.Text = message;
            }
        }

        #region private

        private String CombineEmailBody(Order order, String customerName, String message)
        {
            String result = null;
            var sb = new StringBuilder();
            var url = String.Format("~/flyer/markup/AA_SendToClients_Header.aspx?orderid={0}&customername={1}&message={2}", Helper.GetEncodedUrlParameter(order.order_id.ToString()), Helper.GetEncodedUrlParameter(customerName), Helper.GetEncodedUrlParameter(message));
            var markup = Helper.GetPageMarkup(url);

            sb.Append(markup);
            sb.Append(order.markup);
            url = "~/flyer/markup/AB_SendToClients_Footer.aspx?email=" + Helper.GetUrlEncodedString(GetUnsubscribePattern());
            markup = Helper.GetPageMarkup(url);
            sb.Append(markup);
            result = sb.ToString();

            return result;
        }

        private String GetUnsubscribePattern()
        {
            return "{email}";
        }

        #endregion
    }
}
