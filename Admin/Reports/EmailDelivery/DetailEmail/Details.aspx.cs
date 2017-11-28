using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports.EmailDelivery.DetailEmail
{
    public partial class Details : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            InitGrid();
            InitElements();

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            try
            {
                grid.DataBind();
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = Admin.Controls.MessageClassesEnum.Error;
                message.RedirectToAbsolutPathToShowMessage();
            }
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Subscriber ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Sent" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Delivery Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Opened" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Bounce Back" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Opened Times" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Opened IP" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email First Opened Time" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email Last Opened Time" });
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["subscriber_id"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["email"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_sent"].ToString(),
                                                                    DisplayAsDisabledInput = true,
                                                                    InputType= DataInputTypes.Checkbox
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateStringFromDataColumn("Email_Delivery_Datetime")
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_opened"].ToString(),
                                                                    DisplayAsDisabledInput = true,
                                                                    InputType = DataInputTypes.Checkbox
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_Bounce_Back"].ToString(),
                                                                    DisplayAsDisabledInput = true,
                                                                    InputType = DataInputTypes.Checkbox
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_openedtimes"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_opened_IP"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_opened_datetime"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_last_opened_datetime"].ToString()
                                                                });
        }

        #region private

        private void InitGrid()
        {
            Int64 orderId = 0;

            if (Request["orderid"].HasNoText())
            {
                message.MessageText = "Order ID is required.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }
            else if (!Int64.TryParse(Request["orderid"], out orderId) || orderId <= 0)
            {
                message.MessageText = "Order ID must be a positive integral number.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                grid.GridDataSource.SqlDataSourceConnectionString = ConfigurationManager.ConnectionStrings["fdeliveryDBConnectionString"].ConnectionString;
                grid.GridDataSource.SqlDataSourceSelectCommand = "usp_GetDeliveryDetailReport";
                grid.GridDataSource.SqlDataSourceSelectParameters.Add("order_id", TypeCode.Int64, orderId.ToString());
                grid.GridDataSource.SqlDataSourceSelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
                grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
            }
            else
            {
                grid.Visible = false;
                message.ShowMessage();
            }
        }

        private void InitElements()
        {
            if (Request["orderid"].HasText())
            {
                hlSendFleyrResults.NavigateUrl = "~/admin/reports/senddelivery.aspx?orderid=" + Request["orderid"];
            }
            else
            {
                hlSendFleyrResults.Visible = false;
            }
        }

        #endregion
    }
}