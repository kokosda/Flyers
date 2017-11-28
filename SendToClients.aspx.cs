using FlyerMe.Controls;
using System;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class SendToClients : PageBase
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
                                 .SetPageTitle("Send to Clients | {0}", clsUtility.ProjectName);
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
                    var order = Helper.GetOrder(Request, Response, Page.User.Identity.Name);

                    if (!(String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0 ||
                          String.Compare(order.status, Order.flyerstatus.Scheduled.ToString(), true) == 0 ||
                          String.Compare(order.status, Order.flyerstatus.Queued.ToString(), true) == 0))
                    {
                        Helper.SetErrorResponse(HttpStatusCode.Forbidden, String.Format("Send this flyer to clients operation is available only for flyers in one of these statuses: Sent, Scheduled, Queued. This flyer (ID = {0}) status is {1}.", order.order_id.ToString(), order.status.ToUpper()));
                    }

                    SetControlText("Title", order.title);
                    SetControlText("Address", Helper.GetFullAddress(order));
                    SetControlText("FlyerId", order.order_id.ToString());
                    SetControlText("DeliveryDate", order.delivery_date.FormatDate());
                    SetControlText("Mls", order.mls_number);
                    SetControlText("FlyerType", order.type.Capitalize());
                    SetControlText("Status", order.status.Capitalize());
                    SetControlText("Markets", GetMarket(order));

                    if (!String.IsNullOrEmpty(order.photo1))
                    {
                        imgPhoto1.ImageUrl = Helper.GetPhotoPath(order.order_id, 1, order.photo1);
                    }
                    else
                    {
                        imgPhoto1.ImageUrl = "~/images/no-photo.jpg";
                    }

                    btnSendToClients.Attributes.Add("data-serverbutton", "true");
                }
            }

            ClientScript.GetPostBackEventReference(this, String.Empty);
        }

        protected void btnSendToClients_Click(Object sender, EventArgs e)
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
                message = Helper.GetEncodedUrlParameter("Flyer sent to clients successfully!");
                Response.Redirect("~/sendtoclients.aspx?successmessage=" + message, true);
            }
            else
            {
                divSummaryError.Visible = true;
                ltlMessage.Text = message;
            }
        }

        #region private

        private void SetControlText(String field, String value)
        {
            if(String.IsNullOrEmpty(value))
            {
                (form.FindControl("div" + field) as HtmlContainerControl).Visible = false;
            }
            else
            {
                (form.FindControl("ltl" + field) as Literal).Text = value;
            }
        }

        private String GetMarket(Order order)
        {
            String result = null;

            if (!String.IsNullOrEmpty(order.market_county))
            {
                result = String.Format("<strong>Market County(s):</strong> {0}", order.market_county.Replace("|", ", "));
            }
            else if (!String.IsNullOrEmpty(order.market_msa))
            {
                result = String.Format("<strong>Market MSA(s):</strong> {0}", order.market_msa.Replace("|", ", "));
            }
            else if (!String.IsNullOrEmpty(order.market_association))
            {
                result = String.Format("<strong>Market Association(s):</strong> {0}", order.market_association.Replace("|", ", "));
            }

            return result;
        }

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
