using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports.SpamAbuse
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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Received by Mailbox" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Received on Date" });
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["SubscriberEmail"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["SpamNoticeReceiver"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateTimeStringFromDataColumn("ReceivedOn")
                                                                });
        }

        #region private

        private void InitGrid()
        {
            if (Request["esp"].HasNoText())
            {
                message.MessageText = "ESP is required.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                grid.GridDataSource.SqlDataSourceSelectCommand = "usp_GetAbuseDetailsByISP";
                grid.GridDataSource.SqlDataSourceSelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                grid.GridDataSource.SqlDataSourceSelectParameters.Add("isp", TypeCode.String, Request["esp"].Trim());
                grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
                grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;

                var returnUrl = Request["returnurl"].HasText() ? Request["returnurl"] : ResolveUrl("~/admin/reports/spamabuse.aspx");

                grid.PreheadLiteralText = String.Format("<h2><a href='{0}'>Go Back to Spam Abuse</a></h2>", returnUrl);

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