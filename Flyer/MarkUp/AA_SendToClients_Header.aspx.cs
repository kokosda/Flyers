using FlyerMe.Controls;
using Project.Infrastructure.Helpers;
using System;
using System.Linq;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class AA_SendToClients_Header : EmailMarkupPageBase
    {
        public String OrderId
        {
            get
            {
                return Request["orderid"];
            }
        }

        public String CustomerName
        {
            get
            {
                return Request["customername"];
            }
        }

        public String Message
        {
            get
            {
                return Request["message"];
            }
        }

        protected String GetServiceAccountsMarkup()
        {
            var result = String.Empty;
            var smtpServers = EmailHelper.GetSmtpServersPool();

            result = String.Join(", ", smtpServers.Select(s => String.Format("<a href='{0}' style='color:#285aa4; text-decoration:none;'>{0}</a>", s.SenderAddress)));

            return result;
        }
    }
}