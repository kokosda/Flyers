using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports
{
    public partial class FlyersEmailCount : AdminPageBase
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
                if (CanBindData())
                {
                    grid.DataBind();
                }
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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Market" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Total Emails" });
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
                                                                    Text = e.DataRow["market"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["total_emails"].ToString()
                                                                });
        }

        protected void btnSearchByFlyerId_Command(Object sender, CommandEventArgs e)
        {
            var url = Request.Url.AbsolutePath;
            var nvc = new NameValueCollection();

            if (inputOrderId.Value.HasText())
            {
                nvc.Add("orderid", inputOrderId.Value.Trim());
            }
            else
            {
                message.MessageText = "Flyer ID is required.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                if (nvc.Count > 0)
                {
                    url += "?" + nvc.NameValueToQueryString(false);
                }

                Response.Redirect(url, true);
            }
            else
            {
                message.ShowMessage();
            }
        }

        protected void btnSearchByStatus_Command(Object sender, CommandEventArgs e)
        {
            var url = Request.Url.AbsolutePath;
            var nvc = new NameValueCollection();

            if (ddlStatus.SelectedValue.HasText())
            {
                nvc.Add("status", ddlStatus.SelectedValue);
            }
            else
            {
                message.MessageText = "Select a status.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }

            if (ddlDay.SelectedValue.HasText())
            {
                nvc.Add("day", ddlDay.SelectedValue);
            }
            else
            {
                message.MessageText = "Select a time interval.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                if (nvc.Count > 0)
                {
                    url += "?" + nvc.NameValueToQueryString(false);
                }

                Response.Redirect(url, true);
            }
            else
            {
                message.ShowMessage();
            }
        }

        #region private

        private void InitInputs()
        {
            var containsData = true;

            if (!IsPostBack)
            {
                BindDataToInputs(out containsData);

                if (containsData)
                {
                    if (Request["orderid"].HasText())
                    {
                        grid.GridDataSource.SqlDataSourceSelectParameters.Add("order_id", TypeCode.Int32, inputOrderId.Value.Trim());
                    }
                    else if (Request["status"].HasText() && Request["day"].HasText())
                    {
                        grid.GridDataSource.SqlDataSourceSelectParameters.Add("status", TypeCode.String, ddlStatus.SelectedValue);
                        grid.GridDataSource.SqlDataSourceSelectParameters.Add("day", TypeCode.String, ddlDay.SelectedValue);
                    }
                }
            }
        }

        private void BindDataToInputs(out Boolean containsData)
        {
            containsData = false;

            foreach (String name in Request.QueryString)
            {
                var input = divSearch.FindControl("input" + name) as HtmlInputText;

                if (input != null)
                {
                    input.Value = Request.QueryString[name];
                    containsData = true;
                    continue;
                }

                var ddl = divSearch.FindControl("ddl" + name) as DropDownList;

                if (ddl != null)
                {
                    if (ddl.Items.Count < 2)
                    {
                        ddl.DataBind();
                    }

                    var item = ddl.Items.FindByValue(Request.QueryString[name]);

                    if (item != null)
                    {
                        ddl.SelectedIndex = ddl.Items.IndexOf(item);
                        containsData = true;
                        continue;
                    }
                }
            }
        }

        private void InitGrid()
        {
            if (Request["orderid"].HasText())
            {
                grid.GridDataSource.SqlDataSourceSelectCommand = "fly_sp_TotalEmailsToSendByOrderID";
            }
            else if (Request["status"].HasText() && Request["day"].HasText())
            {
                grid.GridDataSource.SqlDataSourceSelectCommand = "fly_sp_TotalEmailsToSendByStatus";
            }

            grid.GridDataSource.SqlDataSourceSelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
        }

        private void InitHtmlElements()
        {
            if (Request["orderid"].HasText() || (Request["status"].HasText() && Request["day"].HasText()))
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
                ltlMessage.Text = "Please type in Flyer ID or select status and time interval.";
                grid.Visible = false;
            }
        }

        private Boolean CanBindData()
        {
            return Request["orderid"].HasText() || (Request["status"].HasText() && Request["day"].HasText());
        }

        #endregion
    }
}
