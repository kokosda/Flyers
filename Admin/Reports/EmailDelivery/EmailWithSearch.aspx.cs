using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports.EmailDelivery
{
    public partial class EmailWithSearch : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            InitInputs();
            InitGrid();

            if (!IsPostBack)
            {
                DataBind();
            }

            InitHtmlElements();
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
                                                                    Text = e.DataRow["email"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Boolean)e.DataRow["Email_sent"]).ToString(),
                                                                    DisplayAsDisabledInput = true,
                                                                    InputType = DataInputTypes.Checkbox
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateTimeStringFromDataColumn("Email_Delivery_Datetime")
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Boolean)e.DataRow["Email_opened"]).ToString(),
                                                                    DisplayAsDisabledInput = true,
                                                                    InputType = DataInputTypes.Checkbox
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Boolean)e.DataRow["Email_Bounce_Back"]).ToString(),
                                                                    DisplayAsDisabledInput = true,
                                                                    InputType = DataInputTypes.Checkbox
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_openedtimes"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_opened_IP"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateTimeStringFromDataColumn("Email_opened_datetime")
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateTimeStringFromDataColumn("Email_last_opened_datetime")
                                                                });
        }

        protected void btnSearch_Command(Object sender, CommandEventArgs e)
        {
            var url = Request.Url.AbsolutePath;
            var nvc = new NameValueCollection();

            if (inputOrderId.Value.HasText())
            {
                nvc.Add("orderid", inputOrderId.Value.Trim());
            }
            if (inputEmail.Value.HasText())
            {
                nvc.Add("email", inputEmail.Value.Trim());
            }
            if (nvc.Count > 0)
            {
                url += "?" + nvc.NameValueToQueryString(false);
            }

            Response.Redirect(url, true);
        }

        #region private

        private void InitInputs()
        {
            if (!IsPostBack)
            {
                inputOrderId.Value = Request["orderid"];
                inputEmail.Value = Request["email"];
            }
        }

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceConnectionString = ConfigurationManager.ConnectionStrings["fdeliveryDBConnectionString"].ConnectionString;
            grid.GridDataSource.SqlDataSourceSelectCommand = "usp_GetDeliveryDetailReport";
            grid.GridDataSource.SqlDataSourceSelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            grid.GridDataSource.SqlDataSourceSelectParameters.Add("order_id", System.Data.DbType.String, inputOrderId.Value);
   
            if (inputEmail.Value.HasText())
            {
                grid.GridDataSource.SqlDataSourceSelectCommand = "usp_GetDeliveryDetailReportByEmail";
                grid.GridDataSource.SqlDataSourceSelectParameters.Add("email", inputEmail.Value);
            }

            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
        }

        private void InitHtmlElements()
        {
            if (inputOrderId.Value.HasText())
            {
                if (grid.TotalRecords == 0)
                {
                    divEmpty.Visible = true;
                    grid.Visible = false;
                    ltlMessage.Text = "There is no data for provided input.";
                }
                else
                {
                    divEmpty.Visible = false;
                }
            }
            else
            {
                ltlMessage.Text = "Please provide Flyer ID.";
                grid.Visible = false;
            }
        }

        #endregion
    }
}
