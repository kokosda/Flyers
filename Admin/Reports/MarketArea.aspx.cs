using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Reports
{
    public partial class MarketArea : AdminPageBase
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
            if (String.Compare(ddlMarketArea.SelectedValue, "county", true) == 0)
            {
                e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Counties" });
                e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email List Size" });
                e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Falls in MSA" });
            }
            else if (String.Compare(ddlMarketArea.SelectedValue, "association", true) == 0)
            {
                e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Association" });
                e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email List Size" });
                e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Falls in counties" });
            }
            else if (String.Compare(ddlMarketArea.SelectedValue, "msa", true) == 0)
            {
                e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "MSA" });
                e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email List Size" });
                e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Falls in counties" });
            }
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["market"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["listsize"] as String,
                                                                    Data = e.DataRow["listsize"]
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = GetCountyMSA(e.DataRow["market"] as String)
                                                                });
        }

        protected void btnSearch_Command(Object sender, CommandEventArgs e)
        {
            var url = Request.Url.AbsolutePath;
            var nvc = new NameValueCollection();

            if (ddlState.SelectedValue.HasText())
            {
                nvc.Add("state", ddlState.SelectedValue);
            }
            if (ddlMarketArea.SelectedValue.HasText())
            {
                nvc.Add("marketarea", ddlMarketArea.SelectedValue);
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

        protected void ddlMarketArea_SelectedIndexChanged(Object sender, EventArgs e)
        {
            btnSearch_Command(sender, new CommandEventArgs(null, null));
        }

        protected void ddlState_SelectedIndexChanged(Object sender, EventArgs e)
        {
            btnSearch_Command(sender, new CommandEventArgs(null, null));
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
                    if (Request["state"].HasText())
                    {
                        if (ddlState.SelectedIndex > 0)
                        {
                            grid.GridDataSource.SqlDataSourceSelectParameters.Add("stateID", ddlState.SelectedValue);
                        }
                        else
                        {
                            message.MessageText = "Please select a state.";
                            message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                        }
                    }

                    if (Request["marketarea"].HasText())
                    {
                        grid.GridDataSource.SqlDataSourceSelectParameters.Add("marketType", ddlMarketArea.SelectedValue);
                    }
                }
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
            grid.GridDataSource.SqlDataSourceSelectCommand = "fly_sp_GetListSize";
            grid.GridDataSource.SqlDataSourceSelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            grid.GridDataSource.SqlDataSourceSelectParameters.Add("flyerType", TypeCode.String, "1");
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

                var totalAgentsCounts = TryGetTotalListSize();

                if (totalAgentsCounts > 0L)
                {
                    grid.PreheadLiteralText = String.Format("<h2 class='total'>Total agents: {0}</h2>", totalAgentsCounts.ToString());
                }
            }
        }

        private String GetCountyMSA(String market)
        {
            var result = String.Empty;
            var marketSeleted = ddlMarketArea.SelectedValue;
            var objAdmin = new clsAdmin();

            switch (marketSeleted)
            {
                case "county":
                    result = objAdmin.GetMSAForCounty(ddlState.SelectedValue, market);
                    break;
                case "association":
                    result = objAdmin.GetCountyForAssociation(ddlState.SelectedValue, market);
                    break;
                case "msa":
                    result = objAdmin.GetCountyForMSA(ddlState.SelectedValue, market);
                    break;
            }

            return result;
        }

        private Boolean CanBindData()
        {
            return ddlState.SelectedIndex > 0;
        }

        private Int64 TryGetTotalListSize()
        {
            var result = 0L;

            if (grid.Visible == true && grid.Grid != null)
            {
                result = grid.Grid.Body.Rows.Select(r => 
                                                    { 
                                                        Int64 @int64; 
                                                        return Int64.TryParse(r.DataCells[1].Data as String, out @int64) ? @int64 : 0L; 
                                                    })
                                            .Sum();
            }

            return result;
        }

        #endregion
    }
}
