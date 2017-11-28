using FlyerMe.Controls;
using Project.Infrastructure.Helpers;
using System;
using System.Linq;

namespace FlyerMe.EmailTemplates
{
    public partial class WelcomeEmail : EmailMarkupPageBase
    {
        public String Fullname
        {
            get
            {
                return Request["fullname"];
            }
        }

        public String Login
        {
            get
            {
                return Request["login"];
            }
        }

        public String Password
        {
            get
            {
                return Request["password"];
            }
        }

        public String PromoCode
        {
            get
            {
                return Request["promocode"];
            }
        }

        protected String GetServiceAccountsMarkup()
        {
            var result = String.Empty;
            var smtpServers = EmailHelper.GetSmtpServersPool();

            result = String.Join(", ", smtpServers.Select(s => String.Format("<a href='{0}' style=' text-decoration:none; color:#ffffff; '>{0}</a>", s.SenderAddress)));

            return result;
        }
    }
}