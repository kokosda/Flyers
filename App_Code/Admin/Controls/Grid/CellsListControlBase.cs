using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public abstract class CellsListControlBase : UserControl
    {
        public abstract Repeater RptCells { get; }

        public BodyControlBase Body { get; set; }

        public CellControlBase GetCell(Int32 index)
        {
            return RptCells.Items[index].FindControl("cell") as CellControlBase;
        }
    }
}