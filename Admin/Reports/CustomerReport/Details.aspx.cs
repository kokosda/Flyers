using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports.CustomerReport
{
    public partial class Details : AdminPageBase
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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Flyer ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Type" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "State" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Total Price" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Invoice Tax" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Trans. ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Status" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Delivery Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Creation Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Discount" });
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = String.Format("<a href='{1}admin/flyers/details.aspx?orderid={0}' target='_blank'>{0}</a>", e.DataRow["order_id"].ToString(), ResolveUrl("~/"))
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = (e.DataRow["type"] as String).Capitalize()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["market_state"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Decimal)e.DataRow["tota_price"]).FormatPrice()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Decimal)e.DataRow["invoice_tax"]).FormatPrice()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["invoice_transaction_id"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = (e.DataRow["status"] as String).Capitalize()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateStringFromDataColumn("delivery_date")
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateStringFromDataColumn("created_on")
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Decimal)e.DataRow["Discount"]).FormatPrice()
                                                                });
        }

        #region private

        private void InitGrid()
        {
            if (Request["customerid"].HasNoText())
            {
                message.MessageText = "Customer ID is required.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                grid.GridDataSource.SqlDataSourceSelectCommand = "select *";
                grid.GridDataSource.SqlDataSourceFromCommand = "from fly_order";
                grid.GridDataSource.SqlDataSourceWhereCommand = "where customer_id=@customer_id and status<>'Incomplete'";
                grid.GridDataSource.SqlDataSourceSelectParameters.Add("customer_id", TypeCode.String, Request["customerid"]);
                grid.GridDataSource.SqlDataSourceOrderByCommand = "order by created_on";
                grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
                grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
                grid.GridDataSource.SqlExcelExportCommand = String.Format(@"
                        select order_id [Flyer ID], type [Type], market_state [State], CAST(tota_price as decimal(18,2)) [Total Price], CAST(invoice_tax as decimal(18,2)) [Invoice Tax], invoice_transaction_id [Trans. ID], status [Status], delivery_date [Delivery Date], created_on [Creation Date], CAST(Discount as decimal(18,2)) [Discount]
                        from fly_order 
                        where customer_id='{0}' and status<>'Incomplete' 
                        order by created_on", Request["customerid"]);

                var returnUrl = Request["returnurl"].HasText() ? Request["returnurl"] : ResolveUrl("~/admin/reports/customerreport.aspx");

                grid.PreheadLiteralText = String.Format("<h2><a href='{0}'>Go Back to Customer Report</a></h2>", returnUrl);

                if (Request["returnurl"].HasText())
                {
                    grid.EncodeUrlParametersForPager = new NameValueCollection();
                    grid.EncodeUrlParametersForPager.Add("returnurl", Request["returnurl"]);
                }
            }
            else
            {
                grid.Visible = false;
                message.ShowMessage();
            }
        }

        #endregion
    }
}