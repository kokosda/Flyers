using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports.EmailDelivery
{
    public partial class QueuedFlyers : AdminPageBase
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
                grid.Visible = false;
            }
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Flyer ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Emails Left" });
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
                                                                    Text = e.DataRow["emails_left"].ToString()
                                                                });
        }

        #region private

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceConnectionString = ConfigurationManager.ConnectionStrings["fdeliveryDBConnectionString"].ConnectionString;
            grid.GridDataSource.SqlDataSourceSelectCommand = "fly_sp_emails_left";
            grid.GridDataSource.SqlDataSourceSelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            grid.GridDataSource.SqlDataSourceSelectParameters.Add("projectDbo", System.Data.DbType.String, ConfigurationManager.AppSettings["ProjectDbo"] + ".");
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
            grid.PreheadLiteralText = "<h2>Below are the number of emails left to send for the queued flyers.</h2>";
        }

        #endregion
    }
}
