using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports
{
    public partial class CustomersByEsp : AdminPageBase
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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "First Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Middle Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Last Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "State" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "County" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email" });
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
        }

        protected void btnSearch_Command(Object sender, CommandEventArgs e)
        {
            var url = Request.Url.AbsolutePath;
            var nvc = new NameValueCollection();

            if (inputEsp.Value.HasText())
            {
                nvc.Add("esp", inputEsp.Value.Trim());
            }
            if (ddlState.SelectedValue.HasText())
            {
                nvc.Add("state", ddlState.SelectedValue);
            }
            if (nvc.Count > 0)
            {
                url += "?" + nvc.NameValueToQueryString(false);
            }

            Response.Redirect(url, true);
        }

        protected void ddlState_DataBound(Object sender, EventArgs e)
        {
            ddlState.Items.Insert(0, new ListItem("[Select State]", String.Empty));
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
                    if (Request["esp"].HasText())
                    {
                        grid.GridDataSource.SqlDataSourceWhereCommand += "and email like '%@" + inputEsp.Value + "' ";
                    }

                    if (Request["state"].HasText())
                    {
                        if (ddlState.SelectedIndex > 0)
                        {
                            grid.GridDataSource.SqlDataSourceWhereCommand += "and state ='" + ddlState.SelectedValue + "' ";
                        }
                    }
                }
            }

            if (String.Compare(grid.GridDataSource.SqlDataSourceWhereCommand, "where 1=1 ", true) == 0)
            {
                grid.GridDataSource.SqlDataSourceWhereCommand = null;
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
            grid.GridDataSource.SqlDataSourceSelectCommand = "select First_Name, Middle_Name, Last_Name, state, county, email";
            grid.GridDataSource.SqlDataSourceFromCommand = "from fly_Subscribers_With_Email";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by Subscriber_ID";
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
        }

        private void InitHtmlElements()
        {
            if (inputEsp.Value.HasText() || ddlState.SelectedIndex > 0)
            {
                if (grid.TotalRecords == 0)
                {
                    divEmpty.Visible = true;
                    grid.Visible = false;
                    ltlMessage.Text = "There is no data for provided input.";
                }
                else
                {
                    grid.PreheadLiteralText = String.Format("<h2 class='total'>Total Emails: {0}</h2>", grid.TotalRecords.ToString());
                    divEmpty.Visible = false;
                }
            }
            else
            {
                ltlMessage.Text = "Please type in E-mail Service Provider or select a state. Popular ESPs are \"yahoo.com\", \"gmail.com\", etc.";
                grid.Visible = false;
            }
        }

        private Boolean CanBindData()
        {
            return inputEsp.Value.HasText() || ddlState.SelectedIndex > 0;
        }

        #endregion
    }
}
