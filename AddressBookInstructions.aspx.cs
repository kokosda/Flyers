using Project.Infrastructure.Helpers;
using System;
using System.Linq;
using System.Web.UI;

namespace FlyerMe
{
    public partial class AddressBookInstructions : PageBase
    {
        protected String GetServiceAccountsMarkup()
        {
            var result = String.Empty;
            var smtpServers = EmailHelper.GetSmtpServersPool();

            result = String.Join(", ", smtpServers.Select(s => String.Format("<a href='{0}'>{0}</a>", s.SenderAddress.ToUpper())));

            return result;
        }
    }
}