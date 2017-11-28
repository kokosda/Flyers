using FlyerMe.Controls;
using Project.Infrastructure.Helpers;
using System;
using System.Linq;

namespace FlyerMe.EmailTemplates
{
    public partial class VerifySubscriptionEmail : EmailMarkupPageBase
    {
        public String VerificationLink
        {
            get
            {
                return Request["verificationlink"];
            }
        }

        protected String GetServiceAccountsMarkup()
        {
            var result = String.Empty;
            var smtpServers = EmailHelper.GetSmtpServersPool();

            result = String.Join(", ", smtpServers.Select(s => String.Format("<a href='{0}' style='color:#f46a02;'>{0}</a>", s.SenderAddress)));

            return result;
        }
    }
}