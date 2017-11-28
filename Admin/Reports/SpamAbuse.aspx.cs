using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports
{
    public partial class SpamAbuse : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
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
                grid.Visible = false;
            }
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "ESP" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Total Abuses" });
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = String.Format("<a href='{0}spamabuse/details.aspx?esp={1}&returnurl={2}'>{1}</a>", ResolveUrl("~/admin/reports/"), e.DataRow["SpamNoticeSender"], Helper.GetUrlEncodedString(Request.Url.ToString()))
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["TotalCount"].ToString()
                                                                });
        }

        #region private

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceSelectCommand = "usp_GetAbuseReport";
            grid.GridDataSource.SqlDataSourceSelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
        }

        private void InitHtmlElements()
        {
            if (grid.TotalRecords == 0)
            {
                divEmpty.Visible = true;
                grid.Visible = false;
                ltlMessage.Text = "There is no data.";
            }
            else
            {
                divEmpty.Visible = false;
            }
        }

        #endregion
    }
}
