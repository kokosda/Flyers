using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports.EmailDelivery
{
    public partial class DetailEmail : AdminPageBase
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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Total Sent" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Total Opened" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Total Bounce Back" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell());
            e.Grid.Head.HeaderCells.Add(new HeaderCell());
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
                                                                    Text = e.DataRow["Email_Sent"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_Opened"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email_Bounce_Back"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = String.Format("<a href='{0}?orderid={1}'>Chart</a>", ResolveUrl("~/admin/reports/emaildelivery/detailemail/chart.aspx"), e.DataRow["order_id"].ToString()),
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = String.Format("<a href='{0}?orderid={1}'>Details</a>", ResolveUrl("~/admin/reports/emaildelivery/detailemail/details.aspx"), e.DataRow["order_id"].ToString()),
                                                                });
        }

        #region private

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceConnectionString = ConfigurationManager.ConnectionStrings["fdeliveryDBConnectionString"].ConnectionString;
            grid.GridDataSource.SqlDataSourceSelectCommand = "usp_GetEmailDeliveryReport";
            grid.GridDataSource.SqlDataSourceSelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
        }

        #endregion
    }
}
