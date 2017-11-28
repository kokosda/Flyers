using FlyerMe.Admin.Models;
using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public partial class ActionCellsList : CellsListControlBase
    {
        public override Repeater RptCells
        {
            get
            {
                return rptCells;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                foreach (RepeaterItem i in RptCells.Items)
                {
                    (i.FindControl("cell") as CellControlBase).CellsList = this;
                }
            }
        }

        protected void rptCells_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            var cellControl = e.Item.FindControl("cell") as ActionCellControl;
            var ac = e.Item.DataItem as ActionCell;

            cellControl.BindActionCell(ac, this);
        }
    }
}
