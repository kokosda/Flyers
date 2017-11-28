using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Users
{
    public partial class WebsiteUsers : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            Request.RedirectToHttpsIfRequired(Response);
            InitGrid();
            InitInputs();

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            grid.DataBind();
        }

        protected void grid_RowEdited(Object sender, RowEditedEventArgs e)
        {
            var cell = e.DataCellsList.RptCells.Items[0].FindControl("cell");

            cell = e.DataCellsList.RptCells.Items[0].FindControl("cell");
            grid.GridDataSource.SqlDataSourceUpdateParameters["UserId"].DefaultValue = cell.GetType().GetProperty("Value").GetValue(cell) as String;
            cell = e.DataCellsList.RptCells.Items[2].FindControl("cell");
            grid.GridDataSource.SqlDataSourceUpdateParameters["Password"].DefaultValue = cell.GetType().GetProperty("Value").GetValue(cell) as String;
            cell = e.DataCellsList.RptCells.Items[3].FindControl("cell");
            grid.GridDataSource.SqlDataSourceUpdateParameters["IsApproved"].DefaultValue = cell.GetType().GetProperty("Value").GetValue(cell) as String;

            var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

            sds.Update();
        }

        protected void grid_ActionCellCustomBound(Object sender, ActionCellCustomBoundEventArgs e)
        {
            var acc = e.ActionCellControl;
            var lb = e.ActionCellControl.FindControl("lbAction") as LinkButton;

            lb.CssClass = "button";
        }

        protected void grid_ActionCellCustomCommand(Object sender, ActionCellCustomCommandEventArgs e)
        {
            var lb = e.ActionCellControl.FindControl("lbAction") as LinkButton;

            if (String.Compare(lb.Text, "note", true) == 0)
            {

            }
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { HideCell = true });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Password" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Approved" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Create Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Last Login Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Edit));
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Edit));
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[0].ToString(),
                                                                    InputType = DataInputTypes.Hidden,
                                                                    HideCell = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[1] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[2] as String,
                                                                    InputType = DataInputTypes.Text,
                                                                    IsInlineEditable = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[3].ToString(),
                                                                    InputType = DataInputTypes.Checkbox,
                                                                    IsInlineEditable = true,
                                                                    DisplayAsDisabledInput = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[4].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[5].ToString()
                                                                });

            var noteUrl = ResolveUrl("~/admin/users/websiteusernote.aspx?userid=" + e.DataRow[0].ToString());
            var onclickAttribute = GetJsWindowPopupOnClickAttribute(noteUrl, "websiteusernote");

            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Custom)
                                                                {
                                                                    Html = String.Format("<a href='{0}' class='button' onclick=\"{1}\">Note</a>", noteUrl, onclickAttribute)
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Edit));
        }

        protected void btnSearch_Click(Object sender, EventArgs e)
        {
            var url = Request.Url.AbsolutePath;
            
            if (inputSearch.Value.HasText())
            {
                url += "?email=" + inputSearch.Value.Trim();
            }

            Response.Redirect(url, true);
        }

        #region private

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceSelectCommand = "SELECT [UserId], [Email], [Password], [IsApproved], [CreateDate], [LastLoginDate]";
            grid.GridDataSource.SqlDataSourceFromCommand = "FROM [viewUser]";

            if (Request["email"].HasText())
            {
                grid.GridDataSource.SqlDataSourceWhereCommand = "where email like '%" + Request["email"] + "%'";
            }

            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by [CreateDate] DESC";
            grid.GridDataSource.SqlDataSourceUpdateCommand = "UPDATE viewUser SET Password = @Password, IsApproved = @IsApproved WHERE (UserId = @UserId)";
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("IsApproved", DbType.Boolean));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Password", DbType.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("UserId", DbType.Guid));
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
        }

        private void InitInputs()
        {
            if (!IsPostBack)
            {
                inputSearch.Value = Request["email"];
            }
        }

        #endregion
    }
}
