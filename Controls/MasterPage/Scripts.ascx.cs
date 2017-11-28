using System;
using System.Web.Security;

namespace FlyerMe.Controls.MasterPage
{
    public partial class Scripts : System.Web.UI.UserControl
    {
        public String BundleName { get; set; }

        public Boolean RenderAnalytics
        {
            get
            {
                if (renderAnalytics.HasValue)
                {
                    return renderAnalytics.Value;
                }
#if DEBUG
                return false;
#else
                return true;
#endif
            }
            set
            {
                renderAnalytics = value;
            }
        }

        public Boolean RenderChat
        {
            get
            {
                return renderChat ?? true;
            }
            set
            {
                renderChat = value;
            }
        }

        public String VisibleUsername
        {
            get
            {
                if (visibleUsername.HasNoText())
                {
                    var header = Page.Master.FindControl("Header");

                    if (header != null)
                    {
                        var userMenu = header.FindControl("UserMenu");

                        if (userMenu != null)
                        {
                            visibleUsername = userMenu.GetType().GetProperty("VisibleUsername").GetValue(userMenu) as String;
                        }
                    }
                }

                return visibleUsername;
            }
        }

        public Double CreationDateUnix
        {
            get
            {
                var result = 0D;

                if (Page.User.Identity.IsAuthenticated)
                {
                    var dateTime = Membership.GetUser().CreationDate;

                    result = (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
                }

                return result;
            }
        }

        #region private

        private Boolean? renderAnalytics;
        private Boolean? renderChat;
        private String visibleUsername;

        #endregion
    }
}
