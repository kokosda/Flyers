using FlyerMe.Controls;
using System;

namespace FlyerMe
{
    public partial class Verify : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Email verification | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            var verificationCode = Request["vcode"];
            var message = "Email verification failed. Please check to ensure that you have copied the entire URL to the locaton bar in your browser. If you continue to have problems, please <a href=" + ResolveUrl("~/contacts.aspx") + " >contact us</a> for assistance.";
            var result = false;

            if (!String.IsNullOrEmpty(verificationCode) && verificationCode.Length >= 10)
            {
                var subscriberId = verificationCode.Substring(9);

                if (subscriberId.Length > 0 && Helper.IsNumeric(subscriberId))
                {
                    var subscriberBLL = new SubscribeBLL();

                    if (subscriberBLL.Subscribe(Convert.ToInt32(subscriberId)))
                    {
                        TryRemoveFromSpamList(subscriberId);

                        message = "Your email has been verified successfully. You will soon start receiving our email flyer campaigns. Thank you being part of " + clsUtility.SiteBrandName + " community.";
                        result = true;
                    }
                }

                if (result)
                {
                    divSummary.Attributes["class"] += " saved";
                    ltlMessage.Text = message;
                }
                else
                {
                    divSummary.Attributes["class"] += " error";
                    ltlMessage.Text = message;
                }
            }
            else
            {
                divSummary.Attributes["class"] += " error";
                ltlMessage.Text = message;
            }
        }

        #region private

        private void TryRemoveFromSpamList(String subscriberIdString)
        {
            using (var obj = new clsData())
            {
                obj.strSql = "select * from fly_Subscribers_With_Email where subscriber_id = " + subscriberIdString;

                var dt = obj.GetDataTable();

                if (dt.Rows.Count == 1)
                {
                    try
                    {
                        Helper.RemoveFromSpamList(dt.Rows[0]["email"] as String);
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion
    }
}
