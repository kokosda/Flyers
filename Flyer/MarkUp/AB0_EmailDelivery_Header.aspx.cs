using FlyerMe.Controls;
using Project.Infrastructure.Helpers;
using System;
using System.Linq;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class AB0_EmailDelivery_Header : EmailMarkupPageBase
    {
        public String OrderId
        {
            get
            {
                return Request["orderid"];
            }
        }

        public String SenderAgentFullname
        {
            get
            {
                return Request["senderagentfullname"];
            }
        }

        public String SenderAgentCounty
        {
            get
            {
                return Request["senderagentcounty"];
            }
        }

        public String SenderAgentState
        {
            get
            {
                return Request["senderagentstate"];
            }
        }

        public String SenderAgentBrokerageName
        {
            get
            {
                return Request["senderagentbrokeragename"];
            }
        }

        public Boolean ShowSenderAgentIntroduction
        {
            get
            {
                var result = Request.ParseCheckboxValue("showsenderagentintroduction") == true;

                return result;
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