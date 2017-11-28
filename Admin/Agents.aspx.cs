using FlyerMe.Admin.Controls;
using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using FlyerMe.SpecialExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin
{
    public partial class Agents : AdminPageBase
    {
        protected override string ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/admin/agents";
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

            Form.Attributes["data-marketsurl"] = ResolveUrl("~/admin/agents.aspx/getmarkets");
        }

        public override void DataBind()
        {
            grid.DataBind();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static Object GetMarkets(String state)
        {
            var result = new { 
                                Counties = new List<String>(),
                                Associations = new List<String>(),
                                Msa = new List<String>()
                             };
            String message = null;

            if (state.HasText())
            {
                Action<List<String>, DataRowCollection> fillList = (l, drc) =>
                {
                    foreach (DataRow r in drc)
                    {
                        l.Add(r["market"] as String);
                    }
                };

                try
                {
                    DataTable dt;

                    using (var dataObj = new clsData())
                    {
                        dataObj.strSql = String.Format("SELECT DISTINCT [market] FROM [fly_county_listsize] where state='{0}' order by market", state);
                        dt = dataObj.GetDataTable();
                        fillList(result.Counties, dt.Rows);

                        dataObj.strSql = String.Format("SELECT DISTINCT [market] FROM [fly_association_listsize] where state='{0}' order by market", state);
                        dt = dataObj.GetDataTable();
                        fillList(result.Associations, dt.Rows);

                        dataObj.strSql = String.Format("SELECT DISTINCT [market] FROM [fly_msa_listsize] where state='{0}' order by market", state);
                        dt = dataObj.GetDataTable();
                        fillList(result.Msa, dt.Rows);
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }

            if (message.HasText())
            {
                return new { Message = message };
            }

            return result;
        }

        protected void btnSearch_Command(Object sender, CommandEventArgs e)
        {
            var url = GetRedirectUrl();

            Response.Redirect(url, true);
        }

        protected void ddlState_SelectedIndexChanged(Object sender, EventArgs e)
        {
            var url = GetRedirectUrl();

            Response.Redirect(url, true);
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Subs. ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "First Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Middle Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Last Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "State" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "County" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Association" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "MSA" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Unsubscribed" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Skip" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { HideCell = true });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { HideCell = true });
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Edit));
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Delete));
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["id"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["first_name"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["middle_name"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["last_name"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["email"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["state"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Select
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["county"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Select
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["association_name"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Select
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["msa_name"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Select
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["unsubscribed"].GetBooleanStringFromObject(),
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Checkbox,
                                                                    DisplayAsDisabledInput = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["skip"].GetBooleanStringFromObject(),
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Checkbox,
                                                                    DisplayAsDisabledInput = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["vresult"] as String,
                                                                    InputType = DataInputTypes.Hidden,
                                                                    HideCell = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["rcode"] as String,
                                                                    InputType = DataInputTypes.Hidden,
                                                                    HideCell = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Edit));
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Delete));
        }

        protected void grid_RowEditing(Object sender, RowEditingEventArgs e)
        {
            var cell = e.DataCellsList.GetCell(5);
            var ddl = cell.FindControl("gridDdl") as DropDownList;
            var selectedState = ddl.SelectedValue;
            var selectedValue = ddl.SelectedValue;
            ListItem li;

            ddl.DataSource = sdsStates;
            ddl.DataTextField = "StateName";
            ddl.DataValueField = "StateAbr";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem());
            li = ddl.Items.FindByValue(selectedValue);

            if (li != null)
            {
                ddl.SelectedIndex = ddl.Items.IndexOf(li);
            }
            else
            {
                ddl.SelectedIndex = 0;
            }

            cell = e.DataCellsList.GetCell(6);
            ddl = cell.FindControl("gridDdl") as DropDownList;
            selectedValue = ddl.SelectedValue;

            var sds = new SqlDataSource(sdsCounty.ConnectionString, sdsCounty.SelectCommand);

            sds.SelectParameters.Add("state", TypeCode.String, selectedState);

            ddl.DataSource = sds;
            ddl.DataTextField = "market";
            ddl.DataValueField = "market";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem());
            li = ddl.Items.FindByValue(selectedValue);

            if (li != null)
            {
                ddl.SelectedIndex = ddl.Items.IndexOf(li);
            }
            else
            {
                ddl.SelectedIndex = 0;
            }

            cell = e.DataCellsList.GetCell(7);
            ddl = cell.FindControl("gridDdl") as DropDownList;
            selectedValue = ddl.SelectedValue;

            sds = new SqlDataSource(sdsAssociation.ConnectionString, sdsAssociation.SelectCommand);
            sds.SelectParameters.Add("state", TypeCode.String, selectedState);

            ddl.DataSource = sds;
            ddl.DataTextField = "market";
            ddl.DataValueField = "market";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem());
            li = ddl.Items.FindByValue(selectedValue);

            if (li != null)
            {
                ddl.SelectedIndex = ddl.Items.IndexOf(li);
            }
            else
            {
                ddl.SelectedIndex = 0;
            }

            cell = e.DataCellsList.GetCell(8);
            ddl = cell.FindControl("gridDdl") as DropDownList;
            selectedValue = ddl.SelectedValue;

            sds = new SqlDataSource(sdsMsa.ConnectionString, sdsMsa.SelectCommand);
            sds.SelectParameters.Add("state", TypeCode.String, selectedState);

            ddl.DataSource = sds;
            ddl.DataTextField = "market";
            ddl.DataValueField = "market";
            ddl.SelectedIndex = -1;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem());
            li = ddl.Items.FindByValue(selectedValue);

            if (li != null)
            {
                ddl.SelectedIndex = ddl.Items.IndexOf(li);
            }
            else
            {
                ddl.SelectedIndex = 0;
            }
        }

        protected void grid_RowEdited(Object sender, RowEditedEventArgs e)
        {
            try
            {
                var id = (e.DataCellsList.GetCell(0).TdHtmlTableCell.FindControl("ltlText") as Literal).Text;
                var ht = new Hashtable();

                ht.Add("id", Int32.Parse(id));

                var value = e.DataCellsList.GetCell(1).GetPropertyValue("Value");

                if (value.HasText())
                {
                    ht.Add("first_name", value.Trim());
                }

                value = e.DataCellsList.GetCell(2).GetPropertyValue("Value");

                if (value.HasText())
                {
                    ht.Add("middle_name", value.Trim());
                }

                value = e.DataCellsList.GetCell(3).GetPropertyValue("Value");

                if (value.HasText())
                {
                    ht.Add("last_name", value.Trim());
                }

                value = e.DataCellsList.GetCell(4).GetPropertyValue("Value");

                if (value.HasText())
                {
                    ht.Add("email", value.Trim());
                }

                value = e.DataCellsList.GetCell(5).GetPropertyValue("Value");

                if (ddlState.Items.FindByValue(value) != null)
                {
                    ht.Add("state", value);
                }

                value = Request[e.DataCellsList.GetCell(6).TdHtmlTableCell.FindControl("gridDdl").UniqueID];
                ht.Add("county", value);

                value = Request[e.DataCellsList.GetCell(7).TdHtmlTableCell.FindControl("gridDdl").UniqueID];
                ht.Add("association_name", value);

                value = Request[e.DataCellsList.GetCell(8).TdHtmlTableCell.FindControl("gridDdl").UniqueID];
                ht.Add("msa_name", value);

                value = e.DataCellsList.GetCell(9).GetPropertyValue("Value");
                ht.Add("unsubscribed", Boolean.Parse(value));

                value = e.DataCellsList.GetCell(10).GetPropertyValue("Value");
                ht.Add("skip", Boolean.Parse(value));

                value = e.DataCellsList.GetCell(11).GetPropertyValue("Value");
                ht.Add("vresult", value.Trim());

                value = e.DataCellsList.GetCell(12).GetPropertyValue("Value");
                ht.Add("rcode", value.Trim());

                new clsData().ExecuteSql("usp_UpdateSubscription", ht);

                message.MessageText = String.Format("Subscriber {0} has been successfully updated.", ht["email"]);
                message.MessageClass = MessageClassesEnum.Ok;
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            message.RedirectToShowMessage();
        }

        protected void grid_RowDeleted(Object sender, RowDeletedEventArgs e)
        {
            try
            {
                var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
                
                using (var cmd = new SqlCommand())
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString()))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from fly_Subscribers_With_Email where id=" + id;
                    cmd.ExecuteNonQuery();
                    message.MessageText = "Specified email deleted successfully!";
                    message.MessageClass = MessageClassesEnum.Ok;
                }
            }
            catch(Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            message.RedirectToShowMessage();
        }

        protected void ddlLastRecords_SelectedIndexChanged(Object sender, EventArgs e)
        {
            var url = GetRedirectUrl();

            Response.Redirect(url, true);
        }

        protected void ddlRecordsPerPage_SelectedIndexChanged(Object sender, EventArgs e)
        {
            var url = GetRedirectUrl();

            Response.Redirect(url, true);
        }

        #region private

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceSelectCommand = "SELECT";

            var lastRecords = 0;

            Int32.TryParse(ddlLastRecords.SelectedValue, out lastRecords);

            if (lastRecords > 0)
            {
                grid.GridDataSource.SqlDataSourceSelectCommand += " TOP (" + ddlLastRecords.SelectedValue + ") *";
            }
            else
            {
                grid.GridDataSource.SqlDataSourceSelectCommand += " *";
            }

            grid.GridDataSource.SqlDataSourceFromCommand = "FROM [viewAgents]";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by [id] DESC";
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;

            var recordsPerPage = grid.PageSize;

            Int32.TryParse(ddlRecordsPerPage.SelectedValue, out recordsPerPage);

            grid.GridDataSource.SqlDataSourceMaximumRows = recordsPerPage;
            grid.GridDataSource.SqlExcelExportCommand = grid.GridDataSource.SqlDataSourceSelectCommand + " " + grid.GridDataSource.SqlDataSourceFromCommand + " " + grid.GridDataSource.SqlDataSourceWhereCommand + " " + grid.GridDataSource.SqlDataSourceOrderByCommand;
        }

        private void InitInputs()
        {
            Boolean containsData = true;

            if (!IsPostBack)
            {
                BindDataToInputs(out containsData);
            }

            if (containsData)
            {
                var whereCommand = "WHERE 1=1 ";

                if (inputEmail.Value.HasText())
                {
                    whereCommand += "and [email] = '" + inputEmail.Value.Trim() + "' ";
                }
                if (inputFirstName.Value.HasText())
                {
                    whereCommand += "and [first_name] = '" + inputFirstName.Value.Trim() + "' ";
                }
                if (inputLastName.Value.HasText())
                {
                    whereCommand += "and [middle_name] = '" + inputMiddleName.Value.Trim() + "' ";
                }
                if (inputLastName.Value.HasText())
                {
                    whereCommand += "and [last_name] = '" + inputLastName.Value.Trim() + "' ";
                }
                if (ddlState.SelectedValue.HasText())
                {
                    whereCommand += "and [state] = '" + ddlState.SelectedValue + "' ";
                }
                if (ddlCounty.SelectedValue.HasText())
                {
                    whereCommand += "and [county] = '" + ddlCounty.SelectedValue + "' ";
                }
                if (ddlAssociation.SelectedValue.HasText())
                {
                    whereCommand += "and [association_name] = '" + ddlAssociation.SelectedValue + "' ";
                }
                if (ddlMsa.SelectedValue.HasText())
                {
                    whereCommand += "and [msa_name] = '" + ddlMsa.SelectedValue + "' ";
                }

                grid.GridDataSource.SqlDataSourceWhereCommand = whereCommand;
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
                    var sds = divSearch.FindControl("sds" + name) as SqlDataSource;

                    if (sds != null)
                    {
                        ddl.DataSourceID = sds.ID;
                    }

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

        private String GetRedirectUrl()
        {
            var url = Request.Url.AbsolutePath;
            var query = String.Empty;

            foreach (Control c in divSearch.Controls)
            {
                var input = c as HtmlInputText;

                if (input != null && input.Value.HasText())
                {
                    query += "&" + input.ID.Replace("input", null).ToLower() + "=" + Helper.GetUrlEncodedString(input.Value);
                    continue;
                }

                var ddl = c as DropDownList;

                if (ddl != null)
                {
                    query += "&" + ddl.ID.Replace("ddl", null).ToLower() + "=" + Helper.GetUrlEncodedString(ddl.SelectedValue);
                    continue;
                }
            }

            query += "&" + ddlLastRecords.ID.Replace("ddl", null).ToLower() + "=" + ddlLastRecords.SelectedValue;
            query += "&" + ddlRecordsPerPage.ID.Replace("ddl", null).ToLower() + "=" + ddlRecordsPerPage.SelectedValue;
            url += "?" + query.Substring(1, query.Length - 1);

            var result = url;
            return result;
        }

        #endregion
    }
}
