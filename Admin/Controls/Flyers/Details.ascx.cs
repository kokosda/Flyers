using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Flyers
{
    public partial class Details : AdminControlBase
    {
        public override void Page_Load(Object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            SetHlBackToManagerOrder(hlBackToManageOrderTop);
            SetHlBackToManagerOrder(hlBackToManageOrderBottom);

            DataBinding += (s2, e2) =>
            {
                SetLinks();
            };
        }

        protected void delete_Command(Object sender, CommandEventArgs e)
        {
            var sds = (Page as AdminPageBase).SqlDataSource;

            sds.DeleteParameters["order_id"].DefaultValue = e.CommandArgument as String;
            (Page as AdminPageBase).SqlDataSource.Delete();
            Response.Redirect("~/admin/flyers.aspx", true);
        }

        protected void approveFlyer_Command(Object sender, CommandEventArgs e)
        {
            String message;
            MessageClassesEnum messageClass;

            try
            {
                using (var cmd = new SqlCommand())
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_ApproveFlyer";
                    cmd.Parameters.AddWithValue("@OrderID", Int64.Parse(e.CommandArgument as String));
                    cmd.Connection = conn;

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        message = "Flyer has been approved successfully!";
                        messageClass = MessageClassesEnum.Ok;
                    }
                    else
                    {
                        message = "Couldn't approve flyer. No flyer was found for given order id (" + e.CommandArgument + ").";
                        messageClass = MessageClassesEnum.System;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                messageClass = MessageClassesEnum.Error;
            }

            RedirectToShowMessage(message, messageClass);
        }

        protected void rescheduleFlyer_Command(Object sender, CommandEventArgs e)
        {
            String message;
            MessageClassesEnum messageClass;

            try
            {
                using (var cmd = new SqlCommand())
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_RescheduleFlyer";
                    cmd.Parameters.AddWithValue("@OrderID", Int64.Parse(e.CommandArgument as String));
                    cmd.Parameters.AddWithValue("@FDeliveryDbo", ConfigurationManager.AppSettings["FDeliveryDbo"]);
                    cmd.Connection = conn;

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        message = "Flyer has been rescheduled successfully!";
                        messageClass = MessageClassesEnum.Ok;
                    }
                    else
                    {
                        message = "Couldn't reschedule flyer. No flyer was found for given order id (" + e.CommandArgument + ").";
                        messageClass = MessageClassesEnum.System;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                messageClass = MessageClassesEnum.Error;
            }

            RedirectToShowMessage(message, messageClass);
        }

        #region private

        private void SetHlBackToManagerOrder(HyperLink hl)
        {
            hl.NavigateUrl = "~/admin/flyers.aspx";

            if (Request.UrlReferrer != null)
            {
                if (Request.UrlReferrer.AbsolutePath.IndexOf("admin/flyers.aspx", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    hl.NavigateUrl = Request.UrlReferrer.ToString();
                }
            }
        }

        private void SetLinks()
        {
            var status = Eval("status") as String;

            if (String.Compare(status, Order.flyerstatus.Sent.ToString(), true) == 0)
            {
                btnRescheduleFlyer.Visible = true;
            }
            else
            {
                btnRescheduleFlyer.Visible = false;
            }

            if (String.Compare(status, Order.flyerstatus.Pending_Approval.ToString(), true) == 0)
            {
                btnApproveFlyer.Visible = true;
            }
            else
            {
                btnApproveFlyer.Visible = false;
            }

            var url = ResolveUrl("~/admin/flyers/sendcustomercopy.aspx?orderid=" + Eval("order_id"));

            hlSendCustomerCopyTop.NavigateUrl = url;
            hlSendCustomerCopyTop.Attributes.Add("onclick", (Page as AdminPageBase).GetJsWindowPopupOnClickAttribute(url, "sendcustomercopy"));
            hlSendCustomerCopyBottom.NavigateUrl = url;
            hlSendCustomerCopyBottom.Attributes.Add("onclick", (Page as AdminPageBase).GetJsWindowPopupOnClickAttribute(url, "sendcustomercopy"));

            url = ResolveUrl("~/admin/flyers/addcomment.aspx?orderid=" + Eval("order_id"));
            aAddCommentsTop.HRef = url;
            aAddCommentsTop.Attributes.Add("onclick", (Page as AdminPageBase).GetJsWindowPopupOnClickAttribute(url, "addcomment"));
            aAddCommentsBottom.HRef = url;
            aAddCommentsBottom.Attributes.Add("onclick", (Page as AdminPageBase).GetJsWindowPopupOnClickAttribute(url, "addcomment"));
        }

        #endregion
    }
}