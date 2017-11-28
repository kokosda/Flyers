using FlyerMe.Admin.Controls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace FlyerMe.Admin.Reports
{
    public partial class ArchivedDelivery : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
        }

        protected void btnGetOrderDetails_Click(Object sender, EventArgs e)
        {
            Int32 @int32;

            if (inputOrderId.Value.HasNoText())
            {
                message.MessageText = "Order ID is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (inputEmailAddressesCount.Value.HasNoText())
            {
                message.MessageText = "Email Addresses Count is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (!Int32.TryParse(inputEmailAddressesCount.Value, out @int32) || @int32 <= 0)
            {
                message.MessageText = "Email Addresses Count shoud be a positive integral number.";
                message.MessageClass = MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                try
                {
                    inputUserEmail.Value = GetCustomerEmail();

                    var dt = GetDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        hlDownloadFile.NavigateUrl = GetGeneratedRelativeFilePath(dt);
                        hlDownloadFile.Visible = true;
                        divSendFlyerResults.Visible = true;
                        inputSubject.Value = String.Format("Your flyer ID {0} delivery report.", inputOrderId.Value.Trim());

                        message.MessageText = "The file has been successfully generated. You can find download link below.";
                        message.MessageClass = MessageClassesEnum.Ok;
                    }
                    else
                    {
                        message.MessageText = "No entries found.";
                        message.MessageClass = MessageClassesEnum.System;
                    }
                }
                catch (Exception ex)
                {
                    message.MessageText = ex.Message;
                    message.MessageClass = MessageClassesEnum.Error;
                }
            }

            message.ShowMessage();
        }

        protected void btnSendFlyerResults_Click(Object sender, EventArgs e)
        {
            if (inputOrderId.Value.HasNoText())
            {
                message.MessageText = "Order ID is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (inputUserEmail.Value.HasNoText())
            {
                message.MessageText = "User Email is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (inputSubject.Value.HasNoText())
            {
                message.MessageText = "Subject is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (!File.Exists(Server.MapPath(GetRelativeFilePath())))
            {
                message.MessageText = "Generate file before sending email.";
                message.MessageClass = MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                try
                {
                    SendMail();

                    message.MessageText = String.Format("Email has been successfully sent to {0}.", inputUserEmail.Value.Trim());
                    message.MessageClass = MessageClassesEnum.Ok;
                }
                catch (Exception ex)
                {
                    message.MessageText = ex.Message;
                    message.MessageClass = MessageClassesEnum.Error;
                }
            }

            if (message.MessageClass == MessageClassesEnum.Ok)
            {
                message.RedirectToShowMessage();
            }
            else
            {
                message.ShowMessage();
            }
        }

        #region private

        private DataTable GetDataTable()
        {
            DataTable result;

            using (var obj = new clsData())
            {
                obj.objCon.Dispose();
                obj.objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["fdeliveryDBConnectionString"].ConnectionString);

                var ht = new Hashtable();

                ht.Add("rows", inputEmailAddressesCount.Value.Trim());
                ht.Add("order_id", inputOrderId.Value.Trim());

                result = obj.GetDataTable("usp_GetArchivedDeliveryReport", ht);
            }

            return result;
        }

        private String GetGeneratedRelativeFilePath(DataTable dt)
        {
            String result = null;

            var relativeFilePath = GetRelativeFilePath();
            var filePath = Server.MapPath(relativeFilePath);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            using (var sw = File.CreateText(filePath))
            {
                var i = 1;
                String emailToShow;
                sw.Write(" S.No \t User Email Id \t Delivery Date \t Mail Opened IP \t Email First Opened Time \t Email Last Opened Time \r\n ");

                foreach (DataRow row in dt.Rows)
                {
                    var subscriberEmail = row["email"].ToString().Split('@');

                    if (subscriberEmail.Length > 1)
                    {
                        emailToShow = subscriberEmail[0] + "@" + Regex.Replace(subscriberEmail[1].ToLower(), @"[\w]", "x");
                    }
                    else
                    {
                        emailToShow = subscriberEmail[0];
                    }

                    sw.Write(i.ToString() + " \t " + emailToShow +
                        " \t " + row["Email_Delivery_Datetime"].ToString() +
                        "\t " + row["Email_opened_IP"] as String +
                        " \t " + row["Email_opened_datetime"].ToString() +
                        "  \t " + row["Email_last_opened_datetime"].ToString() + "  \r\n ");
                    i++;
                }
            }

            result = relativeFilePath;

            return result;
        }

        private String GetCustomerEmail()
        {
            String result = null;
            var sqlString = "select Customer_id  from fly_order where Order_id=" + inputOrderId.Value.Trim();
            DataTable dt;

            using (var obj = new clsData())
            {
                obj.strSql = sqlString;
                dt = obj.GetDataTable();
            }

            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["customer_id"] as String;
            }

            return result;
        }

        private void SendMail()
        {
            var senderName = clsUtility.DeliveryServiceFromName;
            var recipientEmailAddress = inputUserEmail.Value.Trim();
            var emailSubject = inputSubject.Value.Trim();
            var emailBody = textareaMailBody.Value.Trim().Replace("\n", "<br/>");
            var filePathes = new[] { Server.MapPath(GetRelativeFilePath()) };

            Helper.SendEmail(senderName, recipientEmailAddress, null, emailSubject, emailBody, filePathes);
        }

        private String GetRelativeFilePath()
        {
            return String.Format("~/admin/emailcsv/flyer-{0}.xls", inputOrderId.Value.Trim());
        }

        #endregion
    }
}
