using FlyerMe.Controls;
using Project.Infrastructure.Helpers;
using System;
using System.Linq;

namespace FlyerMe
{
    public partial class EnsureEmailInstructions : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Steps to ensure emails reach your inbox | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs args)
        {
        }

        protected String GetServiceAccountsMarkup()
        {
            var result = String.Empty;
            var smtpServers = EmailHelper.GetSmtpServersPool();

            result = String.Join(", ", smtpServers.Select(s => String.Format("<a href='{0}'>{0}</a>", s.SenderAddress)));

            return result;
        }
    }
}