using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports
{
    public partial class CustomerReport : AdminPageBase
    {
        protected override string ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/admin/reports/customerreport";
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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Customer ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Number of Flyers" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Total Cost" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Average Cost" });
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = String.Format("<a href='{0}customerreport/details.aspx?customerid={1}&returnurl={2}'>{1}</a>", ResolveUrl("~/admin/reports/"), e.DataRow["Customer_ID"], Helper.GetUrlEncodedString(Request.Url.ToString()))
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["NOF"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Decimal)e.DataRow["TotalPrice"]).FormatPrice()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Decimal)e.DataRow["AvgPrice"]).FormatPrice()
                                                                });
        }

        protected void btnGenerateReport_Command(Object sender, CommandEventArgs e)
        {
            var url = Request.Url.AbsolutePath;
            var nvc = new NameValueCollection();

            if (inputFlyerCreatedFrom.Value.HasText())
            {
                nvc.Add("flyercreatedfrom", inputFlyerCreatedFrom.Value);
            }
            else
            {
                message.MessageText = "Order Date From is required.";
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }
            
            if (message.MessageText.HasNoText())
            {
                if (inputFlyerCreatedTo.Value.HasText())
                {
                    nvc.Add("flyercreatedto", inputFlyerCreatedTo.Value);
                }
                else
                {
                    message.MessageText = "Order Date To is required.";
                    message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                }
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
                    DateTime datetime;

                    if (inputFlyerCreatedFrom.Value.HasText())
                    {
                        if (DateTime.TryParse(inputFlyerCreatedFrom.Value.Trim(), out datetime))
                        {
                            grid.GridDataSource.SqlDataSourceSelectParameters.Add("FromDate", inputFlyerCreatedFrom.Value.Trim());
                        }
                        else
                        {
                            message.MessageText = "Order Date From value was not recognized as a valid date.";
                            message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                        }
                    }
                    else
                    {
                        message.MessageText = "Order Date From is required.";
                        message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                    }

                    if (inputFlyerCreatedTo.Value.HasText())
                    {
                        if (DateTime.TryParse(inputFlyerCreatedTo.Value.Trim(), out datetime))
                        {
                            grid.GridDataSource.SqlDataSourceSelectParameters.Add("ToDate", inputFlyerCreatedTo.Value.Trim());
                        }
                        else
                        {
                            message.MessageText = "Order Date To value was not recognized as a valid date.";
                            message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                        }
                    }
                    else
                    {
                        message.MessageText = "Order Date To is required.";
                        message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                    }
                }
                else
                {
                    var now = DateTime.Now;

                    inputFlyerCreatedFrom.Value = new DateTime(now.Year, now.Month, 1).FormatDate();
                    inputFlyerCreatedTo.Value = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)).FormatDate();
                    grid.GridDataSource.SqlDataSourceSelectParameters.Add("FromDate", inputFlyerCreatedFrom.Value);
                    grid.GridDataSource.SqlDataSourceSelectParameters.Add("ToDate", inputFlyerCreatedTo.Value);
                }
            }

            if (message.MessageText.HasText())
            {
                message.RedirectToAbsolutPathToShowMessage();
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
            grid.GridDataSource.SqlDataSourceSelectCommand = "usp_GetCustomerReport";
            grid.GridDataSource.SqlDataSourceSelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
            grid.GridDataSource.SqlExcelExportCommand = String.Format("exec usp_GetCustomerReport '{0}', '{1}'", inputFlyerCreatedFrom.Value.Trim(), inputFlyerCreatedTo.Value.Trim());
        }

        private void InitHtmlElements()
        {
            if (CanBindData())
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
                divEmpty.Visible = true;
                ltlMessage.Text = "Please specify Order Date interval.";
                grid.Visible = false;
            }
        }

        private Boolean CanBindData()
        {
            return inputFlyerCreatedFrom.Value.HasText() && inputFlyerCreatedTo.Value.HasText();
        }

        #endregion
    }
}
