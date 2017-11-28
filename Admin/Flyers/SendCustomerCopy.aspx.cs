using FlyerMe.Admin.Controls;
using FlyerMe.BLL.CreateFlyer;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Flyers
{
    public partial class SendCustomerCopy : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            SetInputs();
        }

        protected void save_Command(Object sender, CommandEventArgs e)
        {
            var orderId = Request["orderid"];

            if (orderId.HasNoText())
            {
                message.MessageText = "Provide Order ID to send customer copy.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else
            {
                try
                {
                    var order = Helper.GetOrder(Request, Response);
                    var flyer = WizardFlyer.FromOrder(order);
                    var sitePath = AdminSiteRootUrl;
                    var ordersTable = new DataTable();
                    var deliveryStartedAt = DateTime.Now.ToString();
                    var emailBody = String.Empty;
                    var url = "~/flyer/markup/aa_sendtoclients_header.aspx";
                    var nvc = new NameValueCollection();

                    nvc.Add("orderid", flyer.OrderId.ToString());
                    nvc.Add("customername", flyer.Name);
                    nvc.Add("message", Helper.GetUrlEncodedString(textareaNote.Value.Replace("\n", "<br />")));
                    url += "?" + nvc.NameValueToQueryString();
                    emailBody = Helper.GetPageMarkup(url);

                    emailBody += order.markup;

                    url = "~/flyer/markup/ab_sendtoclients_footer.aspx";
                    nvc = new NameValueCollection();
                    nvc.Add("subscriberid", "client");
                    url += "?" + nvc.NameValueToQueryString();
                    emailBody += Helper.GetPageMarkup(url);

                    var fromName = flyer.Name + " (" + clsUtility.SiteBrandName + ")";
                    var toEmail = String.Empty;
                    var toName = String.Empty;

                    if (inputCopyEmail.Value.Trim().HasNoText())
                    {
                        toEmail = flyer.Email;
                        toName = flyer.Name;
                    }
                    else
                    {
                        toEmail = inputCopyEmail.Value.Trim();
                        toName = inputCopyEmail.Value.Trim();
                    }

                    Helper.SendEmail(fromName, toEmail, toName, flyer.EmailSubject, emailBody);
                    message.MessageText = "Customer Copy has been sent.";
                    message.MessageClass = MessageClassesEnum.Ok;
                }
                catch (Exception ex)
                {
                    message.MessageText = ex.Message;
                    message.MessageClass = MessageClassesEnum.Error;
                }
            }

            message.RedirectToShowMessage();
        }

        #region private

        private void SetInputs()
        {
            if (!IsPostBack)
            {
                if (Request["orderid"].HasText())
                {
                    var order = Helper.GetOrder(Request, Response);

                    ltlUserEmailId.Text = order.customer_id;
                    ltlSubject.Text = order.email_subject;
                }
                else
                {
                    message.RedirectToShowMessage("Provide Order ID to view customer copy.", MessageClassesEnum.System);
                }
            }
        }

        #endregion
    }
}
