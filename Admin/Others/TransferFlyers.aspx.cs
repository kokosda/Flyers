using FlyerMe.Admin.Controls;
using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using FlyerMe.SpecialExtensions;
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Others
{
    public partial class TransferFlyers : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            InitGrid();

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            grid.DataBind();
        }

        protected void btnTransferOrders_Command(Object sender, CommandEventArgs e)
        {
            try
            {
                if (ddlDays.SelectedValue.HasNoText())
                {
                    message.MessageText = "Select days.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (ddlDays.Items.FindByValue(ddlDays.SelectedValue) == null)
                {
                    message.MessageText = "Days value should be taken from list.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var obj = new clsData();
                    var ht = new Hashtable();

                    ht.Add("Days", ddlDays.SelectedValue);

                    var dt = obj.GetDataTable("usp_TransferOrders", ht);
                    var result = dt.Rows[0][0] as String;

                    if (String.Compare(result, "success", true) == 0)
                    {
                        message.MessageText = "Flyers has been transferred successfully.";
                        message.MessageClass = MessageClassesEnum.Ok;
                    }
                    else
                    {
                        message.MessageText = "Data related error occured while transferring orders. Please try again later.";
                        message.MessageClass = MessageClassesEnum.System;
                    }
                }
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
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

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Flyer ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Customer ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "State" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Type" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Total Price" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Tax" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Transaction ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Status" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Created Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Delivery Date" });
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["order_id"].ToString(),
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["customer_id"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["prop_state"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["type"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["tota_price"].GetDecimalStringFromObject("0.00")
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["invoice_tax"].GetDecimalStringFromObject("0.00")
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["invoice_transaction_id"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["status"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["created_on"].GetDateTimeStringFromObject()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["sent_on"].GetDateTimeStringFromObject()
                                                                });
        }

        #region private

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceSelectCommand = "SELECT [order_id], [customer_id], [type], [tota_price], [invoice_tax], [invoice_transaction_id], [status], [delivery_date], [prop_state], [created_on], [sent_on]";
            grid.GridDataSource.SqlDataSourceFromCommand = "FROM [fly_orderBackup]";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by [order_id] desc";
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
        }

        #endregion
    }
}
