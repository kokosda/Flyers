using FlyerMe.Admin.Controls;
using FlyerMe.BLL.CreateFlyer;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Flyers
{
    public partial class AddComment : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            SetInputs();
        }

        protected void save_Command(Object sender, CommandEventArgs e)
        {
            if (Request.QueryString["orderid"].HasNoText())
            {
                message.MessageText = "Provide Order ID to save comment.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else
            {
                try
                {
                    var order = Helper.GetOrder(Request, Response);

                    using (var cmd = new SqlCommand())
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString()))
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "sp_insertComments";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderID", order.order_id.ToString());
                        cmd.Parameters.AddWithValue("@Comments", textareaComment.Value);

                        if (conn.State != ConnectionState.Open) 
                        {
                            conn.Open(); 
                        }

                        cmd.ExecuteNonQuery();
                        message.MessageText = "Comment Saved.";
                        message.MessageClass = MessageClassesEnum.Ok;
                    }
                }
                catch (Exception ex)
                {
                    message.MessageText = ex.Message;
                    message.MessageClass = MessageClassesEnum.Error;
                }
            }

            message.RedirectToShowMessage();
        }

        #region private

        private void SetInputs()
        {
            if (!IsPostBack)
            {
                if (Request["orderid"].HasText())
                {
                    var order = Helper.GetOrder(Request, Response);

                    using (var cmd = new SqlCommand())
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString()))
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "select Comment, LastModified from fly_Comment where orderid=" + Request["orderid"];

                        if (conn.State != ConnectionState.Open) 
                        { 
                            conn.Open(); 
                        }

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                textareaComment.Value = dr["Comment"].ToString();
                                ltlLastModifiedOn.Text = dr["LastModified"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    message.RedirectToShowMessage("Provide Order ID to view comment.", MessageClassesEnum.System);
                }
            }
        }

        #endregion
    }
}
