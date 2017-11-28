using System;
using System.Net;
using System.Web.UI.WebControls;
using Project.Infrastructure.Helpers;
using FlyerMe.Controls;

namespace FlyerMe
{
    public partial class Unsubscribe : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/unsubscribe";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Unsubscribe | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            Request.RedirectToHttpIfRequired(Response);

            if (Request.IsGet())
            {
                inputEmail.Attributes["type"] = "email";

                if (Request["email"].HasText())
                {
                    inputEmail.Value = Request["email"];
                }

                if (CanUnsubscribeByIdAndKey())
                {
                    divContentUnsubscribe.Visible = false;
                    divUnsubscibeByIdAndSecretPreview.Visible = true;
                }
            }
        }

        protected void btnUnsubscribe_Click(Object sender, EventArgs e)
        {
            if (inputEmail.Value.HasNoText())
            {
                message.MessageText = "Email is required.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                var subBLL = new SubscribeBLL();

                Helper.InsertEmailInSpamList(inputEmail.Value.Trim());

                if (subBLL.UnsubscribeByEmail(inputEmail.Value.Trim()))
                {
                    message.MessageText = "You have been successfully unsubscribed. Please allow 48 hours for this to take effect. Thank you.";
                    message.MessageClass = Admin.Controls.MessageClassesEnum.Ok;
                }
                else
                {
                    message.MessageText = "Your address has been removed from our mailing list.";
                    message.MessageClass = Admin.Controls.MessageClassesEnum.Ok;
                }
            }

            if (message.MessageClass == Admin.Controls.MessageClassesEnum.Ok)
            {
                message.RedirectToShowMessage();
            }
            else
            {
                message.ShowMessage();
            }
        }

        protected void lbUnsubscribeByIdAndSecret_Command(Object sender, CommandEventArgs e)
        {
            var subscriberIdStr = Request["s"];
            var key = Request["k"];

            Int64 @int64;
            Int64 subscriberId = 0L;

            if (!Int64.TryParse(subscriberIdStr, out @int64))
            {
                message.MessageText = "Subscriber ID should be an integral number.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }
            else
            {
                subscriberId = @int64;
            }

            if (message.MessageText.HasNoText())
            {
                try
                {
                    var subBLL = new SubscribeBLL();
                    var subscribersDataTable = subBLL.GetSubscriberById(subscriberId);

                    if (subscribersDataTable.Rows.Count > 0)
                    {
                        var lastName = subscribersDataTable.Rows[0]["last_name"] as String;
                        var lastNameHash = SecurityHelper.GetHashOfString(lastName).Substring(0, 5);

                        if (String.Compare(key, lastNameHash, true) != 0)
                        {
                            message.MessageText = String.Format("k parameter is not valid. Ensure that you opened this page from link in your e-mail box. Also you can unsubscribe <a href='{0}'>here</a>. You can <a href='{1}'>contact us</a> for assistance.", ResolveUrl("~/unsubscribe.aspx"), ResolveUrl("~/contacts.aspx"));
                            message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                        }

                        if (message.MessageText.HasNoText())
                        {
                            if (subBLL.Unsubscribe(subscriberId))
                            {
                                message.MessageClass = Admin.Controls.MessageClassesEnum.Ok;

                                try
                                {
                                    using (var obj = new clsData())
                                    {
                                        obj.strSql = @"insert into tblUnsubscribers(Subscriber_ID, LastName, UnsubscribeDateTime, IPAddress)
        values(" + subscriberId + ", '" + lastName.Replace("'", "''") + "', '" + DateTime.Now.ToString() + "', '" + Request.UserHostAddress + "')";

                                        obj.ExecuteSql();
                                    }
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                message.MessageText = String.Format("System encountered a problem. Please try again later or <a href='{0}'>contact us</a> for assistance.", ResolveUrl("~/contacts.aspx"));
                                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                            }
                        }
                    }
                    else
                    {
                        message.MessageText = String.Format("Subscriber ID {0} doesn't exist.", subscriberIdStr);
                        message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                    }
                }
                catch (Exception ex)
                {
                    message.MessageText = String.Format("Unhandled error occured. Please try again later or <a href='{0}'>contact us</a> for assistance. Message: {1}", ResolveUrl("~/contacts.aspx"), ex.Message);
                    message.MessageClass = Admin.Controls.MessageClassesEnum.Error;
                }
            }

            if (message.MessageClass != Admin.Controls.MessageClassesEnum.Ok)
            {
                message.ShowMessage();
            }
            else
            {
                message.HideMessage();
                divUnsubscibeByIdAndSecretPreview.Visible = false;
                divSubscibeByIdAndSecret.Visible = true;
            }
        }

        protected void lbSubscribeByIdAndSecret_Command(Object sender, CommandEventArgs e)
        {
            if (CanSubscribeByIdAndKey())
            {
                var subscriberIdStr = Request["s"];
                var key = Request["k"];

                Int64 @int64;
                Int64 subscriberId = 0L;

                if (!Int64.TryParse(subscriberIdStr, out @int64))
                {
                    message.MessageText = "Subscriber ID should be an integral number.";
                    message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                }
                else
                {
                    subscriberId = @int64;
                }

                if (message.MessageText.HasNoText())
                {
                    try
                    {
                        var subBLL = new SubscribeBLL();
                        var subscribersDataTable = subBLL.GetSubscriberById(subscriberId);

                        if (subscribersDataTable.Rows.Count > 0)
                        {
                            var lastName = subscribersDataTable.Rows[0]["last_name"] as String;
                            var lastNameHash = SecurityHelper.GetHashOfString(lastName).Substring(0, 5);

                            if (String.Compare(key, lastNameHash, true) != 0)
                            {
                                message.MessageText = String.Format("k parameter is not valid. Ensure that you opened this page from link in your e-mail box. Also you can subscribe <a href='{0}'>here</a>.", ResolveUrl("~/subscribe.aspx"), ResolveUrl("~/contacts.aspx"));
                                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                            }

                            if (subBLL.Subscribe(subscriberId))
                            {
                                message.MessageClass = Admin.Controls.MessageClassesEnum.Ok;
                                message.MessageText = "Subscription has been successfully recovered.";

                                try
                                {
                                    using (var obj = new clsData())
                                    {
                                        obj.strSql = @"delete from tblUnsubscribers where Subscriber_ID=" + subscriberId.ToString();
                                        obj.ExecuteSql();
                                    }
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                message.MessageText = String.Format("System encountered a problem. Please try again later or <a href='{0}'>contact us</a> for assistance.", ResolveUrl("~/contacts.aspx"));
                                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                            }
                        }
                        else
                        {
                            message.MessageText = String.Format("Subscriber ID {0} doesn't exist.", subscriberIdStr);
                            message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                        }
                    }
                    catch (Exception ex)
                    {
                        message.MessageText = String.Format("Unhandled error occured. Please try again later or <a href='{0}'>contact us</a> for assistance. Message: {1}", ResolveUrl("~/contacts.aspx"), ex.Message);
                        message.MessageClass = Admin.Controls.MessageClassesEnum.Error;
                    }
                }
            }
            else
            {
                message.MessageText = String.Format("Not applicable. Subscription recovery is only available for those who have unsubscribed from email link. You can subscribe <a href='{0}'>here</a>.", ResolveUrl("~/subscribe.aspx"));
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }

            if (message.MessageClass != Admin.Controls.MessageClassesEnum.Ok)
            {
                message.ShowMessage();
            }
            else
            {
                message.ShowMessage();
                divUnsubscibeByIdAndSecretPreview.Visible = true;
                divSubscibeByIdAndSecret.Visible = false;
            }
        }

        #region private

        private Boolean CanUnsubscribeByIdAndKey()
        {
            return Request["s"].HasText() && Request["k"].HasText();
        }

        private Boolean CanSubscribeByIdAndKey()
        {
            return Request["s"].HasText() && Request["k"].HasText();
        }

        #endregion
    }
}
