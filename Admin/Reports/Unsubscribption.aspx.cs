using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports
{
    public partial class Unsubscribption : AdminPageBase
    {
        protected override string ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/admin/reports/unsubscribption";
            }
        }

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
                grid.Visible = false;
            }
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "First Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Middle Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Last Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "State" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "County" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Date" });
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["First_Name"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Middle_Name"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Last_Name"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["state"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["county"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["email"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateTimeStringFromDataColumn("UnsubscribeDateTime")
                                                                });
        }

        protected void btnSearch_Command(Object sender, CommandEventArgs e)
        {
            var url = Request.Url.AbsolutePath;
            var nvc = new NameValueCollection();

            if (inputUnsubscribedFrom.Value.HasText())
            {
                nvc.Add("unsubscribedfrom", inputUnsubscribedFrom.Value.Trim());
            }
            if (inputUnsubscribedTo.Value.HasText())
            {
                nvc.Add("unsubscribedto", inputUnsubscribedTo.Value.Trim());
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
            var containsData = true;

            grid.GridDataSource.SqlDataSourceWhereCommand = "where 1=1 ";

            if (!IsPostBack)
            {
                BindDataToInputs(out containsData);

                if (containsData)
                {
                    DateTime dateTime;

                    if (Request["unsubscribedfrom"].HasText())
                    {
                        if (DateTime.TryParse(inputUnsubscribedFrom.Value.Trim(), out dateTime))
                        {
                            grid.GridDataSource.SqlDataSourceWhereCommand += "and UnsubscribeDateTime >= '" + inputUnsubscribedFrom.Value.Trim() + "' ";
                        }
                        else
                        {
                            message.MessageText = String.Format("Unsubscribed from value {0} was not recognized as a valid date.", inputUnsubscribedFrom.Value);
                            message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                        }
                    }
                    if (Request["unsubscribedto"].HasText())
                    {
                        if (DateTime.TryParse(inputUnsubscribedTo.Value.Trim(), out dateTime))
                        {
                            grid.GridDataSource.SqlDataSourceWhereCommand += "and UnsubscribeDateTime <= '" + inputUnsubscribedTo.Value.Trim() + "' ";
                        }
                        else
                        {
                            message.MessageText = String.Format("Unsubscribed to value {0} was not recognized as a valid date.", inputUnsubscribedTo.Value);
                            message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                        }
                    }
                }
            }

            if (String.Compare(grid.GridDataSource.SqlDataSourceWhereCommand, "where 1=1 ", true) == 0)
            {
                grid.GridDataSource.SqlDataSourceWhereCommand = null;
            }

            if (message.MessageText.HasText())
            {
                message.ShowMessage();
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
            grid.GridDataSource.SqlDataSourceSelectCommand = "select se.First_Name, se.Middle_Name, se.Last_Name, se.state, se.county, se.email, u.UnsubscribeDateTime";
            grid.GridDataSource.SqlDataSourceFromCommand = @"from tblUnsubscribers u
                                                             inner join fly_Subscribers_With_Email se on u.Subscriber_ID=se.Subscriber_ID";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by unsubscribeDateTime desc";
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
            grid.GridDataSource.SqlExcelExportCommand = @"select se.First_Name [First Name], se.Middle_Name [Middle Name], se.Last_Name [Last Name], se.state [State], se.county [County], se.email [Email], u.UnsubscribeDateTime [Date]
                                                          from tblUnsubscribers u
                                                          inner join fly_Subscribers_With_Email se on u.Subscriber_ID=se.Subscriber_ID
                                                          {0}
                                                          order by unsubscribeDateTime desc";

            grid.GridDataSource.SqlExcelExportCommand = String.Format(grid.GridDataSource.SqlExcelExportCommand, grid.GridDataSource.SqlDataSourceWhereCommand);
        }

        private void InitHtmlElements()
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

        #endregion
    }
}
