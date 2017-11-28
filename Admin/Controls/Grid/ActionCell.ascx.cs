using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public partial class ActionCellControl : CellControlBase
    {
        public override HtmlTableCell TdHtmlTableCell
        {            
            get
            {
                return tdCell;
            }
        }

        protected void lbAction_Command(Object sender, CommandEventArgs e)
        {
            var linkButton = (sender as LinkButton);
            var bodyRowIndex = Int32.Parse(linkButton.CommandArgument);
            var actionType = GridExtensions.Parse(linkButton.CommandName);
            var dataCellsList = CellsList.Body.GetDataCellsList(bodyRowIndex) as DataCellsList;

            if (actionType == ActionTypes.Edit)
            {
                HandleEditCommand(linkButton, bodyRowIndex);
            }
            else if (actionType == ActionTypes.Delete)
            {
                var args = new RowDeletedEventArgs(bodyRowIndex, dataCellsList);

                CellsList.Body.GridControl.OnRowDeleted(args);
            }
            else if (actionType == ActionTypes.Custom)
            {
                var args = new ActionCellCustomCommandEventArgs(bodyRowIndex, this);

                CellsList.Body.GridControl.OnActionCellCustomCommand(args);
            }
        }

        public void BindActionCell(ActionCell ac, CellsListControlBase cellsList)
        {
            if (ac.Html.HasText())
            {
                ltlText.Text = ac.Html;
                lbAction.Visible = false;
            }
            else
            {
                ltlText.Visible = false;
                lbAction.Text = ac.Text;
                lbAction.CommandArgument = ac.Row.Index.ToString();
                lbAction.CommandName = ac.ActionType.ToString();

                if (ac.ActionType == ActionTypes.Edit)
                {
                    lbAction.CssClass += "button";
                    SetEditingRowMode(EditingRowMode.None, ac.Row.Index);
                }
                else if (ac.ActionType == ActionTypes.Delete)
                {
                    lbAction.CssClass += "close";

                    if (lbAction.Text.HasNoText())
                    {
                        lbAction.Text = "&times;";
                    }
                }
            }

            if (ac.Attributes != null)
            {
                AddTdHtmlTableCellAttributes(ac.Attributes);
            }

            if (ac.HideCell)
            {
                TdHtmlTableCell.Style.Add("display", "none");
            }

            CellsList = cellsList;

            if (ac.ActionType == ActionTypes.Custom)
            {
                lbCancel.Visible = false;

                var args = new ActionCellCustomBoundEventArgs(ac, this);

                CellsList.Body.GridControl.OnActionCellCustomBound(args);
            }
        }

        public void SetDefaultDisplayMode(Int32 bodyRowIndex)
        {
            SetEditingRowMode(EditingRowMode.None, bodyRowIndex);
            lbAction.Text = "Edit";
            lbAction.CssClass = "button";
        }

        #region private

        private Boolean IsEditCell { get; set; }

        private Boolean IsCancelLinkButtonVisible
        {
            get
            {
                return IsEditCell && GetEditingRowMode() == EditingRowMode.Editing;
            }
        }

        private void HandleEditCommand(LinkButton linkButton, Int32 bodyRowIndex)
        {
            var editingRowMode = GetEditingRowMode();
            var dataCellsList = CellsList.Body.GetDataCellsList(bodyRowIndex) as DataCellsList;

            if (editingRowMode == EditingRowMode.None)
            {
                var args = new RowEditingEventArgs(bodyRowIndex, (Parent as RepeaterItem).ItemIndex, dataCellsList);

                CellsList.Body.GridControl.OnRowEditing(args);

                SetEditingRowMode(EditingRowMode.Editing, bodyRowIndex);
                lbAction.Text = "Update";
                lbAction.CssClass = "button update";

                foreach (RepeaterItem i in dataCellsList.RptCells.Items)
                {
                    var dataCellControl = i.FindControl("cell") as DataCellControl;

                    if (dataCellControl != null && dataCellControl.DataInputType != DataInputTypes.None)
                    {
                        dataCellControl.SetDisplayAsInputMode();
                    }
                }
            }
            else
            {
                if (linkButton == lbAction)
                {
                    var args = new RowEditedEventArgs(bodyRowIndex, dataCellsList);
                    DataCellControl dcc;

                    for (var i = 0; i < dataCellsList.RptCells.Items.Count; i++)
                    {
                        dcc = (dataCellsList.GetCell(i) as DataCellControl);

                        if (dcc.DataInputType != DataInputTypes.None)
                        {
                            dcc.UpdatePrevValue();
                        }
                    }

                    CellsList.Body.GridControl.OnRowEdited(args);
                }
                else if (linkButton == lbCancel)
                {
                    var args = new RowEditCanceledEventArgs(bodyRowIndex, dataCellsList);

                    dataCellsList.RecoverInputState();
                    CellsList.Body.GridControl.OnRowEditCanceled(args);
                }

                SetDefaultDisplayMode(bodyRowIndex);
            }
        }

        private void SetEditingRowMode(EditingRowMode editingRowMode, Int32 bodyRowIndex)
        {
            hfActionState.Value = editingRowMode.ToString();
            IsEditCell = true;
            lbCancel.Visible = IsCancelLinkButtonVisible;

            if (!IsPostBack)
            {
                lbCancel.CssClass = "button cancel";
                lbCancel.Text = "Cancel";
                lbCancel.CommandArgument = bodyRowIndex.ToString();
                lbCancel.CommandName = ActionTypes.Edit.ToString();
            }
        }

        private EditingRowMode GetEditingRowMode()
        {
            var result = EditingRowMode.None;

            if (hfActionState != null)
            {
                if (!Enum.TryParse(hfActionState.Value, out result))
                {
                    result = EditingRowMode.None;
                }
            }

            return result;
        }

        private enum EditingRowMode
        {
            None = 0,
            Editing = 10
        }

        #endregion
    }
}